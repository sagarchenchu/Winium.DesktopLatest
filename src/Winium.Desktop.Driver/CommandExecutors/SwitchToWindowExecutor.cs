namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;

    using Winium.StoreApps.Common;
    using Winium.StoreApps.Common.Exceptions;

    #endregion

    internal class SwitchToWindowExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var windowHandle = int.Parse(this.ExecutedCommand.Parameters["name"].ToString());

            var window = FlaUIHelper.Root.FindFirstDescendant(
                cf => cf.ByProperty(FlaUI.UIA3.Identifiers.AutomationObjectIds.NativeWindowHandleProperty, windowHandle));
            if (window == null)
            {
                throw new AutomationException("Window cannot be found", ResponseStatus.NoSuchElement);
            }

            window.Focus();

            return this.JsonResponse();
        }

        #endregion
    }
}
