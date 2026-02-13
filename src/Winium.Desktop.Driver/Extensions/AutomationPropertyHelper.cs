namespace Winium.Desktop.Driver.Extensions
{
    #region using

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using FlaUI.Core.Identifiers;
    using FlaUI.UIA3;
    using FlaUI.UIA3.Identifiers;

    #endregion

    internal static class AutomationPropertyHelper
    {
        #region Static Fields

        private static readonly Dictionary<string, PropertyId> Properties;

        #endregion

        #region Constructors and Destructors

        static AutomationPropertyHelper()
        {
            Properties = new Dictionary<string, PropertyId>();

            var automationObjectIds = typeof(AutomationObjectIds).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(f => f.FieldType == typeof(PropertyId));

            foreach (var field in automationObjectIds)
            {
                Properties[field.Name] = (PropertyId)field.GetValue(null);
            }
        }

        #endregion

        #region Public Methods and Operators

        internal static PropertyId GetAutomationProperty(string propertyName)
        {
            const string Suffix = "Property";
            var fullPropertyName = propertyName.EndsWith(Suffix) ? propertyName : propertyName + Suffix;
            if (Properties.ContainsKey(fullPropertyName))
            {
                return Properties[fullPropertyName];
            }

            Logger.Error("Property '{0}' is not UI Automation Property", propertyName);
            throw new InvalidOperationException("UNSUPPORTED PROPERTY");
        }

        #endregion
    }
}
