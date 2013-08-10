﻿using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Core.Exceptions;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Application
{
	// todo: test
	public sealed class ApplicationManager : IApplicationManager
	{
		private readonly IApplicationRepository _applicationRepository;
		private readonly IApplicationUpdateRepository _applicationUpdater;
		private readonly IStateConfig _stateConfig;
		private readonly IStateService _stateService;
		private readonly ITransitService _transitService;
		private readonly IIdentityService _identity;
		private readonly IAirWaybillRepository _airWaybillRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ICountryRepository _countryRepository;

		public ApplicationManager(
			IApplicationRepository applicationRepository,
			IApplicationUpdateRepository applicationUpdater,
			IStateConfig stateConfig,
			ITransitService transitService,
			IUnitOfWork unitOfWork, 
			IStateService stateService,
			ICountryRepository countryRepository, 
			IIdentityService identity,
			IAirWaybillRepository airWaybillRepository)
		{
			_applicationRepository = applicationRepository;
			_applicationUpdater = applicationUpdater;
			_stateConfig = stateConfig;
			_transitService = transitService;
			_unitOfWork = unitOfWork;
			_stateService = stateService;
			_countryRepository = countryRepository;
			_identity = identity;
			_airWaybillRepository = airWaybillRepository;
		}

		public ApplicationEditModel Get(long id)
		{
			var data = _applicationRepository.Get(id);

			var application = new ApplicationEditModel
			{
				Id = data.Id
			};

			SetAdditionalData(application);

			return application;
		}

		public void SetAdditionalData(params ApplicationEditModel[] applications)
		{
			SetTransitData(applications);

			SetAirWaybillData(applications);

			SetCountryData(applications);
		}

		private void SetCountryData(IEnumerable<ApplicationEditModel> applications)
		{
			var applicationWithCountry = applications.Where(x => x.CountryId.HasValue).ToArray();

			var countries = _countryRepository
				// ReSharper disable PossibleInvalidOperationException
				.Get(applicationWithCountry.Select(x => x.CountryId.Value).ToArray())
				// ReSharper restore PossibleInvalidOperationException
				.ToDictionary(x => x.Id, x => x.Name);

			foreach (var application in applicationWithCountry)
			{
				// ReSharper disable PossibleInvalidOperationException
				// ReSharper disable AssignNullToNotNullAttribute
				application.CountryName = countries[application.CountryId.Value][_identity.TwoLetterISOLanguageName];
				// ReSharper restore AssignNullToNotNullAttribute
				// ReSharper restore PossibleInvalidOperationException
			}
		}

		private void SetAirWaybillData(params ApplicationEditModel[] applications)
		{
			var applicationsWithAirWaybill = applications.Where(x => x.AirWaybillId.HasValue).ToArray();

			if (applicationsWithAirWaybill.Length == 0) return;

			var ids = applicationsWithAirWaybill.Select(x => x.AirWaybillId ?? 0).ToArray();

			var airWaybills = _airWaybillRepository.Get(ids).ToDictionary(x => x.Id, x => x);

			foreach (var application in applicationsWithAirWaybill)
			{
				if (!application.AirWaybillId.HasValue || !airWaybills.ContainsKey(application.AirWaybillId.Value))
					throw new InvalidLogicException();

				var airWaybillData = airWaybills[application.AirWaybillId.Value];

				application.AirWaybill = airWaybillData.Bill;
				application.AirWaybillGTD = airWaybillData.GTD;
				application.AirWaybillDateOfArrival = airWaybillData.DateOfArrival;
				application.AirWaybillDateOfDeparture = airWaybillData.DateOfDeparture;
			}
		}

		private void SetTransitData(params ApplicationEditModel[] applications)
		{
			var ids = applications.Select(x => x.TransitId).ToArray();
			var transits = _transitService.Get(ids).ToDictionary(x => x.Id, x => x);

			foreach (var application in applications)
			{
				application.Transit = transits[application.TransitId];
			}
		}

		public void Update(ApplicationEditModel model, CarrierSelectModel carrierSelectModel)
		{
			using (var ts = _unitOfWork.StartTransaction())
			{
				_transitService.Update(model.Transit, carrierSelectModel);

				//_applicationUpdater.Update(model.GetData(), model.SwiftFile, model.InvoiceFile, model.CPFile, model.DeliveryBillFile,
				//	model.Torg12File, model.PackingFile);

				_unitOfWork.SaveChanges();

				ts.Complete();
			}
		}

		public void Add(ApplicationEditModel model, CarrierSelectModel carrierSelectModel)
		{
			using (var ts = _unitOfWork.StartTransaction())
			{
				model.TransitId = _transitService.AddTransit(model.Transit, carrierSelectModel);
				model.StateId = _stateConfig.DefaultStateId;
				model.StateChangeTimestamp = DateTimeOffset.UtcNow;
				model.CreationTimestamp = DateTimeOffset.UtcNow;

				//var id = _applicationUpdater.Add(model.GetData(), model.SwiftFile, model.InvoiceFile, model.CPFile, model.DeliveryBillFile, model.Torg12File, model.PackingFile);

				_unitOfWork.SaveChanges();

				//model.Id = id();

				ts.Complete();
			}
		}

		public void Delete(long id)
		{
			using (var ts = _unitOfWork.StartTransaction())
			{
				var applicationData = _applicationRepository.Get(id);

				_applicationUpdater.Delete(id);

				_unitOfWork.SaveChanges();

				_transitService.Delete(applicationData.TransitId);

				ts.Complete();
			}
		}

		public void SetTransitReference(long id, string transitReference)
		{
			_applicationUpdater.SetTransitReference(id, transitReference);
			_unitOfWork.SaveChanges();
		}

		public void SetDateOfCargoReceipt(long id, DateTimeOffset? dateOfCargoReceipt)
		{
			_applicationUpdater.SetDateOfCargoReceipt(id, dateOfCargoReceipt);
			_unitOfWork.SaveChanges();
		}

		public void SetState(long applicationId, long stateId)
		{
			if (!_stateService.HasPermissionToSetState(stateId))
				throw new AccessForbiddenException("User don't have access to the state " + stateId);

			using (var ts = _unitOfWork.StartTransaction())
			{
				if (stateId == _stateConfig.CargoInStockStateId)
				{
					_applicationUpdater.SetDateInStock(applicationId, DateTimeOffset.UtcNow);
				}

				_applicationUpdater.SetState(applicationId, stateId);
				_unitOfWork.SaveChanges();

				ts.Complete();
			}
		}
	}
}