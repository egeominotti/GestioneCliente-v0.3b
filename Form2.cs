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
    public partial class Form2 : Form
    {
        Sqlite nuovo = new Sqlite(@"C:\\archivionew.sqlite");
        public Form2()
        {
            InitializeComponent();  
        }

        private void eliminaDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Vuoi davvero cancellare il database?","Cancellazione Totale", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                nuovo.Comando("delete from archivio");
                dataGridView2.Update();
                dataGridView2.Refresh();
                //Qui deve essere diverso da zero per non mostrare i scaduti in lista utenti
                dataGridView2.DataSource = nuovo.ExecuteQuery_DT("select *from archivio where Restante!=0;");
                Application.Restart();
            }
            
           
        }

        private void l(object sender, EventArgs e)
        {
            dataGridView2.Update();
            dataGridView2.Refresh();
            dataGridView2.DataSource = nuovo.ExecuteQuery_DT("select *from archivio where Restante>0");
        }

        private void inviaEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            email Nuovo = new email();
            Nuovo.ShowDialog();
            Form2 n = new Form2();
            n.Hide();
            
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
