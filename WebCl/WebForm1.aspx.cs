using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebCl
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                using (var serv = new ServiceRef.Service1Client())
                {
                    serv.Open();
                    DataSet ds = serv.GetData();
                    var some = ds.Tables["Readers"].Rows;
                    GridView1.DataSource = ds.Tables["Readers"];
                    GridView1.DataBind();
                    DataSet dsReaders = serv.GetDataReaders();
                    DataSet dsBooks = serv.GetDataBooks();
                    if (DropDownList2.Items.Count < 1)
                    {
                        DropDownList2.Items.Clear();
                        DropDownList3.Items.Clear();
                        foreach (DataRow row in dsReaders.Tables["Readers"].Rows)
                        {
                            DropDownList2.Items.Add(string.Join("; ", row.ItemArray));
                        }
                        foreach (DataRow row in dsBooks.Tables["Books"].Rows)
                        {
                            DropDownList3.Items.Add(string.Join("; ", row.ItemArray));
                        }
                        DropDownList2.SelectedIndex = 0;
                        DropDownList3.SelectedIndex = 0;
                    }
                }
                var rows = GridView1.Rows;
                var ids = new int[rows.Count];
                int i = 0;
                foreach (GridViewRow row in rows)
                {
                    ids[i] = int.Parse(row.Cells[0].Text);
                    i++;
                }
                TextBox1.Text = "" + (ids.Max() + 1);
                TextBox2.Text = DateTime.Now.ToShortDateString();
                Calendar1.SelectedDate = DateTime.Now;
                Label1.Text = "";
            }
            catch
            {
                Label1.Text = "Хост был выключен";
            }
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var serv = new ServiceRef.Service1Client())
                {
                    serv.Open();
                    var id = int.Parse(TextBox1.Text);

                    var r_id = int.Parse(DropDownList2.SelectedItem.ToString().Split(';')[0]);
                    var b_id = int.Parse(DropDownList3.SelectedItem.ToString().Split(';')[0]);
                    DateTime dto = DateTime.Now;
                    DateTime dti = DateTime.Now;
                    if (DateTime.TryParse(TextBox3.Text, out dto))
                    {
                        if (dto >= dti)
                        {
                            serv.NewRec(id,
                                r_id,
                                b_id,
                                dti,
                                dto
                                );
                            Label1.Text = "";
                            Response.Redirect("~/WebForm1.aspx");
                            return;
                        }
                        else
                        {
                            Label1.Text = "Дата возврата ошибочна";
                            return;
                        }
                    }
                    else
                    {
                        Label1.Text = "Дата не выбрана";
                    }
                }
                //Form1_Load(sender, e);
            }
            catch
            {
                Label1.Text = "Хост был выключен";
            }
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            TextBox3.Text = Calendar1.SelectedDate.ToShortDateString();
        }
    }
}