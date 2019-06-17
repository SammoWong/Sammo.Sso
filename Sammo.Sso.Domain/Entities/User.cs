using Sammo.Sso.Domain.Core.Models;
using Sammo.Sso.Domain.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sammo.Sso.Domain.Entities
{
    public class User : Entity, ICreatable, IModifiable
    {
        public string UserName { get; set; }

        public string NickName { get; set; }

        public string RealName { get; set; }

        public string IdCard { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public Gender? Gender { get; set; }

        public DateTime? Birthday { get; set; }

        public string Avatar { get; set; }

        public bool Enabled { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? CreatedTime { get; set; }

        public Guid? ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }
    }

    public enum Gender : byte
    {
        [Description("男性")]
        Male = 1,

        [Description("女性")]
        Female = 2,
    }
}
