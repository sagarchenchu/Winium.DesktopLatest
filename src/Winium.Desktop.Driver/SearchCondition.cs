namespace Winium.Desktop.Driver
{
    #region using

    using System;
    using System.Linq;

    using FlaUI.Core;
    using FlaUI.Core.AutomationElements;
    using FlaUI.Core.Definitions;

    #endregion

    internal class SearchCondition
    {
        #region Fields

        private readonly string strategy;

        private readonly string value;

        #endregion

        #region Constructors and Destructors

        public SearchCondition(string strategy, string value)
        {
            this.strategy = strategy;
            this.value = value;
        }

        #endregion

        #region Public Methods and Operators

        public AutomationElement FindFirst(AutomationElement parent)
        {
            switch (this.strategy)
            {
                case "id":
                    return parent.FindFirstDescendant(cf => cf.ByAutomationId(this.value));
                case "name":
                    return parent.FindFirstDescendant(cf => cf.ByName(this.value));
                case "class name":
                    return parent.FindFirstDescendant(cf => cf.ByClassName(this.value));
                case "xpath":
                    return parent.FindFirstByXPath(this.value);
                case "control_type":
                    var controlType = (ControlType)Enum.Parse(typeof(ControlType), this.value);
                    return parent.FindFirstDescendant(cf => cf.ByControlType(controlType));
                default:
                    throw new NotImplementedException(
                        string.Format("'{0}' is not valid or implemented searching strategy.", this.strategy));
            }
        }

        public AutomationElement[] FindAll(AutomationElement parent)
        {
            switch (this.strategy)
            {
                case "id":
                    return parent.FindAllDescendants(cf => cf.ByAutomationId(this.value));
                case "name":
                    return parent.FindAllDescendants(cf => cf.ByName(this.value));
                case "class name":
                    return parent.FindAllDescendants(cf => cf.ByClassName(this.value));
                case "xpath":
                    return parent.FindAllByXPath(this.value);
                case "control_type":
                    var controlType = (ControlType)Enum.Parse(typeof(ControlType), this.value);
                    return parent.FindAllDescendants(cf => cf.ByControlType(controlType));
                default:
                    throw new NotImplementedException(
                        string.Format("'{0}' is not valid or implemented searching strategy.", this.strategy));
            }
        }

        public AutomationElement FindFirstWithProperty(AutomationElement parent, FlaUI.Core.Identifiers.PropertyId property, object propertyValue)
        {
            return parent.FindFirstDescendant(cf => cf.ByProperty(property, propertyValue));
        }

        public AutomationElement[] FindAllWithProperty(AutomationElement parent, FlaUI.Core.Identifiers.PropertyId property, object propertyValue)
        {
            return parent.FindAllDescendants(cf => cf.ByProperty(property, propertyValue));
        }

        #endregion
    }
}
