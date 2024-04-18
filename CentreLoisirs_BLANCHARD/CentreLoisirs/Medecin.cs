using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentreLoisirs
{
    public class Medecin
    {
        public int idmedecin { get; private set; }
        public string nom { get; private set; }
        public string tel { get; private set; }

        public int GetIdMedecin()
        {
            return (idmedecin);
        }
        public string GetNomMedecin()
        {
            return (nom);
        }
        public string GetTelMedecin()
        {
            return (tel);
        }
    }
}
