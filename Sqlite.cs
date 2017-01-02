using System;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsApplication24
{
    class Sqlite
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private string cnnStr;

        public Sqlite(string _path) {  cnnStr = _path; }
        public Sqlite() { }
        public void setConnection()
        {
            sql_con = new SQLiteConnection("Data Source=" + cnnStr + ";Version=3;New=False;Compress=True;");
        }
        public DataTable ExecuteQuery_DT()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                if (sql_con == null) { setConnection(); }
                sql_con.Open();
                sql_cmd = sql_con.CreateCommand();
                string CommandText = "select Nome from archivio;";
                DB = new SQLiteDataAdapter(CommandText, sql_con);
                DB.Fill(ds);
                dt = ds.Tables[0];
                sql_con.Close();

                return dt;
            }
            catch
            {
                return null;
            }
        }
        public DataTable ExecuteQuery_CalcolaResante()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                if (sql_con == null) { setConnection(); }
                sql_con.Open();
                sql_cmd = sql_con.CreateCommand();
                string CommandText = "select IDarchivio,Scadenza from archivio;";
                DB = new SQLiteDataAdapter(CommandText, sql_con);
                DB.Fill(ds);
                dt = ds.Tables[0];
                sql_con.Close();

                return dt;
            }
            catch
            {
                return null;
            }
        }
        public DataTable ExecuteQuery_DT(string query)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                if (sql_con == null) { setConnection(); }
                sql_con.Open();
                sql_cmd = sql_con.CreateCommand();
                string CommandText = query;
                DB = new SQLiteDataAdapter(CommandText, sql_con);
                DB.Fill(ds);
                dt = ds.Tables[0];
                sql_con.Close();

                return dt;
            }
            catch
            {
                return null;
            }
        }
        public DataTable ExecuteQuery_All()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                if (sql_con == null) { setConnection(); }
                sql_con.Open();
                sql_cmd = sql_con.CreateCommand();
                string CommandText = "select *from archivio";
                DB = new SQLiteDataAdapter(CommandText, sql_con);
                DB.Fill(ds);
                dt = ds.Tables[0];
                sql_con.Close();

                return dt;
            }
            catch
            {
                return null;
            }
        }
        public DataTable ExecuteQuery_Email()
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                if (sql_con == null) { setConnection(); }
                sql_con.Open();
                sql_cmd = sql_con.CreateCommand();
                string CommandText = "SELECT Email from archivio where Email>0;";
                DB = new SQLiteDataAdapter(CommandText, sql_con);
                DB.Fill(ds);
                dt = ds.Tables[0];
                sql_con.Close();

                return dt;
            }
            catch
            {
                return null;
            }
        }
        public void Comando(string _query)
        {
            try
            {
                if (sql_con == null) { setConnection(); }
                sql_con.Open();
                sql_cmd = sql_con.CreateCommand();
                sql_cmd.CommandText = _query;
                sql_cmd.ExecuteNonQuery();
                sql_con.Close();
            }
            catch
            {
                MessageBox.Show("Something goes wrong executing SQL-Query. Please, restart application.", "WARNING", MessageBoxButtons.OK);
            }
        }
        public void ProvaInserimento(string arg1, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7, string arg8,string arg9)
        {
            try
            {
                if (sql_con == null) { setConnection(); }
                sql_con.Open();
                sql_cmd = sql_con.CreateCommand();
                sql_cmd.CommandText = "INSERT INTO archivio(Nome,Partitaiva,Email,Indirizzo,Telefono,Cellulare,Immissione,Scadenza,Giorni)VALUES('" + arg1 + "','" + arg2 + "','" + arg3 + "','" + arg4 + "','" + arg5 + "','" + arg6 + "','" + arg7 + "','" + arg8 + "','" + arg9 + "');";
                sql_cmd.ExecuteNonQuery();
                sql_con.Close();
            }
            catch
            {
                MessageBox.Show("Something goes wrong executing SQL-Query. Please, restart application.", "WARNING", MessageBoxButtons.OK);
            }
        }
        public void Restante(double arg1,int i)
        {
            try
            {
                if (sql_con == null) { setConnection(); }
                sql_con.Open();
                sql_cmd = sql_con.CreateCommand();
                sql_cmd.CommandText = "UPDATE archivio set Restante=('" + arg1 + "') where IDarchivio='" +ExecuteQuery_CalcolaResante().Rows[i]["IDarchivio"] + "'"; 
                sql_cmd.ExecuteNonQuery();
                sql_con.Close();
            }
            catch
            {
                MessageBox.Show("Something goes wrong executing SQL-Query. Please, restart application.", "WARNING", MessageBoxButtons.OK);
            }
            /*
              int numero_righe = 0;

              for (int i = 0; i < dbViewer.Rows.Count; i++)
              {
                  numero_righe++;
              }
             MessageBox.Show("Record(Utente) nel database: " + numero_righe.ToString());
             * */

        }
        public int ControllaInserimento(string query)
        {

            SQLiteConnection prova = new SQLiteConnection(sql_con);
            prova.Open();
            SQLiteCommand comando = new SQLiteCommand(prova);
            comando.CommandText = query;
            var count = Convert.ToInt32(comando.ExecuteScalar());


            return count;


        }



      
    }
}
