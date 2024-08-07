using AquaMania.Model;
using AquaMania.Model.Session;

namespace AquaMania.Service.Interface;

public interface ISessionService
{
    Task<Response<SessionResponse>> CreateSession(CreateSession createSession);
}
