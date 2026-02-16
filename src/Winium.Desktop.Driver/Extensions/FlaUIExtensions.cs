namespace Winium.Desktop.Driver.Extensions
{
    #region using

    using System;
    using System.Drawing;

    using FlaUI.Core.AutomationElements;
    using FlaUI.Core.Input;
    using FlaUI.Core.WindowsAPI;

    #endregion

    internal static class FlaUIExtensions
    {
        #region Public Methods and Operators

        public static void ClickWithPressedCtrl(this AutomationElement element)
        {
            Keyboard.Press(VirtualKeyShort.CONTROL);
            try
            {
                element.Click();
            }
            finally
            {
                Keyboard.Release(VirtualKeyShort.CONTROL);
            }
        }

        public static void ClickAtCenter(this AutomationElement element)
        {
            var rect = element.BoundingRectangle;
            var centerX = (int)(rect.Left + rect.Width / 2);
            var centerY = (int)(rect.Top + rect.Height / 2);
            Mouse.Click(new Point(centerX, centerY));
        }

        public static void DoubleClickAtCenter(this AutomationElement element)
        {
            var rect = element.BoundingRectangle;
            var centerX = (int)(rect.Left + rect.Width / 2);
            var centerY = (int)(rect.Top + rect.Height / 2);
            Mouse.DoubleClick(new Point(centerX, centerY));
        }

        public static void RightClickAtCenter(this AutomationElement element)
        {
            var rect = element.BoundingRectangle;
            var centerX = (int)(rect.Left + rect.Width / 2);
            var centerY = (int)(rect.Top + rect.Height / 2);
            Mouse.RightClick(new Point(centerX, centerY));
        }

        public static string GetText(this AutomationElement element)
        {
            if (element.Patterns.Value.IsSupported)
            {
                return element.Patterns.Value.Pattern.Value.Value;
            }

            return element.Name;
        }

        public static void SetText(this AutomationElement element, string text)
        {
            if (element.Patterns.Value.IsSupported)
            {
                element.Patterns.Value.Pattern.SetValue(text ?? string.Empty);
                return;
            }

            element.Focus();
            Keyboard.Press(VirtualKeyShort.CONTROL);
            Keyboard.Type("a");
            Keyboard.Release(VirtualKeyShort.CONTROL);
            if (string.IsNullOrEmpty(text))
            {
                Keyboard.Press(VirtualKeyShort.DELETE);
                Keyboard.Release(VirtualKeyShort.DELETE);
            }
            else
            {
                Keyboard.Type(text);
            }
        }

        public static T GetPropertyValue<T>(this AutomationElement element, FlaUI.Core.Identifiers.PropertyId property)
        {
            var value = element.FrameworkAutomationElement.GetPropertyValue(property);
            if (value is T typed)
            {
                return typed;
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static ComboBox ToComboBox(this AutomationElement element)
        {
            return element.AsComboBox();
        }

        public static DataGridView ToDataGrid(this AutomationElement element)
        {
            return element.AsDataGridView();
        }

        public static Menu ToMenu(this AutomationElement element)
        {
            return element.AsMenu();
        }

        public static ListBox ToListBox(this AutomationElement element)
        {
            return element.AsListBox();
        }

        public static AutomationElement ScrollToItem(this ComboBox comboBox, SearchCondition condition)
        {
            comboBox.Expand();
            var item = condition.FindFirst(comboBox);
            if (item != null && item.Patterns.ScrollItem.IsSupported)
            {
                item.Patterns.ScrollItem.Pattern.ScrollIntoView();
            }

            return item;
        }

        public static AutomationElement ScrollToItem(this ListBox listBox, SearchCondition condition)
        {
            var item = condition.FindFirst(listBox);
            if (item != null && item.Patterns.ScrollItem.IsSupported)
            {
                item.Patterns.ScrollItem.Pattern.ScrollIntoView();
            }

            return item;
        }

        public static AutomationElement GetMenuItem(this Menu menu, string headersPath)
        {
            var headers = headersPath.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries);

            AutomationElement current = menu;
            foreach (var header in headers)
            {
                var item = current.FindFirstDescendant(cf => cf.ByName(header.Trim()));
                if (item == null)
                {
                    return null;
                }

                if (item.Patterns.ExpandCollapse.IsSupported)
                {
                    item.Patterns.ExpandCollapse.Pattern.Expand();
                }

                current = item;
            }

            return current;
        }

        public static void SelectMenuItem(this Menu menu, string headersPath)
        {
            var item = menu.GetMenuItem(headersPath);
            if (item != null)
            {
                item.Click();
            }
        }

        public static AutomationElement GetSelectedItem(this ComboBox comboBox)
        {
            if (comboBox.Patterns.Selection.IsSupported)
            {
                var selection = comboBox.Patterns.Selection.Pattern.Selection.Value;
                if (selection != null && selection.Length > 0)
                {
                    return selection[0];
                }
            }

            return null;
        }

        #endregion
    }
}
