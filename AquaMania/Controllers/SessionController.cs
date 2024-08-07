using AquaMania.Model;
using AquaMania.Model.Session;
using AquaMania.Model.Usuario;
using AquaMania.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AquaMania.Controllers
{
    /// <summary>
    /// Controlador responsável por lidar com operações relacionadas a sessão.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _service;

        public SessionController(ISessionService service)
        {
            _service = service;
        }

        /// <summary>
        /// Responsável por criar uma nova sessão
        /// </summary>
        /// <returns>Dados do usuário cadastrado no sistema</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(Response<SessionResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Response<SessionResponse>>> CreateSession(CreateSession createSession)
        {
            var response = await _service.CreateSession(createSession);
            return Ok(response);
        }
    }
}
