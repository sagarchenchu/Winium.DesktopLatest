namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;

    using FlaUI.Core.Input;

    using Winium.StoreApps.Common;

    #endregion

    internal class MouseClickExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var buttonId = Convert.ToInt32(this.ExecutedCommand.Parameters["button"]);

            switch (buttonId)
            {
                case 0:
                    Mouse.Click(MouseButton.Left);
                    break;

                case 2:
                    Mouse.Click(MouseButton.Right);
                    break;

                default:
                    return this.JsonResponse(ResponseStatus.UnknownCommand, "Mouse button behavior is not implemented");
            }

            return this.JsonResponse();
        }

        #endregion
    }
}
