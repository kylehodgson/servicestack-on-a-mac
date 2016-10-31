using System.Collections.Generic;
using ToDoBackEnd;
using System.Linq;

namespace ToDoBackEnd
{
    public interface ToDoStore {
        void Add(Item todo);
        List<Item> All();
        void Clear();
        Item SingleById(string id);
        void Delete(string id );
    }

    public class SingletonStore : ToDoStore {
         private List<Item> repo = new List<Item>();

         public void Add(Item todo) {
             repo.Add(todo);
         }
         public List<Item> All() {
             return repo;
         }

         public void Clear() {
             repo = new List<Item>();
         }

         public Item SingleById(string id) {
             return repo.FirstOrDefault(item => item.id.Equals(id));
         }

         public void Delete(string id ) {
             var newRepo = new List<Item>();
             foreach (var item in repo)
             {
                 if(!item.id.Equals(id)) newRepo.Add(item);
             }
             repo = newRepo;
         }
    }
}