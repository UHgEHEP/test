﻿using System.Linq;
using System.Net;
using System.Net.Http;
using Alicargo.Core.Enums;
using Alicargo.Core.Helpers;
using Alicargo.Core.Models;
using Alicargo.DataAccess.DbContext;
using Alicargo.TestHelpers;
using Alicargo.Tests.Properties;
using Alicargo.ViewModels;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.Tests.Controllers
{
	[TestClass]
	public class ClientControllerTests
	{
		private WebTestContext _context;
		private AlicargoDataContext _db;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new WebTestContext(Settings.Default.BaseAddress, Settings.Default.AdminLogin, Settings.Default.AdminPassword);
			_db = new AlicargoDataContext(Settings.Default.MainConnectionString);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Create()
		{
			var password = _context.Create<string>();

			var model = _context.Build<Core.Models.Client>()
				.With(x => x.AuthenticationModel, _context.Build<AuthenticationModel>()
					.With(x => x.ConfirmPassword, password)
					.With(x => x.NewPassword, password)
					.Create())
				.Create();

			var carrierSelectModel = _context.Build<CarrierSelectModel>()
				.Create();

			_context.HttpClient.PostAsJsonAsync("Client/Create", new { model, carrierSelectModel })
				.ContinueWith(task =>
				{
					Assert.AreEqual(HttpStatusCode.OK, task.Result.StatusCode);

					var clientData = _db.Clients.First(x => x.LegalEntity == model.LegalEntity);
					var carrierData = _db.Carriers.First(x => x.Name == carrierSelectModel.NewCarrierName);
					var transitData = clientData.Transit;
					var userData = clientData.User;

					var expectedClient = new ClientData();
					var actualClient = new ClientData();
					model.Id = clientData.Id;
					model.UserId = clientData.UserId;
					model.TransitId = clientData.TransitId;
					model.CopyTo(expectedClient);
					clientData.CopyTo(actualClient);

					var actualTransit = new TransitEditModel
					{
						Address = transitData.Address,
						City = transitData.City,
						DeliveryType = (DeliveryType) transitData.DeliveryTypeId,
						MethodOfTransit = (MethodOfTransit) transitData.MethodOfTransitId,
						Phone = transitData.Phone,
						RecipientName = transitData.RecipientName,
						WarehouseWorkingTime = transitData.WarehouseWorkingTime
					};

					_db.Clients.DeleteOnSubmit(clientData);
					_db.Users.DeleteOnSubmit(userData);
					_db.Transits.DeleteOnSubmit(transitData);
					_db.Carriers.DeleteOnSubmit(carrierData);
					_db.SubmitChanges();

					expectedClient.ShouldBeEquivalentTo(actualClient);
					model.Transit.ShouldBeEquivalentTo(actualTransit);
					Assert.AreEqual(model.AuthenticationModel.Login, userData.Login);
				})
				.Wait();
		}
	}
}
