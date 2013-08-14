﻿using System.Linq;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Microsoft.Ajax.Utilities;

namespace Alicargo.Services
{
	// todo: test
	public sealed class TransitService : ITransitService
	{
		private readonly ICarrierService _carrierService;
		private readonly ITransitRepository _transitRepository;
		private readonly IUnitOfWork _unitOfWork;

		public TransitService(IUnitOfWork unitOfWork, ICarrierService carrierService, ITransitRepository transitRepository)
		{
			_unitOfWork = unitOfWork;
			_carrierService = carrierService;
			_transitRepository = transitRepository;
		}

		public TransitEditModel[] Get(params long[] ids)
		{
			var data = _transitRepository.Get(ids);

			return data.Select(TransitEditModel.GetModel).ToArray();
		}

		public void Update(long transitId, TransitEditModel transit, CarrierSelectModel carrierModel)
		{
			using (var ts = _unitOfWork.StartTransaction())
			{
				var carrierId = GetCarrierId(carrierModel);

				var data = TransitEditModel.GetData(transit, carrierId);

				data.Id = transitId;

				_transitRepository.Update(data);

				_unitOfWork.SaveChanges();

				ts.Complete();
			}
		}

		public void Delete(long transitId)
		{
			_transitRepository.Delete(transitId);
			_unitOfWork.SaveChanges();
		}

		public long AddTransit(TransitEditModel model, CarrierSelectModel carrierModel)
		{
			using (var ts = _unitOfWork.StartTransaction())
			{
				var carrierId = GetCarrierId(carrierModel);

				var transitId = _transitRepository.Add(TransitEditModel.GetData(model, carrierId));

				_unitOfWork.SaveChanges();

				ts.Complete();

				return transitId();
			}
		}

		private long GetCarrierId(CarrierSelectModel model)
		{
			if (model.NewCarrierName.IsNullOrWhiteSpace()) return model.CarrierId;

			var id = _carrierService.AddOrGetCarrier(model.NewCarrierName);
			_unitOfWork.SaveChanges();
			return id();
		}
	}
}