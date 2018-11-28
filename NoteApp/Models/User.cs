using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NoteApp.Models
{
    public class User : IdentityUser
    {
        
       public List<Note> Notes { get; set; } 
    }

    public class Note
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }

        public int UserId { get; set; }


       
        public User User { get; set; }
    }
}