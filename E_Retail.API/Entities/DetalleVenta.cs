namespace E_Retail.API.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class DetalleVenta
    {
        [Key]
        public string dven_id { get; set; }

        [Required]
        public decimal dven_cantidad { get; set; }

        [Required]
        public decimal dven_precio_unitario { get; set; }

        public decimal dven_subtotal { get; set; }

        public decimal dven_iva { get; set; }

        public decimal dven_descuento { get; set; }
    }

}
