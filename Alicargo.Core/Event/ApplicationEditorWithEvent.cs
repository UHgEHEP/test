﻿using System;
using Alicargo.Core.Contracts.Event;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories.Application;
using Alicargo.Utilities;

namespace Alicargo.Core.Event
{
	public sealed class ApplicationEditorWithEvent : IApplicationEditor
	{
		private readonly IEventFacade _events;
		private readonly IApplicationEditor _editor;

		public ApplicationEditorWithEvent(IEventFacade events, IApplicationEditor editor)
		{
			_events = events;
			_editor = editor;
		}

		public void Update(ApplicationData application)
		{
			_editor.Update(application);
		}

		public long Add(ApplicationData application)
		{
			var applicationId = _editor.Add(application);

			_events.Add(applicationId, EventType.ApplicationCreated, EventState.Emailing);

			return applicationId;
		}

		public void Delete(long applicationId)
		{
			_editor.Delete(applicationId);
		}

		public void SetAirWaybill(long applicationId, long? airWaybillId)
		{
			_editor.SetAirWaybill(applicationId, airWaybillId);
		}

		public void SetState(long applicationId, long stateId)
		{
			_editor.SetState(applicationId, stateId);

			_events.Add(applicationId,
				EventType.ApplicationSetState, EventState.Emailing,
				new ApplicationSetStateEventData
				{
					StateId = stateId,
					Timestamp = DateTimeProvider.Now
				});
		}

		public void SetDateInStock(long applicationId, DateTimeOffset dateTimeOffset)
		{
			_editor.SetDateInStock(applicationId, dateTimeOffset);
		}

		public void SetTransitReference(long applicationId, string transitReference)
		{
			_editor.SetTransitReference(applicationId, transitReference);

			_events.Add(applicationId, EventType.SetTransitReference, EventState.Emailing, transitReference);
		}

		public void SetDateOfCargoReceipt(long applicationId, DateTimeOffset? dateOfCargoReceipt)
		{
			_editor.SetDateOfCargoReceipt(applicationId, dateOfCargoReceipt);

			_events.Add(applicationId, EventType.SetDateOfCargoReceipt, EventState.Emailing, dateOfCargoReceipt);
		}

		public void SetTransitCost(long applicationId, decimal? transitCost)
		{
			_editor.SetTransitCost(applicationId, transitCost);
		}

		public void SetTransitCostEdited(long applicationId, decimal? transitCost)
		{
			_editor.SetTransitCostEdited(applicationId, transitCost);
		}

		public void SetTariffPerKg(long applicationId, decimal? tariffPerKg)
		{
			_editor.SetTariffPerKg(applicationId, tariffPerKg);
		}

		public void SetPickupCostEdited(long applicationId, decimal? pickupCost)
		{
			_editor.SetPickupCostEdited(applicationId, pickupCost);
		}

		public void SetFactureCostEdited(long applicationId, decimal? factureCost)
		{
			_editor.SetFactureCostEdited(applicationId, factureCost);
		}

		public void SetScotchCostEdited(long applicationId, decimal? scotchCost)
		{
			_editor.SetScotchCostEdited(applicationId, scotchCost);
		}

		public void SetSenderRate(long applicationId, decimal? senderRate)
		{
			_editor.SetSenderRate(applicationId, senderRate);
		}

		public void SetClass(long applicationId, int? classId)
		{
			_editor.SetClass(applicationId, classId);
		}
	}
}
