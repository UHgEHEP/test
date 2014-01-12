﻿using System;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Core.Contracts.Client;

namespace Alicargo.Services.Users.Client
{
	internal sealed class ClientBalance : IClientBalance
	{
		private readonly IClientBalanceRepository _balance;

		public ClientBalance(IClientBalanceRepository balance)
		{
			_balance = balance;
		}

		public void Increase(long clientId, decimal money, string comment, DateTimeOffset timestamp)
		{
			if (money <= 0)
			{
				throw new ArgumentException(@"Money value must be positive", "money");
			}

			var balance = _balance.GetBalance(clientId);

			balance += money;

			_balance.SetBalance(clientId, balance);

			_balance.AddToHistory(clientId, balance, money, EventType.BalanceIncreased, timestamp, comment);
		}

		public void Decrease(long clientId, decimal money, string comment, DateTimeOffset timestamp)
		{
			if (money <= 0)
			{
				throw new ArgumentException(@"Money value must be positive", "money");
			}

			var balance = _balance.GetBalance(clientId);

			balance -= money;

			_balance.SetBalance(clientId, balance);

			_balance.AddToHistory(clientId, balance, money, EventType.BalanceDecreased, timestamp, comment);
		}
	}
}