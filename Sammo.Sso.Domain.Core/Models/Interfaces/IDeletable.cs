using System;
using System.Collections.Generic;
using System.Text;

namespace Sammo.Sso.Domain.Core.Models.Interfaces
{
    public interface IDeletable
    {
        bool IsDeleted { get; set; }

        Guid? DeletedBy { get; set; }

        DateTime? DeletedTime { get; set; }
    }
}
