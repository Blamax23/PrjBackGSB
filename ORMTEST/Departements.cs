namespace ORMTEST
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Departements
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Departements()
        {
            Medecins = new HashSet<Medecins>();
        }

        [Key]
        [StringLength(3)]
        public string IdDepartement { get; set; }

        [Required]
        [StringLength(50)]
        public string NomDep { get; set; }

        [StringLength(50)]
        public string NomRegion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Medecins> Medecins { get; set; }
    }
}
