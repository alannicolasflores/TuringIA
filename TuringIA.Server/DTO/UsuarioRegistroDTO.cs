namespace TuringIA.Server.DTO
{

    public class UsuarioRegistroDTO
    {
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Contraseña { get; set; } = null!;
        public string? Rol { get; set; } = "usuario";
    }
}
