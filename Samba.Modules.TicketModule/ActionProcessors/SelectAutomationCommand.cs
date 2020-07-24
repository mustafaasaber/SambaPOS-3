using Samba.Domain.Models.Tickets;
using Samba.Localization.Properties;
using Samba.Presentation.Common;
using Samba.Presentation.Services.Common;
using Samba.Services.Common;

namespace Samba.Modules.TicketModule.ActionProcessors
{
    public class SelectAutomationCommand : ActionType
    {
        public override void Process(ActionData actionData)
        {
            var ticket = actionData.GetDataValue<Ticket>("Ticket");
            if (ticket != null)
            {
                CommonEventPublisher.EnqueueTicketEvent(EventTopicNames.SelectAutomationCommand);
            }
        }

        protected override object GetDefaultData()
        {
            return new object();
        }

        protected override string GetActionName()
        {
            return Resources.SelectAutomationCommand;
        }

        protected override string GetActionKey()
        {
            return "SelectAutomationCommand";
        }
    }
}
