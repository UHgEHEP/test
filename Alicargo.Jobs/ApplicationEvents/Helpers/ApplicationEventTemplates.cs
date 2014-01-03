﻿using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Jobs.ApplicationEvents.Abstract;
using Alicargo.Jobs.ApplicationEvents.Entities;
using Alicargo.Jobs.Helpers;
using Alicargo.Jobs.Helpers.Abstract;

namespace Alicargo.Jobs.ApplicationEvents.Helpers
{
	public sealed class ApplicationEventTemplates : IApplicationEventTemplates
	{
		private readonly ITemplateRepositoryWrapper _wrapper;
		private readonly ISerializer _serializer;
		private readonly ITemplateRepository _templates;

		public ApplicationEventTemplates(
			ITemplateRepositoryWrapper wrapper,
			ITemplateRepository templates,
			ISerializer serializer)
		{
			_wrapper = wrapper;
			_templates = templates;
			_serializer = serializer;
		}

		public long? GetTemplateId(EventType type, byte[] applicationEventData)
		{
			var templateId = _wrapper.GetTemplateId(type);

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
			return _wrapper.GetLocalization(templateId, language);
		}
	}
}