﻿using Alicargo.Core.Contracts.Event;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.Jobs.ApplicationEvents.Abstract;
using Alicargo.Jobs.Helpers.Abstract;
using Alicargo.Utilities;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	public sealed class ApplicationEventTemplates : IApplicationEventTemplates
	{
		private readonly ITemplateRepositoryHelper _helper;
		private readonly ISerializer _serializer;
		private readonly ITemplateRepository _templates;

		public ApplicationEventTemplates(
			ITemplateRepositoryHelper helper,
			ITemplateRepository templates,
			ISerializer serializer)
		{
			_helper = helper;
			_templates = templates;
			_serializer = serializer;
		}

		public long? GetTemplateId(EventType type, byte[] applicationEventData)
		{
			var templateId = _helper.GetTemplateId(type);

			if (type != EventType.ApplicationSetState)
				return templateId;

			var stateEventData = _serializer.Deserialize<ApplicationSetStateEventData>(applicationEventData);
			var stateTemplate = _templates.GetByStateId(stateEventData.StateId);
			if (stateTemplate != null && stateTemplate.EnableEmailSend && !stateTemplate.UseEventTemplate)
			{
				return stateTemplate.EmailTemplateId;
			}

			return templateId;
		}

		public EmailTemplateLocalizationData GetLocalization(long templateId, string language)
		{
			return _helper.GetLocalization(templateId, language);
		}
	}
}