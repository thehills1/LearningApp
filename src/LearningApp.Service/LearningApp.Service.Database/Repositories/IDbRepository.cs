using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LearningApp.Service.Database.Tables;

namespace LearningApp.Service.Database.Repositories
{
	public interface IDbRepository
	{
		long Add<T>(T newTable) where T : class, ITable;
		void AddRange<T>(IEnumerable<T> newTables) where T : class, ITable;
		IEnumerable<T> Get<T>(Expression<Func<T, bool>> selector, bool noTracking = true) where T : class, ITable;
		T Get<T>(long id, bool noTracking = true) where T : class, ITable;
		IEnumerable<T> GetAll<T>(bool noTracking = true) where T : class, ITable;
		void Remove<T>(Expression<Func<T, bool>> selector) where T : class, ITable;
		void Remove<T>(long id) where T : class, ITable;
		void Remove<T>(T table) where T : class, ITable;
		void RemoveRange<T>(IEnumerable<T> tables) where T : class, ITable;
		int SaveChanges();
	}
}