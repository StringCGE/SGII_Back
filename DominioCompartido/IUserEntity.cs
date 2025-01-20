namespace SGII_Back.Dominio.Shared
{
    public interface IUserEntity
    {
        public long IdUsuario { get; set; }
        public string? Correo { get; set; } //Email
        public string? InicioSesion { get; set; }
        public string? Token { get; set; }
        public string? Identificacion { get; set; } //usuario
        public ICollection<string>? Roles { get; set; }
        public string? Hash { get; set; }


        public string? urlFoto { get; set; }
        public string? role { get; set; }
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public string? cedula { get; set; }
        public DateTime? nacimiento { get; set; }
    }
}
