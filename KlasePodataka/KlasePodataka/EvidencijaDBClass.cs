using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// dodatno ukljuceno
using System.Data;
using DBUtils;

namespace KlasePodataka
{
    public class EvidencijaDBClass: TabelaClass
    /* CRC karta - Class Responsibility Collaboration:  */
    //-----------------------------------------------------
    /* ODGOVORNOST: realizacija CRUD operacija nad tabelom Zvanje u bazi podataka */
    /* ZAVISNOST U ODNOSU NA DRUGE KLASE:
     Standardna klasa iz System.Data - DataSet
     Sopstvena klasa iz DBUtils - TabelaClass (bazna klasa)
     */
    {
        #region ATRIBUTI
        // imamo protected atribut Konekcija iz bazne klase
        // imamo sve iz bazne klase tabela
        #endregion

        // ne postavljamo property, jer postoji rizik da ostane prazan string konekcije, a bez njega ne moze
        // zato obavezujemo da se vrednost obezbedi kroz konstruktor

        #region KONSTRUKTOR

        public EvidencijaDBClass() : base()
        {
            // ovo je obavezan dodatak, jer u baznom konstruktoru bez parametara se ostvaruje konekcija
            this.NazivTabele = "evidencija";
        }

        public EvidencijaDBClass(string nazivTabeleParametar) : base(nazivTabeleParametar)
        // izvrsava se konstruktor bazne klase, znaci istovremeno se instancira sve kao za objekat klase TabelaClass
        // ovde se u okviru ovakvog konstruktora bazne klase instancira objekat za konekciju i mi sada raspolazemo sa time
        // sada raspolazemo sa time i pisemo this.NAZIVI METODA ILI ATRIBUTA OD TE KLASE TabelaClass
        {
            // OVDE PISEMO KOD KOJI SE IZVRSAVA NAKON OSNOVNOG IZVRSAVANJA KONSTRUKTORA BAZNE KLASE           
        }

        public EvidencijaDBClass(KonekcijaClass konekcijaParametar, string nazivTabeleParametar) : base(konekcijaParametar, nazivTabeleParametar)
        // izvrsava se konstruktor bazne klase, znaci istovremeno se instancira sve kao za objekat klase TabelaClass
        // _tabelaObject = new TabelaClass(_konekcijaObject, "Zvanje");
        // sada raspolazemo sa time i pisemo this.NAZIVI METODA ILI ATRIBUTA OD TE KLASE TabelaClass
        {
            // OVDE PISEMO KOD KOJI SE IZVRSAVA NAKON OSNOVNOG IZVRSAVANJA KONSTRUKTORA BAZNE KLASE           
        }

        #endregion
        // privatne metode

        public DataSet DajSveEvidencije()
        {
            DataSet podaciDataSet = new DataSet();

            podaciDataSet = this.DajPodatke("Select * from evidencija");

            return podaciDataSet;
        }

        public DataSet DajSveEvidencijePoMarki(string MarkaEvidencijeParametar)
        {
            DataSet podaciDataSet = new DataSet();
            string upit = "Select * from evidencija where Marka like '%" + MarkaEvidencijeParametar + "%'";
            podaciDataSet = this.DajPodatke(upit);
            return podaciDataSet;
        }

        public bool SnimiNovuEvidenciju(EvidencijaClass novaEvidencijaObjectParametar)
        {
            string strDatum = novaEvidencijaObjectParametar.Datum.Month.ToString() + "/" + novaEvidencijaObjectParametar.Datum.Day.ToString() + "/" + novaEvidencijaObjectParametar.Datum.Year.ToString();
            bool uspeh = false;
            uspeh = this.IzvrsiAzuriranje("INSERT INTO evidencija VALUES ('" + novaEvidencijaObjectParametar.Marka + "', '" + novaEvidencijaObjectParametar.Model + "','" + novaEvidencijaObjectParametar.Vlasnik + "','" + novaEvidencijaObjectParametar.Cena + "','" + strDatum + "','" + novaEvidencijaObjectParametar.OpisKvara + "','" + novaEvidencijaObjectParametar.OpisPopravke + "')");
            return uspeh;
        }
    }
}
