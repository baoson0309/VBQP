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

namespace VBQP
{
    public partial class Form1 : Form
    {
        private string currentRequest;
        private List<string> ss;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            init();
            currentRequest = "";
        }

        private void init()
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
            loadProvincesName();
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
            checkBox3.Checked = false;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }
        private void loadProvincesName()
        {
            try
            {
                using (StreamReader reader = new StreamReader("Location.dat"))
                {
                    ss = new List<string>();
                    while (!reader.EndOfStream)
                    {
                        string s = reader.ReadLine().Trim();
                        int j = s.IndexOf("@");
                        comboBox1.Items.Add(s.Substring(j + 1, s.Length - j - 1));
                        ss.Add(s.Substring(0, j));

                    }
                    comboBox1.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw ex;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SelectAgency s = new SelectAgency();
            string si = s.showSelectBox();
            if (s.SelectDialogResult == System.Windows.Forms.DialogResult.OK)
            {
                currentRequest = si;
                linkLabel1.Text = s.SelectedTreeNode.ToString() + " cơ quan đã chọn.";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = "";
            s = "";
            if (radioButton3.Checked)
            {
                s = s + "VBPQFulltext";
            }
            else
            {
                if (radioButton4.Checked)
                {
                    s = s + "Title";
                }
                else
                {
                    s = s + "Title1,Title";
                }
            }
            s = s + "&DivID=resultSearch&IsVietNamese=True&type=1&s=";
            if (radioButton1.Checked)
            {
                s = s + "0&DonVi=@donvi&Keyword=" + System.Web.HttpUtility.UrlEncode(textBox1.Text.Trim().ToLower()) + "&stemp=0";
            }
            else
            {
                s = s + "1&DonVi=@donvi&Keyword=" + System.Web.HttpUtility.UrlEncode(textBox1.Text.Trim().ToLower()) + "&stemp=1";
            }
            string donvi = "," + ss[comboBox1.SelectedIndex];
            if (checkBox1.Checked)
            {
                donvi = "13" + donvi;

            }
            s = s.Replace("@donvi", donvi);
            s = s + "&TimTrong1=VBPQFulltext&TimTrong1=Title1";
            if (checkBox3.Checked && dateTimePicker1.Checked && dateTimePicker2.Checked)
            {
                s = s + "&fromyear=@from&toyear=@to";

                if (dateTimePicker1.Value > dateTimePicker2.Value)
                {
                    MessageBox.Show("Ngày bắt đầu tìm kiếm phải trước ngày kết thúc tìm kiếm.");
                }
                else
                {
                    DateTime t = dateTimePicker1.Value;
                    string st1 = "";
                    if (t.Day.ToString().Length == 1)
                    {
                        st1 = st1 + "0" + t.Day.ToString();
                    }
                    else
                    {
                        st1 = st1 + t.Day.ToString();
                    }
                    if (t.Month.ToString().Length == 1)
                    {
                        st1 = st1 + "/0" + t.Month.ToString();
                    }
                    else
                    {
                        st1 = st1 + "/" + t.Month.ToString();
                    }
                    st1 = st1 + "/" + t.Year.ToString();
                    s = s.Replace("@from", st1);
                    t = dateTimePicker2.Value;
                    st1 = "";
                    if (t.Day.ToString().Length == 1)
                    {
                        st1 = st1 + "0" + t.Day.ToString();
                    }
                    else
                    {
                        st1 = st1 + t.Day.ToString();
                    }
                    if (t.Month.ToString().Length == 1)
                    {
                        st1 = st1 + "/0" + t.Month.ToString();
                    }
                    else
                    {
                        st1 = st1 + "/" + t.Month.ToString();
                    }
                    st1 = st1 + "/" + t.Year.ToString();
                    s = s.Replace("@to", st1);
                }
            }

            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    s = s + "&order=Rank";
                    break;
                case 1:
                    s = s + "&order=VBPQNgayBanHanh";
                    break;
                case 2:
                    s = s + "&order=VBPQNgaycohieuluc";
                    break;
                case 3:
                    s = s + "&order=VBPQNgayHetHieuLuc";
                    break;
                default:
                    break;
            }
            switch (comboBox3.SelectedIndex)
            {
                case 0:
                    s = s + "&TypeOfOrder=False";
                    break;
                case 1:
                    s = s + "&TypeOfOrder=true";
                    break;
                default:
                    break;
            }
            if (checkedListBox1.CheckedItems.Count < 12) 
            {
                string loaivb = "";
                foreach (var item in checkedListBox1.CheckedItems)
                {
                    loaivb = loaivb + "," + item.ToString();
                }
                loaivb = loaivb.Substring(1, loaivb.Length - 1);
                loaivb = System.Web.HttpUtility.UrlEncode(loaivb);
                s = s + "&LoaiVanBan=" + loaivb;
            }
            if (currentRequest != "")
            {
                s = s + "&CoQuanBanHanh" + System.Web.HttpUtility.UrlEncode(currentRequest);
            }
            if (checkedListBox2.CheckedItems.Count > 0)
            {
                string loaivb = "";
                foreach (var item in checkedListBox2.CheckedItems)
                {
                    loaivb = loaivb + "," + item.ToString();
                }
                loaivb = loaivb.Substring(1, loaivb.Length - 1);
                loaivb = System.Web.HttpUtility.UrlEncode(loaivb);
                s = s + "&TrangThaiHieuLuc=" + loaivb;
            }
            s = "http://vbpl.vn/VBQPPL_UserControls/Publishing_22/TimKiem/p_KetQuaTimKiemVanBan.aspx?SearchIn=" + s;
            webBrowser1.Navigate(s);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
            }
            else
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
            }
        }
    }
}
