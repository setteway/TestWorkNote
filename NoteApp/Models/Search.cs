using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoteApp.Models;


namespace NoteApp.Models
{
    public class Search
    {
        public IEnumerable<Note> Notes { get; set; }
        public string Name { get; set; }
    }
}
