using AgentRegistry.Infrastructure.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AgentRegistry.Core.System.Entities
{
    [DisplayColumn("AgentTypeId")]
    public class AgentType : IEntity
    {
        [Key]
        [Column("AgentTypeId")]
        public int Id { get; protected set; }

        [Required]
        public string AgentTypeName { get; set; }

        public string AgentTypeDescription { get; set; }

        public virtual ICollection<Agent> Agents { get; set; }

        public virtual ICollection<AgentCommand> Commands { get; set; }

    }
}
