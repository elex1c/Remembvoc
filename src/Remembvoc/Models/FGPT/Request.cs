using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remembvoc.Models.FGPT
{
    public class Request
    {
        public string model { get; set; }
        public Message[] messages { get; set; }
        public int max_tokens { get; set; }
        public bool stream { get; set; }
    }
}
