﻿using System;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;

namespace Alicargo.DataAccess.Repositories
{
	internal sealed class CarrierRepository : BaseRepository, ICarrierRepository
	{
		private readonly Expression<Func<DbContext.Carrier, CarrierData>> _selector;

		public CarrierRepository(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
			_selector = x => new CarrierData
			{
				Name = x.Name,
				Id = x.Id
			};
		}

		public CarrierData[] GetAll()
		{
			return Context.Carriers.Select(_selector).ToArray();
		}

		public Func<long> Add(CarrierData carrier)
		{
			var entity = new DbContext.Carrier
			{
				Name = carrier.Name,
				Transits = null,
				Id = 0
			};
			Context.Carriers.InsertOnSubmit(entity);

			return () => entity.Id;
		}

		public CarrierData Get(string name)
		{
			return Context.Carriers.Select(_selector).FirstOrDefault(x => x.Name.Equals(name));
		}
	}
}