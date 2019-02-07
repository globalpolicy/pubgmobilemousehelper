using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUBG_Mouse_Helper
{
    class PresetNotFoundException : Exception
    {
        public PresetNotFoundException(string message) : base(message) { }
        
    }

    class PresetMenuNotPopulatedException : Exception
    {
        public PresetMenuNotPopulatedException(string message) : base(message) { }
    }

}
