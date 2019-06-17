using Sammo.Sso.Domain.Core.Models;
using Sammo.Sso.Domain.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sammo.Sso.Domain.Entities
{
    public class Menu : Entity, ICreatable, IModifiable
    {
        public Guid ApplicationId { get; set; }

        public Guid? ParentId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public MenuType Category { get; set; }

        public string Url { get; set; }

        public string Icon { get; set; }

        public short Rank { get; set; }//等级

        public int SortNumber { get; set; }//排序

        public bool IsExpanded { get; set; }

        public bool Enabled { get; set; }

        public string Remark { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? CreatedTime { get; set; }

        public Guid? ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

        public virtual ICollection<Button> Buttons { get; set; }

        public virtual Application Application { get; set; }
    }

    public enum MenuType : byte
    {
        [Description("目录")]
        Catelog = 1,

        [Description("页面")]
        Page = 2,
    }
}
