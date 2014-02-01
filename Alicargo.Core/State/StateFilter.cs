﻿using System.Linq;
using Alicargo.Core.Contracts.Common;
using Alicargo.Core.Contracts.State;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Exceptions;
using Alicargo.DataAccess.Contracts.Repositories;

namespace Alicargo.Core.State
{
	public sealed class StateFilter : IStateFilter
	{
		private readonly IAwbRepository _awbs;
		private readonly IStateConfig _config;
		private readonly IIdentityService _identity;
		private readonly IStateSettingsRepository _settings;
		private readonly IStateRepository _states;

		public StateFilter(
			IStateRepository states,
			IStateSettingsRepository settings,
			IIdentityService identity,
			IStateConfig config,
			IAwbRepository awbs)
		{
			_states = states;
			_settings = settings;
			_identity = identity;
			_config = config;
			_awbs = awbs;
		}

		// todo: 1. refactor. remove Except(_config.AwbStates), pass role to method, remove ifs
		public long[] GetStateAvailabilityToSet()
		{
			if (_identity.IsInRole(RoleType.Admin))
			{
				return _settings.GetStateAvailabilities()
					.Where(x => x.Role == RoleType.Admin)
					.Select(x => x.StateId)
					.ToArray();
			}

			if (_identity.IsInRole(RoleType.Client))
			{
				return _settings.GetStateAvailabilities()
					.Where(x => x.Role == RoleType.Client)
					.Select(x => x.StateId)
					.Except(_config.AwbStates)
					.ToArray();
			}

			if (_identity.IsInRole(RoleType.Forwarder))
			{
				return _settings.GetStateAvailabilities()
					.Where(x => x.Role == RoleType.Forwarder)
					.Select(x => x.StateId)
					.ToArray();
			}

			if(_identity.IsInRole(RoleType.Carrier))
			{
				return _settings.GetStateAvailabilities()
					.Where(x => x.Role == RoleType.Carrier)
					.Select(x => x.StateId)
					.ToArray();
			}

			if (_identity.IsInRole(RoleType.Sender))
			{
				return _settings.GetStateAvailabilities()
					.Where(x => x.Role == RoleType.Sender)
					.Select(x => x.StateId)
					.Except(_config.AwbStates)
					.ToArray();
			}

			// todo: 3. a Broker should not be here because he don't use states
			if (_identity.IsInRole(RoleType.Broker))
			{
				return _settings.GetStateAvailabilities()
					.Where(x => x.Role == RoleType.Broker)
					.Select(x => x.StateId)
					.ToArray();
			}

			throw new InvalidLogicException("Unsupported role");
		}

		public long[] GetStateVisibility()
		{
			if (_identity.IsInRole(RoleType.Admin))
			{
				return _settings.GetStateVisibilities()
					.Where(x => x.Role == RoleType.Admin)
					.Select(x => x.StateId)
					.ToArray();
			}

			if (_identity.IsInRole(RoleType.Client))
			{
				return _settings.GetStateVisibilities()
					.Where(x => x.Role == RoleType.Client)
					.Select(x => x.StateId)
					.ToArray();
			}

			if (_identity.IsInRole(RoleType.Forwarder))
			{
				return _settings.GetStateVisibilities()
					.Where(x => x.Role == RoleType.Forwarder)
					.Select(x => x.StateId)
					.ToArray();
			}

			if(_identity.IsInRole(RoleType.Carrier))
			{
				return _settings.GetStateVisibilities()
					.Where(x => x.Role == RoleType.Carrier)
					.Select(x => x.StateId)
					.ToArray();
			}

			if (_identity.IsInRole(RoleType.Sender))
			{
				return _settings.GetStateVisibilities()
					.Where(x => x.Role == RoleType.Sender)
					.Select(x => x.StateId)
					.ToArray();
			}

			// todo: 3. Broker should not be here because he don't use states
			if (_identity.IsInRole(RoleType.Broker))
			{
				return _settings.GetStateVisibilities()
					.Where(x => x.Role == RoleType.Broker)
					.Select(x => x.StateId)
					.ToArray();
			}

			throw new InvalidLogicException();
		}

		public long[] FilterByPosition(long[] states, int position)
		{
			return _states.Get(_identity.Language, states)
				.Where(x => x.Value.Position >= position)
				.Select(x => x.Key)
				.ToArray();
		}

		public long[] FilterByBusinessLogic(ApplicationData applicationData, long[] stateAvailability)
		{
			var states = stateAvailability.ToList();

			if (!applicationData.Weight.HasValue || !applicationData.Count.HasValue)
			{
				states.Remove(_config.CargoInStockStateId);
			}

			#region AWB

			if (!applicationData.AirWaybillId.HasValue)
			{
				states.Remove(_config.CargoIsFlewStateId);
			}

			if (applicationData.AirWaybillId.HasValue)
			{
				var airWaybillData = _awbs.Get(applicationData.AirWaybillId.Value).First();
				if (string.IsNullOrWhiteSpace(airWaybillData.GTD))
				{
					states.Remove(_config.CargoAtCustomsStateId);
				}
			}
			else
			{
				states.Remove(_config.CargoAtCustomsStateId);
			}

			#endregion

			return states.ToArray();
		}
	}
}