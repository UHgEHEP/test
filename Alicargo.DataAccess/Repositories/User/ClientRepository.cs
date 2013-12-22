﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.Contracts.Contracts.User;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Contracts.Repositories.User;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Repositories.User
{
	public sealed class ClientRepository : IClientRepository
	{
		private readonly AlicargoDataContext _context;
		private readonly Expression<Func<Client, ClientData>> _selector;

		public ClientRepository(IUnitOfWork unitOfWork)
		{
			_context = (AlicargoDataContext)unitOfWork.Context;

			_selector = x => new ClientData
			{
				ClientId = x.Id,
				BIC = x.BIC,
				Phone = x.Phone,
				Emails = EmailsHelper.SplitEmails(x.Emails),
				LegalEntity = x.LegalEntity,
				Bank = x.Bank,
				Contacts = x.Contacts,
				INN = x.INN,
				KPP = x.KPP,
				KS = x.KS,
				LegalAddress = x.LegalAddress,
				MailingAddress = x.MailingAddress,
				Nic = x.Nic,
				OGRN = x.OGRN,
				RS = x.RS,
				TransitId = x.TransitId,
				TwoLetterISOLanguageName = x.User.TwoLetterISOLanguageName,
				Login = x.User.Login
			};
		}

		public long Count()
		{
			return _context.Clients.LongCount();
		}

		public ClientData[] GetRange(int take, long skip)
		{
			return _context.Clients
				.OrderBy(x => x.LegalEntity)
				.Skip((int)skip)
				.Take(take)
				.Select(_selector)
				.ToArray();
		}

		public Func<long> Add(ClientData client)
		{
			var entity = new Client
			{
				User = new DbContext.User()
			};

			Map(client, entity);

			_context.Clients.InsertOnSubmit(entity);

			return () => entity.Id;
		}

		public void Update(ClientData client)
		{
			var entity = _context.Clients.First(x => x.Id == client.ClientId);

			Map(client, entity);
		}

		public string GetLanguage(long clientId)
		{
			return _context.Clients.Where(x => x.Id == clientId).Select(x => x.User.TwoLetterISOLanguageName).First();
		}

		public ClientData GetByUserId(long userId)
		{
			return _context.Clients
				.Where(x => x.UserId == userId)
				.Select(_selector)
				.FirstOrDefault();
		}

		public ClientData Get(long clientId)
		{
			return _context.Clients
				.Where(x => x.Id == clientId)
				.Select(_selector)
				.FirstOrDefault();
		}

		public void Delete(long id)
		{
			var entity = _context.Clients.First(x => x.Id == id);

			_context.Clients.DeleteOnSubmit(entity);
		}

		public ClientData[] GetAll()
		{
			return _context.Clients.Select(_selector).ToArray();
		}

		public IDictionary<long, string> GetNicByApplications(params long[] appIds)
		{
			return _context.Applications
				.Where(x => appIds.Contains(x.Id))
				.Select(x => new { x.Id, ClientNic = x.Client.Nic })
				.ToDictionary(x => x.Id, x => x.ClientNic);
		}

		public long GetUserId(long clientId)
		{
			return _context.Clients.Where(x => x.Id == clientId).Select(x => x.UserId).First();
		}

		private static void Map(ClientData @from, Client to)
		{
			to.Emails = EmailsHelper.JoinEmails(from.Emails);
			to.User.TwoLetterISOLanguageName = from.TwoLetterISOLanguageName;
			to.User.Login = from.Login;
			to.LegalEntity = @from.LegalEntity;
			to.BIC = @from.BIC;
			to.Nic = @from.Nic;
			to.Contacts = @from.Contacts;
			to.Phone = @from.Phone;
			to.INN = @from.INN;
			to.KPP = @from.KPP;
			to.OGRN = @from.OGRN;
			to.Bank = @from.Bank;
			to.LegalAddress = @from.LegalAddress;
			to.MailingAddress = @from.MailingAddress;
			to.RS = @from.RS;
			to.KS = @from.KS;
			to.TransitId = @from.TransitId;
		}
	}
}