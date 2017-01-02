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
    
    public partial class filtro : Form
    {
        Sqlite nuovaa = new Sqlite(@"C:\\archivionew.sqlite");
        public filtro()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
           
            if (nome.TextLength != 0)
                dataGridView1.DataSource = nuovaa.ExecuteQuery_DT("select *from archivio where Nome='" + nome.Text + "'");
            else
                MessageBox.Show("Inserire un valore valido!", "Parametro non corretto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (piva.TextLength != 0)
                dataGridView1.DataSource = nuovaa.ExecuteQuery_DT("select *from archivio where Partitaiva='" + piva.Text + "'");
            else
                MessageBox.Show("Inserire un valore valido!", "Parametro non corretto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button3_Click(object sender, EventArgs e)
        {
           dataGridView1.DataSource= nuovaa.ExecuteQuery_DT("select *from archivio where Immissione='" + dateTimePicker1.Value.ToShortDateString() + "'");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource= nuovaa.ExecuteQuery_DT("select *from archivio where Scadenza='" + dateTimePicker1.Value.ToShortDateString() + "'");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(textBox3.TextLength!=0)
                dataGridView1.DataSource = nuovaa.ExecuteQuery_DT("select *from archivio where Giorni<'" + textBox3.Text + "'");
            else
                MessageBox.Show("Inserire un valore valido!", "Parametro non corretto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void filtro_Load(object sender, EventArgs e)
        {
            
            for (int i = 0; i < nuovaa.ExecuteQuery_DT("select Nome from archivio;").Rows.Count; i++)
            {
                var _appo = nuovaa.ExecuteQuery_DT("select Nome from archivio;").Rows[i]["Nome"].ToString();
                nome.AutoCompleteCustomSource.Add(_appo);
            }

            for (int i = 0; i < nuovaa.ExecuteQuery_DT("select Partitaiva from archivio;").Rows.Count; i++)
            {
                var _appo = nuovaa.ExecuteQuery_DT("select Partitaiva from archivio;").Rows[i]["Partitaiva"].ToString();
                piva.AutoCompleteCustomSource.Add(_appo);
            }

            for (int i = 0; i < nuovaa.ExecuteQuery_DT("select Giorni from archivio;").Rows.Count; i++)
            {
               var _appo = nuovaa.ExecuteQuery_DT("select Giorni from archivio;").Rows[i]["Giorni"].ToString();
               textBox3.AutoCompleteCustomSource.Add(_appo);
            }
            
            
        }

        private void uguale_Click(object sender, EventArgs e)
        {
            if (textBox3.TextLength != 0)
                dataGridView1.DataSource = nuovaa.ExecuteQuery_DT("select *from archivio where Giorni=='" + textBox3.Text + "'");
            else
                MessageBox.Show("Inserire un valore valido!", "Parametro non corretto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void maggioreuguale_Click(object sender, EventArgs e)
        {
            if (textBox3.TextLength != 0)
                dataGridView1.DataSource = nuovaa.ExecuteQuery_DT("select *from archivio where Giorni>='" + textBox3.Text + "'");
            else
                MessageBox.Show("Inserire un valore valido!", "Parametro non corretto", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void filtro_FormClosing(object sender, FormClosingEventArgs e)
        {
       
        }
        
    }
}
