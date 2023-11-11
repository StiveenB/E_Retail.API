namespace E_Retail.API.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Seguridad
    {
        [Key]
        public int seg_id { get; set; }

        public DateTime seg_fecha { get; set; }

        public TimeSpan seg_hora { get; set; }

        public int tacc_id { get; set; }

        public TipoAccion TipoAccion { get; set; }
    }

}
