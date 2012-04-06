using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DCT.Pathfinding;
using DCT.Util;

namespace DCT.UI
{
    public partial class MobsPanel_v2 : UserControl
    {
        internal List<DataGridMob> Mobs
        {
            get
            { 
                List<DataGridMob> ret = new List<DataGridMob>();
                foreach(DataGridViewRow row in dgvMobs.Rows)
                {
                    ret.Add((DataGridMob)row.Cells[1].Value);
                }
                return ret;
            }
        }

        internal List<DataGridMob> CheckedMobs
        {
            get 
            {
                List<DataGridMob> ret = new List<DataGridMob>();
                foreach (DataGridViewRow row in dgvMobs.Rows)
                {
                    if ((bool)row.Cells[0].Value == true)
                    ret.Add((DataGridMob)row.Cells[1].Value);
                }
                return ret;
            }
        }

        /*internal ListView.CheckedIndexCollection CheckedIndices
        {
            get { return lvMobs.CheckedIndices; }
        }*/

        internal DataGridMob FocusedMob
        {
            get { return (DataGridMob)dgvMobs.SelectedRows[0].Cells[1].Value; }
        }

        internal bool MobsEnabled
        {
            get { return btnMobGo.Enabled; }
            set { btnMobGo.Enabled = value; }
        }

        private readonly CoreUI mUI;

        internal MobsPanel_v2(CoreUI ui)
        {
            mUI = ui;
            InitializeComponent();
        }

        public class DataGridMob
        {
            public Boolean IsChecked { get; set; }
            public String Name { get; set; }
            public int Count { get; set; }
            public long Level { get; set; }
            public long RageCost { get; set; }
            public List<int> Ids { get; set; }
            public List<int> Rooms { get; set; }

            public DataGridMob(String _name)
            {
                this.Name = _name;
                Ids = new List<int>();
                Rooms = new List<int>();
            }

            public override string ToString()
            {
 	             return this.Name;
            }

            public override bool Equals(object obj)
            {
                return this.Name.Equals(obj.ToString());
            }

        }

        internal void BuildView()
        {
            List<DataGridMob> Mobs = new List<DataGridMob>();
            foreach (MappedMob mb in Pathfinder.Mobs)
            {
                if (mb != null)
                {
                    
                    int index = Mobs.IndexOf(new DataGridMob(mb.Name));
                    if (index < 0)
                    {
                        DataGridMob dgm = new DataGridMob(mb.Name);
                        dgm.Ids.Add((int)mb.Id);
                        dgm.Rooms.Add(mb.Room);
                        dgm.RageCost = mb.Rage;
                        dgm.IsChecked = false;
                        dgm.Level = mb.Level;
                        dgm.Count = 1;
                        Mobs.Add(dgm);
                    }
                    else
                    {
                        Mobs[index].Count++;
                        Mobs[index].Ids.Add((int)mb.Id);
                        Mobs[index].Rooms.Add(mb.Room);
                    }
                    /*ListViewItem tmp = new ListViewItem(
                            new[]
                                {
                                    mb.Name, mb.Id.ToString(), mb.Room.ToString(),
                                    mb.Level.ToString(), mb.Rage.ToString()
                                });
                    tmp.Name = mb.Name;
                    lvMobs.Items.Add(tmp);*/

                }
            }
            dgvMobs.Rows.Clear();
            foreach (DataGridMob dgo in Mobs)
            {
                dgvMobs.Rows.Add(new object[] { dgo.IsChecked, dgo, dgo.Count, dgo.Level, dgo.RageCost });
            }
        }

        private void btnMobRage_Click(object sender, EventArgs e)
        {
            CalcMobRage();
        }

        internal void CalcMobRage()
        {
            btnMobRage.Enabled = false;
            UpdateMobRage();
            btnMobRage.Enabled = true;
        }

        private void UpdateMobRage()
        {
            int r = 0;
            foreach (DataGridViewRow i in dgvMobs.Rows)
            {
                if((bool)i.Cells[0].Value)
                {
                    r += ((int)i.Cells[2].Value) * ((int)i.Cells[4].Value);
                }
            }
            lblMobRage.Text = "Using rage: " + r;
        }

        private void lnkUncheckMobs_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (DataGridViewRow i in dgvMobs.Rows)
            {
                i.Cells[0].Value = false;
            }
        }

        /*private void lnkMobLoad_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string s = FileIO.LoadFileToString("Import Mobs");
            if (s == null)
            {
                return;
            }
            int i = 0;
            foreach (string l in s.Split(new[] { ',', '\n', '\r', ';', '\t' }))
            {
                i += SelectMobsByName(l);
            }
            MessageBox.Show("Selected " + i + " mobs.", "Loaded Mobs", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }*/

