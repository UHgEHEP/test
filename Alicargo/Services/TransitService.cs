﻿using System.Linq;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;

namespace Alicargo.Services
{
	internal sealed class TransitService : ITransitService
	{
		private readonly ICarrierRepository _carriers;
		private readonly ITransitRepository _transits;

		public TransitService(ITransitRepository transits, ICarrierRepository carriers)
		{
			_transits = transits;
			_carriers = carriers;
		}

		public void Update(long transitId, TransitEditModel transit, long? forsedCarrierId)
		{
			var data = _transits.Get(transitId).Single();

			TransitMapper.Map(transit, data, GetCarrier(forsedCarrierId, transit.CityId, data.CarrierId));

			_transits.Update(data);
		}

		public void Delete(long transitId)
		{
			_transits.Delete(transitId);
		}

		public long Add(TransitEditModel transit, long? forsedCarrierId)
		{
			var data = new TransitData();

			TransitMapper.Map(transit, data, GetCarrier(forsedCarrierId, transit.CityId, null));

			var transitId = _transits.Add(data);

			return transitId;
		}

		private long GetByCityOrAny(long cityId, long? oldCarrierId)
		{
			var inCity = _carriers.GetByCity(cityId);

			if(inCity.Length == 0)
			{
				return oldCarrierId ?? _carriers.GetAll().Select(x => x.Id).First();
			}

			if(oldCarrierId.HasValue && inCity.Contains(oldCarrierId.Value))
			{
				return oldCarrierId.Value;
			}

			return inCity.First();
		}

		private long GetCarrier(long? carrierId, long cityId, long? oldCarrierId)
		{
			return carrierId.HasValue
				? carrierId.Value
				: GetByCityOrAny(cityId, oldCarrierId);
		}
	}
}