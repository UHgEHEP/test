﻿using System;
using System.Web.Mvc;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Exceptions;
using Alicargo.MvcHelpers.Filters;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.User;
using Resources;

namespace Alicargo.Controllers.User
{
	public partial class UserController : Controller
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		#region List

		[Access(RoleType.Admin, RoleType.Manager), HttpGet]
		public virtual ViewResult Index(RoleType roleType)
		{
			return View(roleType);
		}

		[Access(RoleType.Admin, RoleType.Manager), HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List(RoleType roleType)
		{
			var list = _userService.List(roleType);

			return Json(list);
		}

		#endregion		

		#region Edit/Create

		[Access(RoleType.Admin, RoleType.Manager), HttpGet]
		public virtual ViewResult Edit(RoleType roleType, long id)
		{
			if(roleType != RoleType.Admin && roleType != RoleType.Broker && roleType != RoleType.Manager)
			{
				throw new NotSupportedException();
			}

			var model = _userService.Get(roleType, id);

			return View(model);
		}

		[Access(RoleType.Admin, RoleType.Manager), HttpGet]
		public virtual ViewResult Create(RoleType roleType)
		{
			if(roleType != RoleType.Admin && roleType != RoleType.Broker && roleType != RoleType.Manager)
			{
				throw new NotSupportedException();
			}

			return View();
		}

		[Access(RoleType.Admin, RoleType.Manager), HttpPost]
		public virtual ActionResult Edit(UserModel model)
		{
			if (!ModelState.IsValid) return View();

			try
			{
				_userService.Update(model);

				return RedirectToAction(MVC.User.Index(model.RoleType));
			}
			catch (DublicateLoginException)
			{
				ModelState.AddModelError("Authentication.Login", Validation.LoginExists);

				return View();
			}
		}

		[Access(RoleType.Admin, RoleType.Manager), HttpPost]
		public virtual ActionResult Create(UserModel model)
		{
			if (string.IsNullOrWhiteSpace(model.Authentication.NewPassword))
				ModelState.AddModelError("NewPassword", Validation.EmplyPassword);

			if (!ModelState.IsValid) return View();

			try
			{
				_userService.Add(model);

				return RedirectToAction(MVC.User.Index(model.RoleType));
			}
			catch (DublicateLoginException)
			{
				ModelState.AddModelError("Authentication.Login", Validation.LoginExists);

				return View();
			}
		}

		#endregion
	}
}