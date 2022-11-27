using DBContext;
using DBEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UPC.APIBusiness.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/project")]
    [ApiController]
    public class ProjectController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly IProjectRepository _projectRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectRepository"></param>
        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [AllowAnonymous]
        [HttpGet]
        [Route("getprojects")]
        public ActionResult getProjects()
        {
            var ret = _projectRepository.getProjects();

            return Json(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [Authorize]
        [HttpGet]
        [Route("admin/list")]
        public ActionResult getProyectsAdmin()
        {
            var ret = _projectRepository.getProjects();

            return Json(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [Authorize]
        [HttpPost]
        [Route("admin/insert")]
        public ActionResult insert(EntityProject project)
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;

            var userId = claims.Where(p => p.Type == "client_codigo_usuario").FirstOrDefault()?.Value;
            var userDoc = claims.Where(p => p.Type == "client_numero_documento").FirstOrDefault()?.Value;

            project.UsuarioCrea = int.Parse(userId);

            var ret = _projectRepository.Insert(project);

            return Json(ret);
        }
    }
}