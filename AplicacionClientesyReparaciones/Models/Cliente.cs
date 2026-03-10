using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace AplicacionClientesyReparaciones.Models
{
    [Table("Cliente")]
    public class Cliente : BaseModel
    {
        [PrimaryKey("id", false)]
        public long Id { get; set; }

        [Column("nombre")]
        public string? Nombre { get; set; }

        [Column("dni")]
        public string? Dni { get; set; }

        [Column("apellidos")]
        public string? Apellidos { get; set; }

        [Column("telefono")]
        public long? Telefono { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("direccion")]
        public string? Direccion { get; set; }
    }
}
