namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using FlaUI.Core.Definitions;

    using Winium.Desktop.Driver.Extensions;
    using Winium.StoreApps.Common;

    #endregion

    internal class IsComboBoxExpandedExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var comboBox = element.ToComboBox();
            var isExpanded = comboBox.ExpandCollapseState == ExpandCollapseState.Expanded;
            return this.JsonResponse(ResponseStatus.Success, isExpanded);
        }

        #endregion
    }
}
