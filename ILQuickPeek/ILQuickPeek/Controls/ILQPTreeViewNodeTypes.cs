using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILQuickPeek.Controls
{
    public enum ILQPTreeViewNodeType
    {
        Assembly,
        AssemblyReference,
        Namespace,
        Type,
        Type_Friend,
        Type_Private,
        Type_Protected,
        Type_Sealed,
        ReferenceGrouping
    }
}
