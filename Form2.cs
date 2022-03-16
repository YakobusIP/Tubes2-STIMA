using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tubes_2_Stima
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

        }
        private void Form2_Load(object sender, EventArgs e)
        {
            //create a viewer object
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            //create a graph object
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
            //set graph back color
            graph.Attr.BackgroundColor = Microsoft.Msagl.Drawing.Color.Gray;
            //create the graph content
            graph.AddEdge("FolderA", "FolderB");
            graph.AddEdge("FolderB", "FolderC");
            graph.AddEdge("FolderA", "FolderD");
            graph.AddEdge("FolderA", "FolderE");
            graph.FindNode("FolderA").Attr.FillColor = Microsoft.Msagl.Drawing.Color.Red;
            graph.FindNode("FolderB").Attr.FillColor = Microsoft.Msagl.Drawing.Color.Green;
            Microsoft.Msagl.Drawing.Node c = graph.FindNode("FolderC");
            c.Attr.FillColor = Microsoft.Msagl.Drawing.Color.PaleGreen;
            //bind the graph to the viewer
            viewer.Graph = graph;
            //associate the viewer with the form
            this.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Controls.Add(viewer);
            this.ResumeLayout();
        }
    }

    class TreeStructure
    {
        /*static void Main(string[] args)
        {
            // Ini testing bat tree struct si folder, jadi ganti ke directory computer masing2 :D
            // saran, pilih folder yang ga banyak anaknya gt deh
            // tadi pake folder stima yg ada si overdrive, agak shock panjang bgt treenya AHAHAHHA
            string directory = @"C:\Kuliah\(4)Semester4\Stima\A";
            TreeNode root = new TreeNode(directory);

            // bikin tree file
            root = crateTreeOfFiles(directory, root);
            root.displayTree(0);

            // find folder BFS style
            (List<string> path, List<string> haveVisited) = myBFSMethod("siangg.txt", root);

            //print hasil path ketemunya
            Console.WriteLine("file ketemu di");
            foreach (string dir in path)
            {
                Console.WriteLine(dir);
            }

            //print yang udah dikunjungin
            Console.WriteLine("yang udah dikunjungin");
            foreach (string dir in haveVisited)
            {
                Console.WriteLine(dir);
            }
        }*/

        static TreeNode crateTreeOfFiles(string directory, TreeNode root)
        {
            string[] files = Directory.GetFiles(directory);
            string[] directories = Directory.GetDirectories(directory);


            foreach (string file in files)
            {
                root.AddChild(file);
            }


            foreach (string subDirectory in directories)
            {
                // Call the same method on each directory.
                TreeNode rootChild2 = new TreeNode(subDirectory);
                root.AddChildTree(crateTreeOfFiles(subDirectory, rootChild2));
            }

            return root;
        }
        static (List<string> path, List<string> haveVisited) myBFSMethod(string filename, TreeNode root)
        {
            Queue<TreeNode> strQ = new Queue<TreeNode>();
            List<string> haveVisited = new List<string>();
            List<string> path = new List<string>();

            strQ.Enqueue(root);

            // root.children.Count == 0

            while (strQ.Count > 0)
            {
                /*
                Console.WriteLine(strQ.Peek().getFolderName());
                Console.WriteLine(strQ.Count());
                */
                string isiQueue = strQ.Peek().getFolderName();
                string result;
                result = Path.GetFileName(isiQueue);
                if (strQ.Peek().children.Count == 0)
                {
                    if (result == filename)
                    {
                        path.Add(isiQueue);
                        strQ.Clear();
                    }
                    else
                    {
                        haveVisited.Add(strQ.Peek().getFolderName());
                        strQ.Dequeue();
                    }
                }
                else
                {
                    foreach (var child in strQ.Peek().children)
                    {
                        strQ.Enqueue(child);
                    }
                    haveVisited.Add(strQ.Peek().getFolderName());
                    strQ.Dequeue();
                }
            }

            if (path.Count == 0)
            {
                path.Add("not found");
            }


            //onsole.WriteLine(strQ.Peek());

            return (path, haveVisited);
        }
    }

    class TreeNode
    {
        public string folderName;
        public List<TreeNode> children;

        public TreeNode(string folderName)
        {
            this.folderName = folderName;
            this.children = new List<TreeNode>();
        }

        public void AddChild(string folderName)
        {
            this.children.Add(new TreeNode(folderName));
        }

        public void AddChildTree(TreeNode folderName)
        {
            this.children.Add(folderName);
        }

        public TreeNode GetChild(int index)
        {
            return this.children[index];
        }

        public string getFolderName()
        {
            return this.folderName;
        }

        public int childCount()
        {
            return this.children.Count;
        }

        public void displayTree(int level)
        {
            for (int i = 0; i < level; i++)
            {
                Console.Write("\t");
            }
            Console.WriteLine(this.folderName);
            foreach (var child in this.children)
            {
                child.displayTree(level + 1);
            }
        }
    }

}
