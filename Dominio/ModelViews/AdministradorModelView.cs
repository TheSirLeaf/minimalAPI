using Minimal_Api.Dominio.Enuns;

namespace Minimal_Api.Dominio.ModelViews;

public record AdministradorModelView
{
    public int Id { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Perfil { get; set; } = default!;
}