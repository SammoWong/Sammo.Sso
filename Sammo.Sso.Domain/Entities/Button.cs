using Sammo.Sso.Domain.Core.Models;
using Sammo.Sso.Domain.Core.Models.Interfaces;
using System;

namespace Sammo.Sso.Domain.Entities
{
    public class Button : Entity, ICreatable, IModifiable
    {
        public Guid MenuId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string JsEvent { get; set; }

        public string Url { get; set; }

        public string Icon { get; set; }

        public int SortNumber { get; set; }

        public string Remark { get; set; }

        public bool Enabled { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        public Guid? ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

        public virtual Menu Menu { get; set; }
    }
}
