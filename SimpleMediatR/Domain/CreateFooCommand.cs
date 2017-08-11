using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CreateFooCommand : ICommand
    {
        public string Data { get; set; }
        public DateTime IssuedAt { get; set; }
    }
}
