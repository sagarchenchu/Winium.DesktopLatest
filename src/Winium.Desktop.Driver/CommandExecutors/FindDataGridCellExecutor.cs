namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;

    using FlaUI.Core.AutomationElements;

    using Winium.Desktop.Driver.Extensions;
    using Winium.StoreApps.Common;

    #endregion

    internal class FindDataGridCellExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var dataGridKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var column = int.Parse(this.ExecutedCommand.Parameters["COLUMN"].ToString());
            var row = int.Parse(this.ExecutedCommand.Parameters["ROW"].ToString());

            var dataGrid = this.Automator.ElementsRegistry.GetRegisteredElement(dataGridKey).ToDataGrid();

            AutomationElement dataGridCell;
            try
            {
                var rows = dataGrid.Rows;
                dataGridCell = rows[row].Cells[column];
            }
            catch (Exception exception)
            {
                return this.JsonResponse(ResponseStatus.NoSuchElement, exception);
            }

            var registeredKey = this.Automator.ElementsRegistry.RegisterElement(dataGridCell);
            var registeredObject = new JsonElementContent(registeredKey);

            return this.JsonResponse(ResponseStatus.Success, registeredObject);
        }

        #endregion
    }
}
