using AgentRegistry.Infrastructure.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AgentRegistry.Core.System.Entities
{
    public class ExceptionLog : IEntity
    {
        public ExceptionLog()
        {
            DateTimeLogging = DateTime.Now;
        }

        [Key]
        [Column("ExceptionLogId")]
        public int Id { get; protected set; }

        [Required]
        public string ErrorMessage { get; set; }

        [Required]
        public string StackTrace { get; set; }

        public string InnerExceptionMessage { get; set; }

        public string InnerExceptionStackTrace { get; set; }

        [Required]
        public DateTime DateTimeLogging { get; set; }
    }
}
