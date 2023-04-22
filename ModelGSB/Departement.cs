namespace ModelGSB
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Departement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Departement()
        {
            Medecins = new HashSet<Medecin>();
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
        [JsonIgnoreSerialization]
        public virtual ICollection<Medecin> Medecins { get; set; }
    }
}
