using ILQuickPeek.AssemblyTools.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ILQuickPeek.AssemblyTools
{
    public static class AssemblyStore
    {
        public delegate void NewAssemblyAddedHandler(NewAssemblyAddedEventArgs args);
        public static event NewAssemblyAddedHandler OnNewAssemblyAdded;

        private static Dictionary<Guid, string> _assemblyPaths;
        private static Dictionary<Guid, Assembly> _assemblies;

        public static Dictionary<Guid, Assembly> LoadedAssemblies
        {
            get
            {
                return _assemblies;
            }
        }

        static AssemblyStore()
        {
            _assemblyPaths = new Dictionary<Guid, string>();
            _assemblies = new Dictionary<Guid, Assembly>();
        }

        public static void RegisterNewAssemblyByFileName(string filename)
        {
            Guid assemblyId = Guid.NewGuid();
            Assembly newAssembly = Assembly.LoadFrom(filename);

            _assemblyPaths.Add(assemblyId, filename);
            _assemblies.Add(assemblyId, newAssembly);

            string fileExt = System.IO.Path.GetExtension(filename).ToLower();

            if(OnNewAssemblyAdded != null)
            {
                OnNewAssemblyAdded(new NewAssemblyAddedEventArgs()
                {
                    AssemblyId = assemblyId,
                    AssemblyName = newAssembly.GetName().Name,
                    IsExeFile = fileExt.Equals(".exe")
                });
            }
        }
    }
}
