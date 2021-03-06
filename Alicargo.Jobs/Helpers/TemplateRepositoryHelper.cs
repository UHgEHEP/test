﻿using System.Collections.Generic;
using System.Linq;
using Alicargo.DataAccess.Contracts.Contracts;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.Jobs.Helpers.Abstract;

namespace Alicargo.Jobs.Helpers
{
	public sealed class TemplateRepositoryHelper : ITemplateRepositoryHelper
	{
		private readonly ITemplateRepository _templates;

		public TemplateRepositoryHelper(ITemplateRepository templates)
		{
			_templates = templates;
		}

		public long? GetTemplateId(EventType type)
		{
			var eventTemplate = _templates.GetByEventType(type);

			return eventTemplate == null || !eventTemplate.EnableEmailSend
				? (long?)null
				: eventTemplate.EmailTemplateId;
		}

		public EmailTemplateLocalizationData GetLocalization(long templateId, string language)
		{
			var priority = new List<string>
			{
				TwoLetterISOLanguageName.Russian,
				TwoLetterISOLanguageName.English,
				TwoLetterISOLanguageName.Italian,
				TwoLetterISOLanguageName.Polish
			};

			priority.Remove(language);

			return GetLocalization(templateId, language, priority);
		}

		private EmailTemplateLocalizationData GetLocalization(long templateId, string language,
			ICollection<string> languagePriority)
		{
			while (true)
			{
				var localization = _templates.GetLocalization(templateId, language);

				var haveBoby = !string.IsNullOrWhiteSpace(localization.Body);
				var haveSubject = !string.IsNullOrWhiteSpace(localization.Subject);
				if (haveBoby || haveSubject)
					return localization;

				languagePriority.Remove(language);
				if (languagePriority.Count == 0)
					return null;

				language = languagePriority.First();
			}
		}
	}
}