﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Repositories
{
	internal sealed class StateRepository : IStateRepository
	{
		private readonly Expression<Func<State, StateData>> _selector;
		private readonly AlicargoDataContext _context;

		public StateRepository(IUnitOfWork unitOfWork)
		{
			_context = (AlicargoDataContext)unitOfWork.Context;

			_selector = x => new StateData
			{
				Id = x.Id,
				Name = x.Name,
				Position = x.Position
			};
		}

		public long Count()
		{
			return _context.States.LongCount();
		}

		public StateData[] GetAll()
		{
			var localizations = _context.StateLocalizations.ToArray();

			var states = _context.States
				.Select(_selector)
				.OrderBy(x => x.Position)
				.ToArray();

			foreach (var state in states)
			{
				AdjustLocalization(state, localizations);
			}

			return states;
		}

		public StateData Get(long id)
		{
			return Get(x => x.Id == id);
		}

		public long[] GetAvailableStates(RoleType role)
		{
			return _context.AvailableStates
				.Where(x => x.RoleId == (int)role)
				.OrderBy(x => x.State.Position)
				.Select(x => x.StateId)
				.ToArray();
		}

		public RoleType[] GetAvailableRoles(long stateId)
		{
			return _context.AvailableStates
				.Where(x => x.StateId == stateId)
				.OrderBy(x => x.State.Position)
				.Select(x => x.RoleId)
				.Cast<RoleType>()
				.ToArray();
		}

		public long[] GetVisibleStates(RoleType role)
		{
			return _context.VisibleStates
				.Where(x => x.RoleId == (int)role)
				.OrderBy(x => x.State.Position)
				.Select(x => x.StateId)
				.ToArray();
		}

		#region private

		private StateData Get(Expression<Func<State, bool>> expression)
		{
			var state = _context.States.Where(expression).Select(_selector).First();

			var localizations = _context.StateLocalizations.ToArray();

			AdjustLocalization(state, localizations);

			return state;
		}

		private static void AdjustLocalization(StateData state, StateLocalization[] localizations)
		{
			state.Localization.Add(TwoLetterISOLanguageName.Russian, GetName(localizations, TwoLetterISOLanguageName.Russian, state));
			state.Localization.Add(TwoLetterISOLanguageName.Italian, GetName(localizations, TwoLetterISOLanguageName.Italian, state));
			state.Localization.Add(TwoLetterISOLanguageName.English, GetName(localizations, TwoLetterISOLanguageName.English, state));
		}

		private static string GetName(IEnumerable<StateLocalization> localizations, string twoLetterISOLanguageName,
			StateData state)
		{
			return localizations
				.Where(x => x.TwoLetterISOLanguageName.Equals(twoLetterISOLanguageName, StringComparison.InvariantCultureIgnoreCase)
							&& x.StateId == state.Id)
				.Select(x => x.Name)
				.FirstOrDefault() ?? state.Name;
		}

		#endregion
	}
}
