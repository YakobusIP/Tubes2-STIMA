using System;
using System.Diagnostics;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static class global
        {
            public static List<FileInfo> answ = new List<FileInfo>();
            public static List<string> path = new List<string>();
            public static List<string> haveVisited = new List<string>();
            public static List<string> wayToPath = new List<string>();
            public static Boolean BFSSearchh = true;
            public static string rootFolder;
        }

        private void chooseFolder_Click(object sender, EventArgs e)
        {   
            global.answ.Clear();
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                label3.Text = folderBrowserDialog1.SelectedPath;
                string[] files = Directory.GetFiles(folderBrowserDialog1.SelectedPath);
                string[] directory = Directory.GetDirectories(folderBrowserDialog1.SelectedPath,"*.*",SearchOption.AllDirectories);

                foreach(string file in files)
                {   FileInfo fileInfo = new FileInfo(file);
                    global.answ.Add(fileInfo);
                }

                foreach(string dir in directory)
                {
                    FileInfo infodir = new FileInfo(dir);
                    
                    global.answ.Add(infodir);
                    
                    string[] files2 = Directory.GetFiles(dir);
                    foreach (string file in files2)
                    {
                        FileInfo infofile = new FileInfo(file);
                        global.answ.Add(infofile);
                    }
                }

                
            }
        }

        private void search_Click(object sender, EventArgs e)
        {   
            panel1.Controls.Clear();
            comboBox1.Items.Clear();
            var watch = Stopwatch.StartNew();
            string file = textBox1.Text;
            TreeNode root = new TreeNode(folderBrowserDialog1.SelectedPath);
            root = TreeStructure.createTreeOfFiles(folderBrowserDialog1.SelectedPath, root);
            global.rootFolder = folderBrowserDialog1.SelectedPath;
            if (radioButton1.Checked)
            {
                //bfs 
                global.BFSSearchh = true;
                (global.path,global.haveVisited,global.wayToPath) = BFSDFS.BFSSearch(file,root,checkBox1.Checked);
                foreach (string fil in global.path)
                {
                    comboBox1.Items.Add(fil);
                }
                Form2 frm = new Form2() { Dock = DockStyle.Fill , TopLevel = false, TopMost = true};
                this.panel1.Controls.Add(frm);
                frm.Show();
            }else if (radioButton2.Checked)
            {
                //dfs
                global.BFSSearchh = false;
                List<string> visitedDirectory = new List<string>();
                List<string> pathIn = new List<string>();
                (global.path, global.haveVisited) = BFSDFS.DFSSearch(file, root, pathIn, visitedDirectory, checkBox1.Checked);
                global.wayToPath = BFSDFS.BreakPath(global.path, root.getFolderName());
                foreach (string fil in global.path)
                {
                    comboBox1.Items.Add(fil);
                }
                Form2 frm = new Form2() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                this.panel1.Controls.Add(frm);
                frm.Show();
            }
            watch.Stop();
            long elapsedMs = watch.ElapsedMilliseconds;
            string time = elapsedMs.ToString();
            durationValue.Text = time + " ms";
        }

        private void goToFile_Click(object sender, EventArgs e)
        {
            FileInfo parent = new FileInfo(comboBox1.SelectedItem.ToString());
            string link = parent.DirectoryName.ToString();
            Console.WriteLine(link);
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = link,
                FileName = "explorer.exe"
            };
            Process.Start(startInfo);
        }
    }
}
