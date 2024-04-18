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
    /// Logique d'interaction pour GestionFamille.xaml
    /// </summary>
    public partial class GestionFamille : Window
    {
        public GestionFamille()
        {
            InitializeComponent();
        }

        MySqlConnection Cn;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Cn = new MySqlConnection("server=127.0.0.1;uid=root;password='SxdoRDXjkmQ0YJhI';database=m2l;");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void BtnFamilleAjout_Click(object sender, RoutedEventArgs e)
        {
            int IdParentV = (CentreLoisirs.MainWindow.LesFamilles.Count)+1;

            string NomFamille1V, PrenomFamille1V, TelPersoFamille1V, TelProFamille1V, NomFamille2V, PrenomFamille2V, TelPersoFamille2V, TelProFamille2V;
            NomFamille1V = TbxNomFam1.Text;
            PrenomFamille1V = TbxPrenomFam1.Text;
            TelPersoFamille1V = TbxTelPersoFam1.Text;
            TelProFamille1V = TbxTelProFam1.Text;
            NomFamille2V = TbxNomFam2.Text;
            PrenomFamille2V = TbxPrenomFam2.Text;
            TelPersoFamille2V = TbxTelPersoFam2.Text;
            TelProFamille2V = TbxTelProFam2.Text;



            if ((NomFamille1V.Equals("") && PrenomFamille1V.Equals("") && TelPersoFamille1V.Equals("") && TelProFamille1V.Equals("")) && ((NomFamille2V.Equals("") && PrenomFamille2V.Equals("") && TelPersoFamille2V.Equals("") && TelProFamille2V.Equals(""))))
            {
                MessageBox.Show("Erreur d'insertion, chaque valeurs d'au moins un responsable légal doit être remplie.");
            }
            else
            {
                Cn.Query<Famille>("INSERT INTO famille(idparent, nom1, prenom1, telperso1, telpro1, nom2, prenom2, telperso2, telpro2) VALUES(@Id, @Nom1, @Prenom1, @TelPerso1, @TelPro1, @Nom2, @Prenom2, @TelPerso2, @TelPro2)", new { Id = IdParentV, Nom1 = NomFamille1V, Prenom1 = PrenomFamille1V, TelPerso1 = TelPersoFamille1V, TelPro1 = TelProFamille1V, Nom2 = NomFamille2V, Prenom2 = PrenomFamille2V, TelPerso2 = TelPersoFamille2V, TelPro2 = TelProFamille2V});

                MessageBox.Show("La famille a été ajoutée");
                CentreLoisirs.MainWindow.LesFamilles = new List<Famille>(Cn.Query<Famille>("SELECT * FROM famille"));
            }
            TbxNomFam1.Clear();
            TbxPrenomFam1.Clear();
            TbxTelPersoFam1.Clear();
            TbxTelProFam1.Clear();
            TbxNomFam2.Clear();
            TbxPrenomFam2.Clear();
            TbxTelPersoFam2.Clear();
            TbxTelProFam2.Clear();
        }

        private void ConsulFamille(object sender, RoutedEventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable(); //Table de données en mémoire.
            ds.Tables.Add(dt);
            DataRow dr; // Ligne de données dans un DataTable.
            object[] rowArray = new object[9]; //Tableau contenant deux cases mémoires
            dt.Columns.Add(new DataColumn("N°")); //Ajout de 2 colonnes à la table
            dt.Columns.Add(new DataColumn("Nom1"));
            dt.Columns.Add(new DataColumn("Prenom1"));
            dt.Columns.Add(new DataColumn("TelPerso1"));
            dt.Columns.Add(new DataColumn("TelPro1"));
            dt.Columns.Add(new DataColumn("Nom2"));
            dt.Columns.Add(new DataColumn("Prenom2"));
            dt.Columns.Add(new DataColumn("TelPerso2"));
            dt.Columns.Add(new DataColumn("TelPro2"));
            //Parcours de la collection pour récupérer les salaires f
            for (int i = 0; i < CentreLoisirs.MainWindow.LesFamilles.Count; i++)
            { // Récupération des valeurs de la collection et ajout au tableau
                rowArray[0] = CentreLoisirs.MainWindow.LesFamilles[i].GetIdFamille();
                rowArray[1] = CentreLoisirs.MainWindow.LesFamilles[i].GetNomFamille1();
                rowArray[2] = CentreLoisirs.MainWindow.LesFamilles[i].GetPrenomFamille1();
                rowArray[3] = CentreLoisirs.MainWindow.LesFamilles[i].GetTelPersoFamille1();
                rowArray[4] = CentreLoisirs.MainWindow.LesFamilles[i].GetTelProFamille1();
                rowArray[5] = CentreLoisirs.MainWindow.LesFamilles[i].GetNomFamille2();
                rowArray[6] = CentreLoisirs.MainWindow.LesFamilles[i].GetPrenomFamille2();
                rowArray[7] = CentreLoisirs.MainWindow.LesFamilles[i].GetTelPersoFamille2();
                rowArray[8] = CentreLoisirs.MainWindow.LesFamilles[i].GetTelProFamille2();
                dr = dt.NewRow(); //Allocation de l’espace mémoire pour une ligne
                dr.ItemArray = rowArray; //Définit toutes les valeurs de cette ligne
                dt.Rows.Add(dr); //Ajout de cette ligne au DataTable
            }
            GridFamille.ItemsSource = ds.Tables[0].DefaultView; // Source de données du DataGridView
        }

        private void SupprFamille(object sender, RoutedEventArgs e)
        {
            int i = 0;
            CbxFamilleSuppr.Items.Clear();
            while (i < CentreLoisirs.MainWindow.LesFamilles.Count)
            {
                i = i + 1;
                CbxFamilleSuppr.Items.Add(i);
            }

        }
        private void BtnFamilleSuppr_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Confirmer?", "Confirmation de suppression", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Cn.Query<Famille>("DELETE FROM famille WHERE idparent = @Id", new { Id = CbxFamilleSuppr.SelectedValue });
                int i = 0;
                MessageBox.Show("La famille a été supprimée");
                CbxFamilleSuppr.Items.Clear();
                CentreLoisirs.MainWindow.LesFamilles = new List<Famille>(Cn.Query<Famille>("SELECT * FROM famille"));
                while (i < CentreLoisirs.MainWindow.LesFamilles.Count)
                {
                    i = i + 1;
                    CbxFamilleSuppr.Items.Add(i);
                }
                GridFamille.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Suppression annulée");
            }
        }

        private void ModifierFamille_Selected(object sender, RoutedEventArgs e)
        {
            int i = 0;
            CbxModifierFam.Items.Clear();
            while (i < CentreLoisirs.MainWindow.LesFamilles.Count)
            {
                CbxModifierFam.Items.Add(CentreLoisirs.MainWindow.LesFamilles[i].GetNomFamille1());
            }
        }

        private void CbxModifierFam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TbxModNom.Text = CbxModifierFam.Text;
        }

        private void BtnModifierFam_Click(object sender, RoutedEventArgs e)
        {
            string NomFamille1V, PrenomFamille1V, TelPersoFamille1V, TelProFamille1V, NomFamille2V, PrenomFamille2V, TelPersoFamille2V, TelProFamille2V;
            NomFamille1V = TbxModNom.Text;
            PrenomFamille1V = TbxModPrenom.Text;
            TelPersoFamille1V = TbxModTelPerso.Text;
            TelProFamille1V = TbxModTelPro.Text;
            NomFamille2V = TbxModNom2.Text;
            PrenomFamille2V = TbxModPrenom2.Text;
            TelPersoFamille2V = TbxModTelPerso2.Text;
            TelProFamille2V = TbxModTelPro2.Text;

            string NomFamilleSelect = CbxModifierFam.Text;

            if (MessageBox.Show("Confirmer,", "Confirmation de la modification", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (CbxModifierFam.SelectedIndex == -1)
                {
                    MessageBox.Show("Erreur de modification");
                }
                else
                {

                    Cn.Query<Famille>("UPDATE famille SET(nom1, prenom1, telperso1, telpro1, nom2, prenom2, telperso2, telpro2) VALUES(@Id, @Nom1, @Prenom1, @TelPerso1, @TelPro1, @Nom2, @Prenom2, @TelPerso2, @TelPro2) WHERE nom1 = @NomSelect", new { Nom1 = NomFamille1V, Prenom1 = PrenomFamille1V, TelPerso1 = TelPersoFamille1V, TelPro1 = TelProFamille1V, Nom2 = NomFamille2V, Prenom2 = PrenomFamille2V, TelPerso2 = TelPersoFamille2V, TelPro2 = TelProFamille2V, NomSelect = NomFamilleSelect });

                    MessageBox.Show("Modification effectuée");
                    CbxModifierFam.Items.Clear();
                    for (int i = 0; i < CentreLoisirs.MainWindow.LesFamilles.Count; i++)
                    {
                        CbxModifierFam.Items.Add(CentreLoisirs.MainWindow.LesFamilles[i].GetNomFamille1());
                        CbxModifierFam.Items.Add(CentreLoisirs.MainWindow.LesFamilles[i].GetPrenomFamille1());
                        CbxModifierFam.Items.Add(CentreLoisirs.MainWindow.LesFamilles[i].GetTelPersoFamille1());
                        CbxModifierFam.Items.Add(CentreLoisirs.MainWindow.LesFamilles[i].GetTelProFamille1());
                        CbxModifierFam.Items.Add(CentreLoisirs.MainWindow.LesFamilles[i].GetNomFamille2());
                        CbxModifierFam.Items.Add(CentreLoisirs.MainWindow.LesFamilles[i].GetPrenomFamille2());
                        CbxModifierFam.Items.Add(CentreLoisirs.MainWindow.LesFamilles[i].GetTelPersoFamille2());
                        CbxModifierFam.Items.Add(CentreLoisirs.MainWindow.LesFamilles[i].GetTelProFamille2());
                    }
                }
            }
            else
                MessageBox.Show("Modification annulée");
        }
    }
}
