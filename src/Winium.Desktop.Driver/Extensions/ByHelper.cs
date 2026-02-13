namespace Winium.Desktop.Driver.Extensions
{
    #region using

    using System;

    #endregion

    public static class ByHelper
    {
        #region Public Methods and Operators

        public static SearchCondition GetStrategy(string strategy, string value)
        {
            switch (strategy)
            {
                case "id":
                case "name":
                case "class name":
                case "xpath":
                    return new SearchCondition(strategy, value);
                default:
                    throw new NotImplementedException(
                        string.Format("'{0}' is not valid or implemented searching strategy.", strategy));
            }
        }

        #endregion
    }
}
