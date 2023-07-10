using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using KlasePodataka;

namespace KorisnickiInterfejs
{
    public partial class Form1 : Form
    {
        private DataSet podaciDataSet;
        private EvidencijaDBClass EvidencijaDBObject;

        public Form1()
        {
            InitializeComponent();
        }

        private void PrikaziTabeluPodataka(DataSet podaciDataSet)
        {
            dataGridView1.DataSource = podaciDataSet.Tables[0];
            dataGridView1.Refresh();
        }

        private void IsprazniKontrole()
        {
            comboBox1.Text = "";

            textBox2.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox3.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // inicijalizacija globalne promenljive
            podaciDataSet = new DataSet();
            EvidencijaDBObject = new EvidencijaDBClass();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 1. provera popunjenosti 
            if (comboBox1.Text.Equals("Izaberite marku telefona"))
            {
                MessageBox.Show("Niste uneli Marku telefona!");
                comboBox1.Focus();
                return;
            }

            if(textBox2.Text.Equals(""))
            {
                MessageBox.Show("Niste uneli model telefona!");
                textBox2.Focus();
                return;
            }

            if (textBox4.Text.Equals(""))
            {
                MessageBox.Show("Niste uneli cenu telefona!");
                textBox4.Focus();
                return;
            }

            if (textBox5.Text.Equals(""))
            {
                MessageBox.Show("Niste uneli kolicinu!");
                textBox5.Focus();
                return;
            }

            string poruka = "";

            // preuzimanje vrednosti sa korisnickog interfejsa
            EvidencijaClass EvidencijaObject = new EvidencijaClass();
            EvidencijaObject.Marka = comboBox1.Text;
            EvidencijaObject.Model = textBox2.Text;
            EvidencijaObject.Vlasnik = textBox4.Text;
            EvidencijaObject.Cena = float.Parse(textBox5.Text);
            EvidencijaObject.Datum = dateTimePicker1.Value;
            EvidencijaObject.OpisKvara = textBox3.Text;
            EvidencijaObject.OpisPopravke = textBox6.Text;

            // snimanje

            bool uspehSnimanja = EvidencijaDBObject.SnimiNovuEvidenciju(EvidencijaObject);
            if (uspehSnimanja)
            {
                poruka = "Uspesno snimljeno!";
                
            }
            else
            {
                poruka = "Nije uspesno snimljeno!";
            }
            MessageBox.Show(poruka);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            IsprazniKontrole();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox7.Text.Equals(""))
            {
                MessageBox.Show("Niste uneli kriterijum filtriranja!");
                textBox7.Focus();
                return;
            }
            else
            {
                podaciDataSet = EvidencijaDBObject.DajSveEvidencijePoMarki(textBox7.Text);
                PrikaziTabeluPodataka(podaciDataSet);
                textBox7.Text = "";
            } 
        }

        private void button7_Click(object sender, EventArgs e)
        {
            podaciDataSet = EvidencijaDBObject.DajSveEvidencije();
            PrikaziTabeluPodataka(podaciDataSet);
        }

        private void SnimiXML(DataTable podaci, string putanja)
        {
            DataSet dsPodaciEksport = new DataSet();

            // s obzirom da smo dobili kroz parametar poziva ove procedure "podaci"
            // zapravo samo promenljivu koja sadrzi memorijsku lokaciju, pokazivac 
            // ka podacima, javlja se problem kada ovaj isti DataTable "podaci"
            // vezemo sa drugim datasetom "dsPodaciExport" jer je taj DataTable
            // vec povezan sa dsPodaci u okviru procedure UcitajSve i UcitajTabelarno.
            // Zato moramo da radimo Copy, da kopiramo strukturu i podatke u NOVI DataTable.
            DataTable podaciZaEksport = new DataTable();
            podaciZaEksport = podaci.Copy();
            dsPodaciEksport.Tables.Add(podaciZaEksport);
            dsPodaciEksport.WriteXml(putanja);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SnimiXML(podaciDataSet.Tables[0], Parametri.putanjaXML);
            MessageBox.Show("Uspesno realizovan eksport podataka!");
        }
    }
}
