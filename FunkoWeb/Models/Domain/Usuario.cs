namespace FunkoWeb.Models.Domain;

public class Usuario {
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public Rol Rol { get; set; } = default!; 
}