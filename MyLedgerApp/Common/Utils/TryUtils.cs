namespace MyLedgerApp.Common.Utils
{
    public class TryUtils
    {
        /// <summary>
        /// Run <paramref name="action"/> if <paramref name="shouldTrigger"/> is true. 
        /// </summary>
        /// <param name="shouldTrigger"></param>
        /// <param name="action"></param>
        /// <param name="isTriggered"></param>
        public static void ActionIf(bool shouldTrigger, Action action, ref bool isTriggered)
        {
            if (shouldTrigger)
            {
                action();
                isTriggered = true;
            }
        }
        /// <summary>
        /// Checks if all objects are not null.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool AllNotNull(params object?[] values)
        {
            return values.All(v => v is not null);
        }
    }
}
