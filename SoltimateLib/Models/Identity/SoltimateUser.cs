using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SoltimateLib.Models.Identity
{
    [Table("User")]
    public class SoltimateUser : IdentityUser<string>
    {
        /// <summary>
        /// ID of the User.
        /// </summary>
        [Column("ID")]
        public override string Id { get; set; }
    }
}
