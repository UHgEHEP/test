﻿using System.Collections.Generic;
using System.Linq;
using Alicargo.Core.Enums;
using Alicargo.Core.Exceptions;
using Alicargo.Core.Helpers;
using Alicargo.Core.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Resources;

namespace Alicargo.Services.Application
{
	public sealed class ApplicationListPresenter : IApplicationListPresenter
	{
		private readonly IApplicationRepository _applicationRepository;
		private readonly IApplicationGrouper _applicationGrouper;
		private readonly ICountryRepository _countryRepository;
		private readonly ITransitService _transitService;
		private readonly IIdentityService _identity;
		private readonly IReferenceRepository _referenceRepository;
		private readonly IClientRepository _clientRepository;
		private readonly IStateService _stateService;
		private readonly IStateConfig _stateConfig;

		public ApplicationListPresenter(IApplicationRepository applicationRepository,
			IStateService stateService, IReferenceRepository referenceRepository, 
			IIdentityService identity, IApplicationGrouper applicationGrouper, 
			ICountryRepository countryRepository, ITransitService transitService, 
			IClientRepository clientRepository, IStateConfig stateConfig)
		{
			_applicationRepository = applicationRepository;
			_stateService = stateService;
			_identity = identity;
			_applicationGrouper = applicationGrouper;
			_countryRepository = countryRepository;
			_transitService = transitService;
			_referenceRepository = referenceRepository;
			_clientRepository = clientRepository;
			_stateConfig = stateConfig;
		}

		public ApplicationListCollection List(int take, int skip, Order[] groups)
		{
			var stateIds = _stateService.GetVisibleStates();

			var isClient = _identity.IsInRole(RoleType.Client);

			var orders = PrepareOrders(groups);

			var data = _applicationRepository.Get(take, skip, stateIds, orders,
				isClient ? _identity.Id : null);

			var applications = data.Select(x => new ApplicationListItem(x)).ToArray();

			SetAdditionalData(applications);

			if (groups == null || groups.Length == 0)
			{
				return new ApplicationListCollection
				{
					Data = applications,
					Total = _applicationRepository.Count(stateIds, isClient ? _identity.Id : null),
				};
			}

			return new ApplicationListCollection
			{
				Total = _applicationRepository.Count(stateIds, isClient ? _identity.Id : null),
				Groups = _applicationGrouper.Group(applications, groups),
			};
		}

		private static Order[] PrepareOrders(IEnumerable<Order> orders)
		{
			var byId = new[] { new Order { Desc = true, OrderType = OrderType.Id } };

			return orders == null ? byId : orders.Concat(byId).ToArray();
		}

		public void SetAdditionalData(params ApplicationListItem[] applications)
		{
			SetClientData(applications);

			SetStateData(applications);

			SetTransitData(applications);

			SetReferenceData(applications);

			SetCountryData(applications);
		}

		private void SetCountryData(IEnumerable<ApplicationListItem> applications)
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

		private void SetReferenceData(params ApplicationListItem[] applications)
		{
			var applicationsWithReference = applications.Where(x => x.ReferenceId.HasValue).ToArray();

			if (applicationsWithReference.Length == 0) return;

			var ids = applicationsWithReference.Select(x => x.ReferenceId ?? 0).ToArray();

			var references = _referenceRepository.Get(ids).ToDictionary(x => x.Id, x => x);

			foreach (var application in applicationsWithReference)
			{
				if (!application.ReferenceId.HasValue || !references.ContainsKey(application.ReferenceId.Value))
					throw new InvalidLogicException();

				var referenceData = references[application.ReferenceId.Value];

				application.ReferenceBill = referenceData.Bill;
				application.ReferenceGTD = referenceData.GTD;
				application.ReferenceDateOfArrival = referenceData.DateOfArrival;
				application.ReferenceDateOfDeparture = referenceData.DateOfDeparture;
				application.AirWayBillDisplay = string.Format("{0} &plusmn; {1}_{2} &plusmn; {3}_{4}{5}", referenceData.Bill,
					referenceData.DepartureAirport, referenceData.DateOfDeparture.ToString("ddMMMyyyy").ToUpperInvariant(),
					referenceData.ArrivalAirport, referenceData.DateOfArrival.ToString("ddMMMyyyy").ToUpperInvariant(),
					string.IsNullOrWhiteSpace(referenceData.GTD) ? "" : string.Format(" &plusmn; {0}_{1}", Entities.GTD, referenceData.GTD));
			}
		}

		private void SetTransitData(params ApplicationListItem[] applications)
		{
			var ids = applications.Select(x => x.TransitId).ToArray();
			var transits = _transitService.Get(ids).ToDictionary(x => x.Id, x => x);

			foreach (var application in applications)
			{
				application.Transit = transits[application.TransitId];
			}
		}

		private void SetClientData(params ApplicationListItem[] applications)
		{
			var clientIds = applications.Select(x => x.ClientId).ToArray();
			var clients = _clientRepository.Get(clientIds).ToDictionary(x => x.Id, x => x);

			foreach (var application in applications)
			{
				var clientData = clients[application.ClientId];

				application.ClientUserId = clientData.UserId;
				application.LegalEntity = clientData.LegalEntity;
				application.ClientNic = clientData.Nic;
				application.ClientEmail = clientData.Email;
			}
		}

		private void SetStateData(params ApplicationListItem[] applications)
		{
			var localizedStates = _stateService.GetLocalizedDictionary();

			var availableStates = _stateService.GetAvailableStatesToSet();

			var states = _stateService.GetDictionary();

			foreach (var application in applications)
			{
				var state = states[application.StateId];

				application.CanClose = state.Id == _stateConfig.CargoOnTransitStateId; // todo: test 1.

				// todo: test 1.
				application.CanSetState = availableStates.Contains(application.StateId);

				application.StateName = localizedStates[application.StateId];
			}
		}
	}
}