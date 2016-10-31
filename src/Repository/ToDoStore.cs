using System.Collections.Generic;
using ToDoBackEnd;

namespace ToDoBackEnd {
    public interface ToDoStore {
        void Add(Item todo);
        List<Item> All();
        void Clear();
        Item SingleById(string id);
        void Delete(string id );
        Item Save(Item item);
    }
}