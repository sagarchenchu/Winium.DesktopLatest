namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;
    using System.IO;

    using FlaUI.Core.Capturing;

    using Winium.StoreApps.Common;

    #endregion

    internal class ScreenshotExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var capture = Capture.Screen();
            string screenshotSource;
            using (var ms = new MemoryStream())
            {
                capture.Bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                screenshotSource = Convert.ToBase64String(ms.ToArray());
            }

            return this.JsonResponse(ResponseStatus.Success, screenshotSource);
        }

        #endregion
    }
}
