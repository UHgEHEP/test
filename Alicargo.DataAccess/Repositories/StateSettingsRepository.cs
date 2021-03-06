﻿using System;
using System.Linq;
using Alicargo.DataAccess.Contracts.Contracts.State;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.Helpers;

namespace Alicargo.DataAccess.Repositories
{
	// todo: refactor Get methods: remove StateRole class and pass a condition to a SP (261)
	public sealed class StateSettingsRepository : IStateSettingsRepository
	{
		private readonly ISqlProcedureExecutor _executor;

		public StateSettingsRepository(ISqlProcedureExecutor executor)
		{
			_executor = executor;
		}

		public StateRole[] GetStateAvailabilities()
		{
			return _executor.Array<StateRole>("[dbo].[State_GetStateAvailabilities]");
		}

		public StateRole[] GetStateVisibilities()
		{
			return _executor.Array<StateRole>("[dbo].[State_GetStateVisibilities]");
		}

		public void SetStateAvailabilities(long stateId, RoleType[] roles)
		{
			if(roles == null)
			{
				throw new ArgumentNullException("roles");
			}

			var table = TableParameters.GeIdsTable("Roles", roles.Select(x => (long)x).ToArray());

			_executor.Execute("[dbo].[State_SetStateAvailabilities]", new TableParameters(new { stateId }, table));
		}

		public void SetStateVisibilities(long stateId, RoleType[] roles)
		{
			if(roles == null)
			{
				throw new ArgumentNullException("roles");
			}

			var table = TableParameters.GeIdsTable("Roles", roles.Select(x => (long)x).ToArray());

			_executor.Execute("[dbo].[State_SetStateVisibilities]", new TableParameters(new { stateId }, table));
		}
	}
}