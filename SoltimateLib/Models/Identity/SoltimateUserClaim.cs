using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SoltimateLib.Models.Identity
{
    /// <summary>
    /// User claims.
    /// </summary>
    [Table("UserClaim")]
    public class SoltimateUserClaim : IdentityUserClaim<string>
    {
        /// <summary>
        /// ID of the UserClaim.
        /// </summary>
        [Column("ID")]
        public override int Id { get; set; }
    }
}
