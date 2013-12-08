﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.MvcHelpers.Extensions;

namespace Alicargo.Controllers
{
	public partial class FilesController : Controller
	{
		private static readonly IReadOnlyDictionary<ApplicationFileType, ApplicationEventType> TypesMapping
			= new Dictionary<ApplicationFileType, ApplicationEventType>
			{
				{ApplicationFileType.CP, ApplicationEventType.CPFileUploaded},
				{ApplicationFileType.Invoice, ApplicationEventType.InvoiceFileUploaded},
				{ApplicationFileType.DeliveryBill, ApplicationEventType.DeliveryBillFileUploaded},
				{ApplicationFileType.Torg12, ApplicationEventType.Torg12FileUploaded},
				{ApplicationFileType.Swift, ApplicationEventType.SwiftFileUploaded},
				{ApplicationFileType.Packing, ApplicationEventType.PackingFileUploaded},
			};

		private readonly IApplicationEventRepository _events;
		private readonly IApplicationFileRepository _files;
		private readonly ISerializer _serializer;

		public FilesController(
			IApplicationFileRepository files,
			IApplicationEventRepository events,
			ISerializer serializer)
		{
			_files = files;
			_events = events;
			_serializer = serializer;
		}

		public virtual FileResult Download(long id)
		{
			var file = _files.Get(id);

			return file.GetFileResult();
		}

		[HttpGet]
		public virtual ViewResult Application(long id)
		{
			ViewBag.ApplicationId = id;

			return View();
		}

		[HttpGet]
		public virtual JsonResult Files(long id, ApplicationFileType type)
		{
			var names = _files.GetFileNames(id, type);

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

			return Json(new {id = fileId});
		}

		[HttpPost]
		public virtual HttpStatusCodeResult Delete(long id)
		{
			_files.Delete(id);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		private void AddFileUploadEvent(long applicationId, ApplicationEventType type,
			string fileName, byte[] fileData)
		{
			if (fileData == null || fileData.Length == 0) return;

			_events.Add(applicationId, type, _serializer.Serialize(
				new FileHolder
				{
					Data = fileData,
					Name = fileName
				}));
		}
	}
}