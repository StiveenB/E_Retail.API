namespace E_Retail.API.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Usuario
    {
        [Key]
        public string usu_id { get; set; }

        [Required]
        public string usu_contrasenia { get; set; }

        [ForeignKey("Persona")]
        public string Cedula { get; set; }

        public int TipoUsuarioId { get; set; }

        public string NegocioId { get; set; }

        public Persona Persona { get; set; }

        public TipoUsuario TipoUsuario { get; set; }

        public Negocio Negocio { get; set; }
    }

}
