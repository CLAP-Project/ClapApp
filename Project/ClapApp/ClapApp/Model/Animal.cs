using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClapApp.Model
{
    public enum Status {
        Perdido,
        Ok
    }

    public class Animal
    {
        public string Nome { get; set; }
        public Status Status { get; set; }

        public string StatusString
        {
            get { return "Status: " + (Status == Status.Perdido ? "Perdido!" : "Ok"); }
        }
    }
}
