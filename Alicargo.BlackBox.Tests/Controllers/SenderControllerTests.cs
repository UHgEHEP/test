﻿using System.Data.SqlClient;
using System.Linq;
using Alicargo.BlackBox.Tests.Properties;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.User;
using Alicargo.Controllers;
using Alicargo.Core.Services;
using Alicargo.TestHelpers;
using Alicargo.ViewModels;
using Alicargo.ViewModels.User;
using Dapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ploeh.AutoFixture;

namespace Alicargo.BlackBox.Tests.Controllers
{
	[TestClass]
	public class SenderControllerTests
	{
		private CompositionHelper _composition;
		private SenderController _controller;
		private Fixture _fixture;

		[TestInitialize]
		public void TestInitialize()
		{
			_composition = new CompositionHelper(Settings.Default.MainConnectionString, Settings.Default.FilesConnectionString);
			_controller = _composition.Kernel.Get<SenderController>();
			_fixture = new Fixture();
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_composition.Dispose();
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Create()
		{
			var password = _fixture.Create<string>();

			var model = GetSenderModel(password);

			_controller.Create(model);

			VerifyData(model);

			VerifyPassword(model.Authentication.Login, password);
		}

		[TestMethod, TestCategory("black-box")]
		public void Test_Edit()
		{
			var password = _fixture.Create<string>();

			var model = GetSenderModel(password);

			_controller.Edit(TestConstants.TestSenderId, model);

			VerifyData(model);

			VerifyPassword(model.Authentication.Login, password);
		}

		private static void VerifyData(SenderModel model)
		{
			using (var connection = new SqlConnection(Settings.Default.MainConnectionString))
			{
				connection.Open();

				var actual = connection.Query<SenderData>(
					"select u.Login, s.Name, s.Email, s.TariffOfTapePerBox from sender s " +
					"join [dbo].[user] u on s.userid = u.id where u.login = @login",
					new { model.Authentication.Login }).First();

				actual.ShouldBeEquivalentTo(model, options => options.ExcludingMissingProperties());
			}
		}

		private SenderModel GetSenderModel(string password)
		{
			return _fixture.Build<SenderModel>()
						   .With(x => x.Authentication,
							   _fixture.Build<AuthenticationModel>()
									   .With(x => x.NewPassword, password)
									   .With(x => x.ConfirmPassword, password)
									   .Create())
						   .Create();
		}

		private static void VerifyPassword(string login, string password)
		{
			using (var connection = new SqlConnection(Settings.Default.MainConnectionString))
			{
				connection.Open();

				var passwordData = connection.Query(
					"select u.PasswordHash, u.PasswordSalt from [dbo].[user] u where u.login = @login",
					new { login }).First();

				new PasswordConverter().GetPasswordHash(password, (byte[])passwordData.PasswordSalt)
									   .ShouldBeEquivalentTo((byte[])passwordData.PasswordHash);
			}
		}
	}
}