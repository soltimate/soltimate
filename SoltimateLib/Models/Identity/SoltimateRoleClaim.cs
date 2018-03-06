using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SoltimateLib.Models.Identity
{
    /// <summary>
    /// Role claim.
    /// </summary>
    [Table("RoleClaim")]
    public class SoltimateRoleClaim : IdentityRoleClaim<string>
    {
        /// <summary>
        /// ID of the RoleClaim.
        /// </summary>
        [Column("ID")]
        public override int Id { get; set; }
    }
}
