using System;

namespace MyLedgerApp.Common.Utils
{
    public class TryActionUtils
    {
        public static void TryActionIfCondition<T>(Func<T,bool> condition, T elem, Action action, ref bool isTriggered)
        {
            if (condition(elem))
            {
                action();
                isTriggered = true;
            }
        }
        public static void TryActionIf(bool shouldTrigger, Action action, ref bool isTriggered)
        {
            if (shouldTrigger)
            {
                action();
                isTriggered = true;
            }
        }
    }
}
