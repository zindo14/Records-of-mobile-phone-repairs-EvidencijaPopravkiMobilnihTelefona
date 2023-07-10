using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Forms;
namespace KorisnickiInterfejs
{
    public static class Parametri
    {
        public static string stringKonekcije = "Data Source=DESKTOP-6APOAPD; Initial Catalog=PP_EvidencijaPT; Integrated Security=True;";
        public static string putanjaXML = Application.StartupPath + "\\eksport.XML";
        public static string stringKonekcijeEnt = "metadata=res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;Data Source=BUBILIS;Initial Catalog=GEOGRAFIJASE1;Integrated Security=True;multipleactiveresultsets=True;App=EntityFramework&quot;providerName=System.Data.EntityClient";
    }
}
