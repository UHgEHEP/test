﻿using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Repositories
{
	public interface IEmailTemplateRepository
	{
		void SetForState(long stateId, string language, bool enableEmailSend,
			bool useApplicationEventTemplate, EmailTemplateLocalizationData localization);

		void SetForApplicationEvent(EventType eventType, string language, bool enableEmailSend,
			RoleType[] recipients, EmailTemplateLocalizationData localization);

		ApplicationEventTemplateData GetByEventType(EventType eventType);
		StateEmailTemplateData GetByStateId(long stateId);
		RoleType[] GetRecipientRoles(EventType eventType);
		EmailTemplateLocalizationData GetLocalization(long templateId, string language);
	}
}