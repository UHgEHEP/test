﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Alicargo.Core.Contracts.Common;
using Alicargo.Core.Contracts.State;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Helpers;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Application;

namespace Alicargo.Controllers.Application
{
	public partial class ApplicationListController : Controller
	{
		private readonly IApplicationListPresenter _presenter;
		private readonly IAwbRepository _awbs;
		private readonly IClientRepository _clients;
		private readonly IForwarderRepository _forwarders;
		private readonly IIdentityService _identity;
		private readonly ISenderRepository _senders;
		private readonly IStateConfig _stateConfig;

		public ApplicationListController(IApplicationListPresenter presenter,
			IClientRepository clients, ISenderRepository senders, IAwbRepository awbs,
			IStateConfig stateConfig, IIdentityService identity, IForwarderRepository forwarders)
		{
			_presenter = presenter;
			_clients = clients;
			_senders = senders;
			_awbs = awbs;
			_stateConfig = stateConfig;
			_identity = identity;
			_forwarders = forwarders;
		}

		[Access(RoleType.Admin, RoleType.Client, RoleType.Forwarder, RoleType.Sender)]
		public virtual ViewResult Index()
		{
			var clients = _clients.GetAll()
				.OrderBy(x => x.Nic)
				.ToDictionary(x => x.ClientId, x => x.Nic);

			var model = new ApplicationIndexModel
			{
				Clients = clients,
				AirWaybills = _awbs.Get()
					.Where(x => x.StateId == _stateConfig.CargoIsFlewStateId)
					.OrderBy(x => x.Bill)
					.ToDictionary(x => x.Id, x => x.Bill)
			};

			return View(model);
		}

		[HttpPost]
		[Access(RoleType.Admin, RoleType.Client, RoleType.Forwarder, RoleType.Sender)]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List(int take, int skip, Dictionary<string, string>[] group)
		{
			var orders = OrderHelper.Get(group);

			Debug.Assert(_identity.Id != null);

			var senderId = _senders.GetByUserId(_identity.Id.Value);

			var forwarderId = _forwarders.GetByUserId(_identity.Id.Value);

			var client = _clients.GetByUserId(_identity.Id.Value);

			var clientId = client != null
				? client.ClientId
				: (long?)null;

			var data = _presenter.List(_identity.Language, take, skip, orders, clientId, senderId, forwarderId);

			return Json(data);
		}
	}
}