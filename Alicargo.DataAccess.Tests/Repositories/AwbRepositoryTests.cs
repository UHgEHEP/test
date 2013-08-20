﻿using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Enums;
using Alicargo.DataAccess.Repositories;
using Alicargo.DataAccess.Tests.Helpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.DataAccess.Tests.Repositories
{
    [TestClass]
    public class AwbRepositoryTests
    {
        private IAwbRepository _awbRepository;
        private DbTestContext _context;

        [TestInitialize]
        public void TestInitialize()
        {
            _context = new DbTestContext();

            _awbRepository = new AwbRepository(_context.UnitOfWork);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Cleanup();
        }

        [TestMethod]
        public void Test_AwbRepository_GetAll_Add_Get()
        {
            var oldData = _awbRepository.GetAll();

            var data = CreateTestAirWaybill();

            var newData = _awbRepository.GetAll();

            Assert.AreEqual(oldData.Length + 1, newData.Length);

            var airWaybill = _awbRepository.Get(data.Id).First();

            data.ShouldBeEquivalentTo(airWaybill);
        }

        [TestMethod]
        public void Test_AwbRepository_Count_GetRange()
        {
            var airWaybillDatas = _awbRepository.GetAll();
            var count = _awbRepository.Count();

            Assert.AreEqual(airWaybillDatas.Length, count);

            var range = _awbRepository.GetRange(0, (int) count);

            airWaybillDatas.ShouldBeEquivalentTo(range);
        }

        [TestMethod]
        public void Test_AwbRepository_GetClientEmails()
        {
            var data1 = CreateApplicationData(DbTestContext.TestClientId1);
            var data2 = CreateApplicationData(DbTestContext.TestClientId2);

            var id = _awbRepository.Add(CreateAirWaybillData(), null, null, null, null, null);
            _context.UnitOfWork.SaveChanges();

            var applications = new ApplicationUpdateRepository(_context.UnitOfWork);
            var a1 = applications.Add(data1, null, null, null, null, null, null);
            var a2 = applications.Add(data2, null, null, null, null, null, null);
            _context.UnitOfWork.SaveChanges();

            applications.SetAirWaybill(a1(), id());
            applications.SetAirWaybill(a2(), id());
            _context.UnitOfWork.SaveChanges();

            var emails = _awbRepository.GetClientEmails(id());

            var clients = new ClientRepository(_context.UnitOfWork).Get(DbTestContext.TestClientId1,
                                                                        DbTestContext.TestClientId2);

            emails.ShouldBeEquivalentTo(clients.Select(x => x.Email).ToArray());
        }

        [TestMethod]
        public void Test_AwbRepository_Update()
        {
            var data = CreateTestAirWaybill();

            var newData = CreateAirWaybillData();
            newData.StateId = data.StateId;
            newData.Id = data.Id;
            newData.BrockerId = data.BrockerId;
            newData.CreationTimestamp = data.CreationTimestamp;
            newData.StateChangeTimestamp = data.StateChangeTimestamp;

            var gtdFile = _context.RandomBytes();
            var additionalFile = _context.RandomBytes();
            var packingFile = _context.RandomBytes();
            var invoiceFile = _context.RandomBytes();
            var awbFile = _context.RandomBytes();
            _awbRepository.Update(newData, gtdFile, additionalFile, packingFile, invoiceFile, awbFile);
            _context.UnitOfWork.SaveChanges();

            var actual = _awbRepository.Get(newData.Id).First();
            actual.ShouldBeEquivalentTo(newData);

            _awbRepository.GetGTDFile(newData.Id).FileData.ShouldBeEquivalentTo(gtdFile);
            _awbRepository.GTDAdditionalFile(newData.Id).FileData.ShouldBeEquivalentTo(additionalFile);
            _awbRepository.GetPackingFile(newData.Id).FileData.ShouldBeEquivalentTo(packingFile);
            _awbRepository.GetInvoiceFile(newData.Id).FileData.ShouldBeEquivalentTo(invoiceFile);
            _awbRepository.GetAWBFile(newData.Id).FileData.ShouldBeEquivalentTo(awbFile);
        }

        [TestMethod]
        public void Test_AwbRepository_SetState()
        {
            var data = CreateTestAirWaybill();

            _awbRepository.SetState(data.Id, DbTestContext.CargoIsFlewStateId);
            _context.UnitOfWork.SaveChanges();

            var actual = _awbRepository.Get(data.Id).First();

            actual.StateId.ShouldBeEquivalentTo(DbTestContext.CargoIsFlewStateId);
            actual.StateChangeTimestamp.Should().NotBe(data.StateChangeTimestamp);
        }

        [TestMethod]
        public void Test_AwbRepository_Delete()
        {
            var data = CreateTestAirWaybill();

            _awbRepository.Delete(data.Id);
            _context.UnitOfWork.SaveChanges();

            _awbRepository.Get(data.Id).Count().ShouldBeEquivalentTo(0);
        }

        private AirWaybillData CreateTestAirWaybill()
        {
            var data = CreateAirWaybillData();

            var id = _awbRepository.Add(data, _context.RandomBytes(), _context.RandomBytes(),
                                        _context.RandomBytes(), _context.RandomBytes(),
                                        _context.RandomBytes());

            _context.UnitOfWork.SaveChanges();

            data.Id = id();

            return data;
        }

        [TestMethod]
        public void Test_AwbRepository_GetAggregate()
        {
            var data1 = CreateApplicationData(DbTestContext.TestClientId1);
            var data2 = CreateApplicationData(DbTestContext.TestClientId1);
            var data3 = CreateApplicationData(DbTestContext.TestClientId1);
            var data4 = CreateApplicationData(DbTestContext.TestClientId1);

            var id1 = _awbRepository.Add(CreateAirWaybillData(), null, null, null, null, null);
            var id2 = _awbRepository.Add(CreateAirWaybillData(), null, null, null, null, null);
            _context.UnitOfWork.SaveChanges();

            var applications = new ApplicationUpdateRepository(_context.UnitOfWork);
            var a1 = applications.Add(data1, null, null, null, null, null, null);
            var a2 = applications.Add(data2, null, null, null, null, null, null);
            var a3 = applications.Add(data3, null, null, null, null, null, null);
            var a4 = applications.Add(data4, null, null, null, null, null, null);
            _context.UnitOfWork.SaveChanges();

            applications.SetAirWaybill(a1(), id1());
            applications.SetAirWaybill(a2(), id1());
            applications.SetAirWaybill(a3(), id2());
            applications.SetAirWaybill(a4(), id2());
            _context.UnitOfWork.SaveChanges();

            var aggregates = _awbRepository.GetAggregate(id1(), id2());

            aggregates.Count().ShouldBeEquivalentTo(2);

            var aggregate1 = aggregates.First(x => x.AirWaybillId == id1());
            var aggregate2 = aggregates.First(x => x.AirWaybillId == id2());

            aggregate1.TotalCount.ShouldBeEquivalentTo(data1.Count + data2.Count);
            aggregate2.TotalCount.ShouldBeEquivalentTo(data3.Count + data4.Count);

            aggregate1.TotalWeight.ShouldBeEquivalentTo(data1.Weigth + data2.Weigth);
            aggregate2.TotalWeight.ShouldBeEquivalentTo(data3.Weigth + data4.Weigth);
        }

        private ApplicationData CreateApplicationData(long clientId)
        {
            return _context.Fixture
                           .Build<ApplicationData>()
                           .With(x => x.ClientId, clientId)
                           .With(x => x.AirWaybillId, null)
                           .With(x => x.CountryId, null)
                           .With(x => x.StateId, DbTestContext.DefaultStateId)
                           .With(x => x.TransitId, 1)
                           .With(x => x.CurrencyId, (int) CurrencyType.Dollar)
                           .Create();
        }

        private AirWaybillData CreateAirWaybillData()
        {
            return _context.Fixture
                           .Build<AirWaybillData>()
                           .With(x => x.StateId, DbTestContext.DefaultStateId)
                           .With(x => x.BrockerId, DbTestContext.TestBrockerId)
                           .Create();
        }
    }
}