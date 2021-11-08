using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrikusApp.Models
{
    public class MessageModel
    {
        public string Name { get; set; }
        public MarkupString Message { get; set; }
        public string Url { get; set; }
    }
}
