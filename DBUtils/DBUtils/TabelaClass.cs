using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//dodato
using System.Data;
using System.Data.SqlClient;

namespace DBUtils
{
    public class TabelaClass
    {
        /* CRC karta - Class Responsibility Collaboration:  */
    //-----------------------------------------------------
    /* ODGOVORNOST: Rad sa podacima iz tabela u bazi podataka SQL server tipa  */
    /* ZAVISNOST U ODNOSU NA DRUGE KLASE: 
     * Iz ove biblioteke - Sopstvena klasa KonekcijaClass 
     * Standardne klase iz biblioteka:
     * iz System.Data - DataSet
     * iz SqlClient - SqlDataAdapter, SqlCommand, SqlTransaction*/
        #region ATRIBUTI

        private string _nazivTabele;
        private SqlDataAdapter _adapter;
        private DataSet _kolekcijaDataSet;
        protected KonekcijaClass _konekcijaObject;

        #endregion

        #region PROPERTY

        public string NazivTabele
        {
            get { return _nazivTabele; }
            set { _nazivTabele = value; }
        }

        #endregion

        #region KONSTRUKTOR

        protected TabelaClass()
        {
            // ostvarivanje konekcije, gde se parametri preuzimaju iz Parametri.xml fajla
            KonekcijaClass konekcijaObject = new KonekcijaClass();
            konekcijaObject.OtvoriKonekciju();
            _konekcijaObject = konekcijaObject;
            // u ovom slucaju _nazivTabele se treba napuniti primenom setera
        }

        protected TabelaClass(string nazivTabeleParametar)
        {
            // ostvarivanje konekcije, gde se parametri preuzimaju iz Parametri.xml fajla
            KonekcijaClass konekcijaObject = new KonekcijaClass();
            konekcijaObject.OtvoriKonekciju();
            _konekcijaObject = konekcijaObject;
            //
            _nazivTabele = nazivTabeleParametar;
        }

        protected TabelaClass(KonekcijaClass konekcijaParametar, string nazivTabeleParametar)
        {
            // ovde se dobija otvorena konekcija koja je kroz objekat prosledjena
            _konekcijaObject = konekcijaParametar;
            _nazivTabele = nazivTabeleParametar;
        }

        #endregion

        #region PRIVATNE METODE

        private void KreirajAdapter(string selectUpitParametar, string insertUpitParametar, string deleteUpitParametar, string updateUpitParametar)
        // NAMENA: Kreira adapter potreban za izvrsavanje upita, dodelom samih stringova upita
        {
            SqlCommand selectKomanda, insertKomanda, deleteKomanda, updateKomanda;

            selectKomanda = new SqlCommand();
            selectKomanda.CommandText = selectUpitParametar;
            selectKomanda.Connection = _konekcijaObject.DajKonekciju();

            insertKomanda = new SqlCommand();
            insertKomanda.CommandText = insertUpitParametar;
            insertKomanda.Connection = _konekcijaObject.DajKonekciju();

            updateKomanda = new SqlCommand();
            updateKomanda.CommandText = updateUpitParametar;
            updateKomanda.Connection = _konekcijaObject.DajKonekciju();

            deleteKomanda = new SqlCommand();
            deleteKomanda.CommandText = deleteUpitParametar;
            deleteKomanda.Connection = _konekcijaObject.DajKonekciju();

            _adapter = new SqlDataAdapter();
            _adapter.SelectCommand = selectKomanda;
            _adapter.InsertCommand = insertKomanda;
            _adapter.UpdateCommand = updateKomanda;
            _adapter.DeleteCommand = deleteKomanda;
        }

        private void KreirajDataset()
        // NAMENA: Kreira objekat tipa DataSet i puni ga posredstvom poziva adaptera
        {
            _kolekcijaDataSet = new DataSet();
            _adapter.Fill(_kolekcijaDataSet, _nazivTabele);

        }

        private void ZatvoriAdapterDataset()
        {
            // NAMENA: Unistava objekte
            _adapter.Dispose();
            _kolekcijaDataSet.Dispose();
        }

        #endregion

        #region JAVNE METODE

        protected DataSet DajPodatke(string selectUpitParametar)
        // izdvaja podatke u odnosu na dat select upit i smesta ih u Dataset
        {
            KreirajAdapter(selectUpitParametar, "", "", "");
            KreirajDataset();
            return _kolekcijaDataSet; // atribut koji je napunjen kroz metodu Kreiraj DataSet
        }

        protected int DajBrojSlogova()
        // vraca ukupan broj zapisa iz dataset-a
        {
            int brojSlogova = _kolekcijaDataSet.Tables[0].Rows.Count;
            return brojSlogova;
        }

        protected bool IzvrsiAzuriranje(string aktivanUpitParametar)
        // izvrzava azuriranje unos/brisanje/izmena u odnosu na dati aktivan upit
        {
            bool uspeh = false;
            SqlConnection konekcija; // lokalna promenljiva
            SqlCommand komanda;// lokalna promenljiva
            try
            {
                konekcija = _konekcijaObject.DajKonekciju();
                // povezivanje
                komanda = new SqlCommand();
                komanda.Connection = konekcija;
                komanda = konekcija.CreateCommand();
                // pokretanje
                komanda.CommandText = aktivanUpitParametar;
                komanda.ExecuteNonQuery();
                uspeh = true;
            }
            catch
            {
                uspeh = false;
            }
            return uspeh;
        }

        protected bool IzvrsiAzuriranje(List<string> listaUpitaParametar)
        // izvrzava azuriranje unos/brisanje/izmena 
        // moze se dodeliti kao parametar lista od vise upita
        // sada transakcija ima smisla, jer izvrsava vise upita u paketu
        {
            //
            bool uspeh = false;
            SqlConnection konekcija; // lokalna promenljiva, odvajamo posebnu konekciju od zajednicke za celu klasu
            SqlCommand komanda;// lokalna promenljiva
            SqlTransaction transakcija = null;  // lokalna promenljiva
            string upit = "";
            try
            {
                konekcija = _konekcijaObject.DajKonekciju();
                // povezivanje
                komanda = new SqlCommand();
                komanda.Connection = konekcija;
                komanda = konekcija.CreateCommand();
                transakcija = konekcija.BeginTransaction();
                komanda.Transaction = transakcija;
                for (int i = 0; i < listaUpitaParametar.Count(); i++)
                {
                    upit = listaUpitaParametar[i];
                    komanda.CommandText = upit;
                    komanda.ExecuteNonQuery();
                }
                transakcija.Commit();
                uspeh = true;
            }
            catch
            {
                transakcija.Rollback();
                uspeh = false;
            }
            return uspeh;
        }
        #endregion
    }
}
