using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication24
{
    public partial class Form3 : Form
    {
        Sqlite Scaduti = new Sqlite(@"C:\\archivionew.sqlite");
        public Form3()
        {
         InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
          
            dataGridView1.DataSource = Scaduti.ExecuteQuery_DT("select *from archivio where Restante<=0");
            dataGridView1.Refresh();
        }

        private void eliminaScadutiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult uscita = MessageBox.Show("Vuoi davvero cancellare la lista scaduti?", "Attenzione", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (uscita == DialogResult.OK)
            {
                Scaduti.Comando("delete from archivio where Restante<0");
                dataGridView1.Refresh();
                dataGridView1.DataSource = Scaduti.ExecuteQuery_DT("select *from archivio where Restante<=0");
                dataGridView1.Refresh();
               DialogResult riavvia= MessageBox.Show("E' consigliabile riavviare il programma per effettuare opportuni aggiornamenti al DATABASE!","Attenzione",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation);
               if (riavvia == DialogResult.OK)
               {
                   Application.Restart();
               }
            }
           

           
        }
    }
}
