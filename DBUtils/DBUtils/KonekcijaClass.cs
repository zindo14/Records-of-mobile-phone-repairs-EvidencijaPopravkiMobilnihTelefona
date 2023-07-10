using System;
using System.Collections.Generic;
using System.Text;
//
using System.Data.SqlClient;
using System.Data;

namespace DBUtils
{
    public class KonekcijaClass
    {

        /* CRC karta - Class Responsibility Collaboration:  */
        //-----------------------------------------------------
        /* ODGOVORNOST: Konekcija na celinu baze podataka, SQL server tipa  */
        /* ZAVISNOST U ODNOSU NA DRUGE KLASE: Standardna klasa iz SqlClient - SqlConnection*/

        #region ATRIBUTI
        private SqlConnection _konekcija;
        private string _putanjaBaze;
        private string _nazivBaze;
        private string _nazivDBMSInstance;
        private string _stringKonekcije;
        #endregion

        #region KONSTRUKTOR

        // podrazumevani konstruktor cita parametre konekcije iz eksternog fajla
        public KonekcijaClass()
        {
            // citanje iz XML 
            DataSet ParametriDataSet = new DataSet();
            ParametriDataSet.ReadXml("ParametriKonekcije.XML");
            _putanjaBaze = ParametriDataSet.Tables[0].Rows[0].ItemArray[0].ToString();
            _nazivBaze = ParametriDataSet.Tables[0].Rows[0].ItemArray[1].ToString();
            _nazivDBMSInstance = ParametriDataSet.Tables[0].Rows[0].ItemArray[2].ToString();
            // formiranje stringa konekcije treba da je ovde u konstruktoru, a ne u okviru metode za otvaranje konekcije
            // inicijalizacija SVIH PROMENLJIVIH TREBA DA JE OVDE, TO JE INTERNA KOHEZIJA METODE, A NE DA ZAVISI OD DRUGIH METODA
            _stringKonekcije = this.DajStringKonekcije(this._nazivBaze, this._putanjaBaze, this._nazivDBMSInstance);
        }

        public KonekcijaClass(string nazivDBMSInstanceParametar, string putanjaBazeParametar, string nazivBazeParametar)
        {
            _putanjaBaze = putanjaBazeParametar;
            _nazivBaze = nazivBazeParametar;
            _nazivDBMSInstance = nazivDBMSInstanceParametar;
            // formiranje stringa konekcije treba da je ovde u konstruktoru, a ne u okviru metode za otvaranje konekcije
            _stringKonekcije = this.DajStringKonekcije(this._nazivBaze, this._putanjaBaze, this._nazivDBMSInstance);
        }

        public KonekcijaClass(string noviStringKonekcijeParametar)
        // overload metoda - konstruktor koji prima kompletan string konekcije
        {

            _putanjaBaze = "";
            _nazivBaze = "";
            _nazivDBMSInstance = "";
            _stringKonekcije = noviStringKonekcijeParametar;
        }
        #endregion

        #region PRIVATNE METODE
        private string DajStringKonekcije(string nazivBazeParametar, string putanjaBazeParametar, string nazivDBMSInstanceParametar)
        {
            // NAMENA: Formira string konekcije iz komponenti, 
            //na 2 nacina - ako je baza podataka vec ukljucena u DBMS ili sa fajlom baze koji se dinamicki povezuje za DBMS
            string stringKonekcije; // lokalna promenljiva u ovoj metodi

            // ako kompletan string vec nije dat kroz konstruktor
            if (putanjaBazeParametar.Length.Equals(0) || putanjaBazeParametar == null)
            {
                stringKonekcije = "Data Source=" + nazivDBMSInstanceParametar + " ;Initial Catalog=" + nazivBazeParametar + ";Integrated Security=True";
            }
            else
            {
                stringKonekcije = "Data Source=.\\" + nazivDBMSInstanceParametar + ";AttachDbFilename=" + putanjaBazeParametar + "\\" + nazivBazeParametar + ";Integrated Security=True;Connect Timeout=30;User Instance=True";
            }


            return stringKonekcije;
        }
        #endregion

        #region JAVNE METODE
        public bool OtvoriKonekciju()
        // NAMENA: Otvara konekciju ka bazi podataka
        {
            bool uspeh;
            _konekcija = new SqlConnection();
            // pisemo this, iako je prihvatljivo da se pise bez toga, zbog citljivosti
            // nadovezujemo se na prethodno preuzet ili formiran string konekcije u okviru konstruktora
            _konekcija.ConnectionString = this._stringKonekcije;

            try
            {
                _konekcija.Open();
                uspeh = true;
            }
            catch
            {
                uspeh = false;
            }
            return uspeh;
        }

        public SqlConnection DajKonekciju()
        // NAMENA: Vraca objekat tipa SqlConnection iz vrednosti privatnog atributa (kao GET metoda)
        {
            return _konekcija;
        }

        public void ZatvoriKonekciju()
        // NAMENA: Zatvara konekciju ka bazi podataka
        {
            _konekcija.Close();
            _konekcija.Dispose();
        }

        #endregion
    }
}
