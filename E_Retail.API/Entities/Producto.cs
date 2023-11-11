namespace E_Retail.API.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Producto
    {
        [Key]
        public string pro_id { get; set; }

        [Required]
        public string pro_nombre { get; set; }

        public string pro_descripcion { get; set; }

        public int pro_cantidad { get; set; }

        public decimal pro_precio_compra { get; set; }

        public decimal pro_precio_venta { get; set; }

        public string pro_url_imagen { get; set; }

        public int pro_cant_notificacion { get; set; }

        public string tpro_id { get; set; }

        public string usu_id { get; set; }

        public TipoProducto TipoProducto { get; set; }

        public Usuario Usuario { get; set; }
    }

}
