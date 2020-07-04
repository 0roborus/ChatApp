using System;
using System.ComponentModel.DataAnnotations;
    
namespace ChatApp 
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
        
        public string UserId {get; set;}

        public int ChatId { get; set; }
        public Chat Chat { get; set;}
    }
}