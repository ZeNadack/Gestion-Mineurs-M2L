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
using System.Data;
using MySql.Data.MySqlClient;
using Dapper;
using System.Diagnostics;

namespace CentreLoisirs
{
    /// <summary>
    /// Logique d'interaction pour GestionMedecin.xaml
    /// </summary>
    public partial class GestionMedecin : Window
    {
        public GestionMedecin()
        {
            InitializeComponent();
            BtnAjouter.IsEnabled = false;
            TbxNom.Clear();
            TbxTel.Clear();
            TbxNom.Focus();
        }

        MySqlConnection Cn;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void TbxNom_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TbxNom.Text.Length != 0 && TbxTel.Text.Length != 0)
            {
                BtnAjouter.IsEnabled = true;
            }
            else
            {
                BtnAjouter.IsEnabled = false;
            }
        }

        private void TbxTel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TbxTel.Text.Length < 10)
            {
                BtnAjouter.IsEnabled = false;
            }
            else if (TbxTel.Text.Length > 10)
            {
                BtnAjouter.IsEnabled = false;
            }
            else
            {
                BtnAjouter.IsEnabled = true;
            }
        }

        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            int IdMedecin = (CentreLoisirs.MainWindow.LesMedecins.Count) + 1;

            string UnNomMedecin, UnTelMedecin;

            UnNomMedecin = TbxNom.Text;
            UnTelMedecin = TbxTel.Text;

            try
            {
                Cn = new MySqlConnection("server=127.0.0.1;uid=root;password='SxdoRDXjkmQ0YJhI';database=m2l;");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

            int nbligne = Cn.Execute("INSERT INTO medecin VALUES (@IdMed, @NomMed, @TelMed)", new { IdMed = IdMedecin, NomMed = UnNomMedecin, TelMed = UnTelMedecin });

            TbxNom.Clear();
            TbxTel.Clear();
            TbxNom.Focus();
        }

        private void ConsulMedecin(object sender, RoutedEventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable(); //Table de données en mémoire.
            ds.Tables.Add(dt);
            DataRow dr; // Ligne de données dans un DataTable.
            object[] rowArray = new object[3]; //Tableau contenant deux cases mémoires
            dt.Columns.Add(new DataColumn("N°"));//Ajout de 2 colonnes à la table
            dt.Columns.Add(new DataColumn("Nom"));
            dt.Columns.Add(new DataColumn("Téléphone"));
            //Parcours de la collection pour récupérer les salaires f
            for (int i = 0; i < CentreLoisirs.MainWindow.LesMedecins.Count; i++)
            { // Récupération des valeurs de la collection et ajout au tableau
                rowArray[0] = i + 1;
                rowArray[1] = CentreLoisirs.MainWindow.LesMedecins[i].GetNomMedecin();
                rowArray[2] = CentreLoisirs.MainWindow.LesMedecins[i].GetTelMedecin();
                dr = dt.NewRow(); //Allocation de l’espace mémoire pour une ligne
                dr.ItemArray = rowArray; //Définit toutes les valeurs de cette ligne
                dt.Rows.Add(dr); //Ajout de cette ligne au DataTable
            }
            GridMedecin.ItemsSource = ds.Tables[0].DefaultView; // Source de données du DataGridVie
        }

        private void Btnsupprimer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cn = new MySqlConnection("server=127.0.0.1;uid=root;password='SxdoRDXjkmQ0YJhI';database=m2l;");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

            if (MessageBox.Show("Confirmer,", "Confirmation de la suppresion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                string UnNomMed = Convert.ToString(CBoxSupprimer.SelectedValue);


                Cn.Query<Medecin>("DELETE FROM medecin WHERE nom = @NomMed", new { NomMed = UnNomMed });
                int i = 0;
                MessageBox.Show("Le medecin a été supprimé");
                CBoxSupprimer.Items.Clear();

                while (i < CentreLoisirs.MainWindow.LesMedecins.Count)
                {
                    i = i + 1;
                    CBoxSupprimer.Items.Add(i);
                }
                GridMedecin.Items.Refresh();

            }
            else
                MessageBox.Show("Suppression annulée");
        }

        private void OngletSupp(object sender, RoutedEventArgs e)
        {
            CBoxSupprimer.Items.Clear();
            for (int i = 0; i < CentreLoisirs.MainWindow.LesMedecins.Count; i++)
            {
                CBoxSupprimer.Items.Add(CentreLoisirs.MainWindow.LesMedecins[i].GetNomMedecin());
            }
        }
        private void BtnModifier_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Cn = new MySqlConnection("server=127.0.0.1;uid=root;password='SxdoRDXjkmQ0YJhI';database=m2l;");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

            string UnNomMedecin, UnTelMedecin;
            UnNomMedecin = tbxModNom.Text;
            UnTelMedecin = tbxModTelephone.Text;

            if (MessageBox.Show("Confirmer,", "Confirmation de la modification", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (CbxModifier.SelectedIndex == -1)
                {
                    MessageBox.Show("Erreur de modification");

                }
                else
                {
                    string NomMedSelect = Convert.ToString(CbxModifier.SelectedValue);
                    Cn.Query<Medecin>("UPDATE medecin SET nom = @NomMed, tel = @TelMed WHERE nom = @Select", new { NomMed = UnNomMedecin, TelMed = UnTelMedecin, Select = NomMedSelect });
                    tbxModNom.Text = "";
                    tbxModTelephone.Text = "";
                    MessageBox.Show("Modification effectuée");
                    CbxModifier.Items.Clear();
                    for (int i = 0; i < CentreLoisirs.MainWindow.LesMedecins.Count; i++)
                    {
                        CbxModifier.Items.Add(CentreLoisirs.MainWindow.LesMedecins[i].GetNomMedecin());
                    }
                }
            }
            else
                MessageBox.Show("Modification annulée");
        }
        private void tabModifier_Selected(object sender, RoutedEventArgs e)
        {
            CbxModifier.Items.Clear();
            for (int i = 0; i < CentreLoisirs.MainWindow.LesMedecins.Count; i++)
            {
                CbxModifier.Items.Add(CentreLoisirs.MainWindow.LesMedecins[i].GetNomMedecin());
            }
        }
    }
}
