namespace Winium.Desktop.Driver.Input
{
    #region using

    using System.Collections.Generic;

    using FlaUI.Core.WindowsAPI;

    using OpenQA.Selenium;

    #endregion

    internal class KeyboardModifiers : List<string>
    {
        #region Static Fields

        private static readonly List<string> Modifiers = new List<string>
                                                             {
                                                                 Keys.Control, 
                                                                 Keys.LeftControl, 
                                                                 Keys.Shift, 
                                                                 Keys.LeftShift, 
                                                                 Keys.Alt, 
                                                                 Keys.LeftAlt
                                                             };

        private static readonly Dictionary<string, VirtualKeyShort> ModifiersMap =
            new Dictionary<string, VirtualKeyShort>
                {
                    { Keys.Control, VirtualKeyShort.CONTROL }, 
                    { Keys.Shift, VirtualKeyShort.SHIFT }, 
                    { Keys.Alt, VirtualKeyShort.MENU }, 
                };

        #endregion

        #region Public Methods and Operators

        public static string GetKeyFromUnicode(char key)
        {
            return Modifiers.Find(modifier => modifier[0] == key);
        }

        public static VirtualKeyShort GetVirtualKeyShort(string key)
        {
            VirtualKeyShort virtualKey;

            if (ModifiersMap.TryGetValue(key, out virtualKey))
            {
                return virtualKey;
            }

            return default(VirtualKeyShort);
        }

        public static bool IsModifier(string key)
        {
            return Modifiers.Contains(key);
        }

        #endregion
    }
}
