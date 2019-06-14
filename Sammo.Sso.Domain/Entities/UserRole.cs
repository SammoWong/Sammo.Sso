﻿using Sammo.Sso.Domain.Core.Models;
using System;

namespace Sammo.Sso.Domain.Entities
{
    public class UserRole : Entity
    {
        public Guid UserId { get; set; }

        public Guid RoleId { get; set; }

        public virtual User User { get; set; }

        public virtual Role Role { get; set; }
    }
}