        /*private void lnkMobSave_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            List<string> rs = new List<string>();
            foreach (ListViewItem m in lvMobs.CheckedItems)
            {
                if (!rs.Contains(m.Text))
                {
                    rs.Add(m.Text);
                }
            }
            StringBuilder sb = new StringBuilder();
            foreach (string s in rs)
            {
                sb.Append(s).Append("\n");
            }
            FileIO.SaveFileFromString("Export Mobs List", "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                "DCT mobs.txt", sb.ToString());
        }*/

        private int SelectMobsByName(string name)
        {
            int i = 0;
            foreach (DataGridViewRow row in dgvMobs.Rows)
            {
                if (((string)row.Cells[0].Value).ToLower().Equals(name))
                {
                    row.Cells[0].Value = true;
                    i++;
                }
            }
            return i;
        }

        private void lnkMobsSelect_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int i = 0;
            string input = InputBox.Prompt("Mob Selection", "Check all mobs with the following names (separated by commas):");
            if (string.IsNullOrEmpty(input))
                return;
            foreach (string s in input.Split(new[] { ',' }))
            {
                i += SelectMobsByName(s.Trim());
            }
            MessageBox.Show("Selected " + i + " mobs.", "Select Mobs", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnPotionMobsSelect_Click(object sender, EventArgs e)
        {
            List<string> check = new List<string>();
            switch (cmbPotionMobs.Text.ToLower())
            {
                case "kix":
                    check.Add("Kix Oak Tree");
                    check.Add("Kix Spider");
                    check.Add("Mountain Weeds");
                    break;
                case "kinetic":
                    check.Add("Deadly Ripscale");
                    check.Add("Poison Drake");
                    check.Add("Enraged Centaur");
                    check.Add("Earth Troll");
                    check.Add("Evil Sherpa");
                    if (MessageBox.Show("Do you want to select Entropic Horrors and Belligerent Zombies too (lower drop rates)?", "Select Potion Mobs", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        check.Add("Entropic Horror");
                        check.Add("Belligerent Zombie");
                    }
                    break;
                case "fire":
                    check.Add("Haunter");
                    check.Add("Forgotten Warrior");
                    check.Add("Lost Demon");
                    break;
                case "holy":
                    check.Add("Fallen Angel");
                    check.Add("Apparitional Veteran");
                    check.Add("Rabid Wallabee");
                    break;
                case "shadow":
                    check.Add("Caustic Corpse");
                    check.Add("Infuriated Savage");
                    check.Add("Choleric Ancient");
                    break;
                case "arcane":
                    check.Add("Spectral Warrior");
                    check.Add("Ancient Deserter");
                    check.Add("Bearded Recluse");
                    break;
                default:
                    MessageBox.Show("Choose a preset option.", "Select Potion Mobs", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
            }
            int i = 0;
            foreach (string m in check)
            {
                i += SelectMobsByName(m);
            }
            MessageBox.Show("Selected " + i + " mobs.", "Loaded Mobs", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        /*private int mSortColumn;
        private void lvMobs_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != mSortColumn)
            {
                mSortColumn = e.Column;
                lvMobs.Sorting = SortOrder.Ascending;
            }
            else
            {
                if (lvMobs.Sorting == SortOrder.Ascending)
                    lvMobs.Sorting = SortOrder.Descending;
                else
                    lvMobs.Sorting = SortOrder.Ascending;
            }


            lvMobs.ListViewItemSorter = new MobViewItemComparer(e.Column,
                                                                lvMobs.Sorting);

            dgvMobs.Sort();
        }*/

        private void btnMobGo_Click(object sender, EventArgs e)
        {
            if (dgvMobs.SelectedRows.Count < 1)
            {
                mUI.LogPanel.Log("E: No mob selected.");
                return;
            }
            DataGridMob dgo = (DataGridMob)dgvMobs.SelectedRows[0].Cells[1].Value;
            mUI.LogPanel.Log(string.Format("Moving to {0} in room {1}", dgo.Name, dgo.Rooms[0]));
            mUI.InvokePathfind((int)dgo.Rooms[0]);
        }

        private void lvMobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgvMobs.SelectedRows.Count < 1)
            {
                btnMobGo.Text = "Go to selection";
                return;
            }
            string txt = (string)dgvMobs.SelectedRows[0].Cells[1].Value;
            if (txt.StartsWith("A "))
                txt = txt.Substring(2);
            btnMobGo.Text = string.Format("Go to {0}", txt);
        }
    }
}
