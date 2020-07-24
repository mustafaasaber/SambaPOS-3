﻿using Samba.Localization.Properties;
using Samba.Presentation.Services;
using Samba.Presentation.Services.Common;
using Samba.Services.Common;

namespace Samba.Modules.TicketModule.ActionProcessors
{
    public class CreateTicket : ActionType
    {
        private readonly IApplicationState _applicationState;
        private readonly ITicketService _ticketService;


        public CreateTicket(IApplicationState applicationState, ITicketService ticketService)
        {
            _applicationState = applicationState;
            _ticketService = ticketService;
        }

        public override void Process(ActionData actionData)
        {
            var ticketId = actionData.GetDataValueAsInt("TicketId");
            if (ticketId > 0 && !_applicationState.IsLocked)
            {
                var ticket = _ticketService.OpenTicket(ticketId);
                ticket.PublishEvent(EventTopicNames.SetSelectedTicket);
            }
            EventServiceFactory.EventService.PublishEvent(EventTopicNames.CreateTicket);
        }

        protected override object GetDefaultData()
        {
            return new object();
        }

        protected override string GetActionName()
        {
            return string.Format(Resources.Create_f, Resources.Ticket);
        }

        protected override string GetActionKey()
        {
            return ActionNames.CreateTicket;
        }
    }
}
