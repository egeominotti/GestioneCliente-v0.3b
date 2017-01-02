using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.IO;


namespace WindowsFormsApplication24
{
    public partial class email : Form
    {
        public string _nome; public string _partitaiva;
        Sqlite nuovo = new Sqlite(@"C:\\archivionew.sqlite");
       

        public email()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ImportaValori_Manuale();
        }
        private bool Invio_email()
        {
            bool fatto = false;
            try
            {
                MailMessage messaggio = new MailMessage();
                messaggio.From = new MailAddress(mittente.Text); 
                messaggio.Subject = oggetto.Text;
                messaggio.Body = corpo.Text;
                if (textBox1.TextLength != 0)
                messaggio.Attachments.Add(new Attachment(textBox1.Text));
                if(textBox2.TextLength!=0)
                messaggio.Attachments.Add(new Attachment(textBox2.Text));
                foreach (string s in checkedListBox1destinatari.CheckedItems)
                {
                    messaggio.To.Add(s);
                }

                SmtpClient connessione = new SmtpClient();
                connessione.Credentials = new NetworkCredential(mittente.Text, password.Text);
                connessione.Host = host.Text;
                connessione.Port = Int32.Parse(porta.Text);
                connessione.EnableSsl = true;
                connessione.Send(messaggio);

            }
            catch
            {
                MessageBox.Show("Impossibile inoltrare l'email! Controllare i parametri di configurazione");
            }
            finally
            {
                fatto = true;
            }
            return fatto;
        }
        private void ImportaValori_Automatica()
        {
            //Leggo il file e inserisco in ogni textbox il valore corrente
            // rispettando la cardinalità dell'array.

            string path = @"C:\\prova.cfg";
            try
            {
                var configurazione = File.ReadAllLines(path);
                mittente.Text = configurazione[0]; 
                password.Text = configurazione[1]; 
                host.Text = configurazione[2];
                porta.Text =configurazione[3]; 
                oggetto.Text = configurazione[4]; 
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ImportaValori_Manuale()
        {
            //Leggo il file e inserisco in ogni textbox il valore corrente
            // rispettando la cardinalità dell'array.

            string path = comboBox1.Text;
            try
            {
                var configurazione = File.ReadAllLines(path);
                mittente.Text = configurazione[0];
                password.Text = configurazione[1];
                host.Text = configurazione[2];
                porta.Text = configurazione[3];
                oggetto.Text = configurazione[4];

            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void email_Load(object sender, EventArgs e)
        {
            /*

            if (nuovo.ControllaInserimento("select count(*) from archivio where Restante<=0") != 0)
            {
              
                    for (int i = 0; i < nuovo.ExecuteQuery_DT("select Nome from archivio where Restante<=0;").Rows.Count; i++)
                    {

                        _nome = nuovo.ExecuteQuery_DT("select Nome from archivio;").Rows[i]["Nome"].ToString();
                    }
                    for (int i = 0; i < nuovo.ExecuteQuery_DT("select Partitaiva from archivio where Restante<=0;").Rows.Count; i++)
                    {
                        _partitaiva = nuovo.ExecuteQuery_DT("select Partitaiva from archivio;").Rows[i]["Partitaiva"].ToString();

                    }
                    Invio_email_Scaduti();
            }
            else
            {
                MessageBox.Show("Non ci sono nominativi scaduti", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            */


            for (int j = 0; j < nuovo.ExecuteQuery_Email().Rows.Count; j++)
            {
                var _app = nuovo.ExecuteQuery_Email().Rows[j]["Email"].ToString();
                
                if(_app.Length!=0)
                checkedListBox1destinatari.Items.Add(_app.Trim());
            }
         
            var pat = @"C:\\prova.cfg";
            comboBox1.Items.Add(pat);

         
                ImportaValori_Automatica();
             
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Invio_email())
                MessageBox.Show("Email Inviata con Successo!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                {
                    for (int i = 0; i < checkedListBox1destinatari.Items.Count; i++)
                        checkedListBox1destinatari.SetItemChecked(i, true);
                }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                for (int i = 0; i < checkedListBox1destinatari.Items.Count; i++)
                {
                    checkedListBox1destinatari.SetItemChecked(i, false);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = true;
            openFileDialog1.InitialDirectory = @"C:\";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName.ToString(); ;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog2.Multiselect = true;
            openFileDialog2.InitialDirectory = @"C:\";
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = openFileDialog2.FileName.ToString(); ;
            }
        }

        private void email_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult uscita = MessageBox.Show("Vuoi davvero chiudere il form Email?", "Chiusura Email", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (uscita == DialogResult.OK)
                Application.Exit();
            else if (uscita == DialogResult.No)
                e.Cancel = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            /*
            if (nuovo.ControllaInserimento("select count(*) from archivio where Restante<=0") != 0)
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < nuovo.ExecuteQuery_DT("select Nome from archivio where Restante<=0;").Rows.Count; i++)
                    {

                        _nome = nuovo.ExecuteQuery_DT("select Nome from archivio;").Rows[i]["Nome"].ToString();
                    }
                    for (int i = 0; i < nuovo.ExecuteQuery_DT("select Partitaiva from archivio where Restante<=0;").Rows.Count; i++)
                    {
                        _partitaiva = nuovo.ExecuteQuery_DT("select Partitaiva from archivio;").Rows[i]["Partitaiva"].ToString();
                        
                    }
                    Invio_email_Scaduti();
                }
            }
            else
            {
                MessageBox.Show("Non ci sono nominativi scaduti","Attenzione",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
            */

        }
        private bool Invio_email_Scaduti()
        {
            bool fatto = false;
            try
            {
                MailMessage messaggio = new MailMessage();
                messaggio.From = new MailAddress(mittente.Text);
                messaggio.Subject = "Nominativi Scaduti";
                messaggio.Body = "Nominativi:   "+"Nome: "+_nome+","+"Partitaiva: "+_partitaiva;
                messaggio.To.Add("egeominotti@gmail.com");
              
                SmtpClient connessione = new SmtpClient();
                connessione.Credentials = new NetworkCredential(mittente.Text, password.Text);
                connessione.Host = host.Text;
                connessione.Port = Int32.Parse(porta.Text);
                connessione.EnableSsl = true;
                connessione.Send(messaggio);

            }
            catch
            {
                MessageBox.Show("Impossibile inoltrare l'email! Controllare i parametri di configurazione");
            }
            finally
            {
                fatto = true;
            }
            return fatto;
        }
      
    }
}
