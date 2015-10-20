using ILQuickPeek.AssemblyTools;
using ILQuickPeek.UserActivityStore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ILQuickPeek.Controls
{
    /// <summary>
    /// Interaction logic for ILQPToolbar.xaml
    /// </summary>
    public partial class ILQPToolbar : UserControl
    {
        public ILQPToolbar()
        {
            InitializeComponent();
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Compiled .NET Files(*.exe;*.dll)|*.exe;*.dll|All Files (*.*)|*.*";
            fileDialog.Multiselect = true;

            if(UserActivityManger.History.OpenFileDialogLastPath.LastValue != default(string))
            {
                fileDialog.InitialDirectory = UserActivityManger.History.OpenFileDialogLastPath.LastValue;
            }
            else
            {
                fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            }
            
            if(fileDialog.ShowDialog() == true)
            {
                foreach(string filename in fileDialog.FileNames)
                {
                    AssemblyStore.RegisterNewAssemblyByFileName(filename);
                }


                UserActivityManger.History.OpenFileDialogLastPath.LastValue = System.IO.Path.GetDirectoryName(fileDialog.FileNames.First());
            }
        }
    }
}
