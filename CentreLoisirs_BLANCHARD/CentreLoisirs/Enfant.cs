using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Security.Cryptography.X509Certificates;

namespace CentreLoisirs
{
    public class Enfant
    {
        public int idenfant { get; private set; }
        public string nom { get; private set; }
        public string prenom { get; private set; }
        public string sexe { get; private set; }
        public DateTime date { get; private set; }
        public string numsecu { get; private set; }
        public string documents { get; private set; }
        public string cantine { get; private set; }
        public string contrainte { get; private set; }
        public string observation { get; private set; }
        public string vaccin { get; private set; }
        public string nomresponsable { get; private set; }
        public string nommedecin { get; private set; }

        public int GetIdEnfant()
        {
            return (idenfant);
        }

        public string GetNomEnfant()
        {
            return (nom);
        }

        public string GetPrenomEnfant()
        {
            return (prenom);
        }
        public string GetSexeEnfant()
        {
            return (sexe);
        }

        public DateTime GetDateEnfant()
        {
            return (date);
        }
        public string GetSecuEnfant()
        {
            return (numsecu);
        }
        public string GetDocumentEnfant()
        {
            return (documents);
        }
        public string GetCantineEnfant()
        {
            return (cantine);
        }
        public string GetContrainteEnfant()
        {
            return (contrainte);
        }
        public string GetObservationEnfant()
        {
            return (observation);
        }
        public string GetVaccinEnfant()
        {
            return (vaccin);
        }
        public string GetResponsableEnfant()
        {
            return (nomresponsable);
        }
        public string GetMedecinEnfant()
        {
            return (nommedecin);
        }
    }
}
