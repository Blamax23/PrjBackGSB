namespace ModelGSB
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Medecin
    {
        [Key]
        public int IdMedecin { get; set; }

        [Required]
        [StringLength(50)]
        public string NomMed { get; set; }

        [Required]
        [StringLength(50)]
        public string PrenomMed { get; set; }

        [Required]
        [StringLength(80)]
        public string AdresseMed { get; set; }

        [Required]
        [StringLength(10)]
        public string TelephoneMed { get; set; }

        public bool SpecialiteComplementaire { get; set; }

        [Required]
        [StringLength(3)]
        public string IdDepartement { get; set; }

        [JsonIgnoreSerialization]
        public virtual Departement Departement { get; set; }

        //recherche par nom
        [JsonIgnore]
        [NotMapped]
        public string nom { get; set; }
    }
}
