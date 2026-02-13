namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;
    using System.Drawing;

    using FlaUI.Core.Input;

    using Winium.StoreApps.Common;

    #endregion

    internal class MouseMoveToExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var haveElement = this.ExecutedCommand.Parameters.ContainsKey("element");
            var haveOffset = this.ExecutedCommand.Parameters.ContainsKey("xoffset")
                             && this.ExecutedCommand.Parameters.ContainsKey("yoffset");

            if (!(haveElement || haveOffset))
            {
                return this.JsonResponse(ResponseStatus.UnknownError, "WRONG PARAMETERS");
            }

            var resultPoint = Mouse.Position;
            if (haveElement)
            {
                var registeredKey = this.ExecutedCommand.Parameters["element"].ToString();
                var element = this.Automator.ElementsRegistry.GetRegisteredElementOrNull(registeredKey);
                if (element != null)
                {
                    var rect = element.BoundingRectangle;
                    resultPoint = new Point((int)rect.Left, (int)rect.Top);
                    if (!haveOffset)
                    {
                        resultPoint = new Point(
                            (int)(rect.Left + rect.Width / 2),
                            (int)(rect.Top + rect.Height / 2));
                    }
                }
            }

            if (haveOffset)
            {
                resultPoint = new Point(
                    resultPoint.X + Convert.ToInt32(this.ExecutedCommand.Parameters["xoffset"]),
                    resultPoint.Y + Convert.ToInt32(this.ExecutedCommand.Parameters["yoffset"]));
            }

            Mouse.Position = resultPoint;

            return this.JsonResponse();
        }

        #endregion
    }
}
