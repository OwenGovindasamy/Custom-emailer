using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Mailer.Dto
{
    public class CustomerDto
    {
        public Int64 Id { get; set; }
        public DateTime Time { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Int64 Cell { get; set; }
        public string Description { get; set; }
        public bool EmailSent { get; set; }
        public string Campaign { get; set; }
    }
}