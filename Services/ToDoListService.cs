using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListProject.Models;

namespace ToDoListProject.Services
{

    public class ToDoListService : IToDoListServices
    {

        private readonly Dictionary<string, ToDoListItem> _todolistItem;
        public ToDoListService()
        {
            _todolistItem = new Dictionary<string, ToDoListItem>();
        }

        public ToDoListItem AddItem(ToDoListItem item)
        {
            _todolistItem.Add(item.ItemName, item);

            return item;
        }

        public Dictionary<string, ToDoListItem> GetToDoListItem()
        {
            return _todolistItem;
        }
    }
}
