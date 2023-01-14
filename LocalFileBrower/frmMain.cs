using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LocalFileBrower
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            Preparing();
        }

        private bool Preparing()
        {
            DriveInfo[] drivers = DriveInfo.GetDrives();

            foreach(DriveInfo d in drivers)
            {
                toolStripComboBox1.Items.Add(d.RootDirectory);
            }

            if(toolStripComboBox1.Items.Count > 0)
            {
                toolStripComboBox1.SelectedIndex = 0;
                toolStripStatusLabel1.Text = drivers.Count().ToString();
            }

            toolStripTextBox1.Enabled = false;
            return true;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout fa = new frmAbout();
            fa.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            toolStripTextBox1.Width = this.Width - 100;
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(toolStripTextBox1.Text);

            DirectoryInfo[] dir = null;
            listView1.Items.Clear();
            listView1.Items.Add(new ListViewItem("..."));

            try
            {
                dir = di.GetDirectories();
                toolStripStatusLabel4.Text = dir.Count().ToString();

                foreach (DirectoryInfo d in dir)
                {
                    ListViewItem lvi = new ListViewItem(d.Name);
                    lvi.SubItems.Add("");
                    lvi.SubItems.Add(d.Extension.ToUpper());

                    listView1.Items.Add(lvi);
                }

            }
            catch
            {
                MessageBox.Show("تعذر عملية العرض");
            }


            FileInfo[] fi = null;
            try
            {
                fi = di.GetFiles();

                foreach (FileInfo f in fi)
                {
                    ListViewItem lvi = new ListViewItem(f.Name);
                    lvi.SubItems.Add(f.Length.ToString());
                    lvi.SubItems.Add(f.Extension.ToUpper());

                    listView1.Items.Add(lvi);
                }

                toolStripStatusLabel6.Text = fi.Count().ToString();


            }
            catch
            {
                MessageBox.Show("تعذر عملية العرض");
            }

         
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = toolStripComboBox1.Text;
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (listView1.SelectedItems[0].Index >0)
                {
                    string path = toolStripTextBox1.Text + "\\" + listView1.SelectedItems[0].Text;

                    if (Directory.Exists(path))
                    {
                        toolStripTextBox1.Text = path;
                    }
                }
                else
                {
                    int len = toolStripTextBox1.Text.LastIndexOf("\\");

                    if(len >=3)
                    {
                        string path = toolStripTextBox1.Text.Substring(0, len);
                        toolStripTextBox1.Text = path;
                    }
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            toolStripTextBox1.Text = path;

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            toolStripTextBox1.Text = path;
        }
    }
}
