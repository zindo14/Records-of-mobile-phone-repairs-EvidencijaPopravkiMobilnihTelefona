using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KlasePodataka
{
    public class EvidencijaClass
    // NAMENA: 
    /* CRC karta - Class Responsibility Collaboration:  */
    //-----------------------------------------------------
    /* ODGOVORNOST: Klasa tipa pojedinac (ima samo atribute i property) za rad sa zvanjem */
    /* ZAVISNOST U ODNOSU NA DRUGE KLASE: nema
     */
    {
        #region ATRIBUTI
        private string _marka;
        private string _model;
        private string _vlasnik;
        private float _cena;
        private DateTime _date;
        private string _opiskvara;
        private string _opispopravke;

        #endregion

        #region PROPERTY
        // property
        public string Marka
        {
            get
            {
                return _marka;
            }
            set
            {
                if (this._marka != value)
                    this._marka = value;
            }
        }
        public string Model
        {
            get
            {
                return _model;
            }
            set
            {
                if (this._model != value)
                    this._model = value;
            }
        }
        public string Vlasnik
        {
            get
            {
                return _vlasnik;
            }
            set
            {
                if (this._vlasnik != value)
                    this._vlasnik = value;
            }
        }

        public float Cena
        {
            get
            {
                return _cena;
            }
            set
            {
                if (this._cena != value)
                    this._cena = value;
            }
        }
        
        public DateTime Datum
        {
            get
            {
                return _date;
            }
            set
            {
                if (this._date != value)
                    this._date = value;
            }
        }

        public string OpisKvara
        {
            get
            {
                return _opiskvara;
            }
            set
            {
                if (this._opiskvara != value)
                    this._opiskvara = value;
            }
        }
        public string OpisPopravke
        {
            get
            {
                return _opispopravke;
            }
            set
            {
                if (this._opispopravke != value)
                    this._opispopravke = value;
            }
        }
        #endregion

        #region KONSTRUKTOR
        // konstruktor
        public EvidencijaClass()
        {
            _marka = "";
            _model = "";
            _vlasnik = "";
            _cena = 0;
            _date = DateTime.Now;
            _opiskvara = "";
            _opispopravke = "";
        }
        #endregion
    }
}
