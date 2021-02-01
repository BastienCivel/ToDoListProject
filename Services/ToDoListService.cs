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

        //Ajouter un objet à la todolist en y associant le nom de l'utilisateur l'ayant ajouté
        public ToDoListItem AddItem(ToDoListItem item, string UserName)
        {
            item.userName = UserName;
            _todolistItem.Add(item.ItemName + "." + item.Id, item);

            return item;
        }
        //Récupérer la liste des items d'un utilisateur
        public Dictionary<string, ToDoListItem> GetToDoListItem(string Username)
        {
            Dictionary<string, ToDoListItem> result = _todolistItem.Where
                (re => re.Value.userName == Username).ToDictionary(i => i.Key, i => i.Value);
            return result;
        }
    }
}
