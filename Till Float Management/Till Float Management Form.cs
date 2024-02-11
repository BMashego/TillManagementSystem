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
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Till_Float_Management
{
    public partial class TillManagement : Form
    {
        public TillManagement()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            string fileExt = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            int endTillMoney = 0;
            int tillAmount = 500; 

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                fileExt = Path.GetExtension(filePath);
                if(fileExt.CompareTo(".txt") == 0)
                {
                    try
                    {
                        StreamReader streamReader = new StreamReader(filePath);
                        StringBuilder stringBuilder = new StringBuilder();
                        string line = "";
                        stringBuilder.Append("Till Start, Transaction Total, Paid, Change Total, Change Breakdown");
                        stringBuilder.Append(Environment.NewLine);
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            List<string> listCal = new List<string>();
                            List<string> totalPaid = new List<string>();
                            int itemsAmount = 0;
                            int amountPaid = 0;
                            int change = 0;
                            string breakdown = "";
                            string breakLine = "";

                            var tillMoney = new[] {
                                new { name = "R50", rands = 5 },
                                new { name = "R20", rands = 5 },
                                new { name = "R10", rands = 6 },
                                new { name = "R5", rands = 12 },
                                new { name = "R2", rands = 10 },
                                new { name = "R1", rands = 10 }
                            };

                            string[] paid = line.Split(',');
                            string[] paid1 = paid[1].Split('-');

                            string[] toCal = paid[0].Split(';');

                            foreach (string item in paid1)
                            {
                                int indexofR = item.IndexOf('R');
                                string addToCal = item.Remove(0, indexofR + 1);

                                totalPaid.Add(addToCal);
                            }
                            amountPaid = totalPaid.Sum(b => Convert.ToInt32(b));

                            foreach (string line2 in toCal)
                            {
                                int indexofR = line2.IndexOf('R');
                                string addToCal = line2.Remove(0, indexofR+1);
                                
                                listCal.Add(addToCal);
                            }

                            itemsAmount = listCal.Sum(b => Convert.ToInt32(b));
                            change = amountPaid - itemsAmount;

                            CalChange calChange = new CalChange(change);

                            breakdown = BreakdownOfChange(calChange);

                            if (breakdown.EndsWith("-"))
                                breakLine = breakdown.Remove(breakdown.Length - 1);
                            else
                            breakLine = breakdown;

                            if (endTillMoney != 0)
                            {
                                tillAmount = tillAmount - itemsAmount + amountPaid;
                            }
                            endTillMoney++;

                            stringBuilder.Append(tillAmount);
                            stringBuilder.Append(" , ");
                            stringBuilder.Append(itemsAmount);
                            stringBuilder.Append(" , ");
                            stringBuilder.Append(amountPaid);
                            stringBuilder.Append(" , ");
                            stringBuilder.Append(change);
                            stringBuilder.Append(" , ");
                            stringBuilder.Append(breakLine);
                            stringBuilder.Append(Environment.NewLine);
                        }
                        streamReader.Close();
                        richTextBox1.Text = stringBuilder.ToString();
                    }
                    catch (Exception ex)
                    {
                        richTextBox1.Text = ex.Message;
                    }
                }
            }
        }

        public string BreakdownOfChange(CalChange calChange)
        {
            string breakdown = "";

            for (int b = 0; b < calChange.FiftyRands; b++)
            {
                if (calChange.FiftyRands != 0)
                {
                    breakdown += "R50";
                }
                if (breakdown != "")
                    breakdown += "-";
            }
            for (int b = 0; b < calChange.TwentyRands; b++)
            {
                if (calChange.TwentyRands != 0)
                {
                    breakdown += "R20";
                }
                if (breakdown != "")
                    breakdown += "-";
            }
            for (int b = 0; b < calChange.TenRands; b++)
            {
                if (calChange.TenRands != 0)
                {
                    breakdown += "R10";
                }
                if (breakdown != "")
                    breakdown += "-";
            }
            for (int b = 0; b < calChange.FiveRands; b++)
            {
                if (calChange.FiveRands != 0)
                {
                    breakdown += "R5";
                }
                if (breakdown != "")
                    breakdown += "-";
            }
            for (int b = 0; b < calChange.TwoRands; b++)
            {
                if (calChange.TwoRands != 0)
                {
                    breakdown += "R2";
                }
                if (breakdown != "")
                    breakdown += "-";
            }
            for (int b = 0; b < calChange.OneRands; b++)
            {
                if (calChange.OneRands != 0)
                {
                    breakdown += "R1";
                }
                if (breakdown != "")
                    breakdown += "-";
            }

            return breakdown;
        }
    }
}
