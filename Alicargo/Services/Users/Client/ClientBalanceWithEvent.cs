﻿using System;
using System.Transactions;
using Alicargo.Core.Contracts.Client;
using Alicargo.Core.Contracts.Event;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.Jobs.Client.Balance;

namespace Alicargo.Services.Users.Client
{
	internal sealed class ClientBalanceWithEvent : IClientBalance
	{
		private readonly IEventFacade _events;
		private readonly IClientBalance _instance;

		public ClientBalanceWithEvent(IClientBalance instance, IEventFacade events)
		{
			_instance = instance;
			_events = events;
		}

		public void Increase(long clientId, decimal money, string comment, DateTimeOffset timestamp, bool isCalculation)
		{
			using(var scope = new TransactionScope())
			{
				_instance.Increase(clientId, money, comment, timestamp, isCalculation);

				AddEvent(clientId, money, comment, timestamp, EventType.BalanceIncreased);

				scope.Complete();
			}
		}

		public void Decrease(long clientId, decimal money, string comment, DateTimeOffset timestamp, bool isCalculation)
		{
			using(var scope = new TransactionScope())
			{
				_instance.Decrease(clientId, money, comment, timestamp, isCalculation);

				AddEvent(clientId, money, comment, timestamp, EventType.BalanceDecreased);

				scope.Complete();
			}
		}

		private void AddEvent(long clientId, decimal money, string comment, DateTimeOffset timestamp, EventType eventType)
		{
			_events.Add(clientId, eventType, EventState.Emailing,
				new PaymentEventData
				{
					Money = money,
					Comment = comment,
					Timestamp = timestamp
				});
		}
	}
}