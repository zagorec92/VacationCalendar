using App.DAL.Repositories.Interfaces;

namespace App.Service.Base
{
	public class ServiceBase<T>
		where T : IRepository
	{
		#region Properties

		public T Repository { get; }

		#endregion

		#region Constructor

		/// <summary>
		/// Instantiates a new instance of <see cref="ServiceBase{T}"/> class.
		/// </summary>
		/// <param name="repository">The injected <see cref="IRepository"/> instance.</param>
		public ServiceBase(T repository)
		{
			Repository = repository;
		}

		#endregion
	}
}
