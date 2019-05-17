using AgentRegistry.Bootstrapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgentRegistry
{
    public static class SystemHelper
    {
        public static void TryCatchDefault(Action action)
        {
            try
            {
                if (action != null)
                {
                    action.Invoke();
                }
            }
            catch (Exception ex)
            {
                Common.BuildServices();
                Common.Logger.LogException(ex);
                throw;
            }
        }
    }
}
