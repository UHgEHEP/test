﻿//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using Alicargo.Contracts.Contracts;
//using Alicargo.Contracts.Enums;
//using Alicargo.Contracts.Helpers;
//using Alicargo.Contracts.Repositories;
//using Alicargo.Core.Enums;
//using Alicargo.Core.Helpers;
//using Alicargo.Core.Resources;
//using Alicargo.Core.Services;
//using Alicargo.Core.Services.Abstract;
//using Alicargo.Jobs.Entities;

//namespace Alicargo.Jobs.ApplicationEvents.Helpers
//{
//	[Obsolete]
//	public sealed class MessageFactory : IMessageFactory
//	{
//		private readonly IApplicationRepository _applications;
//		private readonly IAwbRepository _awbs;
//		private readonly IClientRepository _clients;
//		private readonly string _defaultFrom;
//		private readonly IRecipients _recipients;
//		private readonly IStateConfig _stateConfig;
//		private readonly IStateRepository _states;
//		private readonly ISerializer _serializer;
//		private readonly IApplicationFileRepository _files;

//		public MessageFactory(
//			IStateRepository states,
//			ISerializer serializer,
//			IApplicationFileRepository files,
//			IRecipients recipients,
//			IStateConfig stateConfig,
//			IApplicationRepository applications,
//			IAwbRepository awbs,
//			IClientRepository clients,
//			string defaultFrom)
//		{
//			_states = states;
//			_serializer = serializer;
//			_files = files;
//			_recipients = recipients;
//			_stateConfig = stateConfig;
//			_applications = applications;
//			_awbs = awbs;
//			_clients = clients;
//			_defaultFrom = defaultFrom;
//		}

//		public EmailMessage[] Get(long applicationId, ApplicationEventType type, byte[] bytes)
//		{
//			switch (type)
//			{
//				case ApplicationEventType.Created:
//					return GetOnCreated(applicationId, bytes).ToArray();

//				case ApplicationEventType.SetState:
//					return GetOnSetState(applicationId, bytes).ToArray();

//				case ApplicationEventType.CPFileUploaded:
//					return GetOnCPFileUploaded(applicationId, bytes).ToArray();

//				case ApplicationEventType.InvoiceFileUploaded:
//					return GetOnInvoiceFileUploaded(applicationId, bytes).ToArray();

//				case ApplicationEventType.PackingFileUploaded:
//					return GetOnPackingFileUploaded(applicationId, bytes).ToArray();

//				case ApplicationEventType.SwiftFileUploaded:
//					return GetOnSwiftFileUploaded(applicationId, bytes).ToArray();

//				case ApplicationEventType.DeliveryBillFileUploaded:
//					return GetOnDeliveryBillFileUploaded(applicationId, bytes).ToArray();

//				case ApplicationEventType.Torg12FileUploaded:
//					return GetOnTorg12FileUploaded(applicationId, bytes).ToArray();

//				case ApplicationEventType.SetDateOfCargoReceipt:
//					return GetOnSetDateOfCargoReceipt(applicationId, bytes).ToArray();

//				case ApplicationEventType.SetTransitReference:
//					return GetOnSetTransitReference(applicationId).ToArray();

//				default:
//					throw new ArgumentOutOfRangeException("type");
//			}
//		}

//		private IEnumerable<EmailMessage> GetOnCPFileUploaded(long applicationId, byte[] bytes)
//		{
//			ClientData client;
//			string displayNumber;
//			string subject;
//			var data = GetData(applicationId, bytes, out client, out displayNumber, out subject);

//			var body = string.Format(Mail.Application_CPFileAdded, client.LegalEntity, displayNumber);

//			var admins = _recipients.GetAdminEmails().Select(x => x.Email).ToArray();

//			yield return new EmailMessage(subject, body, _defaultFrom, client.Email) { Files = new[] { data.File }, CopyTo = admins };
//		}

