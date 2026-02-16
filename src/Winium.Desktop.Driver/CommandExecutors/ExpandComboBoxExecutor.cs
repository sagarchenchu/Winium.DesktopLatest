namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using Winium.Desktop.Driver.Extensions;
    using Winium.StoreApps.Common;

    #endregion

    internal class ExpandComboBoxExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey).ToComboBox().Expand();

            return this.JsonResponse();
        }

        #endregion
    }
}
