using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LearningApp.Service.Database.Tables;
using Microsoft.EntityFrameworkCore;

namespace LearningApp.Service.Database.Repositories
{
	public class DbRepository : IDbRepository
	{
		private readonly DatabaseContext _databaseContext;

		public DbRepository(DatabaseContext databaseContext)
		{
			_databaseContext = databaseContext;
		}

		public int SaveChanges()
		{
			return _databaseContext.SaveChanges();
		}

		public long Add<T>(T newTable) where T : class, ITable
		{
			var table = _databaseContext.Set<T>().Add(newTable);
			SaveChanges();

			return table.Entity.Id;
		}

		public void AddRange<T>(IEnumerable<T> newTables) where T : class, ITable
		{
			_databaseContext.Set<T>().AddRange(newTables);
			SaveChanges();
		}

		public void Remove<T>(long id) where T : class, ITable
		{
			var set = _databaseContext.Set<T>();
			var currentTable = set.FirstOrDefault(t => t.Id == id);
			if (currentTable == null) return;

			set.Remove(currentTable);
			SaveChanges();
		}

		public void Remove<T>(Expression<Func<T, bool>> selector) where T : class, ITable
		{
			var set = _databaseContext.Set<T>();
			var tables = set.Where(selector);
			if (!tables.Any()) return;

			set.RemoveRange(tables);
			SaveChanges();
		}

		public IEnumerable<T> Get<T>(Expression<Func<T, bool>> selector, bool noTracking = false) where T : class, ITable
		{
			var set = _databaseContext.Set<T>().Where(selector);
			if (noTracking) set = set.AsNoTracking();

			return set.ToList();
		}

		public T Get<T>(long id, bool noTracking = false) where T : class, ITable
		{
			var set = _databaseContext.Set<T>().Where(t => t.Id == id);
			if (noTracking) set = set.AsNoTracking();

			return set.FirstOrDefault();
		}

		public IEnumerable<T> GetAll<T>(bool noTracking = false) where T : class, ITable
		{
			var set = _databaseContext.Set<T>();
			if (noTracking)
			{
				return set.AsNoTracking().ToList();
			}
			else
			{
				return set.ToList();
			}
		}

		public void Remove<T>(T table) where T : class, ITable
		{
			_databaseContext.Set<T>().Remove(table);
			SaveChanges();
		}

		public void RemoveRange<T>(IEnumerable<T> tables) where T : class, ITable
		{
			_databaseContext.Set<T>().RemoveRange(tables);
			SaveChanges();
		}
	}
}
