using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace WindowsFormsApplication24
{
    public partial class Form1 : Form
    {

       
        Form2 Utenti = new Form2();
        Form3 Scaduti = new Form3();
        private SQLiteConnection sql_con;
        Sqlite nuovaa = new Sqlite(@"C:\archivionew.sqlite");
        Class1 a = new Class1();
       

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            if (nominativo.TextLength != 0 && partitaiva.TextLength != 0)
            {

                if (nuovaa.ControllaInserimento("select count(*) from archivio where Partitaiva like '" + partitaiva.Text + "'") == 0)
                {
                    
                    var num = a.CalcolaRestante(dateTimePicker1.Value.ToShortDateString());
                    nuovaa.Comando("INSERT INTO archivio(Nome,Partitaiva,Email,Indirizzo,Telefono,Cellulare,Immissione,Scadenza,Giorni)VALUES('" + nominativo.Text + "','" + partitaiva.Text + "','" + email.Text + "','" + indirizzo.Text + "','" + telefono.Text + "','" + cellulare.Text + "','" + DateTime.Now.ToShortDateString() + "','" + dateTimePicker1.Value.ToShortDateString() + "','" + num.TotalDays + "')");
                    CalcolRestante();
                    dataGridView1.DataSource = nuovaa.ExecuteQuery_DT("select Nome,Partitaiva,Giorni,Restante from archivio where Restante>=0");

                    if (nuovaa.ControllaInserimento("select count(*) from archivio where Restante<=0") != 0)
                        label9.Text = nuovaa.ControllaInserimento("select count(*) from archivio where Restante<=0").ToString();
                    else
                        label9.Text = "Nessun Scaduto";
                    // Lettura dei valori
                    for (int i = 0; i < nuovaa.ExecuteQuery_DT("select Partitaiva from archivio;").Rows.Count; i++)
                    {
                        var _appo = nuovaa.ExecuteQuery_DT("select Partitaiva from archivio;").Rows[i]["Partitaiva"].ToString();
                        piva.AutoCompleteCustomSource.Add(_appo);
                    }
                    for (int i = 0; i < nuovaa.ExecuteQuery_DT("select Nome from archivio;").Rows.Count; i++)
                    {
                        var _appo = nuovaa.ExecuteQuery_DT("select Nome from archivio;").Rows[i]["Nome"].ToString();
                        nome.AutoCompleteCustomSource.Add(_appo);
                    }
                }

                else
                {
                    MessageBox.Show("Nel database è presente un nominativo con la stessa PartitaIva", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Information) ;
                    partitaiva.Clear(); partitaiva.Focus(); 
                }

            }
            else
                MessageBox.Show("Parametri Obbligatori Mancanti!", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }
        private void CalcolRestante()
        {
            for (int i = 0; i < nuovaa.ExecuteQuery_CalcolaResante().Rows.Count; i++)
            {
                var appo = nuovaa.ExecuteQuery_CalcolaResante().Rows[i]["Scadenza"].ToString();
                sql_con = new SQLiteConnection("Data Source=" + @"C:\\archivionew.sqlite" + ";Version=3;New=False;Compress=True;");
                SQLiteConnection prova = new SQLiteConnection(sql_con);
                prova.Open();
                SQLiteCommand comando = new SQLiteCommand(prova);
              
  
                    comando.CommandText = "update archivio set Restante=('" + a.CalcolaRestante(appo).TotalDays + "') where IDarchivio='" + nuovaa.ExecuteQuery_CalcolaResante().Rows[i]["IDarchivio"] + "'";
                    comando.ExecuteNonQuery();
                    prova.Close();
            }
        }
        private void esciToolStripMenuItem_Click(object sender, EventArgs e)
        {
                Application.Exit();  
        }
        private void listaUtentiToolStripMenuItem_Click(object sender, EventArgs e)
        {
        
          Utenti.ShowDialog();
        }
        private void scadutiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nuovaa.ControllaInserimento("select count(*) from archivio where Restante<=0") != 0)
            {
                Scaduti.ShowDialog();
            }
            else
            {
               MessageBox.Show("ATTENZIONE! Non sono presenti Nominativi scaduti nel DATABASE!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Question);
            }
            
        } 
        private void Form1_Load(object sender, EventArgs e)
        {
            CalcolRestante();

            dataGridView1.DataSource = nuovaa.ExecuteQuery_DT("select Nome,Partitaiva,Giorni,Restante from archivio where Restante>=0 ");
            dataGridView1.Refresh();


            for (int i = 0; i < nuovaa.ExecuteQuery_DT("select Partitaiva from archivio;").Rows.Count; i++)
            {
                var _appo = nuovaa.ExecuteQuery_DT("select Partitaiva from archivio;").Rows[i]["Partitaiva"].ToString();
                piva.AutoCompleteCustomSource.Add(_appo);
            }
            for (int i = 0; i < nuovaa.ExecuteQuery_DT("select Nome from archivio;").Rows.Count; i++)
            {
                var _appo = nuovaa.ExecuteQuery_DT("select Nome from archivio;").Rows[i]["Nome"].ToString();
                nome.AutoCompleteCustomSource.Add(_appo);
            }

        
            if (nuovaa.ControllaInserimento("select count(*) from archivio where Restante<=0") != 0)
            {
                label9.Text = nuovaa.ControllaInserimento("select count(*) from archivio where Restante<=0").ToString();
            }
            else
            {
                label9.Text = "Nessun Scaduto";
            }
           
       }
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (nuovaa.ControllaInserimento("select count(*) from archivio where Restante<=0") != 0)
            {
                Scaduti.ShowDialog();
            }
            else
            {
                MessageBox.Show("ATTENZIONE! Non sono presenti Nominativi scaduti nel DATABASE!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
         
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (nome.TextLength != 0)
            {
                var _immissione = DateTime.Now.ToShortDateString().ToString();

                nuovaa.Comando("UPDATE archivio set immissione='" + _immissione + "' where nome='" + nome.Text + "'");
                CalcolRestante();
                if (nuovaa.ControllaInserimento("select count(*) from archivio where Restante<=0") != 0)
                {
                    label9.Text = nuovaa.ControllaInserimento("select count(*) from archivio where Restante<=0").ToString();
                }
                else
                {
                    label9.Text = "Nessun Scaduto";
                }
                nome.Clear(); nome.Focus();
              
            }
            dataGridView1.Refresh();
            dataGridView1.Update();
            dataGridView1.DataSource = nuovaa.ExecuteQuery_DT("select Nome,Partitaiva,Giorni,Restante from archivio where Restante>=0");


                if (piva.TextLength != 0)
                {
                    var immissione = DateTime.Now.ToShortDateString().ToString();

                    nuovaa.Comando("UPDATE archivio set immissione='" + immissione + "' where partitaiva='" + piva.Text + "'");
                    CalcolRestante();
                    if (nuovaa.ControllaInserimento("select count(*) from archivio where Restante<=0") != 0)
                    {
                        label9.Text = nuovaa.ControllaInserimento("select count(*) from archivio where Restante<=0").ToString();
                    }
                    else
                    {
                        label9.Text = "Nessun Scaduto";
                    }
                    piva.Clear(); piva.Focus();
                }
           
                dataGridView1.Refresh();
                dataGridView1.Update();
                dataGridView1.DataSource = nuovaa.ExecuteQuery_DT("select Nome,Partitaiva,Giorni,Restante from archivio where Restante>=0");

          
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (nome.TextLength != 0)
            {
                nuovaa.Comando("DELETE FROM archivio WHERE Nome='" + nome.Text + "'");
                if (nuovaa.ControllaInserimento("select count(*) from archivio where Restante<=0") != 0)
                {
                    label9.Text = nuovaa.ControllaInserimento("select count(*) from archivio where Restante<=0").ToString();
                    nome.Clear(); nome.Focus();
                }
                else
                {
                    label9.Text = "Nessun Scaduto";
                }
                dataGridView1.Refresh();
                dataGridView1.Update();
                dataGridView1.DataSource = nuovaa.ExecuteQuery_DT("select Nome,Partitaiva,Giorni,Restante from archivio where Restante>=0");
            }



            if (piva.TextLength != 0)
            {
                nuovaa.Comando("DELETE FROM archivio WHERE partitaiva='" + piva.Text + "'");
                if (nuovaa.ControllaInserimento("select count(*) from archivio where Restante<=0") != 0)
                {
                    label9.Text = nuovaa.ControllaInserimento("select count(*) from archivio where Restante<=0").ToString();
                    piva.Clear(); piva.Focus();
                }
                else
                {
                    label9.Text = "Nessun Scaduto";
                }
                dataGridView1.Refresh();
                dataGridView1.Update();
                dataGridView1.DataSource = nuovaa.ExecuteQuery_DT("select Nome,Partitaiva,Giorni,Restante from archivio where Restante>=0");
            }
         
        }

        private void emailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            email nuovo = new email();
            nuovo.ShowDialog();
        }

        private void filtroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 nu = new Form2();
            nu.Show();
        }

        private void hoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void filtroToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            filtro nuovo = new filtro();
            nuovo.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult uscita = MessageBox.Show("Vuoi davvero chiudere il programma?", "Chiusura Programma", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (uscita == DialogResult.OK)
                Application.Exit();
            else if (uscita == DialogResult.No)
                e.Cancel = true;

        }

        private void riavviaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

    }
}
