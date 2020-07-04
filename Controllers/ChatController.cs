using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatApp.Models;
using ChatApp.Data;
using Newtonsoft.Json;

namespace ChatApp.Controllers
{

    // we need a data structure for us to parse the JSON input
    public class ChatPOCO
    {
        public string UserId { get; set;}

        public ChatPOCO() { }

        public ChatPOCO(string  _userId)
        {
            this.UserId = _userId;   
        }
    }

    [Route("api/chat")]
    [ApiController]
    public class ChatController : ControllerBase 
    {
        private readonly ChatAppContext _context;

        public ChatController(ChatAppContext context) {
            this._context = context;
        }
 
        // POST: api/chat/new
        [HttpPost("new")]
        public IEnumerable<Chat> Post([FromBody] ChatPOCO value) 
        {
            var chatPOCO = value; 
            var chat = new Chat
                { UserId = chatPOCO.UserId, 
                  CreatedAt = System.DateTime.Now
                };
            this._context.Chat.Add(chat);
            this._context.SaveChanges();
            List<Chat> chats = this._context.Chat
                                .Where(c => c.Id == chat.Id)
                                .ToList();
            return chats;
        }

        // GET: api/chat/all
        [HttpGet("all")] 
        public IEnumerable<Chat> Get(){
            List<Chat> chats = this._context.Chat.ToList();
            return chats;
        }

        // GET: api/chat/get/1 
        [HttpGet("get/{id}")] 
        public IEnumerable<Chat> Get(int id){ 
            /*
            List<Chat> chats = this._context.Chat
                            .Where(c=>c.Id == id).ToList(); 
                            */
            // Exercise 2
            // Query syntax
            // List<Chat> chats = (from c in _context.Chat where c.Id == id select c).ToList();
            // Exercise 5
            List <Chat> chats = _context.Chat
                        .Where(c => c.Id == id)
                        .Include(c => c.Messages)
                        .ToList();
            return chats; 
        } 
    }
}