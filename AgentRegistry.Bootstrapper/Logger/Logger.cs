using AgentRegistry.Core.System.Entities;
using AgentRegistry.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AgentRegistry.Bootstrapper.Logger
{
    public class Logger : ILogger
    {
        public void LogException(Exception ex)
        {
            if (ex != null)
            {
                Common.ExceptionLogRepository.Add(new ExceptionLog
                {
                    DateTimeLogging = DateTime.Now,
                    ErrorMessage = ex.Message,
                    StackTrace = ex.StackTrace,
                    InnerExceptionMessage = ex.InnerException?.Message,
                    InnerExceptionStackTrace = ex.InnerException?.StackTrace
                });

                Common.DataContext.SaveChanges();
            }
        }
    }
}
