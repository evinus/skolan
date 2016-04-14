using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrädVy
{
    public partial class Form1 : Form
    {
          const int BATALJON = 0, KOMPANI = 1, PLUTON = 2, GRUPP = 3;
          private string[] info = { "Bataljon", "Kompani", "Pluton", "Grupp" };
          Stack<TreeNode> stacken = new Stack<TreeNode>();
          
        public Form1()
        {
            InitializeComponent();
            tvwBataljon.SelectedNode = tvwBataljon.Nodes[0];
        }

        private void btnLäggTill_Click(object sender, EventArgs e)
        {
            TreeNode valdNod = tvwBataljon.SelectedNode;

            if (valdNod != null)
            {
                TreeNode nyNod = new TreeNode(tbxNamn.Text);
                valdNod.Nodes.Add(nyNod);
                stacken.Push(nyNod);
                nyNod.ImageIndex = nyNod.Level;
                nyNod.SelectedImageIndex = nyNod.Level;
                if (nyNod.Level == GRUPP)
                {
                    try
                    {
                        nyNod.Tag = int.Parse(tbxNr.Text);
                    }
                    catch
                    {
                        MessageBox.Show("Glömde antal soldater");
                    }
                }
            }
            tvwBataljon.ExpandAll();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode valdNod = tvwBataljon.SelectedNode;
            tbxInfo.Text = info[valdNod.Level];
            tbxInfo.AppendText("\r\nNamn: " + valdNod.Text);
            try
            {
                tbxInfo.AppendText("\r\nAntal soldater: " + RänkaSoldaternaIn().ToString());
            }
            catch { MessageBox.Show("Gick inte att visa"); }

            if (valdNod.Level == GRUPP) gbxNyEnhet.Enabled = false;
            else gbxNyEnhet.Enabled = true;

            if (valdNod.Level == PLUTON) tbxNr.Enabled = true;
            else tbxNr.Enabled = false;
        }

        private void btnÅngra_Click(object sender, EventArgs e)
        {
            TreeNode n = stacken.Pop();
            n.Remove();
        }

        private void btnTaBort_Click(object sender, EventArgs e)
        {
            TreeNode valdnod = tvwBataljon.SelectedNode;
            valdnod.Remove();
        }
        int RänkaSoldaternaIn()
        {
            int antalSoldater =0;
            TreeNode valdNod = tvwBataljon.SelectedNode;

            for (int i = 0; i < stacken.Count;i++ )
            {
                TreeNode n = stacken.ElementAt(i);

                //if (nod.Nodes[i].Level == GRUPP)
                if (n.Level == GRUPP)
                    antalSoldater = (int) stacken.ElementAt(i).Tag;
                else if ( n.Level == PLUTON )
                {
                    
                    if ( n == valdNod /*|| n.Parent == valdNod || n.Parent.Parent == valdNod*/ )
                    {
                        antalSoldater = 0;
                        foreach (TreeNode child in n.Nodes)
                            if (child.Level == GRUPP)
                            {
                                antalSoldater += (int)child.Tag;
                            }
                    }
                }
                else if(n.Level == KOMPANI)
                {
                    
                    if(n == valdNod)
                    {
                        antalSoldater = 0;
                        foreach(TreeNode pluton in n.Nodes)
                            foreach ( TreeNode grupp in pluton.Nodes )
                            if (grupp.Level == GRUPP)
                            {
                                antalSoldater += (int)grupp.Tag;
                            }
                    }
                }
                else if(n.Level == BATALJON)
                {
                    antalSoldater = 0;
                    if(n == valdNod)
                    {
                        foreach (TreeNode child in n.Nodes)
                            if (child.Level == GRUPP)
                            {
                                antalSoldater += (int)child.Tag;
                            }
                    }
                }

            }
                return antalSoldater;
        }

       int RäknaSoldaternaRec(TreeNode trädnod )
        {
            int antalSoldater = 0;

            if ( trädnod.Level == GRUPP )
            {
                antalSoldater = (int)trädnod.Tag;
            }
            else
            {
                foreach ( TreeNode n in trädnod.Nodes )
                {
                    antalSoldater += RäknaSoldaternaRec(n);
                }
            }


            return antalSoldater; 
        }
    }
}
