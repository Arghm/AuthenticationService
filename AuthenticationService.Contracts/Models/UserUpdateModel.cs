using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationService.Contracts.Models
{
    /// <summary>
    /// User data for update.
    /// </summary>
    public class UserUpdateModel
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string[] IpAddresses { get; set; }

        public bool? IsActive { get; set; } = true;

        public bool? IsDeleted { get; set; } = false;
    }
}
