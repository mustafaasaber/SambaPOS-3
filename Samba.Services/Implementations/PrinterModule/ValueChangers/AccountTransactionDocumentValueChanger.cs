using System.ComponentModel.Composition;
using Samba.Domain.Models.Accounts;
using Samba.Domain.Models.Settings;

namespace Samba.Services.Implementations.PrinterModule.ValueChangers
{
    
    public class AccountTransactionDocumentValueChanger : AbstractValueChanger<AccountTransactionDocument>
    {
        private readonly AccountTransactionValueChanger _accountTransactionValueChanger;

        
        public AccountTransactionDocumentValueChanger(AccountTransactionValueChanger accountTransactionValueChanger)
        {
            _accountTransactionValueChanger = accountTransactionValueChanger;
        }

        public override string GetTargetTag()
        {
            return "LAYOUT";
        }

        protected override string ReplaceTemplateValues(string templatePart, AccountTransactionDocument model, PrinterTemplate template)
        {
            var result = _accountTransactionValueChanger.Replace(template, templatePart, model.AccountTransactions);
            return result;
        }
    }
}