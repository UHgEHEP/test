﻿using System;
using System.Data;
using System.Linq;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.DataAccess.DbContext;
using Alicargo.Utilities;

namespace Alicargo.DataAccess.Repositories.Application
{
	public sealed class ApplicationEditor : IApplicationEditor
	{
		private readonly ISqlProcedureExecutor _executor;
		private readonly long _defaultStateId;
		private readonly AlicargoDataContext _context;

		public ApplicationEditor(IDbConnection connection, ISqlProcedureExecutor executor, long defaultStateId)
		{
			_executor = executor;
			_defaultStateId = defaultStateId;
			_context = new AlicargoDataContext(connection);
		}

		public long Add(ApplicationEditData application)
		{
			var entity = new DbContext.Application();

			Map(application, entity);

			entity.StateId = _defaultStateId;
			entity.StateChangeTimestamp = DateTimeProvider.Now;
			entity.DisplayNumber = _executor.Query<int>("[dbo].[GetNextDisplayNumber]");
			entity.CreationTimestamp = DateTimeProvider.Now;

			_context.Applications.InsertOnSubmit(entity);

			_context.SaveChanges();

			return entity.Id;
		}

		public void Delete(long id)
		{
			var application = _context.Applications.First(x => x.Id == id);
			_context.Applications.DeleteOnSubmit(application);

			_context.SaveChanges();
		}

		public void SetState(long id, long stateId)
		{
			Update(id, application =>
			{
				application.StateId = stateId;
				application.StateChangeTimestamp = DateTimeProvider.Now;
			});
		}

		public void SetAirWaybill(long id, long? airWaybillId)
		{
			Update(id, application => application.AirWaybillId = airWaybillId);
		}

		public void SetDateInStock(long id, DateTimeOffset dateTimeOffset)
		{
			Update(id, application => application.DateInStock = dateTimeOffset);
		}

		public void SetTransitReference(long id, string transitReference)
		{
			Update(id, application => application.TransitReference = transitReference);
		}

		public void SetCount(long id, int? value)
		{
			Update(id, application => application.Count = value);
		}

		public void SetWeight(long id, float? value)
		{
			Update(id, application => application.Weight = value);
		}

		public void SetInsuranceRate(long id, float insuranceRate)
		{
			Update(id, application => application.InsuranceRate = insuranceRate);
		}

		public void SetDateOfCargoReceipt(long id, DateTimeOffset? dateOfCargoReceipt)
		{
			Update(id, application => application.DateOfCargoReceipt = dateOfCargoReceipt);
		}

		public void SetTransitCost(long id, decimal? transitCost)
		{
			Update(id, application => application.TransitCost = transitCost);
		}

		public void SetTariffPerKg(long id, decimal? tariffPerKg)
		{
			Update(id, application => application.TariffPerKg = tariffPerKg);
		}

		public void SetFactureCostExEdited(long id, decimal? factureCostEx)
		{
			Update(id, application => application.FactureCostExEdited = factureCostEx);
		}

		public void SetPickupCostEdited(long id, decimal? pickupCost)
		{
			Update(id, application => application.PickupCostEdited = pickupCost);
		}

		public void SetFactureCostEdited(long id, decimal? factureCost)
		{
			Update(id, application => application.FactureCostEdited = factureCost);
		}

		public void SetTransitCostEdited(long id, decimal? transitCost)
		{
			Update(id, application => application.TransitCostEdited = transitCost);
		}

		public void SetScotchCostEditedByTotalCost(long id, decimal? totalScotchCost)
		{
			Update(id, application => application.ScotchCostEdited = totalScotchCost / application.Count);
		}

		public void SetSenderRate(long id, decimal? senderRate)
		{
			Update(id, application => application.SenderRate = senderRate);
		}

		public void SetClass(long id, int? classId)
		{
			Update(id, application => application.ClassId = classId);
		}

		public void Update(long applicationId, ApplicationEditData application)
		{
			Update(applicationId, entity => Map(application, entity));
		}

		public void SetTotalTariffCost(long id, decimal? value)
		{
			Update(id, application => application.CalculationTotalTariffCost = value);
		}

		public void SetProfit(long id, decimal? value)
		{
			Update(id, application => application.CalculationProfit = value);
		}

		private void Update(long id, Action<DbContext.Application> action)
		{
			var application = _context.Applications.First(x => x.Id == id);

			action(application);

			_context.SaveChanges();
		}

		private static void Map(ApplicationEditData from, DbContext.Application to)
		{
			to.Characteristic = from.Characteristic;
			to.AddressLoad = from.AddressLoad;
			to.WarehouseWorkingTime = from.WarehouseWorkingTime;
			to.Weight = from.Weight;
			to.Count = from.Count;
			to.Volume = from.Volume;
			to.TermsOfDelivery = from.TermsOfDelivery;
			to.Value = from.Value;
			to.CurrencyId = (int)@from.CurrencyId;
			to.MethodOfDeliveryId = (int)from.MethodOfDelivery;
			to.DateInStock = from.DateInStock;
			to.DateOfCargoReceipt = from.DateOfCargoReceipt;
			to.TransitReference = from.TransitReference;
			to.IsPickup = from.IsPickup;

			to.ClientId = from.ClientId;
			to.TransitId = from.TransitId;
			to.ClassId = (int?)from.Class;
			to.AirWaybillId = from.AirWaybillId;
			to.CountryId = from.CountryId;
			to.SenderId = from.SenderId;
			to.ForwarderId = from.ForwarderId;

			to.FactoryName = from.FactoryName;
			to.FactoryPhone = from.FactoryPhone;
			to.FactoryEmail = from.FactoryEmail;
			to.FactoryContact = from.FactoryContact;
			to.MarkName = from.MarkName;
			to.Invoice = from.Invoice;

			to.FactureCost = from.FactureCost;
			to.FactureCostEx = from.FactureCostEx;
			to.TariffPerKg = from.TariffPerKg;
			to.TransitCost = from.TransitCost;
			to.PickupCost = from.PickupCost;
			to.FactureCostEdited = from.FactureCostEdited;
			to.FactureCostExEdited = from.FactureCostExEdited;
			to.TransitCostEdited = from.TransitCostEdited;
			to.PickupCostEdited = from.PickupCostEdited;
			to.ScotchCostEdited = from.ScotchCostEdited;
			to.SenderRate = from.SenderRate;
			to.InsuranceRate = from.InsuranceRate;
			to.CalculationProfit = from.CalculationProfit;
			to.CalculationTotalTariffCost = from.CalculationTotalTariffCost;

			to.MRN = from.MRN;
			to.CountInInvoce = from.CountInInvoce;
			to.DocumentWeight = from.DocumentWeight;
		    to.Comments = from.Comments;
		}
	}
}