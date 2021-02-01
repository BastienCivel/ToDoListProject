using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoListProject.Models;
using ToDoListProject.Services;

namespace ToDoListProject.Controllers
{
    [Authorize]
    [Route("v1/")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoListServices _services;
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;

        public ToDoListController(IJwtAuthenticationManager jwtAuthenticationManager, IToDoListServices services)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            _services = services;
        }

        /*PARTIE TODOLIST */
        [HttpPost]
        [Route("AddItem")]
        public ActionResult<ToDoListItem> AddItem(ToDoListItem item)
        {
            var todolistItem = _services.AddItem(item, User.Identity.Name);
            if(todolistItem == null)
            {
                return NotFound();
            }

            return todolistItem;
        }

        [HttpGet]
        [Route("GetToDoListItem")]
        public ActionResult<Dictionary<string, ToDoListItem>> GetItem()
        {
               var todolistItem = _services.GetToDoListItem(User.Identity.Name);
               if(todolistItem.Count == 0)
               {
                   return NotFound();
               }

               return todolistItem;
        }
        /* FIN PARTIE TODOLIST*/

        /* Partie USER */
        [HttpGet]
        public IEnumerable<string> Get()
        {
            yield return User.Identity.Name;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Authenticate")]
        public IActionResult Authenticate([FromBody] UserCredentials userCredentials)
        {
            var token = jwtAuthenticationManager.Authenticate(userCredentials.Username, userCredentials.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
        /* Fin partie USER */
    }
}
