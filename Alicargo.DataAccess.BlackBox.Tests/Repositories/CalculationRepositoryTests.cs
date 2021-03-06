﻿using System;
using Alicargo.DataAccess.BlackBox.Tests.Properties;
using Alicargo.DataAccess.Contracts.Contracts.Calculation;
using Alicargo.DataAccess.Contracts.Exceptions;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.BlackBox.Tests.Repositories
{
	[TestClass]
	public class CalculationRepositoryTests
	{
		private CalculationRepository _calculation;
		private DbTestContext _context;
		private Fixture _fixture;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext(Settings.Default.MainConnectionString);
			_fixture = new Fixture();

			_calculation = new CalculationRepository(new SqlProcedureExecutor(Settings.Default.MainConnectionString));
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestMethod]
		
		[ExpectedException(typeof(DublicateException))]
		public void Test_Uniqueness()
		{
			var data1 = GenerateData();
			var data2 = GenerateData();
			data2.ClientId = data1.ClientId;

			var result = AddNew(data1, TestConstants.TestApplicationId);
			result.Should().NotBeNull();

			AddNew(data2, TestConstants.TestApplicationId);
		}

		[TestMethod]
		
		public void Test_Add_GetByApplication()
		{
			var data = GenerateData();

			var actual = AddNew(data, TestConstants.TestApplicationId);

			actual.ShouldBeEquivalentTo(data);
			actual.CreationTimestamp.Should().NotBe(default(DateTimeOffset));
		}

		[TestMethod]
		
		public void Test_RemoveByApplication()
		{
			var data = GenerateData();

			AddNew(data, TestConstants.TestApplicationId).Should().NotBeNull();

			_calculation.RemoveByApplication(TestConstants.TestApplicationId);

			_calculation.GetByApplication(TestConstants.TestApplicationId).Should().BeNull();
		}

		private CalculationData GenerateData()
		{
			return _fixture.Build<CalculationData>().With(x => x.ClientId, TestConstants.TestClientId1).Create();
		}

		private CalculationData AddNew(CalculationData data, long applicationId)
		{
			_calculation.Add(data, applicationId);

			return _calculation.GetByApplication(applicationId);
		}
	}
}