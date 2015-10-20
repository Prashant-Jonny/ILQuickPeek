using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILQuickPeek.AssemblyTools.EventArgs
{
    public class NewAssemblyAddedEventArgs
    {
        public Guid AssemblyId { get; set; }
        public string AssemblyName { get; set; }
        public bool IsExeFile { get; set; }
    }
}
