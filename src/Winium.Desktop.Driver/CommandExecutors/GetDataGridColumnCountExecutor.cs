namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using Winium.Desktop.Driver.Extensions;
    using Winium.StoreApps.Common;

    #endregion

    internal class GetDataGridColumnCountExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var dataGrid = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey).ToDataGrid();

            var header = dataGrid.Header;
            var columnCount = header != null ? header.Columns.Length : 0;
            return this.JsonResponse(ResponseStatus.Success, columnCount);
        }

        #endregion
    }
}
