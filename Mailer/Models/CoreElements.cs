using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Mailer.Models
{
    public class CoreElements
    {
        [Key]
        public Int64 Id { get; set; }
        public DateTime Time { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Int64 Cell { get; set; }
        public string Description { get; set; }
        public bool EmailSent { get; set; }
        public string Campaign { get; set; }
        public DateTime DateSent { get; set; } = DateTime.Now;

    }
}