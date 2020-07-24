using Samba.Domain.Models.Tickets;
using Samba.Localization.Properties;
using Samba.Presentation.Services.Common;
using Samba.Services.Common;

namespace Samba.Modules.TicketModule.ActionProcessors
{
    public class LockTicket : ActionType
    {
        public override void Process(ActionData actionData)
        {
            var ticket = actionData.GetDataValue<Ticket>("Ticket");
            if (ticket != null)
            {
                ticket.RequestLock();
            }
        }

        protected override object GetDefaultData()
        {
            return new object();
        }

        protected override string GetActionName()
        {
            return Resources.LockTicket;
        }

        protected override string GetActionKey()
        {
            return ActionNames.LockTicket;
        }
    }
}
