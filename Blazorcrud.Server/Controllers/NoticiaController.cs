using Blazorcrud.Server.Authorization;
using Blazorcrud.Server.Models;
using Blazorcrud.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blazorcrud.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NoticiaController : ControllerBase
    {
        private readonly INoticiaRepository _noticiaRepository;

        public NoticiaController(INoticiaRepository noticiaRepository)
        {
            _noticiaRepository = noticiaRepository;
        }

        /// <summary>
        /// Returns a list of paginated people with a default page size of 5.
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetPeople([FromQuery] string? name, int page)
        {
            return Ok(_noticiaRepository.GetNoticia(name, page));
        }

        /// <summary>
        /// Gets a specific noticia by Id.
        /// </summary>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult> Getnoticia(int id)
        {
            return Ok(await _noticiaRepository.GetNoticia(id));
        }

        /// <summary>
        /// Creates a noticia with child addresses.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Addnoticia(Noticia noticia)
        {
            return Ok(await _noticiaRepository.AddNoticia(noticia));
        }
        
        /// <summary>
        /// Updates a noticia with a specific Id.
        /// </summary>
        [HttpPut]
        public async Task<ActionResult> Updatenoticia(Noticia noticia)
        {
            return Ok(await _noticiaRepository.UpdateNoticia(noticia));
        }

        /// <summary>
        /// Deletes a noticia with a specific Id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Deletenoticia(int id)
        {
            return Ok(await _noticiaRepository.DeleteNoticia(id));
        }
    }
}
