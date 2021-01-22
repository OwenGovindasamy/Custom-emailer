using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mailer.Models;

namespace Mailer.ViewModels
{
    public class ManualSendViewModel
    {
        public CoreElements CoreElements { get; set; }


        public string ApiAction { get; set; }
    }
}