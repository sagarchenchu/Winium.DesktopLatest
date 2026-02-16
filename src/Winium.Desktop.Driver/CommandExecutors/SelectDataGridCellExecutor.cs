namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;

    using Winium.Desktop.Driver.Extensions;
    using Winium.StoreApps.Common;

    #endregion

    internal class SelectDataGridCellExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var dataGridKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var column = int.Parse(this.ExecutedCommand.Parameters["COLUMN"].ToString());
            var row = int.Parse(this.ExecutedCommand.Parameters["ROW"].ToString());

            var dataGrid = this.Automator.ElementsRegistry.GetRegisteredElement(dataGridKey).ToDataGrid();

            try
            {
                var rows = dataGrid.Rows;
                if (row < rows.Length && column < rows[row].Cells.Length)
                {
                    rows[row].Cells[column].Click();
                }
            }
            catch (Exception exception)
            {
                return this.JsonResponse(ResponseStatus.NoSuchElement, exception);
            }

            return this.JsonResponse();
        }

        #endregion
    }
}
