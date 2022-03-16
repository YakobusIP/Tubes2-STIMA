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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static class global
        {
            public static List<FileInfo> answ = new List<FileInfo>();
        }

        private void chooseFolder_Click(object sender, EventArgs e)
        {
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
                    /*Console.WriteLine(infodir.FullName);*/
                    global.answ.Add(infodir);
                    /*Console.WriteLine(infodir.DirectoryName);*/
                    /*answ.Add(infodir.DirectoryName);*/
                    string[] files2 = Directory.GetFiles(dir);
                    foreach (string file in files2)
                    {
                        FileInfo infofile = new FileInfo(file);
                        global.answ.Add(infofile);
                    }
                }

                /*foreach (FileInfo infofile in global.answ)
                {
                    Console.WriteLine(infofile.Directory);
                    Console.WriteLine(infofile.Name);
                }*/
            }
        }

        private void search_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                //bfs
                string file = textBox1.Text;
                Form2 frm = new Form2() { Dock = DockStyle.Fill , TopLevel = false, TopMost = true};
                this.panel1.Controls.Add(frm);
                frm.Show();
            }else if (radioButton2.Checked)
            {
                //dfs
                string file = textBox1.Text;
                Form2 frm = new Form2() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                this.panel1.Controls.Add(frm);
                frm.Show();
            }
            
        }



    }
}
