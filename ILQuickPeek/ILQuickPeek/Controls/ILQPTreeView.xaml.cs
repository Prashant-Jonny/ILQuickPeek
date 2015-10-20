using ILQuickPeek.AssemblyTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for ILQPTreeview.xaml
    /// </summary>
    public partial class ILQPTreeView : UserControl
    {
        public ILQPTreeView()
        {
            InitializeComponent();
            AssemblyStore.OnNewAssemblyAdded += AssemblyStore_OnNewAssemblyAdded;
        }

        #region External Event Handlers
        private void AssemblyStore_OnNewAssemblyAdded(AssemblyTools.EventArgs.NewAssemblyAddedEventArgs args)
        {
            TreeViewItem assemblyNode = BuildTreeViewNodesForAssembly(args);
            assemblyNode.IsExpanded = true;
            NavigationTreeView.Items.Add(assemblyNode);
        }
        #endregion

        #region Internal Event Handlers
        private void Node_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem clickedNode = (TreeViewItem)sender;
            ILQPTreeViewItemTagData nodeTagData = (ILQPTreeViewItemTagData)clickedNode.Tag;

            switch(nodeTagData.NodeType)
            {
                case ILQPTreeViewNodeType.Assembly:
                    break;
                case ILQPTreeViewNodeType.AssemblyReference:
                    break;
                case ILQPTreeViewNodeType.Namespace:
                    break;
                case ILQPTreeViewNodeType.ReferenceGrouping:
                    break;
                case ILQPTreeViewNodeType.Type:
                case ILQPTreeViewNodeType.Type_Friend:
                case ILQPTreeViewNodeType.Type_Private:
                case ILQPTreeViewNodeType.Type_Protected:
                case ILQPTreeViewNodeType.Type_Sealed:

                    break;
            }
        }
        #endregion

        #region Node Building Helpers
        private TreeViewItem BuildTreeViewNodesForAssembly(AssemblyTools.EventArgs.NewAssemblyAddedEventArgs args)
        {
            //build assembly node
            ILQPTreeViewItemTagData tagData = new ILQPTreeViewItemTagData()
            {
                AssemblyId = args.AssemblyId,
                NodeType = ILQPTreeViewNodeType.Assembly
            };
            TreeViewItem assemblyNode = BuildTreeviewItem(args.AssemblyName, tagData);

            assemblyNode.Items.Add(BuildReferencesNodeForAssembly(args.AssemblyId));

            foreach(TreeViewItem namespaceNode in BuildNamespaceNodes(args.AssemblyId))
            {
                assemblyNode.Items.Add(namespaceNode);
            }

            return assemblyNode;
        }

        private TreeViewItem BuildReferencesNodeForAssembly(Guid assemblyId)
        {
            ILQPTreeViewItemTagData tagData = new ILQPTreeViewItemTagData()
            {
                NodeType = ILQPTreeViewNodeType.ReferenceGrouping
            };
            TreeViewItem referencesGroupNode = BuildTreeviewItem("References", tagData);

            AssemblyName[] referencedAssemblies = AssemblyStore.LoadedAssemblies[assemblyId].GetReferencedAssemblies();
            foreach(AssemblyName name in referencedAssemblies)
            {
                ILQPTreeViewItemTagData referenceTagData = new ILQPTreeViewItemTagData()
                {
                    AssemblyId = assemblyId,
                    Name = name.FullName,
                    NodeType = ILQPTreeViewNodeType.AssemblyReference
                };
                TreeViewItem referenceTreeViewItem = BuildTreeviewItem(name.Name, referenceTagData);
                referencesGroupNode.Items.Add(referenceTreeViewItem);
            }

            return referencesGroupNode;
        }

        private List<TreeViewItem> BuildNamespaceNodes(Guid assemblyId)
        {
            List<TreeViewItem> namespaceNodes = new List<TreeViewItem>();

            //Reflect for types
            Type[] assemblyTypes = AssemblyStore.LoadedAssemblies[assemblyId].GetTypes();

            //Find all distinct namespaces
            List<string> namespaces = assemblyTypes.Select(T => T.Namespace).Distinct().ToList();

            //create namespace nodes
            foreach (string namespaceValue in namespaces)
            {
                ILQPTreeViewItemTagData namespaceTagData = new ILQPTreeViewItemTagData()
                {
                    AssemblyId = assemblyId,
                    NodeType = ILQPTreeViewNodeType.Namespace
                };

                TreeViewItem namespaceNode = BuildTreeviewItem(namespaceValue, namespaceTagData);

                //create type nodes per namespace
                foreach (Type type in assemblyTypes.Where(T => T.Namespace.Equals(namespaceValue)))
                {
                    namespaceNode.Items.Add(BuildTypeNode(assemblyId, type));
                }

                namespaceNodes.Add(namespaceNode);
            }

            return namespaceNodes;
        }

        private TreeViewItem BuildTypeNode(Guid assemblyId, Type type)
        {
            TypeAttributes attributes = type.Attributes;
            TypeAttributes visibility = attributes & TypeAttributes.VisibilityMask;

            ILQPTreeViewNodeType nodeType = default(ILQPTreeViewNodeType);
            //TODO: make sure visibility matches the icons
            switch (visibility)
            {
                case TypeAttributes.Public:
                case TypeAttributes.NestedPublic:
                    nodeType = ILQPTreeViewNodeType.Type;
                    break;
                case TypeAttributes.NotPublic:
                case TypeAttributes.NestedPrivate:
                case TypeAttributes.NestedAssembly:
                    nodeType = ILQPTreeViewNodeType.Type_Private;
                    break;
                case TypeAttributes.NestedFamily:
                case TypeAttributes.NestedFamANDAssem:
                    nodeType = ILQPTreeViewNodeType.Type_Protected;
                    break;
                case TypeAttributes.NestedFamORAssem:
                    nodeType = ILQPTreeViewNodeType.Type_Sealed;
                    break;
            }

            ILQPTreeViewItemTagData typeTagData = new ILQPTreeViewItemTagData()
            {
                AssemblyId = assemblyId,
                NodeType = nodeType,
                Name = type.FullName
            };

            TreeViewItem typeNode = BuildTreeviewItem(type.Name, typeTagData);
            return typeNode;
        }

        private TreeViewItem BuildTreeviewItem(string content, ILQPTreeViewItemTagData tagValue)
        {
            TreeViewItem newNode = new TreeViewItem();
            newNode.Tag = tagValue;

            StackPanel itemStackPanel = new StackPanel();
            itemStackPanel.Orientation = Orientation.Horizontal;

            Image nodeImage = new Image();
            switch(tagValue.NodeType)
            {
                case ILQPTreeViewNodeType.Assembly:
                case ILQPTreeViewNodeType.AssemblyReference:
                    nodeImage.Source = (FindResource("AssemblyImage") as Image).Source;
                    break;
                case ILQPTreeViewNodeType.Namespace:
                    nodeImage.Source = (FindResource("NamespaceImage") as Image).Source;
                    break;
                case ILQPTreeViewNodeType.ReferenceGrouping:
                    nodeImage.Source = (FindResource("ReferencesImage") as Image).Source;
                    break;
                case ILQPTreeViewNodeType.Type:
                    nodeImage.Source = (FindResource("TypeDeffImage") as Image).Source;
                    break;
                case ILQPTreeViewNodeType.Type_Friend:
                    nodeImage.Source = (FindResource("TypeDeffFriendImage") as Image).Source;
                    break;
                case ILQPTreeViewNodeType.Type_Private:
                    nodeImage.Source = (FindResource("TypeDeffPrivateImage") as Image).Source;
                    break;
                case ILQPTreeViewNodeType.Type_Protected:
                    nodeImage.Source = (FindResource("TypeDeffProtectedImage") as Image).Source;
                    break;
                case ILQPTreeViewNodeType.Type_Sealed:
                    nodeImage.Source = (FindResource("TypeDeffSealedImage") as Image).Source;
                    break;
            }

            Label nodeLabel = new Label();
            nodeLabel.Content = content;

            itemStackPanel.Children.Add(nodeImage);
            itemStackPanel.Children.Add(nodeLabel);

            newNode.Header = itemStackPanel;

            newNode.MouseDoubleClick += Node_DoubleClick;

            return newNode;
        }
        #endregion
    }
}