//		private IEnumerable<EmailMessage> GetOnInvoiceFileUploaded(long applicationId, byte[] bytes)
//		{
//			ClientData client;
//			string displayNumber;
//			string subject;
//			var data = GetData(applicationId, bytes, out client, out displayNumber, out subject);

//			var body = string.Format(Mail.Application_InvoiceFileAdded,
//				displayNumber, data.FactoryName, data.MarkName,
//				data.Invoice, data.File.Name);

//			var senders = _recipients.GetSenderEmails();

//			yield return new EmailMessage(subject, body, _defaultFrom, client.Email) { Files = new[] { data.File } };

//			yield return new EmailMessage(subject, body, _defaultFrom, senders.Select(x => x.Email).ToArray()) { Files = new[] { data.File } };
//		}

//		private IEnumerable<EmailMessage> GetOnPackingFileUploaded(long applicationId, byte[] bytes)
//		{
//			ClientData client;
//			string displayNumber;
//			string subject;
//			var data = GetData(applicationId, bytes, out client, out displayNumber, out subject);

//			var body = string.Format(Mail.Application_PackingFileAdded,
//				displayNumber, data.FactoryName, data.MarkName, data.Invoice);

//			var senders = _recipients.GetSenderEmails().Select(x => x.Email).ToArray();

//			yield return new EmailMessage(subject, body, _defaultFrom, client.Email) { Files = new[] { data.File } };

//			yield return new EmailMessage(subject, body, _defaultFrom, senders) { Files = new[] { data.File } };
//		}

//		private IEnumerable<EmailMessage> GetOnSwiftFileUploaded(long applicationId, byte[] bytes)
//		{
//			ClientData client;
//			string displayNumber;
//			string subject;
//			var data = GetData(applicationId, bytes, out client, out displayNumber, out subject);

//			var body = string.Format(Mail.Application_SwiftFileAdded, displayNumber,
//				data.FactoryName, data.MarkName, data.Invoice);

//			var senders = _recipients.GetSenderEmails().Select(x => x.Email).ToArray();

//			yield return new EmailMessage(subject, body, _defaultFrom, client.Email) { Files = new[] { data.File } };

//			yield return new EmailMessage(subject, body, _defaultFrom, senders) { Files = new[] { data.File } };
//		}

//		private IEnumerable<EmailMessage> GetOnDeliveryBillFileUploaded(long applicationId, byte[] bytes)
//		{
//			ClientData client;
//			string displayNumber;
//			string subject;
//			var data = GetData(applicationId, bytes, out client, out displayNumber, out subject);

//			var body = string.Format(Mail.Application_DeliveryBillFileAdded,
//				displayNumber, data.FactoryName, data.MarkName, data.Invoice);

//			yield return new EmailMessage(subject, body, _defaultFrom, client.Email) { Files = new[] { data.File } };
//		}

//		private IEnumerable<EmailMessage> GetOnTorg12FileUploaded(long applicationId, byte[] bytes)
//		{
//			ClientData client;
//			string displayNumber;
//			string subject;
//			var data = GetData(applicationId, bytes, out client, out displayNumber, out subject);

//			var body = string.Format(Mail.Application_Torg12FileAdded, client.LegalEntity, displayNumber);

//			var admins = _recipients.GetAdminEmails().Select(x => x.Email).ToArray();

//			yield return new EmailMessage(subject, body, _defaultFrom, client.Email) { Files = new[] { data.File }, CopyTo = admins };
//		}

//		private ApplicationFileUploadedEventData GetData(
//			long applicationId, byte[] bytes, out ClientData client,
//			out string displayNumber, out string subject)
//		{
//			var data = _serializer.Deserialize<ApplicationFileUploadedEventData>(bytes);

//			var clientId = _applications.GetClientId(applicationId);

//			client = _clients.Get(clientId);

//			displayNumber = ApplicationHelper.GetDisplayNumber(applicationId, data.Count);

//			subject = GetApplicationSubject(displayNumber);

//			return data;
//		}

//		private IEnumerable<EmailMessage> GetOnSetDateOfCargoReceipt(long applicationId, byte[] bytes)
//		{
//			var dateOfCargoReceipt = _serializer.Deserialize<DateTimeOffset?>(bytes);

