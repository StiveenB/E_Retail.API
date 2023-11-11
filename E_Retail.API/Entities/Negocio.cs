namespace E_Retail.API.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Negocio
    {
        [Key]
        public string neg_id { get; set; }

        [Required]
        public string neg_nombre { get; set; }

        public string neg_descripcion { get; set; }

        public bool neg_empleado { get; set; }

        public int neg_num_empleados { get; set; }
    }

}
