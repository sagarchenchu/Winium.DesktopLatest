namespace Winium.Desktop.Driver
{
    #region using

    using System;

    using FlaUI.Core.AutomationElements;
    using FlaUI.UIA3;

    #endregion

    internal static class FlaUIHelper
    {
        #region Static Fields

        private static readonly Lazy<UIA3Automation> LazyAutomation = new Lazy<UIA3Automation>(() => new UIA3Automation());

        private static int searchTimeout = 5000;

        #endregion

        #region Public Properties

        public static UIA3Automation Automation
        {
            get { return LazyAutomation.Value; }
        }

        public static AutomationElement Root
        {
            get { return Automation.GetDesktop(); }
        }

        public static AutomationElement FocusedElement
        {
            get { return Automation.FocusedElement(); }
        }

        public static int SearchTimeout
        {
            get { return searchTimeout; }
            set { searchTimeout = value; }
        }

        #endregion
    }
}
