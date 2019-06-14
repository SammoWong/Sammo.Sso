using Sammo.Sso.Domain.Core.Models;
using Sammo.Sso.Domain.Core.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace Sammo.Sso.Domain.Entities
{
    public class Role : Entity, ICreatable, IModifiable
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        public Guid? ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
