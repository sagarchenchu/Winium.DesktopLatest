namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;

    using FlaUI.Core.Definitions;

    using Winium.Desktop.Driver.Extensions;
    using Winium.StoreApps.Common;

    #endregion

    internal class GetElementAttributeExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var propertyName = this.ExecutedCommand.Parameters["NAME"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            try
            {
                var property = AutomationPropertyHelper.GetAutomationProperty(propertyName);
                var propertyObject = element.FrameworkAutomationElement.GetPropertyValue(property);

                return this.JsonResponse(ResponseStatus.Success, PrepareValueToSerialize(propertyObject));
            }
            catch (Exception)
            {
                return this.JsonResponse();
            }
        }

        private static object PrepareValueToSerialize(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            if (obj.GetType().IsPrimitive)
            {
                return obj.ToString();
            }

            if (obj is ControlType)
            {
                return obj.ToString();
            }

            return obj;
        }

        #endregion
    }
}
