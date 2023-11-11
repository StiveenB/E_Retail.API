namespace E_Retail.API.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class TipoProducto
    {
        [Key]
        public string tpro_id { get; set; }

        [Required]
        public string tpro_nombre { get; set; }

        public bool tpro_primera_necesidad { get; set; }
    }

}
