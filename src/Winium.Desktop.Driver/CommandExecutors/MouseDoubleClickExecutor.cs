namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using Winium.Desktop.Driver.Extensions;

    #endregion

    internal class MouseDoubleClickExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["id"].ToString();
            this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey).DoubleClickAtCenter();

            return this.JsonResponse();
        }

        #endregion
    }
}
