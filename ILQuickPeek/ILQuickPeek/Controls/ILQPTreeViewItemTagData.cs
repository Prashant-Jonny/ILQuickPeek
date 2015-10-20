using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILQuickPeek.Controls
{
    public class ILQPTreeViewItemTagData
    {
        public Guid AssemblyId { get; set; }
        public ILQPTreeViewNodeType NodeType { get; set; }
        public string Name { get; set; }
    }
}
