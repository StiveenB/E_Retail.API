namespace E_Retail.API.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Ventas
    {
        [Key]
        public string ven_id { get; set; }

        public DateTime ven_fecha { get; set; }

        public decimal ven_total { get; set; }

        public TimeSpan ven_hora { get; set; }

        public string dven_id { get; set; }

        public string usu_id { get; set; }

        public DetalleVenta DetalleVenta { get; set; }

        public Usuario Usuario { get; set; }
    }

}
