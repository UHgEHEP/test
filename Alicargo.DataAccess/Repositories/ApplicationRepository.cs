﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Helpers;
using Alicargo.Core.Contracts;
using Alicargo.Core.Repositories;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Helpers;

namespace Alicargo.DataAccess.Repositories
{
	internal sealed class ApplicationRepository : BaseRepository, IApplicationRepository
	{
		private readonly IApplicationRepositoryOrderer _orderer;
		private readonly Expression<Func<Application, ApplicationData>> _selector;

		public ApplicationRepository(IUnitOfWork unitOfWork, IApplicationRepositoryOrderer orderer)
			: base(unitOfWork)
		{
			_orderer = orderer;

			_selector = x => new ApplicationData
			{
				Id = x.Id,
				CreationTimestamp = x.CreationTimestamp,

				FactoryEmail = x.FactoryEmail,
				FactoryName = x.FactoryName,
				FactoryPhone = x.FactoryPhone,
				FactoryContact = x.FactoryContact,

				InvoiceFileName = x.InvoiceFileName,
				CPFileName = x.CPFileName,
				DeliveryBillFileName = x.DeliveryBillFileName,
				SwiftFileName = x.SwiftFileName,
				Torg12FileName = x.Torg12FileName,
				PackingFileName = x.PackingFileName,

				Weigth = x.Gross,
				AddressLoad = x.AddressLoad,
				Characteristic = x.Characteristic,
				Count = x.Count,
				Invoice = x.Invoice,
				MethodOfDeliveryId = x.MethodOfDeliveryId,
				TermsOfDelivery = x.TermsOfDelivery,
				Value = x.Value,
				CurrencyId = x.CurrencyId,
				Volume = x.Volume,
				WarehouseWorkingTime = x.WarehouseWorkingTime,
				DateInStock = x.DateInStock,
				DateOfCargoReceipt = x.DateOfCargoReceipt,
				TransitReference = x.TransitReference,
				MarkName = x.MarkName,

				ReferenceId = x.ReferenceId,
				CountryId = x.CountryId,
				StateChangeTimestamp = x.StateChangeTimestamp,
				StateId = x.StateId,
				ClientId = x.ClientId,

				TransitId = x.TransitId
			};
		}

		public ApplicationData Get(long id)
		{
			return Context.Applications.Where(x => x.Id == id).Select(_selector).FirstOrDefault();
		}

		public long Count(IEnumerable<long> stateIds, long? clientUserId = null)
		{
			var applications = Context.Applications.Where(x => stateIds.Contains(x.StateId));

			if (clientUserId.HasValue)
			{
				applications = applications.Where(x => x.Client.UserId == clientUserId.Value);
			}

			return applications.LongCount();
		}

		public ApplicationData[] List(int take, int skip, IEnumerable<long> stateIds,
			Order[] orders = null, long? clientUserId = null)
		{
			var applications = GetQueryable(take, skip, stateIds, orders, clientUserId);

			return applications.Select(_selector).ToArray();
		}

		private IQueryable<Application> GetQueryable(int take, int skip, IEnumerable<long> stateIds, IEnumerable<Order> orders, long? clientUserId)
		{
			var applications = Context.Applications.Where(x => stateIds.Contains(x.StateId));

			if (clientUserId.HasValue)
			{
				applications = applications.Where(x => x.Client.UserId == clientUserId.Value);
			}

			applications = _orderer.Order(applications, orders)
								   .Skip(skip)
								   .Take(take);
			return applications;
		}

		public ApplicationData[] GetByReference(long id)
		{
			return Context.Applications.Where(x => x.ReferenceId == id).Select(_selector).ToArray();
		}

		public ApplicationData GetByTransit(long id)
		{
			return Context.Applications.Where(x => x.TransitId == id).Select(_selector).FirstOrDefault();
		}

		#region Files

		public FileHolder GetInvoiceFile(long id)
		{
			return GetFile(
				x => x.Id == id && x.InvoiceFileName != null && x.InvoiceFileData != null,
				application => new FileHolder
				{
					FileName = application.InvoiceFileName,
					FileData = application.InvoiceFileData.ToArray()
				});
		}

		public FileHolder GetSwiftFile(long id)
		{
			return GetFile(
				x => x.Id == id && x.SwiftFileName != null && x.SwiftFileData != null,
				application => new FileHolder
				{
					FileName = application.SwiftFileName,
					FileData = application.SwiftFileData.ToArray()
				});
		}

		public FileHolder GetCPFile(long id)
		{
			return GetFile(
				x => x.Id == id && x.CPFileName != null && x.CPFileData != null,
				application => new FileHolder
				{
					FileName = application.CPFileName,
					FileData = application.CPFileData.ToArray()
				});
		}

		public FileHolder GetDeliveryBillFile(long id)
		{
			return GetFile(
				x => x.Id == id && x.DeliveryBillFileName != null && x.DeliveryBillFileData != null,
				application => new FileHolder
				{
					FileName = application.DeliveryBillFileName,
					FileData = application.DeliveryBillFileData.ToArray()
				});
		}

		public FileHolder GetTorg12File(long id)
		{
			return GetFile(
				x => x.Id == id && x.Torg12FileName != null && x.Torg12FileData != null,
				application => new FileHolder
				{
					FileName = application.Torg12FileName,
					FileData = application.Torg12FileData.ToArray()
				});
		}

		public FileHolder GetPackingFile(long id)
		{
			return GetFile(
				x => x.Id == id && x.PackingFileName != null && x.PackingFileData != null,
				application => new FileHolder
				{
					FileName = application.PackingFileName,
					FileData = application.PackingFileData.ToArray()
				});
		}		

		private FileHolder GetFile(Expression<Func<Application, bool>> where, Expression<Func<Application, FileHolder>> selector)
		{
			return Context.Applications.Where(where).Select(selector).FirstOrDefault();
		}

		#endregion		
	}
}