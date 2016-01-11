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
        public SelectAgency()
        {
            InitializeComponent();
        }

        public int SelectedTreeNode
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public DialogResult DialogResult
        {
            get
            {
                return this.dg;
            }
        }

        private void SelectAgency_Load(object sender, EventArgs e)
        {
            lst = new List<TreeNode>();
            showSelectBox();
            
        }

        public void showSelectBox()
        {
            try
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader("Agency.dat"))
                {
                    this.treeView1.Nodes.Add("0", "Cơ quan ban hành");
                    while (!reader.EndOfStream)
                    {
                        string[] s = reader.ReadLine().Split((char)"-"[0]);
                        TreeNode[] n = this.treeView1.Nodes.Find(s[2], true);
                        if (n.Length > 0)
                        {
                           TreeNode ne = n[0].Nodes.Add(s[0], s[1]);
                           lst.Add(ne);
                        }
                    }
                    this.treeView1.Nodes[0].Expand();
                    this.treeView1.AfterCheck += treeView1_AfterCheck;
                    this.treeView1.NodeMouseClick += treeView1_NodeMouseClick;
                }
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
            s = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dg = DialogResult.OK;
            foreach (var item in lst)
            {
                if (item.)
                {
                    
                }
            }
        }
    }
}
