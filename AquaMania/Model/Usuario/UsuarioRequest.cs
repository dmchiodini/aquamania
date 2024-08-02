using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AquaMania.Model.Usuario;

public class UsuarioRequest
{
    [Key]
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }    
    public string Senha { get; set; }
    public DateTime Data_nascimento { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public DateTime Criado_em { get; set; }
    public DateTime Atualizado_em { get; set; }
}
