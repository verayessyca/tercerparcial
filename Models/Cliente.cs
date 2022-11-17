namespace PrimerParcial.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public Ciudad? IdCiudad { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Documento { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? FechaNacimiento { get; set; }
        public string? Ciudad { get; set; }
        public string? Nacionalidad { get; set; }

    }
}
