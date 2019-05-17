using AgentRegistry.Infrastructure.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AgentRegistry.Core.System.Entities
{
    [DisplayColumn("AgentCommandId")]
    public class AgentCommand : IEntity
    {
        [Key]
        [Column("AgentCommandId")]
        public int Id { get; protected set; }

        [Required]
        [Column("AgentTypeId")]
        public virtual AgentType AgentType { get; set; }

        [Required]
        public string CommandName { get; set; }

        public string CommandDescription { get; set; }

        public virtual ICollection<AgentCommandResponse> AgentCommandResponses { get; set; }
    }
}
