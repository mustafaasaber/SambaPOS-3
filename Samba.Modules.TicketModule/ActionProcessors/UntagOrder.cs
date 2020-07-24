using Samba.Localization.Properties;
using Samba.Presentation.Services;
using Samba.Presentation.Services.Common;
using Samba.Services;

namespace Samba.Modules.TicketModule.ActionProcessors
{
    public class UntagOrder : OrderTagOperation
    {

        public UntagOrder(ICacheService cacheService, ITicketService ticketService)
            : base(cacheService, ticketService)
        {
        }

        protected override string GetActionName()
        {
            return Resources.UntagOrder;
        }

        protected override string GetActionKey()
        {
            return ActionNames.UntagOrder;
        }

        protected override object GetDefaultData()
        {
            return new { OrderTagName = "", OrderTagValue = "" };
        }
    }
}
