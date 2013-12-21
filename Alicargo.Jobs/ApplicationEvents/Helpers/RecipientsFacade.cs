﻿using System;
using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.Application;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Jobs.ApplicationEvents.Abstract;
using Alicargo.Jobs.ApplicationEvents.Entities;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	public sealed class RecipientsFacade : IRecipientsFacade
	{
		private readonly IAdminRepository _admins;
		private readonly IAwbRepository _awbs;
		private readonly IBrokerRepository _brokers;
		private readonly IClientRepository _clients;
		private readonly IForwarderRepository _forwarders;
		private readonly ISenderRepository _senders;
		private readonly ISerializer _serializer;
		private readonly IStateSettingsRepository _stateSettings;
		private readonly IEmailTemplateRepository _templates;

		public RecipientsFacade(
			IAwbRepository awbs,
			ISerializer serializer,
			IStateSettingsRepository stateSettings,
			IAdminRepository admins,
			ISenderRepository senders,
			IClientRepository clients,
			IForwarderRepository forwarders,
			IBrokerRepository brokers,
			IEmailTemplateRepository templates)
		{
			_awbs = awbs;
			_serializer = serializer;
			_stateSettings = stateSettings;
			_admins = admins;
			_senders = senders;
			_clients = clients;
			_forwarders = forwarders;
			_brokers = brokers;
			_templates = templates;
		}

		public RecipientData[] GetRecipients(ApplicationDetailsData application, ApplicationEventType type, byte[] data)
		{
			var roles = GetRoles(type, data);

			return roles.Length == 0
				? null
				: GetRecipients(application, roles).ToArray();
		}

		private RoleType[] GetRoles(ApplicationEventType type, byte[] data)
		{
			RoleType[] roles;
			if (type == ApplicationEventType.SetState)
			{
				var stateEventData = _serializer.Deserialize<ApplicationSetStateEventData>(data);

				roles = _stateSettings.GetStateEmailRecipients()
					.Where(x => x.StateId == stateEventData.StateId)
					.Select(x => x.Role)
					.ToArray();
			}
			else
			{
				roles = _templates.GetRecipientRoles(type);
			}

			return roles;
		}

		private IEnumerable<RecipientData> GetRecipients(ApplicationDetailsData application, IEnumerable<RoleType> roles)
		{
			foreach (var role in roles)
			{
				switch (role)
				{
					case RoleType.Admin:
						var recipients =
							_admins.GetAll().Select(user => new RecipientData(user.Email, user.TwoLetterISOLanguageName, RoleType.Admin));
						foreach (var recipient in recipients)
						{
							yield return recipient;
						}
						break;

					case RoleType.Sender:
						var sender = _senders.Get(application.SenderId);
						yield return new RecipientData(sender.Email, sender.TwoLetterISOLanguageName, role);
						break;

					case RoleType.Broker:
						if (application.AirWaybillId.HasValue)
						{
							var awb = _awbs.Get(application.AirWaybillId.Value).Single();
							var broker = _brokers.Get(awb.BrokerId);

							yield return new RecipientData(broker.Email, broker.TwoLetterISOLanguageName, role);
						}
						break;

					case RoleType.Forwarder:
						// todo: 1. get forwarder from application data
						var forwarders = _forwarders.GetAll();
						foreach (var user in forwarders)
						{
							yield return new RecipientData(user.Email, user.TwoLetterISOLanguageName, role);
						}
						break;

					case RoleType.Client:
						var client = _clients.Get(application.ClientId);
						foreach (var email in client.Emails)
						{
							yield return new RecipientData(email, client.TwoLetterISOLanguageName, role);
						}
						break;

					default:
						throw new InvalidOperationException("Unknown role " + role);
				}
			}
		}
	}
}