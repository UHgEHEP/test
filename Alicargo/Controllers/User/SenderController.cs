﻿using System.Linq;
using System.Web.Mvc;
using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.User;
using Resources;

namespace Alicargo.Controllers.User
{
	public partial class SenderController : Controller
	{
		private readonly ICountryRepository _countries;
		private readonly IIdentityService _identity;
		private readonly ISenderService _senders;

		public SenderController(
			ISenderService senders,
			IIdentityService identity,
			ICountryRepository countries)
		{
			_senders = senders;
			_identity = identity;
			_countries = countries;
		}

		[HttpGet]
		[Access(RoleType.Admin)]
		public virtual ViewResult Create()
		{
			BindBag();

			return View();
		}

		[HttpGet]
		[Access(RoleType.Admin)]
		public virtual ViewResult Edit(long id)
		{
			BindBag();

			var model = _senders.Get(id);

			return View(model);
		}

		[HttpPost]
		[Access(RoleType.Admin)]
		public virtual ActionResult Create(SenderModel model)
		{
			if(string.IsNullOrWhiteSpace(model.Authentication.NewPassword))
				ModelState.AddModelError("NewPassword", Validation.EmplyPassword);

			if(!ModelState.IsValid)
			{
				BindBag();

				return View();
			}

			try
			{
				var id = _senders.Add(model);

				return RedirectToAction(MVC.Sender.Edit(id));
			}
			catch(DublicateLoginException)
			{
				ModelState.AddModelError("Authentication.Login", Validation.LoginExists);

				BindBag();

				return View();
			}
		}

		[HttpPost]
		[Access(RoleType.Admin)]
		public virtual ActionResult Edit(long id, SenderModel model)
		{
			if(!ModelState.IsValid)
			{
				BindBag();

				return View();
			}

			try
			{
				_senders.Update(id, model);

				return RedirectToAction(MVC.Sender.Edit(id));
			}
			catch(DublicateLoginException)
			{
				ModelState.AddModelError("Authentication.Login", Validation.LoginExists);

				BindBag();

				return View();
			}
		}

		private void BindBag()
		{
			ViewBag.Countries = _countries.All(_identity.Language).ToDictionary(x => x.Id, x => x.Name);
		}
	}
}