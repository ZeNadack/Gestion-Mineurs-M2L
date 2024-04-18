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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using MySql.Data.MySqlClient;
using Dapper;
using System.Security.Cryptography;

namespace CentreLoisirs
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Famille> LesFamilles;
        public static List<Enfant> LesEnfants;
        public static List<Medecin> LesMedecins;
        MySqlConnection Cn;

        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Cn = new MySqlConnection("server=127.0.0.1;uid=root;password='SxdoRDXjkmQ0YJhI';database=m2l;");
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message);
            }

            LesFamilles = new List<Famille>(Cn.Query<Famille>("SELECT * FROM famille"));
            LesEnfants = new List<Enfant>(Cn.Query<Enfant>("SELECT * FROM enfant"));
            LesMedecins = new List<Medecin>(Cn.Query<Medecin>("SELECT * FROM medecin"));
        }

        private void BtnFamilles_Click(object sender, RoutedEventArgs e)
        {
            GestionFamille famille = new GestionFamille();
            famille.Show();
        }

        private void BtnEnfants_Click(object sender, RoutedEventArgs e)
        {
            GestionEnfant f = new GestionEnfant();
            f.Show();
        }

        private void BtnMedecins_Click(object sender, RoutedEventArgs e)
        {
            GestionMedecin g = new GestionMedecin();
            g.Show();
        }
    }
}
