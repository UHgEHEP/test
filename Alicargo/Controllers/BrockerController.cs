﻿using System.Linq;
using System.Web.Mvc;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Exceptions;
using Alicargo.Contracts.Repositories;
using Alicargo.Helpers;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.AirWaybill;
using Resources;

namespace Alicargo.Controllers
{
	public partial class BrockerController : Controller
	{
		private readonly IAwbRepository _awbRepository;
		private readonly IAwbUpdateManager _awbUpdateManager;
		private readonly IBrockerRepository _brockerRepository;
		private readonly IStateConfig _stateConfig;

		public BrockerController(
			IBrockerRepository brockerRepository,
			IStateConfig stateConfig,
			IAwbRepository awbRepository,
			IAwbUpdateManager awbUpdateManager)
		{
			_brockerRepository = brockerRepository;
			_stateConfig = stateConfig;
			_awbRepository = awbRepository;
			_awbUpdateManager = awbUpdateManager;
		}

		[ChildActionOnly]
		public virtual PartialViewResult Select(string name, long? selectedId)
		{
			var all = _brockerRepository.GetAll();

			var model = new SelectModel
			{
				Id = selectedId.HasValue
					? selectedId.Value
					: all.First().Id, // todo: 3. test
				List = all.ToDictionary(x => x.Id, x => x.Name),
				Name = name
			};

			return PartialView(model);
		}

		[Access(RoleType.Brocker), HttpGet]
		public virtual ViewResult AWB(long id)
		{
			var data = _awbRepository.Get(id).First();

			if (data.StateId == _stateConfig.CargoIsCustomsClearedStateId)
			{
				return View("Message", (object) string.Format(Pages.CantEditAirWaybill, data.Bill));
			}

			var model = Map(data);

			BindBag(data);

			return View(model);
		}

		private static BrockerAwbModel Map(AirWaybillData data)
		{
			return new BrockerAwbModel
			{
				GTD = data.GTD,
				GTDAdditionalFileName = data.GTDAdditionalFileName,
				InvoiceFileName = data.InvoiceFileName,
				GTDFileName = data.GTDFileName,
				PackingFileName = data.PackingFileName,
				GTDAdditionalFile = null,
				InvoiceFile = null,
				GTDFile = null,
				PackingFile = null,
				BrokerCost = data.BrokerCost,
				CustomCost = data.CustomCost
			};
		}

		private void BindBag(AirWaybillData data)
		{
			ViewBag.AWB = data.Bill;
			ViewBag.AwbId = data.Id;
		}

		// todo: 1.5. bb test
		[Access(RoleType.Brocker), HttpPost]
		public virtual ActionResult AWB(long id, BrockerAwbModel model)
		{
			if (!ModelState.IsValid)
			{
				var data = _awbRepository.Get(id).First();
				BindBag(data);

				return View(model);
			}

			try
			{
				_awbUpdateManager.Update(id, model);
			}
			catch (UnexpectedStateException ex)
			{
				if (ex.StateId == _stateConfig.CargoIsCustomsClearedStateId)
				{
					var data = _awbRepository.Get(id).First();

					return View("Message", (object) string.Format(Pages.CantEditAirWaybill, data.Bill));
				}

				throw;
			}

			return RedirectToAction(MVC.Brocker.AWB(id));
		}
	}
}