﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Event;
using Alicargo.MvcHelpers.Extensions;
using Alicargo.MvcHelpers.Filters;

namespace Alicargo.Controllers
{
	public partial class FilesController : Controller
	{
		private static readonly IReadOnlyDictionary<ApplicationFileType, EventType> TypesMapping
			= new Dictionary<ApplicationFileType, EventType>
			{
				{ApplicationFileType.CP, EventType.CPFileUploaded},
				{ApplicationFileType.Invoice, EventType.InvoiceFileUploaded},
				{ApplicationFileType.DeliveryBill, EventType.DeliveryBillFileUploaded},
				{ApplicationFileType.Torg12, EventType.Torg12FileUploaded},
				{ApplicationFileType.Swift, EventType.SwiftFileUploaded},
				{ApplicationFileType.Packing, EventType.PackingFileUploaded},
			};

		private readonly IEventFacadeForApplication _facade;
		private readonly IApplicationFileRepository _files;

		public FilesController(
			IApplicationFileRepository files,
			IEventFacadeForApplication facade)
		{
			_files = files;
			_facade = facade;
		}

		public virtual FileResult Download(long id)
		{
			var file = _files.Get(id);

			return file.GetFileResult();
		}

		[HttpGet]
		[Access(RoleType.Admin)]
		public virtual ViewResult AdminApplication(long id)
		{
			ViewBag.ApplicationId = id;

			return View();
		}

		[HttpGet]
		[Access(RoleType.Client)]
		public virtual ViewResult ClientApplication(long id)
		{
			ViewBag.ApplicationId = id;

			return View();
		}

		[HttpGet]
		[Access(RoleType.Sender)]
		public virtual ViewResult SenderApplication(long id)
		{
			ViewBag.ApplicationId = id;

			return View();
		}

		[HttpGet]
		public virtual JsonResult Files(long id, ApplicationFileType type)
		{
			var names = _files.GetNames(id, type);

			ViewBag.ApplicationId = id;

			return Json(names.Select(x => new
			{
				id = x.Key,
				name = x.Value
			}), JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public virtual JsonResult Upload(long id, ApplicationFileType type, HttpPostedFileBase file)
		{
			var bytes = file.GetBytes();

			var fileId = _files.Add(id, type, file.FileName, bytes);

			AddFileUploadEvent(id, TypesMapping[type], file.FileName, bytes);

			return Json(new { id = fileId });
		}

		[HttpPost]
		public virtual HttpStatusCodeResult Delete(long id)
		{
			_files.Delete(id);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		private void AddFileUploadEvent(long applicationId, EventType type,
			string fileName, byte[] fileData)
		{
			if (fileData == null || fileData.Length == 0) return;

			_facade.Add(applicationId,
				type, EventState.Emailing,
				new FileHolder
				{
					Data = fileData,
					Name = fileName
				});
		}
	}
}