using AgentRegistry.Bootstrapper;
using AgentRegistry.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgentRegistry
{
    public static class IDataContextExtensions
    {
        public static void TryBeginCommitTransaction(this IDataContext dataContext, Action action)
        {
            using (var tran = dataContext.Database.BeginTransaction())
            {
                SystemHelper.TryCatchDefault(() =>
                {
                    try
                    {
                        if (action != null)
                        {
                            action.Invoke();
                            dataContext.SaveChanges();
                            tran.Commit();
                        }
                    }
                    catch (Exception)
                    {
                        dataContext.Database.CurrentTransaction.Rollback();
                        throw;
                    }
                });
            }
        }
    }
}
