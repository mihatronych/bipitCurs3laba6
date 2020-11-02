using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Host;
namespace WcfServiceLibrary
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде и файле конфигурации.
    public class Service1 : IService1
    {
        public DataSet GetData()
        {
            string path = @"workstation id = epiclibrary.mssql.somee.com; packet size = 4096; user id = Mihail12336_SQLLogin_1; pwd = 1edtmfxeen; data source = epiclibrary.mssql.somee.com; persist security info = False; initial catalog = epiclibrary";
            string query = "SELECT Outputs.o_id, Readers.r_id, Readers.r_fio, Cast(Readers.r_dt_birth As VarChar(11)), Readers.r_passport, Books.b_id, Books.b_name, Books.b_author, Cast(Books.b_publ As VarChar(11)), Cast(Books.b_born  As VarChar(11)), Cast(Outputs.o_dt_out As VarChar(11)), Cast(Outputs.o_dt_in As VarChar(11))," +
                "Year(Outputs.o_dt_in)*12*30+Month(Outputs.o_dt_in)*30+Day(Outputs.o_dt_in)-Year(Outputs.o_dt_out)*12*30-Month(Outputs.o_dt_out)*30-Day(Outputs.o_dt_out) FROM Readers, Books, Outputs where Outputs.R_id = Readers.r_id and Outputs.B_id = Books.b_id";
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(path))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(ds, "Readers");
            }
            ds.Tables["Readers"].Columns[0].ColumnName = "ID";
            ds.Tables["Readers"].Columns[1].ColumnName = "ID Читателя";
            ds.Tables["Readers"].Columns[2].ColumnName = "ФИО Читателя";
            ds.Tables["Readers"].Columns[3].ColumnName = "Год рожд. Чит-ля";
            ds.Tables["Readers"].Columns[4].ColumnName = "Пасспорт Чит-ля";
            ds.Tables["Readers"].Columns[5].ColumnName = "ID Кн.";
            ds.Tables["Readers"].Columns[6].ColumnName = "Название Кн.";
            ds.Tables["Readers"].Columns[7].ColumnName = "Автор Кн.";
            ds.Tables["Readers"].Columns[8].ColumnName = "Дата издания";
            ds.Tables["Readers"].Columns[9].ColumnName = "Дата написания";
            ds.Tables["Readers"].Columns[10].ColumnName = "Дата выдачи";
            ds.Tables["Readers"].Columns[11].ColumnName = "Последний срок приема";
            ds.Tables["Readers"].Columns[12].ColumnName = "Дней до просрочки";
            var str = ds.Tables["Readers"].Rows;
            Program.PrintMessage($"[{DateTime.Now.ToLongTimeString()}-{DateTime.Now.ToShortDateString()} Success: Запрос журнала]");
            return ds;
        }

        public DataSet GetDataReaders()
        {
            string path = @"workstation id = epiclibrary.mssql.somee.com; packet size = 4096; user id = Mihail12336_SQLLogin_1; pwd = 1edtmfxeen; data source = epiclibrary.mssql.somee.com; persist security info = False; initial catalog = epiclibrary";
            string query = "SELECT Readers.r_id, Readers.r_fio, Cast(Readers.r_dt_birth As VarChar(11)), Readers.r_passport FROM Readers";
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(path))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(ds, "Readers");
                ds.Tables["Readers"].Columns[0].ColumnName = "ID";
                ds.Tables["Readers"].Columns[1].ColumnName = "ФИО";
                ds.Tables["Readers"].Columns[2].ColumnName = "Дата рождения";
                ds.Tables["Readers"].Columns[3].ColumnName = "Паспорт";
            }
            Program.PrintMessage($"[{DateTime.Now.ToLongTimeString()}-{DateTime.Now.ToShortDateString()} Success: Запрос таблицы читателей]");
            return ds;
        }

        public DataSet GetDataBooks()
        {
            string path = @"workstation id = epiclibrary.mssql.somee.com; packet size = 4096; user id = Mihail12336_SQLLogin_1; pwd = 1edtmfxeen; data source = epiclibrary.mssql.somee.com; persist security info = False; initial catalog = epiclibrary";
            string query = "SELECT Books.b_id, Books.b_name, Books.b_author, Cast(Books.b_publ As VarChar(11)), Cast(Books.b_born As VarChar(11)) FROM Books";
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(path))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(ds, "Books");
                ds.Tables["Books"].Columns[0].ColumnName = "ID";
                ds.Tables["Books"].Columns[1].ColumnName = "Название Кн.";
                ds.Tables["Books"].Columns[2].ColumnName = "Автор Кн.";
                ds.Tables["Books"].Columns[3].ColumnName = "Дата издания";
                ds.Tables["Books"].Columns[4].ColumnName = "Дата написания";
            }
            Program.PrintMessage($"[{DateTime.Now.ToLongTimeString()}-{DateTime.Now.ToShortDateString()} Success: Запрос таблицы книг]");
            return ds;
        }

        public string NewRec(int id, int r_id, int b_id, DateTime o_dt_out, DateTime o_dt_in)
        {
            string path = @"workstation id = epiclibrary.mssql.somee.com; packet size = 4096; user id = Mihail12336_SQLLogin_1; pwd = 1edtmfxeen; data source = epiclibrary.mssql.somee.com; persist security info = False; initial catalog = epiclibrary";
            string query = string.Format("INSERT into Outputs(o_id, R_id, B_id, o_dt_out, o_dt_in) VALUES(@id, @r_id, @b_id, @o_dt_out, @o_dt_in)");
            var str = "";
            using (var conn = new SqlConnection(path))
            {
                using (var cmd = new SqlCommand(query))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@r_id", r_id);
                    cmd.Parameters.AddWithValue("@b_id", b_id);
                    cmd.Parameters.AddWithValue("@o_dt_out", o_dt_out);
                    cmd.Parameters.AddWithValue("@o_dt_in", o_dt_in);
                    conn.Open();
                    str = "Изменено " + cmd.ExecuteNonQuery() + " строк в таблице Outputs\r\n";
                    
                }
            }

            
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(path))
            {
                query = "SELECT Outputs.o_id, Readers.r_id, Readers.r_fio, Cast(Readers.r_dt_birth As VarChar(11)), Readers.r_passport, Books.b_id, Books.b_name, Books.b_author, Cast(Books.b_publ As VarChar(11)), Cast(Books.b_born  As VarChar(11)), Cast(Outputs.o_dt_out As VarChar(11)), Cast(Outputs.o_dt_in As VarChar(11))," +
                "Year(Outputs.o_dt_in)*12*30+Month(Outputs.o_dt_in)*30+Day(Outputs.o_dt_in)-Year(Outputs.o_dt_out)*12*30-Month(Outputs.o_dt_out)*30-Day(Outputs.o_dt_out) FROM Readers, Books, Outputs where Outputs.R_id = Readers.r_id and Outputs.B_id = Books.b_id";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(ds, "Readers");
            }
            str += "Количество записей в таблице: " + ds.Tables["Readers"].Rows.Count;

            Program.PrintMessage($"[{DateTime.Now.ToLongTimeString()}-{DateTime.Now.ToShortDateString()} Success: Добавление новой записи в журнал]");
            return str;
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
