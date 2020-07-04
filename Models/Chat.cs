using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
    
namespace ChatApp 
{
    public class Chat
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
        // the owner of the chat
        public string UserId { get; set;}

        public List<Message> Messages {get; set;}
    }
}