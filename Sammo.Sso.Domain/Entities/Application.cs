using Sammo.Sso.Domain.Core.Models;
using Sammo.Sso.Domain.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sammo.Sso.Domain.Entities
{
    public class Application : Entity, ICreatable, IModifiable
    {
        public string ClientId { get; set; }

        public string ClientName { get; set; }

        public string ClientSecrets { get; set; }

        public string RedirectUris { get; set; }

        public string PostLogoutRedirectUris { get; set; }

        public string DisplayName { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        public Guid? ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }
    }
}
