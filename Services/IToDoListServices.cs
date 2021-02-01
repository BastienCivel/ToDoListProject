using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListProject.Models;

namespace ToDoListProject.Services
{
    public interface IToDoListServices
    {
        ToDoListItem AddItem(ToDoListItem item, string UserName);
        Dictionary<string, ToDoListItem> GetToDoListItem(string UserName);
    }
}
