using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListProject.Models;
using ToDoListProject.Services;

namespace ToDoListProject.Controllers
{
    [Route("v1/")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoListServices _services;

        public ToDoListController(IToDoListServices services)
        {
            _services = services;
        }

        [HttpPost]
        [Route("AddItem")]
        public ActionResult<ToDoListItem> AddItem(ToDoListItem item)
        {
            var todolistItem = _services.AddItem(item);
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
            var todolistItem = _services.GetToDoListItem();
            if(todolistItem.Count == 0)
            {
                return NotFound();
            }

            return todolistItem;
        }
    }
}
