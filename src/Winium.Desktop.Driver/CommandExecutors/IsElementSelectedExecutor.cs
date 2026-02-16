namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;

    using FlaUI.Core.Definitions;

    using Winium.StoreApps.Common;

    #endregion

    internal class IsElementSelectedExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            bool isSelected;

            try
            {
                if (element.Patterns.SelectionItem.IsSupported)
                {
                    isSelected = element.Patterns.SelectionItem.Pattern.IsSelected.Value;
                }
                else if (element.Patterns.Toggle.IsSupported)
                {
                    var toggleState = element.Patterns.Toggle.Pattern.ToggleState.Value;
                    isSelected = toggleState == ToggleState.On;
                }
                else
                {
                    isSelected = false;
                }
            }
            catch (Exception)
            {
                isSelected = false;
            }

            return this.JsonResponse(ResponseStatus.Success, isSelected);
        }

        #endregion
    }
}
