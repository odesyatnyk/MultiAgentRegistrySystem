using AgentRegistry.Infrastructure.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AgentRegistry.Core.System.Entities
{
    [DisplayColumn("AgentId")]
    public class Agent : IEntity
    {
        [Key]
        [Column("AgentId")]
        public int Id { get; protected set; }

        [Required]
        [Column("ScannerLogId")]
        public virtual ScannerLog ScannerLog { get; set; }

        [Required]
        [Column("AgentTypeId")]
        public virtual AgentType AgentType { get; set; }

        [Required]
        public string IpAddress { get; set; }

        [Required]
        public int Port { get; set; }

    }
}
