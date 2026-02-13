namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System.Linq;

    using FlaUI.Core.Definitions;

    using Winium.StoreApps.Common;

    #endregion

    internal class GetWindowHandlesExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var windows = FlaUIHelper.Root.FindAllDescendants(cf => cf.ByControlType(ControlType.Window));

            var handles = windows.Select(element => element.Properties.NativeWindowHandle.Value.ToInt32());

            return this.JsonResponse(ResponseStatus.Success, handles);
        }

        #endregion
    }
}
