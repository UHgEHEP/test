﻿using Alicargo.Contracts.Contracts;
using Alicargo.Services.Contract;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Abstract
{
	public interface IMessageBuilder
	{
		Recipient[] GetAdminEmails();
		Recipient[] GetSenderEmails();
		Recipient[] GetForwarderEmails();

		string DefaultSubject { get; }
		string ClientAdd(ClientModel model, AuthenticationModel authenticationModel);

		string AwbCreate(AirWaybillData model, string culture, float totalWeight, int totalCount);
		string AwbSet(AirWaybillData model, string applicationNumber, string culture, float totalWeight, int totalCount);
		string AwbPackingFileAdded(AirWaybillData model);
		string AwbAWBFileAdded(AirWaybillData model);
		string AwbGTDAdditionalFileAdded(AirWaybillData model);
		string AwbGTDFileAdded(AirWaybillData model);
		string AwbInvoiceFileAdded(AirWaybillData model);

		string ApplicationUpdate { get; }
		string ApplicationAdd(ApplicationDetailsModel modell, string culture);
		string ApplicationDelete { get; }
		string ApplicationSubject { get; }
		string ApplicationSetState(ApplicationDetailsModel modell, string culture);
		string ApplicationSetDateOfCargoReceipt(ApplicationDetailsModel model, string culture);

		string ApplicationInvoiceFileAdded(ApplicationDetailsModel model);
		string ApplicationSwiftFileAdded(ApplicationDetailsModel model);
		string ApplicationPackingFileAdded(ApplicationDetailsModel model);
		string ApplicationDeliveryBillFileAdded(ApplicationDetailsModel model);
		string ApplicationTorg12FileAdded(ApplicationDetailsModel model);
		string ApplicationCPFileAdded(ApplicationDetailsModel model);
	}
}