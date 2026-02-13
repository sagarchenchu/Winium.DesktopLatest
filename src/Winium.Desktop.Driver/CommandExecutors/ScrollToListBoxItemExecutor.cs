namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;

    using FlaUI.Core.AutomationElements;

    using Winium.Desktop.Driver.Extensions;
    using Winium.StoreApps.Common;

    #endregion

    internal class ScrollToListBoxItemExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var dataGridKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var searchStrategy = this.ExecutedCommand.Parameters["using"].ToString();
            var searchValue = this.ExecutedCommand.Parameters["value"].ToString();

            var strategy = ByHelper.GetStrategy(searchStrategy, searchValue);

            var listBox = this.Automator.ElementsRegistry.GetRegisteredElement(dataGridKey).ToListBox();

            AutomationElement element;
            try
            {
                element = listBox.ScrollToItem(strategy);
            }
            catch (Exception exception)
            {
                return this.JsonResponse(ResponseStatus.NoSuchElement, exception);
            }

            var elementKey = this.Automator.ElementsRegistry.RegisterElement(element);

            return this.JsonResponse(ResponseStatus.Success, new JsonElementContent(elementKey));
        }

        #endregion
    }
}
