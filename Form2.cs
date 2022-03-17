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
            graph.Attr.BackgroundColor = Microsoft.Msagl.Drawing.Color.White;
            //create the graph content
            foreach (FileInfo file in Form1.global.answ)
            {
                /*Console.WriteLine(file.DirectoryName);
                Console.WriteLine(file.FullName);*/
                graph.AddEdge(file.DirectoryName,file.FullName);
            }


            foreach (string dir in Form1.global.haveVisited)
            {
                graph.FindNode(dir).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Red;
            }

            foreach (string file in Form1.global.wayToPath)
            {
                graph.FindNode(file).Attr.FillColor= Microsoft.Msagl.Drawing.Color.Blue;
            }
            /*graph.AddEdge("FolderA", "FolderB");
            graph.AddEdge("FolderB", "FolderC");
            graph.AddEdge("FolderA", "FolderD");
            graph.AddEdge("FolderA", "FolderE");
            graph.FindNode("FolderA").Attr.FillColor = Microsoft.Msagl.Drawing.Color.Red;
            graph.FindNode("FolderB").Attr.FillColor = Microsoft.Msagl.Drawing.Color.Green;
            Microsoft.Msagl.Drawing.Node c = graph.FindNode("FolderC");
            c.Attr.FillColor = Microsoft.Msagl.Drawing.Color.PaleGreen;*/
            //bind the graph to the viewer
            viewer.Graph = graph;
            //associate the viewer with the form
            this.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Controls.Add(viewer);
            this.ResumeLayout();
        }

    }

    

}
