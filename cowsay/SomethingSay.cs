using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cowsay
{
    interface SomethingSay
    {
        string thisSays(string message);
        string thisObject();
    }
}
