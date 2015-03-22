﻿using System;
using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Resources;
using Alicargo.Utilities.Localization;

namespace Alicargo.ViewModels.User
{
	// todo: add TransitEditModel field (61)
	public sealed class ClientModel
	{
		[Required]
		[DisplayNameLocalized(typeof(Entities), "Contacts")]
		public string Contacts { get; set; }

		[DataType(DataType.PhoneNumber)]
		[DisplayNameLocalized(typeof(Entities), "OfficePhone")]
		public string Phone { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "Emails")]
		public string Emails { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "Nic")]
		public string Nic { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "LegalEntity")]
		public string LegalEntity { get; set; }

		[DisplayNameLocalized(typeof(Entities), "INN")]
		public string INN { get; set; }

		[DisplayNameLocalized(typeof(Entities), "KPP")]
		public string KPP { get; set; }

		[DisplayNameLocalized(typeof(Entities), "OGRN")]
		public string OGRN { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Bank")]
		public string Bank { get; set; }

		[DisplayNameLocalized(typeof(Entities), "BIC")]
		public string BIC { get; set; }

		[DisplayNameLocalized(typeof(Entities), "LegalAddress")]
		public string LegalAddress { get; set; }

		[DisplayNameLocalized(typeof(Entities), "MailingAddress")]
		public string MailingAddress { get; set; }

		[DisplayNameLocalized(typeof(Entities), "RS")]
		public string RS { get; set; }

		[DisplayNameLocalized(typeof(Entities), "KS")]
		public string KS { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Contract")]
		public string ContractFileName { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Contract")]
		public byte[] ContractFile { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "ContractNumber")]
		public string ContractNumber { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "ContractDate")]
		public string ContractDate { get; set; }

		public AuthenticationModel Authentication { get; set; }
	}
}