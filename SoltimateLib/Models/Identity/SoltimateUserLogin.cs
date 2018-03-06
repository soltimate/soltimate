using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SoltimateLib.Models.Identity
{
    /// <summary>
    /// User logins.
    /// </summary>
    [Table("UserLogin")]
    public class SoltimateUserLogin : IdentityUserLogin<string>
    {
        //
    }
}
