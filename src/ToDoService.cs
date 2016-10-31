using System;
using System.Collections.Generic;
using ServiceStack;
using ToDoBackend;

namespace ToDoBackEnd
{
    [Route("/items", "GET")]
    public class ViewListRequest : IReturn<List<Item>>
    { }

    [Route("/items/{id}","GET")]
    public class ViewItemRequest : IReturn<Item>
    {
        public string id { get; set; }
    }

    [Route("/items/{id}", "PATCH")]
    public class PatchItemRequest : IReturn<Item>
    {
        public string id { get; set; }
        public bool? completed { get; set;}
        public string title { get; set;}
        public int? order { get; set;}
    }

    [Route("/items/{id}", "DELETE")]
    public class DeleteItemRequest : IReturnVoid
    {
        public string id { get; set; }
    }

    [Route("/items", "POST")]
    public class NewItemRequest : IReturn<Item>
    {
        public string title { get; set; }
        public int? order { get; set; }
    }

    [Route("/items", "DELETE")]
    public class DeleteAllItemsRequest : IReturnVoid
    { }

    public class Item
    {
        public string title { get; set; }
        public bool completed { get; set; }
        public string url { get; set; }
        public string id { get; set;}
        public int order { get; set;}
    }

    public class TodoService : Service
    {
        public ToDoStore Repo { get ; set; }

        public List<Item> Get(ViewListRequest request)
        {
            return Repo.All();
        }

        public Item Get(ViewItemRequest request) 
        {
            return Repo.SingleById(request.id);
        }

        public Item Patch(PatchItemRequest request) 
        {
            var original = Repo.SingleById(request.id);
            if(request.completed!=null) original.completed = (bool)request.completed;
            if(request.title!=null) original.title = request.title;
            if(request.order!=null) original.order = (int)request.order;
            return original;
        }
        
        public void Delete(DeleteItemRequest request) 
        {
            Repo.Delete(request.id);
        }

        public Item Post(NewItemRequest request)
        {
            var baseUrl = "http://localhost:1337/items/";
            var itemGuid = Guid.NewGuid().ToString();
            var created = new Item { title = request.title, url = baseUrl + itemGuid, id = itemGuid};
            if(request.order!=null) created.order = (int)request.order;
            Repo.Add(created);
            return created;
        }

        public void Delete(DeleteAllItemsRequest request)
        { 
            Repo.Clear();
        }
    }


}