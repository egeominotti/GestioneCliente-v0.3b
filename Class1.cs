using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;

namespace WindowsFormsApplication24
{
    public class Class1
    {
    

        public Class1() { }
        /*
        public void Inserisci()
        {
          //Calcolo i giorni totali di rimanenza alla scadenza
          TimeSpan finale = Splitter(scadenza) - Splitter(immissione);
          giorni =finale.TotalDays.ToString();
          if (giorni.Length != 0)
          {
              //Inserisco i valori nel database
              nuovaa.ProvaInserimento(nome, partitaiva, email, indirizzo, telefono, cellulare, immissione, scadenza, giorni);
          }
          else
          {
              MessageBox.Show("Non posso inserire un Nominativo con giorni di scadenza pari a Zero! ");
          }
          
          //Calcolo i giorni restanti alla scadenza leggendo dal databaase la query scadenza 
        }
         * */
        public TimeSpan CalcolaRestante(string _appo)
        {
            TimeSpan _resante = Splitter(_appo) - Splitter(DateTime.Now.ToShortDateString().ToString());
            return _resante;
        }
        public DateTime Splitter(string _value)
        {
            /* Funzione per l'automazione dello splitting 
             * della stringa per spezzarla in un semiarray            
             */
            var _appo = _value.Split('/');
            var day = int.Parse(_appo[0]);
            var month = int.Parse(_appo[1]);
            var year = int.Parse(_appo[2]);

            DateTime _inizio = new DateTime(year, month, day);
            return _inizio;

        }
        

       

    }
}
