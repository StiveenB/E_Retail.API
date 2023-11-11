namespace E_Retail.API.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class TipoUsuario
    {
        [Key]
        public int tusu_id { get; set; }

        public string tusu_nombre { get; set; }
    }

}
