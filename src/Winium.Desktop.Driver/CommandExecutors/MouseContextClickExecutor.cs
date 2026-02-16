namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using Winium.Desktop.Driver.Extensions;

    #endregion

    internal class MouseContextClickExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["id"].ToString();
            this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey).RightClickAtCenter();

            return this.JsonResponse();
        }

        #endregion
    }
}
