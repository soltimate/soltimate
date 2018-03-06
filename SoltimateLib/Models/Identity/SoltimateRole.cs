using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SoltimateLib.Models.Identity
{
    /// <summary>
    /// Role of user identity.
    /// </summary>
    [Table("Role")]
    public class SoltimateRole : IdentityRole<string>
    {
        /// <summary>
        /// ID of the role.
        /// </summary>
        [Column("ID")]
        public override string Id { get; set; }
    }
}
