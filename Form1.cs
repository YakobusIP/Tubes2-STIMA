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



        private void chooseFolder_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                label3.Text = folderBrowserDialog1.SelectedPath;
                
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
