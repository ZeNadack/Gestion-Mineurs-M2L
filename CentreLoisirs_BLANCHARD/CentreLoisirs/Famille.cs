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
    public class Famille
    {
        public int idparent { get; private set; }
        public string nom1 { get; private set; }
        public string prenom1 { get; private set; }
        public string telperso1 { get; private set; }
        public string telpro1 { get; private set; }
        public string nom2 { get; private set; }
        public string prenom2 { get; private set; }
        public string telperso2 { get; private set; }
        public string telpro2 { get; private set; }

        public int GetIdFamille()
        {
            return (idparent);
        }
        public string GetNomFamille1()
        {
            return(nom1);
        }
        public string GetPrenomFamille1()
        {
            return (prenom1);
        }
        public string GetTelPersoFamille1()
        {
            return (telperso1);
        }
        public string GetTelProFamille1()
        {
            return (telpro1);
        }
        public string GetNomFamille2()
        {
            return (nom2);
        }
        public string GetPrenomFamille2()
        {
            return (prenom2);
        }
        public string GetTelPersoFamille2()
        {
            return (telperso2);
        }
        public string GetTelProFamille2()
        {
            return (telperso2);
        }
    }
    
}
