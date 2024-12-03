using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using LearningApp.Service.API.Contracts.Users.Common;

namespace LearningApp.Service.Langs.Core
{
	public static class LangManager
	{
		private static ConcurrentDictionary<(Language, string), ILang> _translators = new();
		private static ConcurrentDictionary<Language, List<ILang>> _langs = new();

		public static Dictionary<Language, Language> LangsMap { get; }

		public static Language DefaultLang { get; set; } = Language.English;

		static LangManager()
		{
			LangsMap = new Dictionary<Language, Language>()
			{
				{ Language.Russian, Language.English }
			};
		}

		public static void LoadLangs()
		{
			var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory);
			var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach (var filePath in files)
			{
				if (Path.GetExtension(filePath) != ".dll") continue;

				LoadLang(filePath);
			}
		}

		public static void LoadLang(string path)
		{
			if (!File.Exists(path)) throw new FileNotFoundException();

			var assembly = Assembly.Load(File.ReadAllBytes(path));
			var name = assembly.GetName().Name;
			try
			{
				var loadedLangs = new HashSet<Language>();

				foreach (var type in assembly.GetTypes())
				{
					if (!type.IsDefined(typeof(LangAttribute), true) && !typeof(ILang).IsAssignableFrom(type)) continue;
					if (type.IsDefined(typeof(LangAttribute), true))
					{
						var langAttribute = type.GetCustomAttribute<LangAttribute>(true);
						if (langAttribute == null) continue;

						var langName = langAttribute.Lang;
						var ctor = type.GetConstructor(Type.EmptyTypes);
						if (ctor != null)
						{
							var lang = ctor.Invoke(null);
							LoadLang((ILang) lang, langName);
							loadedLangs.Add(langName);
						}
					}
				}

				foreach (var loadedLang in loadedLangs)
				{
					Console.WriteLine($"Lang {name} -> {loadedLang} successfully loaded.");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error while loading lang from assembly - [{assembly.FullName}]\n{e}");
			}
		}

		public static void LoadLang(ILang lang, Language langName)
		{
			var langSet = _langs.GetOrAdd(langName, new List<ILang>());
			langSet.Add(lang);
		}

		public static string GetTranslation(string translationKey, Language lang, params object[] args)
		{
			TranslateArgs(args, lang);

			if (!_langs.ContainsKey(lang))
			{
				if (lang == DefaultLang) throw new Exception("No default lang.");

				return GetTranslationByParent(translationKey, lang, args);
			}

			var translation = TryGetTranslation(translationKey, lang, args);
			if (string.IsNullOrEmpty(translation))
			{
				if (lang == DefaultLang) return args.Length == 0 ? translationKey : string.Format(translationKey, args);

				return GetTranslationByParent(translationKey, lang, args);
			}

			return translation;
		}

		public static string GetTranslationByParent(string translationKey, Language lang, object[] args)
		{
			if (LangsMap.TryGetValue(lang, out var parent))
			{
				return GetTranslation(translationKey, parent, args);
			}

			return GetTranslation(translationKey, DefaultLang, args);
		}

#nullable enable
		public static string? TryGetTranslation(string translationKey, Language lang, params object[] args)
		{
			if (_translators.TryGetValue((lang, translationKey), out var translator))
			{
				return TryGetTranslation(translator, translationKey, args);
			}
			else
			{
				_translators.TryAdd((lang, translationKey), TryGetTranslator(_langs[lang], translationKey, args, out var translation));
				return translation;
			}
		}
#nullable restore

		private static ILang TryGetTranslator(IEnumerable<ILang> langSet, string translationKey, object[] args, out string translation)
		{
			foreach (var lang in langSet)
			{
				translation = TryGetTranslation(lang, translationKey, args);
				if (!string.IsNullOrEmpty(translation)) return lang;
			}

			translation = null;
			return null;
		}

		private static string TryGetTranslation(ILang translator, string translationKey, params object[] args)
		{
			var translation = translator?.GetTranslation(translationKey, args);

			if (!string.IsNullOrEmpty(translation)) return args.Length == 0 ? translation : string.Format(translation, args);

			return null;
		}

		private static void TranslateArgs(object[] args, Language lang)
		{
			if (args == null) return;

			for (var i = 0; i < args.Length; i++)
			{
				var arg = args[i];

				if (arg is Func<Language, dynamic> func)
				{
					args[i] = func(lang);
				}
			}
		}
	}
}