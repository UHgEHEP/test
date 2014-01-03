﻿using Alicargo.Contracts.Enums;

namespace Alicargo.Core.Event
{
	public interface IEventFacade
	{
		void Add<T>(long entityId, EventType type, EventState state, T obj);
		void Add(long entityId, EventType type, EventState state);
	}
}