namespace AquaMania.Model.Session;

public class SessionResponse
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public DateTime Data_nascimento { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string Token { get; set; }
}
