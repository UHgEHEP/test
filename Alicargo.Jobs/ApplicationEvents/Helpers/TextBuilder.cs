﻿using System;
using System.Globalization;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.Application;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Contracts.Repositories.Application;
using Alicargo.Core.Enums;
using Alicargo.Core.Helpers;
using Alicargo.Jobs.ApplicationEvents.Abstract;
using Alicargo.Jobs.ApplicationEvents.Entities;
using Alicargo.Jobs.Helpers.Abstract;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	internal sealed class TextBuilder : ITextBuilder
	{
		private readonly ITextBuilder<TextLocalizedData> _bulder;
		private readonly IApplicationFileRepository _files;
		private readonly ISerializer _serializer;
		private readonly IStateRepository _states;

		public TextBuilder(ISerializer serializer, IStateRepository states, IApplicationFileRepository files,
			ITextBuilder<TextLocalizedData> bulder)
		{
			_serializer = serializer;
			_states = states;
			_files = files;
			_bulder = bulder;
		}

		public string GetText(string template, string language, EventType type, ApplicationDetailsData application,
			byte[] bytes)
		{
			var data = GetTextLocalizedData(type, application, language, bytes);

			return _bulder.GetText(template, language, data);
		}

		private TextLocalizedData GetTextLocalizedData(EventType type,
			ApplicationDetailsData application, string language, byte[] bytes)
		{
			var culture = CultureInfo.GetCultureInfo(language);

			var localizedData = GetTextLocalizedData(application, language, culture);

			switch (type)
			{
				case EventType.ApplicationSetState:
					OnSetState(bytes, language, localizedData);
					break;

				case EventType.SetTransitReference:
				case EventType.ApplicationCreated:
					break;

				case EventType.Calculate:
				case EventType.CalculationCanceled:
					OnCalculation(bytes, language, localizedData);
					break;

				case EventType.CPFileUploaded:
				case EventType.InvoiceFileUploaded:
				case EventType.PackingFileUploaded:
				case EventType.SwiftFileUploaded:
				case EventType.DeliveryBillFileUploaded:
				case EventType.Torg12FileUploaded:
					OnFileUpload(bytes, localizedData);
					break;

				case EventType.SetDateOfCargoReceipt:
					OnSetDateOfCargoReceipt(bytes, culture, localizedData);
					break;

				default:
					throw new ArgumentOutOfRangeException("type");
			}

			return localizedData;
		}

		private void OnCalculation(byte[] bytes, string language, TextLocalizedData localizedData)
		{
			throw new NotImplementedException();
		}

		private void OnFileUpload(byte[] bytes, TextLocalizedData localizedData)
		{
			var holder = _serializer.Deserialize<FileHolder>(bytes);

			localizedData.UploadedFile = holder.Name;
		}

		private void OnSetDateOfCargoReceipt(byte[] bytes, CultureInfo culture, TextLocalizedData localizedData)
		{
			var dateOfCargoReceipt = _serializer.Deserialize<DateTimeOffset?>(bytes);

			localizedData.DateOfCargoReceipt = LocalizationHelper.GetDate(dateOfCargoReceipt, culture);
		}

		private TextLocalizedData GetTextLocalizedData(ApplicationDetailsData application, string language,
			CultureInfo culture)
		{
			var state = _states.Get(language, application.StateId).Select(x => x.Value).FirstOrDefault();
			var countryName = application.CountryName.First(x => x.Key == language).Value;
			var value = LocalizationHelper.GetValueString(application.Value, (CurrencyType)application.CurrencyId, culture);

			var deliveryBill = _files.GetNames(application.Id, ApplicationFileType.DeliveryBill).Select(x => x.Value).ToArray();
			var invoice = _files.GetNames(application.Id, ApplicationFileType.Invoice).Select(x => x.Value).ToArray();
			var packing = _files.GetNames(application.Id, ApplicationFileType.Packing).Select(x => x.Value).ToArray();
			var swift = _files.GetNames(application.Id, ApplicationFileType.Swift).Select(x => x.Value).ToArray();
			var torg12 = _files.GetNames(application.Id, ApplicationFileType.Torg12).Select(x => x.Value).ToArray();

			return new TextLocalizedData
			{
				AddressLoad = application.AddressLoad,
				FactoryName = application.FactoryName,
				Id = application.Id.ToString(culture),
				Count = application.Count.HasValue ? application.Count.Value.ToString(culture) : null,
				MarkName = application.MarkName,
				Invoice = application.Invoice,
				CountryName = countryName,
				CreationTimestamp = LocalizationHelper.GetDate(application.CreationTimestamp, culture),
				Value = value,
				Weight = application.Weight.HasValue ? application.Weight.Value.ToString(culture) : null,
				AirWaybill = application.AirWaybill,
				AirWaybillDateOfArrival = LocalizationHelper.GetDate(application.AirWaybillDateOfArrival, culture),
				AirWaybillDateOfDeparture = LocalizationHelper.GetDate(application.AirWaybillDateOfDeparture, culture),
				AirWaybillGTD = application.AirWaybillGTD,
				Characteristic = application.Characteristic,
				ClientNic = application.ClientNic,
				DateOfCargoReceipt = LocalizationHelper.GetDate(application.DateOfCargoReceipt, culture),
				DaysInWork = ApplicationHelper.GetDaysInWork(application.CreationTimestamp).ToString(culture),
				DeliveryBillFiles = string.Join(", ", deliveryBill),
				DeliveryType = LocalizationHelper.GetDeliveryType((DeliveryType)application.TransitDeliveryTypeId, culture),
				DisplayNumber = ApplicationHelper.GetDisplayNumber(application.Id, application.Count),
				FactoryContact = application.FactoryContact,
				FactoryEmail = application.FactoryEmail,
				FactoryPhone = application.FactoryPhone,
				InvoiceFiles = string.Join(", ", invoice),
				LegalEntity = application.ClientLegalEntity,
				MethodOfDelivery = LocalizationHelper.GetMethodOfDelivery((MethodOfDelivery)application.MethodOfDeliveryId, culture),
				MethodOfTransit =
					LocalizationHelper.GetMethodOfTransit((MethodOfTransit)application.TransitMethodOfTransitId, culture),
				PackingFiles = string.Join(", ", packing),
				StateChangeTimestamp = LocalizationHelper.GetDate(application.StateChangeTimestamp, culture),
				StateName = state != null ? state.LocalizedName : null,
				SwiftFiles = string.Join(", ", swift),
				TermsOfDelivery = application.TermsOfDelivery,
				Torg12Files = string.Join(", ", torg12),
				TransitAddress = application.TransitAddress,
				TransitCarrierName = application.TransitCarrierName,
				TransitCity = application.TransitCity,
				TransitPhone = application.TransitPhone,
				TransitRecipientName = application.TransitRecipientName,
				TransitReference = application.TransitReference,
				TransitWarehouseWorkingTime = application.TransitWarehouseWorkingTime,
				Volume = application.Volume.ToString("N2", culture),
				WarehouseWorkingTime = application.WarehouseWorkingTime
			};
		}

		private void OnSetState(byte[] bytes, string language, TextLocalizedData templateData)
		{
			var data = _serializer.Deserialize<ApplicationSetStateEventData>(bytes);

			var state = _states.Get(language, data.StateId).Select(x => x.Value).FirstOrDefault();

			if (state != null)
			{
				templateData.StateName = state.LocalizedName;
				templateData.StateChangeTimestamp = LocalizationHelper.GetDate(data.Timestamp, CultureInfo.GetCultureInfo(language));
			}
		}
	}
}