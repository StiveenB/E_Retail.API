namespace E_Retail.API.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Proveedor
    {
        [Key]
        public string prov_id { get; set; }

        [Required]
        public string prov_nombre_comercial { get; set; }
    }

}
