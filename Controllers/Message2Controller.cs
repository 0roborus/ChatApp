using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ChatApp.Models;
using ChatApp.Data;
using Newtonsoft.Json;

namespace ChatApp.Controllers
{
    // we need a data structure for us to parse the JSON input
    public class MessagePOCO
    {
        public string UserId { get; set;}
        public int ChatId { get; set; }
        public string Text { get; set;}

        public MessagePOCO() { }

        public MessagePOCO(string  _userId, int _chatId, string _text)
        {
            this.UserId = _userId;
            this.ChatId = _chatId;
            this.Text   = _text;   
        }
    }

    [Route("api/message")]
    [ApiController]
    public class Message2Controller : ControllerBase 
    {
        private readonly ChatAppContext _context;

        public Message2Controller(ChatAppContext context) {
            this._context = context;
        }
 
        // POST: api/message/new
        [HttpPost("new")]
        public IEnumerable<Message> Post([FromBody] MessagePOCO value) 
        {
            var messagePOCO = value; 
            var message = new Message
                { UserId = messagePOCO.UserId, 
                  CreatedAt = System.DateTime.Now,
                  Text = messagePOCO.Text,
                  ChatId = messagePOCO.ChatId
                };
            this._context.Message.Add(message);
            this._context.SaveChanges();
            List<Message> messages = this._context.Message
                                .Where(m => m.Id == message.Id)
                                .Select(m => m)
                                .ToList();
            return messages;
        }

        // GET: api/message/all
        [HttpGet("all")] 
        public IEnumerable<Message> Get(){
            List<Message> messages = this._context.Message.Select(m => m).ToList();
            return messages;
        }

        // GET: api/message/get/1
        [HttpGet("get/{id}")]
        public IEnumerable<Message> Get(int id){
            /*
            List<Message> messages = this._context.Message
                .Where(m => m.Id == id).ToList(); 
                */
            /*
            List<Message> messages = this._context.Message
                .Where(m => m.Id == id)
                .Join(_context.Chat, 
                      m => m.ChatId, 
                      c => c.Id,
                      (m,c) => new Message {
                        Id = m.Id,
                        Text = m.Text,
                        UserId = m.UserId,
                        CreatedAt = m.CreatedAt,
                        ChatId = m.ChatId,
                        Chat = c
                      }
                    )  
                .ToList();
                */
                /*
            List<Message> messages = (from m in this._context.Message 
                        join c in this._context.Chat on m.ChatId equals c.Id
                        where m.Id == id select new Message {
                            Id = m.Id,
                            Text = m.Text,
                            UserId = m.UserId,
                            CreatedAt = m.CreatedAt,
                            ChatId = m.ChatId,
                            Chat = c
                        }).ToList();
            */
            List<Message> messages = this._context.Message 
                .Where(m => m.Id == id) 
                .Include(m => m.Chat) 
                .ToList(); 
            return messages;
        }
   }
}
