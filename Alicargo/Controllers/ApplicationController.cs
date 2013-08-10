﻿using System.Linq;
using System.Web.Mvc;
using Alicargo.Core.Enums;
using Alicargo.Core.Exceptions;
using Alicargo.Core.Repositories;
using Alicargo.Helpers;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using System.Net;
using Alicargo.ViewModels.Application;

namespace Alicargo.Controllers
{
	// todo: refactor contracts
	public partial class ApplicationController : Controller
	{
		private readonly IApplicationManager _applicationManager;
		private readonly IApplicationPresenter _applicationPresenter;
		private readonly IIdentityService _identityService;
		private readonly IClientService _clientService;
		private readonly ICountryRepository _countryRepository;
		private readonly IApplicationRepository _applicationRepository;

		public ApplicationController(
			IApplicationManager applicationManager,
			IApplicationPresenter applicationPresenter,
			IIdentityService identityService,
			IClientService clientService,
			ICountryRepository countryRepository,
			IApplicationRepository applicationRepository)
		{
			_applicationManager = applicationManager;
			_applicationPresenter = applicationPresenter;
			_identityService = identityService;
			_clientService = clientService;
			_countryRepository = countryRepository;
			_applicationRepository = applicationRepository;
		}

		#region Details

		[HttpGet, ChildActionOnly]
		[Access(RoleType.Client)]
		public virtual PartialViewResult Details(long id)
		{
			var application = _applicationPresenter.Get(id);

			return PartialView(application);
		}

		public virtual FileResult InvoiceFile(long id)
		{
			var file = _applicationRepository.GetInvoiceFile(id);

			return file.FileData.GetFileResult(file.FileName);
		}

		public virtual FileResult DeliveryBillFile(long id)
		{
			var file = _applicationRepository.GetDeliveryBillFile(id);

			return file.FileData.GetFileResult(file.FileName);
		}

		public virtual FileResult CPFile(long id)
		{
			var file = _applicationRepository.GetCPFile(id);

			return file.FileData.GetFileResult(file.FileName);
		}

		public virtual FileResult SwiftFile(long id)
		{
			var file = _applicationRepository.GetSwiftFile(id);

			return file.FileData.GetFileResult(file.FileName);
		}

		public virtual FileResult Torg12File(long id)
		{
			var file = _applicationRepository.GetTorg12File(id);

			return file.FileData.GetFileResult(file.FileName);
		}

		public virtual FileResult PackingFile(long id)
		{
			var file = _applicationRepository.GetPackingFile(id);

			return file.FileData.GetFileResult(file.FileName);
		}

		#endregion

		private void BindBag(long? clientId)
		{
			var client = _clientService.GetClient(clientId);

			ViewBag.ClientNic = client.Nic;

			ViewBag.Countries = _countryRepository.Get()
				.ToDictionary(x => x.Id, x => x.Name[_identityService.TwoLetterISOLanguageName]);
		}

		[HttpPost]
		[Access(RoleType.Admin)]
		public virtual HttpStatusCodeResult Delete(long id)
		{
			_applicationManager.Delete(id);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		#region Edit

		[HttpGet]
		[Access(RoleType.Admin)]
		public virtual ViewResult Edit(long id)
		{
			var application = _applicationManager.Get(id);

			var clientId = _applicationRepository.GetClientId(id);

			BindBag(clientId);

			return View(application);
		}

		// todo: test
		[HttpPost]
		//[ValidateAntiForgeryToken]
		[Access(RoleType.Admin)]
		public virtual ActionResult Edit(long id, ApplicationEditModel model, CarrierSelectModel carrierSelectModel)
		{
			if (!ModelState.IsValid)
			{
				var clientId = _applicationRepository.GetClientId(id);
				BindBag(clientId);
				return View(model);
			}

			_applicationManager.Update(model, carrierSelectModel);

			return RedirectToAction(MVC.ApplicationList.Index());
		}

		#endregion

		#region Create

		[Access(RoleType.Admin, RoleType.Client)]
		public virtual ViewResult Create(long? clientId)
		{
			BindBag(clientId);

			return View();
		}

		// todo: test
		[HttpPost]
		//[ValidateAntiForgeryToken]
		[Access(RoleType.Admin, RoleType.Client)]
		public virtual ActionResult Create(long? clientId, ApplicationEditModel model, CarrierSelectModel carrierSelectModel)
		{
			if (!ModelState.IsValid)
			{
				BindBag(clientId);
				return View(model);
			}

			try
			{
				_applicationManager.Add(model, carrierSelectModel);
			}
			catch (DublicateException ex)
			{
				ModelState.AddModelError("DublicateException", ex.ToString());
				return View(model);
			}

			return RedirectToAction(MVC.ApplicationList.Index());
		}

		#endregion
	}
}