//			var application = _applications.Get(applicationId);

//			var subject = GetApplicationSubject(ApplicationHelper.GetDisplayNumber(application.Id, application.Count));

//			var language = _clients.GetLanguage(application.ClientId);

//			var body = string.Format(Mail.Application_SetDateOfCargoReceipt, LocalizationHelper.GetDate(dateOfCargoReceipt, CultureInfo.GetCultureInfo(language)));

//			var client = _clients.Get(application.ClientId);

//			yield return new EmailMessage(subject, body, _defaultFrom, client.Email);
//		}

//		private IEnumerable<EmailMessage> GetOnSetTransitReference(long applicationId)
//		{
//			var details = _applications.GetDetails(applicationId);

//			var displayNumber = ApplicationHelper.GetDisplayNumber(details.Id, details.Count);

//			var subject = GetApplicationSubject(displayNumber);

//			var body = string.Format(Mail.Application_SetTransitReference,
//				displayNumber, details.FactoryName, details.MarkName, details.Invoice, details.TransitReference);

//			yield return new EmailMessage(subject, body, _defaultFrom, details.ClientEmail);
//		}

//		private IEnumerable<EmailMessage> GetOnSetState(long applicationId, byte[] bytes)
//		{
//			var state = _serializer.Deserialize<ApplicationSetStateEventData>(bytes);

//			var details = _applications.GetDetails(applicationId);

//			details.StateId = state.StateId;

//			details.StateChangeTimestamp = state.Timestamp;

//			return SendOnSetState(details);
//		}

//		public string ApplicationSetState(ApplicationDetailsData data, string culture)
//		{
//			var stateData = _states.Get(culture, data.StateId).Values.First();
//			var cultureInfo = CultureInfo.GetCultureInfo(culture);

//			return string.Format(Mail.Application_SetState,
//								 ApplicationHelper.GetDisplayNumber(data.Id, data.Count),
//								 LocalizationHelper.GetDate(data.DateOfCargoReceipt, cultureInfo),
//								 data.TransitCarrierName,
//								 data.FactoryName,
//								 data.FactoryEmail,
//								 data.FactoryPhone,
//								 data.FactoryContact,
//								 ApplicationHelper.GetDaysInWork(data.CreationTimestamp),
//								 data.Invoice,
//								 LocalizationHelper.GetDate(data.CreationTimestamp, cultureInfo),
//								 LocalizationHelper.GetDate(data.StateChangeTimestamp, cultureInfo),
//								 data.MarkName,
//								 data.Count,
//								 data.Volume,
//								 data.Weight,
//								 data.Characteristic,
//								 LocalizationHelper.GetValueString(data.Value, (CurrencyType)data.CurrencyId, cultureInfo),
//								 data.AddressLoad,
//								 data.CountryName.Where(x => x.Key == culture).Select(x => x.Value).First(),
//								 data.WarehouseWorkingTime,
//								 data.TermsOfDelivery,
//								 LocalizationHelper.GetMethodOfDelivery((MethodOfDelivery)data.MethodOfDeliveryId, cultureInfo),
//								 data.TransitCity,
//								 data.TransitAddress,
//								 data.TransitRecipientName,
//								 data.TransitPhone,
//								 data.TransitWarehouseWorkingTime,
//								 LocalizationHelper.GetMethodOfTransit((MethodOfTransit)data.TransitMethodOfTransitId, cultureInfo),
//								 LocalizationHelper.GetDeliveryType((DeliveryType)data.TransitDeliveryTypeId, cultureInfo),
//								 data.AirWaybill,
//								 LocalizationHelper.GetDate(data.AirWaybillDateOfDeparture, cultureInfo),
//								 LocalizationHelper.GetDate(data.AirWaybillDateOfArrival, cultureInfo),
//								 data.AirWaybillGTD,
//								 data.TransitReference,
//								 stateData.LocalizedName);
//		}

