namespace E_Retail.API.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Persona
    {
        [Key]
        public string? per_cedula { get; set; }

        [Required]
        public string per_correo { get; set; }

        [Required]
        public string per_nombres { get; set; }

        public string per_apellidos { get; set; }

        public DateTime? per_fecha_nacimiento { get; set; }

        public string per_telefono { get; set; }

        public string per_url_imagen { get; set; }
    }

}
