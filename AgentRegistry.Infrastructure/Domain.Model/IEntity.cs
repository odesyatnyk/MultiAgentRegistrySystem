using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AgentRegistry.Infrastructure.Domain.Model
{
    public interface IEntity
    {
        int Id { get; }
    }
}
