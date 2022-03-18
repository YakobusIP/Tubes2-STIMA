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

        async Task PutTaskDelay()
        {
            await Task.Delay(200);
        }

        private async void Form2_Load(object sender, EventArgs e)
        {   

            
            //create a viewer object
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            //create a graph object
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
            //set graph back color
            graph.Attr.BackgroundColor = Microsoft.Msagl.Drawing.Color.White;
            //create the graph content
            /*foreach (FileInfo file in Form1.global.answ)
            {
                graph.AddEdge(file.DirectoryName, file.FullName);
            }*/
            for(int i = Form1.global.answ.Count ; i -->0;)
            {
                graph.AddEdge(Form1.global.answ[i].DirectoryName, Form1.global.answ[i].FullName);
            }

            //edit graph name
            foreach (FileInfo file in Form1.global.answ)
            {
                Microsoft.Msagl.Drawing.Node c = graph.FindNode(file.FullName);
                string dirnya = file.FullName;
                string result = Path.GetFileName(dirnya);
                c.LabelText = result;
            }

            foreach (string dir in Form1.global.haveVisited)
            {   
                await PutTaskDelay(); 
                this.SuspendLayout();
                graph.FindNode(dir).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Red;
                this.ResumeLayout();
                viewer.Graph = graph;
                this.Controls.Add(viewer);
                viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            }

            foreach (string file in Form1.global.wayToPath)
            {
                this.SuspendLayout();
                graph.FindNode(file).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Blue;
                this.ResumeLayout();
            }

            
            
            
        }

    }

    

}
