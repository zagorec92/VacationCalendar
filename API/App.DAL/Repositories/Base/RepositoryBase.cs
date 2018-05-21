using App.Model.Base;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace App.DAL.Repositories.Base
{
	public class RepositoryBase<T>
		where T : DbContext
	{
		#region Properties

		public T Context { get; set; }

		#endregion

		#region Constructor

		public RepositoryBase(T context)
		{
			Context = context;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Inserts or updates entity and performs save action.
		/// </summary>
		/// <typeparam name="TEntity">Type that has to be derived from <see cref="EntityBase"/> class.</typeparam>
		/// <param name="entity">The <see cref="{TEntity}"/> instance.</param>
		/// <returns>The <see cref="Task{System.Int32}"/> instance.</returns>
		public async Task<int> InsertOrUpdateAsync<TEntity>(TEntity entity)
			where TEntity : EntityBase
		{
			if (entity.ID.HasValue)
				Context.Set<TEntity>().Update(entity);
			else
				Context.Set<TEntity>().Add(entity);

			return await Context.SaveChangesAsync();
		}

		#endregion
	}
}
