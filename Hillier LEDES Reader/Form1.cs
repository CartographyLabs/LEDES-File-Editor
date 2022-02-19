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
using System.Data;

namespace Hillier_LEDES_Reader
{
    public partial class LEDESeditor : Form
    {
        string[] strData;
        string[] strDataBody;

        public LEDESeditor()
        {
            InitializeComponent();
        }

        public DataTable GenerateDataGridView(string[] LEDES)
        {
            DataTable dt = new DataTable();

            dt.Columns.AddRange(new DataColumn[24] { 
                new DataColumn("INVOICE_DATE", typeof(string)),
                new DataColumn("INVOICE_NUMBER", typeof(string)),
                new DataColumn("CLIENT_ID", typeof(string)),
                new DataColumn("LAW_FIRM_MATTER_ID", typeof(string)),
                new DataColumn("INVOICE_TOTAL", typeof(string)),
                new DataColumn("BILLING_START_DATE", typeof(string)),
                new DataColumn("BILLING_END_DATE", typeof(string)),
                new DataColumn("INVOICE_DESCRIPTION", typeof(string)),
                new DataColumn("LINE_ITEM_NUMBER", typeof(string)),
                new DataColumn("EXP/FEE/INV_ADJ_TYPE", typeof(string)),
                new DataColumn("LINE_ITEM_NUMBER_OF_UNITS", typeof(string)),
                new DataColumn("LINE_ITEM_ADJUSTMENT_AMOUNT", typeof(string)),
                new DataColumn("LINE_ITEM_TOTAL", typeof(string)),
                new DataColumn("LINE_ITEM_DATE", typeof(string)),
                new DataColumn("LINE_ITEM_TASK_CODE",typeof(string)),
                new DataColumn("LINE_ITEM_EXPENSE_CODE",typeof(string)),
                new DataColumn("LINE_ITEM_ACTIVITY_CODE",typeof(string)),
                new DataColumn("TIMEKEEPER_ID",typeof(string)),
                new DataColumn("LINE_ITEM_DESCRIPTION",typeof(string)),
                new DataColumn("LAW_FIRM_ID",typeof(string)),
                new DataColumn("LINE_ITEM_UNIT_COST",typeof(string)),
                new DataColumn("TIMEKEEPER_NAME",typeof(string)),
                new DataColumn("TIMEKEEPER_CLASSIFICATION",typeof(string)),
                new DataColumn("CLIENT_MATTER_ID[]",typeof(string))
            });

            string[] j;
            foreach (string i in LEDES)
            {
                j = i.Split('|');
                dt.Rows.Add(j);
            }

            return dt;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectLEDES = new OpenFileDialog();
            selectLEDES.InitialDirectory = @"C:\";

            selectLEDES.Filter = "LED files (*.LED)|*.LED|Text Files (*.txt)|*.txt";

            if (selectLEDES.ShowDialog() == DialogResult.OK)
            {
                strData = File.ReadAllLines(selectLEDES.FileName);
                strDataBody = strData.Skip(2).ToArray();
            }

            DataTable dgvData = GenerateDataGridView(strDataBody);
            dataGridView1.DataSource = dgvData;
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            LEDESoutput.Text = "LEDES1998B[]\r\n";

            int cnt = dataGridView1.Columns.Count;
            for (int i = 1; i <= cnt; i++)
            {
                int j = i - 1;
                if (i < cnt)
                {
                    LEDESoutput.Text += dataGridView1.Columns[j].Name + "|";
                }
                else
                {
                    LEDESoutput.Text += dataGridView1.Columns[j].Name;
                }
            }
            LEDESoutput.Text += "\r\n";
            int cntRow = dataGridView1.Rows.Count;
            for (int i = 1; i < cntRow; i++)
            {
                int cntCell = dataGridView1.Rows[i - 1].Cells.Count;
                for (int j = 1; j <= cntCell; j++)
                {
                    int k = j - 1;
                    if (j < cntCell)
                    {
                        LEDESoutput.Text += dataGridView1.Rows[i - 1].Cells[k].Value + "|";
                    }
                    else
                    {
                        LEDESoutput.Text += dataGridView1.Rows[i - 1].Cells[k].Value;
                    }
                }
                LEDESoutput.Text += "\r\n";
            }
            /*foreach (DataGridViewRow i in dataGridView1.Rows)
            {
                int cRow = 0;
                foreach (DataGridViewCell j in i.Cells)
                {
                    var cell = i.Cells[cRow].Value;
                    int cCell = 1;
                    if (cell != null)
                    {
                        if (cCell < i.Cells.Count-10)
                        {
                            LEDESoutput.Text += cell.ToString() + "|";
                        }
                        else
                       {
                            LEDESoutput.Text += cell.ToString();
                        }
                        cCell++;
                    }
                    cRow++;
                }
                LEDESoutput.Text += "\r\n";
            }*/
            tc1.SelectedTab = tabPage2;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            SaveFileDialog writeLEDES = new SaveFileDialog();
            writeLEDES.InitialDirectory = @"C:\";
            writeLEDES.Filter = "LEDES 98B file (*.LED)|*.LED|Text File (*.txt)|*.txt";

            if (writeLEDES.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(writeLEDES.FileName, LEDESoutput.Text);
            }
        }
    }
}
