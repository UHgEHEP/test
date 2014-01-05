﻿using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Core.Enums;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.Services.Excel;

namespace Alicargo.Controllers.Calculation
{
	public partial class CalculationController : Controller
	{
		private readonly IApplicationManager _applicationManager;
		private readonly IAwbUpdateManager _awbUpdateManager;
		private readonly ICalculationService _calculation;
		private readonly IIdentityService _identity;
		private readonly IAdminCalculationPresenter _presenter;

		public CalculationController(
			IAwbUpdateManager awbUpdateManager,
			IAdminCalculationPresenter presenter,
			ICalculationService calculation,
			IIdentityService identity,
			IApplicationManager applicationManager)
		{
			_awbUpdateManager = awbUpdateManager;
			_presenter = presenter;
			_calculation = calculation;
			_identity = identity;
			_applicationManager = applicationManager;
		}

		[Access(RoleType.Admin), HttpGet]
		public virtual ActionResult Index()
		{
			ViewBag.Balance = _presenter.GetTotalBalance();

			return View();
		}

		[Access(RoleType.Admin)]
		public virtual FileResult Excel()
		{
			var data = _presenter.List(int.MaxValue, 0);

			var excel = new ExcelAdminCalculation();

			var stream = excel.Get(data, _identity.Language);

			return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "calculation.xlsx");
		}

		[Access(RoleType.Admin), HttpPost, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List(int take, long skip)
		{
			var data = _presenter.List(take, skip);

			return Json(data);
		}

		[Access(RoleType.Admin), HttpPost, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult SetTariffPerKg(long id, long awbId, decimal? tariffPerKg)
		{
			_applicationManager.SetTariffPerKg(id, tariffPerKg);

			var data = _presenter.Row(awbId);

			return Json(data);
		}

		[Access(RoleType.Admin), HttpPost, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult Calculate(long id, long awbId)
		{
			_calculation.Calculate(id);

			var data = _presenter.Row(awbId);

			return Json(data);
		}

		[Access(RoleType.Admin), HttpPost, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult RemoveCalculatation(long id, long awbId)
		{
			_calculation.CancelCalculatation(id);

			var data = _presenter.Row(awbId);

			return Json(data);
		}

		[Access(RoleType.Admin), HttpPost, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult SetSenderRate(long id, long awbId, decimal? senderRate)
		{
			_applicationManager.SetSenderRate(id, senderRate);

			var data = _presenter.Row(awbId);

			return Json(data);
		}

		[Access(RoleType.Admin), HttpPost, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult SetScotchCostEdited(long id, long awbId, decimal? scotchCost)
		{
			_applicationManager.SetScotchCostEdited(id, scotchCost);

			var data = _presenter.Row(awbId);

			return Json(data);
		}

		[Access(RoleType.Admin), HttpPost, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult SetFactureCostEdited(long id, long awbId, decimal? factureCost)
		{
			_applicationManager.SetFactureCostEdited(id, factureCost);

			var data = _presenter.Row(awbId);

			return Json(data);
		}

		[Access(RoleType.Admin), HttpPost, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult SetTransitCostEdited(long id, long awbId, decimal? transitCost)
		{
			_applicationManager.SetTransitCostEdited(id, transitCost);

			var data = _presenter.Row(awbId);

			return Json(data);
		}

		[Access(RoleType.Admin), HttpPost, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult SetPickupCostEdited(long id, long awbId, decimal? pickupCost)
		{
			_applicationManager.SetPickupCostEdited(id, pickupCost);

			var data = _presenter.Row(awbId);

			return Json(data);
		}

		[Access(RoleType.Admin), HttpPost, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult SetAdditionalCost(long awbId, decimal? additionalCost)
		{
			_awbUpdateManager.SetAdditionalCost(awbId, additionalCost);

			var data = _presenter.Row(awbId);

			return Json(data);
		}

		[Access(RoleType.Admin), HttpPost, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult SetClass(long id, long awbId, int? classId)
		{
			_applicationManager.SetClass(id, (ClassType?)classId);

			var data = _presenter.Row(awbId);

			return Json(data);
		}
	}
}