namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System.Threading;

    using FlaUI.Core;

    using Newtonsoft.Json;

    using Winium.Desktop.Driver.Automator;
    using Winium.Desktop.Driver.Input;
    using Winium.StoreApps.Common;

    #endregion

    internal class NewSessionExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var serializedCapability =
                JsonConvert.SerializeObject(this.ExecutedCommand.Parameters["capabilities"]["firstMatch"].First);
            this.Automator.ActualCapabilities = Capabilities.CapabilitiesFromJsonString(serializedCapability);

            this.InitializeApplication(this.Automator.ActualCapabilities.DebugConnectToRunningApp);
            this.InitializeKeyboardEmulator(this.Automator.ActualCapabilities.KeyboardSimulator);

            Thread.Sleep(this.Automator.ActualCapabilities.LaunchDelay);

            return this.JsonResponse(ResponseStatus.Success, this.Automator.ActualCapabilities);
        }

        private void InitializeApplication(bool debugDoNotDeploy = false)
        {
            var appPath = this.Automator.ActualCapabilities.App;
            var appArguments = this.Automator.ActualCapabilities.Arguments;

            if (debugDoNotDeploy)
            {
                var processes = System.Diagnostics.Process.GetProcessesByName(
                    System.IO.Path.GetFileNameWithoutExtension(appPath));
                if (processes.Length > 0)
                {
                    this.Automator.Application = Application.Attach(processes[0]);
                }
            }
            else
            {
                this.Automator.Application = Application.Launch(appPath, appArguments);
            }
        }

        private void InitializeKeyboardEmulator(KeyboardSimulatorType keyboardSimulatorType)
        {
            this.Automator.WiniumKeyboard = new WiniumKeyboard(keyboardSimulatorType);

            Logger.Debug("Current keyboard simulator: {0}", keyboardSimulatorType);
        }

        #endregion
    }
}
