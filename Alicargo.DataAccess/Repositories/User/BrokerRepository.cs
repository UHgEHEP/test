﻿using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.DataAccess.Contracts.Repositories.User;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Repositories.User
{
	// todo: tests and refactor 276
	public sealed class BrokerRepository : IBrokerRepository
	{
		private readonly AlicargoDataContext _context;
		private readonly Expression<Func<Broker, BrokerData>> _selector;

		public BrokerRepository(IDbConnection connection)
		{
			_context = new AlicargoDataContext(connection);

			_selector = x => new BrokerData
			{
				Id = x.Id,
				Name = x.Name,
				Email = x.Email,
				UserId = x.UserId,
				Language = x.User.TwoLetterISOLanguageName,
				Login = x.User.Login
			};
		}

		public BrokerData Get(long brokerId)
		{
			return _context.Brokers.Where(x => x.Id == brokerId).Select(_selector).FirstOrDefault();
		}

		public BrokerData GetByUserId(long userId)
		{
			return _context.Brokers.Where(x => x.UserId == userId).Select(_selector).FirstOrDefault();
		}

		public BrokerData[] GetAll()
		{
			return _context.Brokers.Select(_selector).ToArray();
		}

		public long Update(long brokerId, string name, string login, string email)
		{
			var entity = _context.Brokers.First(x => x.Id == brokerId);
			entity.Name = name;
			entity.User.Login = login;
			entity.Email = email;

			_context.SaveChanges();

			return entity.UserId;
		}
		
		public long Add(string name, string login, string email, string language)
		{
			var user = new DbContext.User
			{
				Login = login,
				TwoLetterISOLanguageName = language,
				PasswordHash = new byte[0],
				PasswordSalt = new byte[0]
			};

			_context.Brokers.InsertOnSubmit(new Broker
			{
				Name = name,
				User = user,
				Email = email
			});

			_context.SaveChanges();

			return user.Id;
		}
	}
}