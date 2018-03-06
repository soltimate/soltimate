using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SoltimateLib.Models.Identity
{
    /// <summary>
    /// Link users to roles.
    /// </summary>
    [Table("UserRole")]
    public class SoltimateUserRole : IdentityUserRole<string>
    {
        //
    }
}
