﻿using System.Globalization;
using System.Linq;
using Alicargo.Core.Contracts.Common;
using Alicargo.Core.Helpers;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.AirWaybill;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.AirWaybill
{
	internal sealed class AwbPresenter : IAwbPresenter
	{
		private readonly IAwbRepository _awbRepository;
		private readonly IBrokerRepository _brokerRepository;
		private readonly IStateRepository _states;

		public AwbPresenter(
			IAwbRepository awbRepository,
			IBrokerRepository brokerRepository,
			IStateRepository states)
		{
			_awbRepository = awbRepository;
			_brokerRepository = brokerRepository;
			_states = states;
		}

		public ListCollection<AirWaybillListItem> List(int take, int skip, long? brokerId, string language)
		{
			var data = _awbRepository.GetRange(take, skip, brokerId);
			var ids = data.Select(x => x.Id).ToArray();

			var aggregates = _awbRepository.GetAggregate(ids).ToDictionary(x => x.AirWaybillId, x => x);

			var states = _states.Get(language);
			var currentCulture = CultureInfo.CurrentCulture;

			var items = data.Select(x => new AirWaybillListItem
			{
				Id = x.Id,
				PackingFileName = x.PackingFileName,
				InvoiceFileName = x.InvoiceFileName,
				State = new ApplicationStateModel
				{
					StateName = states[x.StateId].LocalizedName,
					StateId = x.StateId
				},
				AWBFileName = x.AWBFileName,
				ArrivalAirport = x.ArrivalAirport,
				Bill = x.Bill,
				CreationTimestampLocalString = LocalizationHelper.GetDate(x.CreationTimestamp, currentCulture),
				DateOfArrivalLocalString = LocalizationHelper.GetDate(x.DateOfArrival, currentCulture),
				DateOfDepartureLocalString = LocalizationHelper.GetDate(x.DateOfDeparture, currentCulture),
				StateChangeTimestampLocalString = LocalizationHelper.GetDate(x.StateChangeTimestamp, currentCulture),
				DepartureAirport = x.DepartureAirport,
				GTD = x.GTD,
				GTDAdditionalFileName = x.GTDAdditionalFileName,
				GTDFileName = x.GTDFileName,
				TotalCount = aggregates[x.Id].TotalCount,
				TotalWeight = aggregates[x.Id].TotalWeight,
				AdditionalCost = x.AdditionalCost,
				TotalCostOfSenderForWeight = x.TotalCostOfSenderForWeight,
				BrokerCost = x.BrokerCost,
				CustomCost = x.CustomCost,
				FlightCost = x.FlightCost,
			}).ToArray();

			var total = _awbRepository.Count(brokerId);

			return new ListCollection<AirWaybillListItem> { Data = items, Total = total };
		}

		public AwbAdminModel Get(long id)
		{
			var data = _awbRepository.Get(id).FirstOrDefault();

			if (data == null) throw new EntityNotFoundException("Refarence: " + id);

			return Map(data);
		}

		public AwbSenderModel GetSenderAwbModel(long id)
		{
			var data = _awbRepository.Get(id).First();

			return new AwbSenderModel
			{
				AWBFile = null,
				AWBFileName = data.AWBFileName,
				ArrivalAirport = data.ArrivalAirport,
				Bill = data.Bill,
				BrokerId = data.BrokerId,
				DateOfArrivalLocalString = LocalizationHelper.GetDate(data.DateOfArrival, CultureInfo.CurrentCulture),
				DateOfDepartureLocalString = LocalizationHelper.GetDate(data.DateOfDeparture, CultureInfo.CurrentCulture),
				DepartureAirport = data.DepartureAirport,
				PackingFile = null,
				PackingFileName = data.PackingFileName,
				FlightCost = data.FlightCost,
				TotalCostOfSenderForWeight = data.TotalCostOfSenderForWeight
			};
		}

		public static AwbAdminModel Map(AirWaybillData data)
		{
			return new AwbAdminModel
			{
				PackingFileName = data.PackingFileName,
				InvoiceFileName = data.InvoiceFileName,
				PackingFile = null,
				AWBFileName = data.AWBFileName,
				ArrivalAirport = data.ArrivalAirport,
				Bill = data.Bill,
				GTDAdditionalFileName = data.GTDAdditionalFileName,
				DateOfArrivalLocalString = LocalizationHelper.GetDate(data.DateOfArrival, CultureInfo.CurrentCulture),
				DateOfDepartureLocalString = LocalizationHelper.GetDate(data.DateOfDeparture, CultureInfo.CurrentCulture),
				DepartureAirport = data.DepartureAirport,
				GTD = data.GTD,
				GTDFileName = data.GTDFileName,
				InvoiceFile = null,
				AWBFile = null,
				BrokerId = data.BrokerId,
				GTDAdditionalFile = null,
				GTDFile = null,
				AdditionalCost = data.AdditionalCost,
				BrokerCost = data.BrokerCost,
				CustomCost = data.CustomCost,
				FlightCost = data.FlightCost,
				TotalCostOfSenderForWeight = data.TotalCostOfSenderForWeight
			};
		}

		public AirWaybillData GetData(long id)
		{
			return _awbRepository.Get(id).First();
		}

		public AirWaybillAggregate GetAggregate(long id)
		{
			return _awbRepository.GetAggregate(id).First();
		}

		public BrokerData GetBroker(long brokerId)
		{
			return _brokerRepository.Get(brokerId);
		}
	}
}