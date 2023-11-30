using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMade.Core.ViewModels
{
    public class TwilioResult
    {
        public TwilioResult(string sid)
        {
            Sid = sid;
            IsValid = true;
        }

        public TwilioResult(List<string> errors)
        {
            Errors = errors;
            IsValid = false;
        }

        public bool IsValid { get; set; }

        public string Sid { get; set; }

        public List<string> Errors { get; set; }
    }
}
