using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KlasePodataka
{
    public class EvidencijaListaClass
    /* CRC karta - Class Responsibility Collaboration:  */
    //-----------------------------------------------------
    /* ODGOVORNOST: Klasa tipa liste koja sadrzi kao elemente objekte klase tipa pojedinac ZvanjeClass */
    /* ZAVISNOST U ODNOSU NA DRUGE KLASE:
     Standardna klasa iz System.Collections.Generic - List
     Sopstvena klasa iz ove biblioteke - ZvanjeClass
     */
    {
        #region ATRIBUTI
        private List<EvidencijaClass> _listaEvidencije;
        #endregion

        #region PROPERTY
        // property
        public List<EvidencijaClass> ListaEvidencije
        {
            get
            {
                return _listaEvidencije;
            }
            set
            {
                if (this._listaEvidencije != value)
                    this._listaEvidencije = value;
            }
        }
        #endregion

        #region KONSTRUKTOR
        public EvidencijaListaClass()
        {
            _listaEvidencije = new List<EvidencijaClass>();

        }
        #endregion


        // privatne metode

        #region JAVNE METODE
        public void DodajElementListe(EvidencijaClass NovoZvanjeParametar)
        {
            _listaEvidencije.Add(NovoZvanjeParametar);
        }

        public void ObrisiElementListe(EvidencijaClass ZvanjeZaBrisanjeParametar)
        {
            _listaEvidencije.Remove(ZvanjeZaBrisanjeParametar);
        }

        public void ObrisiElementNaPoziciji(int pozicija)
        {
            _listaEvidencije.RemoveAt(pozicija);
        }

        public void IzmeniElementListe(EvidencijaClass objStaraEvidencijaParametar, EvidencijaClass NovaEvidencijaParametar)
        {
            int indexStareEvidencije = 0;
            indexStareEvidencije = _listaEvidencije.IndexOf(objStaraEvidencijaParametar);
            _listaEvidencije.RemoveAt(indexStareEvidencije);
            _listaEvidencije.Insert(indexStareEvidencije, NovaEvidencijaParametar);
        }
        #endregion
    }
}
