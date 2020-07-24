using Samba.Domain.Models.Tickets;
using Samba.Localization.Properties;
using Samba.Presentation.Services.Common;
using Samba.Services.Common;

namespace Samba.Modules.TicketModule.ActionProcessors
{
    public class StopActiveTimers : ActionType
    {
        public override void Process(ActionData actionData)
        {
            var ticket = actionData.GetDataValue<Ticket>("Ticket");
            if (ticket != null)
            {
                ticket.StopActiveTimers();
            }
        }

        protected override object GetDefaultData()
        {
            return new object();
        }

        protected override string GetActionName()
        {
            return Resources.StopProductTimers;
        }

        protected override string GetActionKey()
        {
            return ActionNames.StopActiveTimers;
        }
    }
}
