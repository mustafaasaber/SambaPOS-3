using Samba.Localization.Properties;
using Samba.Presentation.Services;
using Samba.Presentation.Services.Common;
using Samba.Services;

namespace Samba.Modules.TicketModule.ActionProcessors
{
    public class TagOrder : OrderTagOperation
    {

        public TagOrder(ICacheService cacheService, ITicketService ticketService)
            : base(cacheService, ticketService)
        {
        }

        protected override string GetActionName()
        {
            return Resources.TagOrder;
        }

        protected override string GetActionKey()
        {
            return ActionNames.TagOrder;
        }
    }
}
