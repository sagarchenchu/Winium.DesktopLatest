namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using FlaUI.Core.Input;
    using FlaUI.Core.WindowsAPI;

    #endregion

    internal class SubmitElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            Keyboard.Press(VirtualKeyShort.ENTER);
            Keyboard.Release(VirtualKeyShort.ENTER);
            return this.JsonResponse();
        }

        #endregion
    }
}
