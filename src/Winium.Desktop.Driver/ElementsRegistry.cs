namespace Winium.Desktop.Driver
{
    #region using

    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;

    using FlaUI.Core.AutomationElements;
    using Winium.StoreApps.Common;
    using Winium.StoreApps.Common.Exceptions;

    #endregion

    internal class ElementsRegistry
    {
        #region Static Fields

        private static int safeInstanceCount;

        #endregion

        #region Fields

        private readonly Dictionary<string, AutomationElement> registeredElements;

        #endregion

        #region Constructors and Destructors

        public ElementsRegistry()
        {
            this.registeredElements = new Dictionary<string, AutomationElement>();
        }

        #endregion

        #region Public Methods and Operators

        public void Clear()
        {
            this.registeredElements.Clear();
        }

        /// <summary>
        /// Returns AutomationElement registered with specified key if any exists. Throws if no element is found.
        /// </summary>
        /// <exception cref="AutomationException">
        /// Registered element is not found or element has been garbage collected.
        /// </exception>
        public AutomationElement GetRegisteredElement(string registeredKey)
        {
            var element = this.GetRegisteredElementOrNull(registeredKey);
            if (element != null)
            {
                return element;
            }

            throw new AutomationException("Stale element reference", ResponseStatus.StaleElementReference);
        }

        public string RegisterElement(AutomationElement element)
        {
            var runtimeId = element.Properties.RuntimeId.IsSupported
                ? element.Properties.RuntimeId.Value
                : null;

            string registeredKey = null;
            if (runtimeId != null)
            {
                registeredKey = this.registeredElements.FirstOrDefault(
                    x =>
                    {
                        var existingId = x.Value.Properties.RuntimeId.IsSupported
                            ? x.Value.Properties.RuntimeId.Value
                            : null;
                        return existingId != null && runtimeId.SequenceEqual(existingId);
                    }).Key;
            }

            if (registeredKey == null)
            {
                Interlocked.Increment(ref safeInstanceCount);

                registeredKey = element.GetHashCode() + "-"
                                + safeInstanceCount.ToString(string.Empty, CultureInfo.InvariantCulture);
                this.registeredElements.Add(registeredKey, element);
            }

            return registeredKey;
        }

        public IEnumerable<string> RegisterElements(IEnumerable<AutomationElement> elements)
        {
            return elements.Select(this.RegisterElement);
        }

        #endregion

        #region Methods

        internal AutomationElement GetRegisteredElementOrNull(string registeredKey)
        {
            AutomationElement element;
            this.registeredElements.TryGetValue(registeredKey, out element);
            return element;
        }

        #endregion
    }
}
