using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            using (var serv = new ServiceReference.Service1Client())
            {
                serv.Open();
                DataSet ds = serv.GetData();
                var some = ds.Tables["Readers"].Rows;
                dataGridView1.DataSource = ds.Tables["Readers"];
                DataSet dsReaders = serv.GetDataReaders();
                DataSet dsBooks = serv.GetDataBooks();
                foreach (DataRow row in dsReaders.Tables["Readers"].Rows)
                { 
                    comboBox1.Items.Add(string.Join("; ", row.ItemArray));
                }
                foreach (DataRow row in dsBooks.Tables["Books"].Rows)
                {
                    comboBox2.Items.Add(string.Join("; ", row.ItemArray));
                }
            }
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            var rows = dataGridView1.Rows;
            var ids = new int[rows.Count];
            int i = 0;
            foreach (DataGridViewRow row in rows)
            {
                ids[i] = (int)row.Cells[0].Value;
                i++;
            }
            textBox1.Text = "" + (ids.Max() + 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var serv = new ServiceReference.Service1Client())
                {

                    serv.Open();
                    var id = int.Parse(textBox1.Text);
                    var r_id = int.Parse(comboBox1.SelectedItem.ToString().Split(';')[0]);
                    var b_id = int.Parse(comboBox2.SelectedItem.ToString().Split(';')[0]);
                    DateTime dto = dateTimePicker1.Value;
                    DateTime dti = dateTimePicker2.Value;
                    serv.NewRec(id,
                        r_id,
                        b_id,
                        dti,
                        dto);
                }
                Form1_Load(sender, e);
            }
            catch
            {
                MessageBox.Show("Хост был выключен");
            }
        }
    }
}
