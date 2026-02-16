namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System.Globalization;

    using FlaUI.Core.AutomationElements;
    using FlaUI.Core.Definitions;

    using Winium.StoreApps.Common;

    #endregion

    internal class GetCurrentWindowHandleExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var automation = FlaUIHelper.Automation;
            var node = automation.FocusedElement();
            var rootElement = automation.GetDesktop();
            var treeWalker = automation.TreeWalkerFactory.GetControlViewWalker();
            while (node != null && !Equals(node, rootElement) && node.ControlType != ControlType.Window)
            {
                node = treeWalker.GetParent(node);
            }

            var result = (node == null || Equals(node, rootElement))
                             ? string.Empty
                             : node.Properties.NativeWindowHandle.Value.ToInt32().ToString(CultureInfo.InvariantCulture);
            return this.JsonResponse(ResponseStatus.Success, result);
        }

        #endregion
    }
}
