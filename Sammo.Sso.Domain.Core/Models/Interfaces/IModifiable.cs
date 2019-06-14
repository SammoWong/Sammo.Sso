using System;
using System.Collections.Generic;
using System.Text;

namespace Sammo.Sso.Domain.Core.Models.Interfaces
{
    public interface IModifiable
    {
        Guid? ModifiedBy { get; set; }

        DateTime? ModifiedTime { get; set; }
    }
}
