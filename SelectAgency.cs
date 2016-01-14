using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VBQP
{
    public partial class SelectAgency : Form
    {
        string s = "";
        private DialogResult dg;
        private List<TreeNode> lst;
        private int count = 0;
        public SelectAgency()
        {
            InitializeComponent();
        }

        public int SelectedTreeNode
        {
            get
            {
                return count;
            }
            set
            {
            }
        }

        public DialogResult SelectDialogResult
        {
            get
            {
                return this.dg;
            }
        }

        private void SelectAgency_Load(object sender, EventArgs e)
        {
           
        }

        public string showSelectBox()
        {
            try
            {
                lst = new List<TreeNode>();
                using (System.IO.StreamReader reader = new System.IO.StreamReader("Agency.dat"))
                {
                    this.treeView1.Nodes.Add("0", "Cơ quan ban hành");
                    while (!reader.EndOfStream)
                    {
                        string[] si = reader.ReadLine().Split((char)"-"[0]);
                        TreeNode[] n = this.treeView1.Nodes.Find(si[2], true);
                        if (n.Length > 0)
                        {
                           TreeNode ne = n[0].Nodes.Add(si[0], si[1]);
                           lst.Add(ne);
                        }
                    }
                    this.treeView1.Nodes[0].Expand();
                    this.treeView1.AfterCheck += treeView1_AfterCheck;
                    this.treeView1.NodeMouseClick += treeView1_NodeMouseClick;
                }
                this.ShowDialog();
                return this.s;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && e.Clicks == 1)
            {
                if (e.Node.Checked)
                {
                    e.Node.Checked = false;
                    foreach (TreeNode item in e.Node.Nodes)
                    {
                        item.Checked = false;
                    }
                }
                else
                {
                    e.Node.Checked = true;
                    foreach (TreeNode item in e.Node.Nodes)
                    {
                        item.Checked = true;
                    }
                }
            }
        }

        void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode n in e.Node.Nodes)
            {
                n.Checked = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dg = DialogResult.Cancel;
            this.Close();
            s = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dg = DialogResult.OK;
            foreach (var item in lst)
            {
                if (item.Checked == true && item.Nodes.Count ==0)
                {
                    s = s + "," + item.Text;
                    count++;
                }
            }
            s = s.Substring(1, s.Length - 1);
            this.Close();
        }
    }
}
