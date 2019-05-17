using AgentRegistry.Infrastructure.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AgentRegistry.Core.System.Entities
{
    [DisplayColumn("AgentCommunicationLogId")]
    public class AgentsCommunicationLog : IEntity
    {
        [Key]
        [Column("AgentCommunicationLogId")]
        public int Id { get; protected set; }

        [Required]
        [Column("AgentFromId")]
        public virtual Agent AgentFrom { get; set; }

        [Required]
        [Column("AgentToId")]
        public virtual Agent AgentTo { get; set; }

        [Required]
        [Column("AgentCommandId")]
        public virtual AgentCommand Command { get; set; }

        [Required]
        public DateTime DateTimeCommunication { get; set; }

        public bool? IsSuccess { get; set; }

        public string ErrorMessage { get; set; }

        public string StackTrace { get; set; }
    }
}
