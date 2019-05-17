using AgentRegistry.Infrastructure.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AgentRegistry.Core.System.Entities
{
    [DisplayColumn("ScannerLogId")]
    public class ScannerLog : IEntity
    {

        public ScannerLog()
        {
            DateTimeScanStart = DateTime.Now;
        }

        [Key]
        [Column("ScannerLogId")]
        public int Id { get; protected set; }

        [Required]
        public DateTime DateTimeScanStart { get; set; }

        public DateTime? DateTimeScanEnd { get; set; }

        public bool? IsSuccess { get; set; }

        public string ErrorMessage { get; set; }

        public string StackTrace { get; set; }

        public virtual ICollection<Agent> Agents { get; set; }
    }
}
