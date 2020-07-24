using Samba.Localization.Properties;
using Samba.Services.Common;
using System.Text.RegularExpressions;

namespace Samba.Presentation.Common.ActionProcessors
{
    public class ModifyVariable : ActionType
    {
        public override void Process(ActionData actionData)
        {
            var variableName = actionData.GetAsString("VariableName");
            var find = actionData.GetAsString("Find");
            var replace = actionData.GetAsString("Replace");

            if (!string.IsNullOrEmpty(variableName))
            {
                var value = actionData.GetDataValueAsString(variableName);
                if (string.IsNullOrEmpty(find) && !string.IsNullOrEmpty(replace) && replace.Contains("(") && replace.Contains(")") && Regex.IsMatch(value, replace))
                    value = Regex.Replace(value, replace, (match) => match.Groups[1].Value);
                else if (string.IsNullOrEmpty(find) && !string.IsNullOrEmpty(replace))
                    value = replace;
                else if (!string.IsNullOrEmpty(find) && replace != null && Regex.IsMatch(value, find))
                    value = Regex.Replace(value, find, replace);
                else if (!string.IsNullOrEmpty(find) && replace != null)
                    value = value.Replace(find, replace);
                actionData.SetDataValue(variableName, value);
            }
        }

        protected override object GetDefaultData()
        {
            return new { VariableName = "", Find = "", Replace = "" };
        }

        protected override string GetActionName()
        {
            return Resources.ModifyVariable;
        }

        protected override string GetActionKey()
        {
            return "ModifyVariable";
        }
    }
}
