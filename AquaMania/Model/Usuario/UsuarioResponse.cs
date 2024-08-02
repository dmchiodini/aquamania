namespace AquaMania.Model.Usuario;

public class UsuarioResponse
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public DateTime Data_nascimento { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
}
