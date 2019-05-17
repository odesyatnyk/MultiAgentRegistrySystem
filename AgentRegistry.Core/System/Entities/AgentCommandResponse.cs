using AgentRegistry.Infrastructure.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AgentRegistry.Core.System.Entities
{
    [DisplayColumn("AgentCommandResponseId")]
    public class AgentCommandResponse : IEntity
    {
        [Key]
        [Column("AgentCommandResponseId")]
        public int Id { get; protected set; }

        [Required]
        [Column("AgentCommandId")]
        public virtual AgentCommand AgentCommand { get; set; }

        [Required]
        public string ResponseCode { get; set; }

        public string ResponseCodeDescription { get; set; }

    }
}
