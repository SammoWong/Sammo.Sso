using System;
using System.Collections.Generic;
using System.Text;

namespace Sammo.Sso.Domain.Core.Models.Interfaces
{
    public interface ICreatable
    {
        Guid CreatedBy { get; set; }

        DateTime CreatedTime { get; set; }
    }
}
