using Sammo.Sso.Domain.Core.Models;
using Sammo.Sso.Domain.Core.Models.Interfaces;
using System;
using System.ComponentModel;

namespace Sammo.Sso.Domain.Entities
{
    public class Permission : Entity, ICreatable, IModifiable
    {
        public MasterType MasterType { get; set; }

        public string MasterId { get; set; }

        public AccessType AccessType { get; set; }

        public string AccessId { get; set; }

        public int SortNumber { get; set; }

        public bool Enabled { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? CreatedTime { get; set; }

        public Guid? ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }
    }

    public enum MasterType : byte
    {
        [Description("角色")]
        Role = 1,

        [Description("用户")]
        User = 2
    }

    public enum AccessType : byte
    {
        [Description("菜单")]
        Menu = 1,

        [Description("按钮")]
        Button = 2
    }
}
