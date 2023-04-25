namespace ORMTEST
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Pseudo { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }
    }
}
