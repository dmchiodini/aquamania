using AquaMania.Model;
using AquaMania.Model.Session;
using AquaMania.Model.Usuario;
using AquaMania.Service.Interface;
using AquaMania.Utils;

namespace AquaMania.Service
{
    public class SessionService : ISessionService
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioService _usuarioService;
        private readonly SystemUtils _systemUtils = new SystemUtils();

        public SessionService(IUsuarioService usuarioService, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _configuration = configuration;
        }

        public async Task<Response<SessionResponse>> CreateSession(CreateSession createSession)
        {
            try
            {
                UsuarioRequest user = new UsuarioRequest();

                var passwordHasher = string.Empty;
                var getUser = await _usuarioService.GetByEmail(createSession.Email);

                user = getUser.Data;

                if (user == null)
                {
                    return new Response<SessionResponse>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Success = false,
                        Message = "Não há usuário cadastrado com este e-mail",
                        Data = null,
                    };
                }

                passwordHasher = user.Senha;

                if(!_systemUtils.VerifyPasswordHash(createSession.Password, passwordHasher))
                {
                    return new Response<SessionResponse>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Success = false,
                        Message = "Senha incorreta",
                        Data = null,
                    };
                }

                var key = _configuration.GetSection("PrivateKey").Value;

                var createdSession = new SessionResponse
                {
                    Id = user.Id,
                    Nome = user.Nome,
                    Email = user.Email,
                    Data_nascimento = user.Data_nascimento,
                    Cidade = user.Cidade,
                    Estado = user.Estado,
                    Token = _systemUtils.GenerateToken(key, user.Id, user.Nome, user.Email),
                };

                return new Response<SessionResponse>()
                {
                    Status = StatusCodes.Status201Created,
                    Success = true,
                    Message = "Login efetuado com sucesso",
                    Data = createdSession,
                };
            }
            catch (Exception ex)
            {
                return new Response<SessionResponse>()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Message = $"{ex.Message}",
                    Data = null,
                    Error = "InternalServerError"
                };
            }
        }
    }
}
