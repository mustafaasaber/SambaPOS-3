using System.ComponentModel.Composition;
using Samba.Localization.Properties;
using Samba.Presentation.Services.Common;
using Samba.Services;
using Samba.Services.Common;

namespace Samba.Modules.AutomationModule.ActionProcessors
{

   public class SendMessage : ActionType
    {
        private readonly IMessagingService _messagingService;

        
        public SendMessage(IMessagingService messagingService)
        {
            _messagingService = messagingService;
        }

        public override void Process(ActionData actionData)
        {
            _messagingService.SendMessage("ActionMessage", actionData.GetAsString("Command"));
        }

        protected override object GetDefaultData()
        {
            return new { Command = "" };
        }

        protected override string GetActionName()
        {
            return Resources.BroadcastMessage;
        }

        protected override string GetActionKey()
        {
            return ActionNames.SendMessage;
        }
    }
}
