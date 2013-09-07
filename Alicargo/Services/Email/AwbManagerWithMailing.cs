﻿using System.Linq;
using Alicargo.Services.Abstract;
using Alicargo.Services.Contract;
using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.Email
{
    internal sealed class AwbManagerWithMailing : IAwbManager
    {
        private readonly IAwbPresenter _awbPresenter;
        private readonly IMailSender _mailSender;
        private readonly IMessageBuilder _messageBuilder;
        private readonly IAwbManager _manager;

        public AwbManagerWithMailing(
            IAwbManager manager,
            IAwbPresenter awbPresenter,
            IMailSender mailSender,
            IMessageBuilder messageBuilder)
        {
            _manager = manager;
            _awbPresenter = awbPresenter;
            _mailSender = mailSender;
            _messageBuilder = messageBuilder;
        }

        public long Create(long applicationId, AirWaybillEditModel model)
        {
            var id = _manager.Create(applicationId, model);

            SendOnCreate(id);

            return id;
        }

	    public long Create(long applicationId, SenderAwbModel model)
	    {
			var id = _manager.Create(applicationId, model);

			SendOnCreate(id);

			return id;
	    }

	    public void Delete(long awbId)
        {
            _manager.Delete(awbId);
        }

        private void SendOnCreate(long id)
        {
            var model = _awbPresenter.GetData(id);
            var broker = _awbPresenter.GetBroker(model.BrokerId);

            var to = new[]
                {
                    new Recipient
                        {
                            Culture = broker.TwoLetterISOLanguageName,
                            Email = broker.Email
                        }
                }
                .Concat(_messageBuilder.GetForwarderEmails())
                .ToArray();

            var aggregate = _awbPresenter.GetAggregate(id);

            foreach (var recipient in to)
            {
                var body = _messageBuilder.AwbCreate(model, recipient.Culture, aggregate.TotalWeight,
                                                     aggregate.TotalCount);
                _mailSender.Send(new Message(_messageBuilder.DefaultSubject, body, recipient.Email));
            }
        }
    }
}