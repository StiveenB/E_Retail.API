namespace E_Retail.API.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class TipoAccion
    {
        [Key]
        public int tacc_id { get; set; }

        [Required]
        public string tacc_descripcion { get; set; }
    }

}
