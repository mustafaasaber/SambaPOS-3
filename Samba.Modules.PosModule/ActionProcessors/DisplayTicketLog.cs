using Samba.Domain.Models.Tickets;
using Samba.Localization.Properties;
using Samba.Presentation.Services.Common;
using Samba.Services.Common;

namespace Samba.Modules.PosModule.ActionProcessors
{
    public class DisplayTicketLog : ActionType
    {
        public override void Process(ActionData actionData)
        {
            var ticket = actionData.GetDataValue<Ticket>("Ticket");
            if (ticket != null)
            {
                ticket.PublishEvent(EventTopicNames.DisplayTicketLog);
            }
        }

        protected override object GetDefaultData()
        {
            return null;
        }

        protected override string GetActionName()
        {
            return Resources.DisplayTicketLog;
        }

        protected override string GetActionKey()
        {
            return "DisplayTicketLog";
        }
    }
}