//		private IEnumerable<EmailMessage> SendOnSetState(ApplicationDetailsData details)
//		{
//			var subject = GetApplicationSubject(ApplicationHelper.GetDisplayNumber(details.Id, details.Count));

//			if (details.StateId == _stateConfig.CargoReceivedStateId)
//			{
//				var to = _recipients.GetAdminEmails().Concat(_recipients.GetForwarderEmails()).ToArray();

//				foreach (var recipient in to)
//				{
//					var body = ApplicationSetState(details, recipient.Culture);

//					yield return new EmailMessage(subject, body, _defaultFrom, recipient.Email);
//				}
//			}
//			else
//			{
//				var files = GeAllFiles(details.AirWaybillId, details.Id);
//				var culture = _clients.GetLanguage(details.ClientId);
//				var body = ApplicationSetState(details, culture);

//				yield return new EmailMessage(subject, body, _defaultFrom, details.ClientEmail) { Files = files };

//				if (details.StateId == _stateConfig.CargoAtCustomsStateId || details.StateId == _stateConfig.CargoIsCustomsClearedStateId)
//				{
//					var to = _recipients.GetAdminEmails().Concat(_recipients.GetForwarderEmails()).ToArray();
//					foreach (var recipient in to)
//					{
//						body = ApplicationSetState(details, recipient.Culture);

//						yield return new EmailMessage(subject, body, _defaultFrom, recipient.Email);
//					}
//				}
//			}
//		}

//		private FileHolder[] GeAllFiles(long? airWaybillId, long id)
//		{
//			var files = new List<FileHolder>(8);

//			var invoiceFile = _files.GetInvoiceFile(id);
//			var deliveryBillFile = _files.GetDeliveryBillFile(id);
//			var cpFile = _files.GetCPFile(id);
//			var packingFile = _files.GetPackingFile(id);
//			var swiftFile = _files.GetSwiftFile(id);
//			var torg12File = _files.GetTorg12File(id);

//			if (airWaybillId.HasValue)
//			{
//				var gtdFile = _awbs.GetGTDFile(airWaybillId.Value);
//				var gtdAdditionalFile = _awbs.GTDAdditionalFile(airWaybillId.Value);

//				if (gtdFile != null) files.Add(gtdFile);
//				if (gtdAdditionalFile != null) files.Add(gtdAdditionalFile);
//			}

//			if (invoiceFile != null) files.Add(invoiceFile);
//			if (deliveryBillFile != null) files.Add(deliveryBillFile);
//			if (cpFile != null) files.Add(cpFile);
//			if (packingFile != null) files.Add(packingFile);
//			if (swiftFile != null) files.Add(swiftFile);
//			if (torg12File != null) files.Add(torg12File);

//			return files.ToArray();
//		}

//		private IEnumerable<EmailMessage> GetOnCreated(long applicationId, byte[] bytes)
//		{
//			var data = _serializer.Deserialize<ApplicationCreatedEventData>(bytes);
//			var client = _clients.Get(data.ClientId);
//			var language = _clients.GetLanguage(data.ClientId);
//			var displayNumber = ApplicationHelper.GetDisplayNumber(applicationId, data.Count);
//			var subject = GetApplicationSubject(displayNumber);

//			return _recipients.GetAdminEmails()
//				.Concat(_recipients.GetSenderEmails())
//				.Concat(new[]
//				{
//					new RecipientData
//					{
//						Culture = language,
//						Email = client.Email
//					}
//				})
//				.Select(recipient =>
//				{
//					string culture = recipient.Culture;
//					var body = string.Format(Mail.Application_Add,
//						displayNumber, client.Nic, data.FactoryName, data.MarkName,
//						LocalizationHelper.GetDate(data.CreationTimestamp, CultureInfo.GetCultureInfo(culture)));

//					return new EmailMessage(subject, body, _defaultFrom, recipient.Email);
//				});
//		}

//		private static string GetApplicationSubject(string displayNumber)
//		{
//			return string.Format(Mail.Application_Subject, displayNumber);
//		}
//	}
//}