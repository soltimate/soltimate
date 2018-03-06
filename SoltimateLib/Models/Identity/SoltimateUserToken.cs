using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SoltimateLib.Models.Identity
{
    /// <summary>
    /// Tokens for users.
    /// </summary>
    [Table("UserToken")]
    public class SoltimateUserToken : IdentityUserToken<string>
    {
        //
    }
}
