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
using UPC.APIBusiness.API.Security;

namespace UPC.Business.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/User")]
    [ApiController]
    public class UserController : Controller
    {

        /// <summary>
        /// Constructor
        /// </summary>
        protected readonly IUserRepository _UserRepository;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserRepository"></param>
        public UserController(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [AllowAnonymous]
        [HttpPost]
        [Route("insert")]
        public ActionResult Insert(EntityUser user)
        {
            var ret = _UserRepository.Insert(user);

            return Json(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(EntityLogin login)
        {
            var ret = _UserRepository.Login(login);

            if(ret.data != null)
            {
                var responseLogin = ret.data as EntityLoginResponse;
                var userId = responseLogin.IdUsuario.ToString();
                var userDoc = responseLogin.DocumentoIdentidad;

                var token = JsonConvert
                                    .DeserializeObject<AccessToken>(
                                        await new Authentication()
                                        .GenerateToken(userDoc, userId)
                                        ).access_token;

                responseLogin.token = token;
                ret.data = responseLogin;
            }

            return Json(ret);
        }
    }
}