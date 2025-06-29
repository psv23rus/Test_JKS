using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Test_JKS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            SqlQuery_TotalCheque sqlproc = new SqlQuery_TotalCheque();

            int selectedState = comboBox1.SelectedIndex;

            DataRow row = ((DataTable)comboBox1.DataSource).Rows[comboBox1.SelectedIndex];

            int Combobox1_Id = (int)row["Client_Id"];
            string Combobox1_Name = (string)row["Client_Name"];


            Task<DataTable> res = sqlproc.GetCheque(Combobox1_Id);

            gridControl_TotCheque.DataSource = await res;
        }
        private async void button2_Click(object sender, EventArgs e)
        {
            SqlQuery_ItemInStore sqlproc= new SqlQuery_ItemInStore();

            int selectedState = comboBox2.SelectedIndex;

          DataRow row = ((DataTable)comboBox2.DataSource).Rows[comboBox2.SelectedIndex];

            int Combobox2_Id = (int)row["Item_Id"];
            string Combobox2_Name = (string)row["Item_Name"];


            Task<DataTable> res1 = sqlproc.GetItemInStore(Combobox2_Id);

            gridControl_TotCheque.DataSource = await res1;
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.DropDown += new EventHandler(ComboBox1_DropDown);
            comboBox2.DropDown += new EventHandler(ComboBox2_DropDown);
        }

        private async void ComboBox1_DropDown(object sender, EventArgs e)
        {

            SqlQuery_GetClientsColumnValues sqlproc1 = new SqlQuery_GetClientsColumnValues();

            Task<DataTable> resname = sqlproc1.GetColumnValues("Name");

            DataTable dt = await resname;

        //                gridControl_TotCheque.DataSource = dtb;
//                        MessageBox.Show($"Это тип данных {dt.GetType()} ");
              if (dt != null)
            {
                try
                {
                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "Client_Name";
                    comboBox1.ValueMember = "Client_Id";

                    comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка SQL при заполнении {comboBox1}:\r\n {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private async void ComboBox2_DropDown(object sender, EventArgs e)
        {

            SqlQuery_GetItemsColumnValues sqlproc = new SqlQuery_GetItemsColumnValues();

            Task<DataTable> resitem = sqlproc.GetItemsColumnValues("Name");

            DataTable dti = await resitem;

            //                gridControl_TotCheque.DataSource = dtb;
            //                        MessageBox.Show($"Это тип данных {dt.GetType()} ");
            if (dti != null)
            {
                try
                {
                    comboBox2.DataSource = dti;
                    comboBox2.DisplayMember = "Item_name";
                    comboBox2.ValueMember = "Item_id";

                    comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка SQL при заполнении {comboBox2}:\r\n {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            //          MessageBox.Show($"Вы выбрали {Combobox1_Id} ");
            //          String Id = comboBox1.SelectedValue.ToString();
            //          String Id = comboBox1.SelectedValue.ToString();
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            //          MessageBox.Show($"Вы выбрали {Combobox1_Id} ");
            //          String Id = comboBox1.SelectedValue.ToString();
            //          String Id = comboBox1.SelectedValue.ToString();
        }

        private void gridControl_TotCheque_Click(object sender, EventArgs e)
        {

        }
    }
}
