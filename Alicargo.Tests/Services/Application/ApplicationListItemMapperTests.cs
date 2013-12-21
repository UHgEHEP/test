﻿using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.Application;
using Alicargo.Contracts.Contracts.State;
using Alicargo.Contracts.Enums;
using Alicargo.Services.Application;
using Alicargo.TestHelpers;
using Alicargo.ViewModels.Application;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Alicargo.Tests.Services.Application
{
	[TestClass]
	public class ApplicationListItemMapperTests
	{
		private const int CargoOnTransitStateId = 0;
		private Dictionary<long, long> _calculations;
		private MockContainer _context;
		private List<CountryData> _countries;
		private ApplicationListItemData[] _data;
		private Dictionary<long, string> _localazedStates;
		private ApplicationListItemMapper _mapper;
		private long[] _stateAvailability;

		[TestInitialize]
		public void TestInitialize()
		{
			const int count = 10;
			_context = new MockContainer();
			_countries = new List<CountryData>();
			_localazedStates = new Dictionary<long, string>();
			_stateAvailability = Enumerable.Range(0, count / 2).Select(x => (long)x).ToArray();
			_data = _context.CreateMany<ApplicationListItemData>(count).ToArray();
			_calculations = _data.Take(count / 2).ToDictionary(x => x.Id, x => _context.Create<long>());

			for (var i = 0; i < count; i++)
			{
				var country = new CountryData(i, new Dictionary<string, string>
				{
					{
						TwoLetterISOLanguageName.English, _context.Create<string>()
					}
				});
				_countries.Add(country);
				_localazedStates.Add(i, _context.Create<string>());

				_data[i].StateId = i;
				if (_data[i].CountryId.HasValue)
				{
					_data[i].CountryId = i * 2;
				}
			}

			_context.ApplicationFileRepository.Setup(x => x.GetInfo(It.IsAny<long[]>(), It.IsAny<ApplicationFileType>()))
				.Returns(new Dictionary<long, FileInfo[]>(0));

			_context.IdentityService.SetupGet(x => x.TwoLetterISOLanguageName).Returns(TwoLetterISOLanguageName.English);
			_context.CountryRepository.Setup(x => x.Get()).Returns(_countries.ToArray());
			_context.StateService.Setup(x => x.GetStateAvailabilityToSet()).Returns(_stateAvailability);
			_context.StateConfig.SetupGet(x => x.CargoOnTransitStateId).Returns(CargoOnTransitStateId);
			_context.ApplicationRepository.Setup(x => x.GetCalculations(It.IsAny<long[]>())).Returns(_calculations);
			_context.StateRepository.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<long[]>())).Returns(
				(string lang, long[] ids) => ids.ToDictionary(
				x => x,
				x =>
					new StateData
					{
						Name = "State " + x,
						Position = (int)x,
						LocalizedName = _localazedStates[x]
					}
				));

			_mapper = _context.Create<ApplicationListItemMapper>();
		}

		[TestMethod]
		public void Test_ApplicationListItemMapper_Map()
		{
			var items = _mapper.Map(_data);

			_data.ShouldAllBeEquivalentTo(items, options => options.ExcludingMissingProperties());
			for (var i = 0; i < _data.Length; i++)
			{
				var item = items[i];
				var data = _data[i];

				var country = data.CountryId.HasValue && _countries.Any(x => x.Id == data.CountryId.Value)
					? _countries.First(x => x.Id == data.CountryId.Value).Name.First().Value
					: null;

				item.CountryName.ShouldBeEquivalentTo(country);
				item.State.ShouldBeEquivalentTo(new ApplicationStateModel
				{
					StateId = i,
					StateName = _localazedStates[i]
				});
				item.CanClose.ShouldBeEquivalentTo(item.StateId == CargoOnTransitStateId);
				item.CanSetState.ShouldBeEquivalentTo(_stateAvailability.Contains(item.StateId));
				item.TransitDeliveryTypeString.Should().NotBeNullOrEmpty();
				item.TransitMethodOfTransitString.Should().NotBeNullOrEmpty();
				item.CanSetTransitCost.ShouldBeEquivalentTo(!_calculations.ContainsKey(item.Id));
				item.CPFiles.Should().BeNull();
				item.Torg12Files.Should().BeNull();
				item.DeliveryBillFiles.Should().BeNull();
				item.PackingFiles.Should().BeNull();
				item.SwiftFiles.Should().BeNull();
				item.InvoiceFiles.Should().BeNull();
			}
		}
	}
}