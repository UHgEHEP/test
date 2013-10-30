﻿using System.Web.Mvc;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.User;
using Resources;

namespace Alicargo.Controllers
{
	public partial class AuthenticationController : Controller
	{
		private readonly IAuthenticationService _authentication;
		private readonly IIdentityService _identity;
		private readonly IAuthenticationRepository _authentications;

		public AuthenticationController(
			IIdentityService identity,
			IAuthenticationRepository authentications,
			IAuthenticationService authentication)
		{
			_identity = identity;
			_authentications = authentications;
			_authentication = authentication;
		}

		#region Authentication

		[HttpGet]
		public virtual ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public virtual ActionResult Login(SignIdModel user)
		{
			if (!ModelState.IsValid) return View(user);

			if (!_authentication.Authenticate(user))
			{
				ModelState.AddModelError("Login", Validation.WrongLoginOrPassword);
				return View(user);
			}

			if (_identity.IsInRole(RoleType.Admin))
				return RedirectToAction(MVC.Home.Index());

			return RedirectToAction(MVC.Home.Index());
		}

		[Access(RoleType.Admin), HttpGet]
		public virtual ActionResult LoginAsUser(int id)
		{
			var user = _authentications.GetById(id);

			_authentication.AuthenticateForce(user.Id, false);

			return RedirectToAction(MVC.Home.Index());
		}

		#endregion

		[ChildActionOnly]
		public virtual PartialViewResult Client(long? clientId)
		{
			ViewData.TemplateInfo.HtmlFieldPrefix = "Authentication";

			if (!clientId.HasValue) return PartialView();

			var data = _authentications.GetByClientId(clientId.Value);

			var model = new AuthenticationModel(data.Login);

			return PartialView(model);
		}
	}
}
