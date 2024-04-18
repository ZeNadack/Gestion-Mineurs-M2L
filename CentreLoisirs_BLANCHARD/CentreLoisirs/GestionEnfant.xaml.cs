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
    /// Logique d'interaction pour GestionEnfant.xaml
    /// </summary>
    public partial class GestionEnfant : Window
    {
        public GestionEnfant()
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

        private void AjoutEnfant(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < CentreLoisirs.MainWindow.LesFamilles.Count; i++)
            {
                CbxResponsEnf.Items.Add(CentreLoisirs.MainWindow.LesFamilles[i].GetNomFamille1());
            }
            for (int i = 0; i < CentreLoisirs.MainWindow.LesMedecins.Count; i++)
            {
                CbxMedecinEnf.Items.Add(CentreLoisirs.MainWindow.LesMedecins[i].GetNomMedecin());
            }


            for (int i = 0; i < CentreLoisirs.MainWindow.LesFamilles.Count; i++)
            {
                CbxModifResponsableEnf.Items.Add(CentreLoisirs.MainWindow.LesFamilles[i].GetNomFamille1());
            }
            for (int i = 0; i < CentreLoisirs.MainWindow.LesMedecins.Count; i++)
            {
                CbxModifMedecinEnf.Items.Add(CentreLoisirs.MainWindow.LesMedecins[i].GetNomMedecin());
            }
        }

        private void BtnEnfantAjout_Click(object sender, RoutedEventArgs e)
        {
            string NomEnfantV, PrenomEnfantV, SexeEnfantV, SecuEnfantV, ObservationEnfantV, VaccinEnfantV, DocumentEnfantV, CantineEnfantV, ContrainteEnfantV, ResponsableEnfantV, MedecinEnfantV;
            DateTime DateEnfantV;

            int IdEnfantV = (CentreLoisirs.MainWindow.LesEnfants.Count) + 1;

            NomEnfantV = TbxNomEnf.Text;
            PrenomEnfantV = TbxPrenomEnf.Text;
            if (RadGarconEnf.IsChecked == true)
            {
                SexeEnfantV = RadGarconEnf.Content.ToString();
            }
            else
            {
                SexeEnfantV = RadFilleEnf.Content.ToString();
            }

            DateEnfantV = DatePickEnf.DisplayDate;
            SecuEnfantV = TbxSecuEnf.Text;

            if (CheckDocEnf.IsChecked == true)
            {
                DocumentEnfantV = "Oui";
            }
            else
            {
                DocumentEnfantV = "Non";
            }
            if (CheckCantEnf.IsChecked == true)
            {
                CantineEnfantV = "Oui";
            }
            else
            {
                CantineEnfantV = "Non";
            }
            if (CheckContrEnf.IsChecked == true)
            {
                ContrainteEnfantV = "Oui";
            }
            else
            {
                ContrainteEnfantV = "Non";
            }
            ObservationEnfantV = TbxObservEnf.Text;
            VaccinEnfantV = TbxVaccinEnf.Text;
            ResponsableEnfantV = Convert.ToString(CbxResponsEnf.SelectedValue);
            MedecinEnfantV = Convert.ToString(CbxMedecinEnf.SelectedValue);

            Cn.Query<Enfant>("INSERT INTO enfant(idenfant, nom, prenom, sexe, date, numsecu, documents, cantine, contrainte, observation, vaccin, nomresponsable, nommedecin) VALUES(@Id, @Nom, @Prenom, @Sexe, @Date, @Secu, @Docs, @Cantine, @Contrainte, @Observ, @Vaccin, @NomRes, @NomMed)", new { Id = IdEnfantV, Nom = NomEnfantV, Prenom = PrenomEnfantV, Sexe = SexeEnfantV, Date = DateEnfantV, Secu = SecuEnfantV, Docs = DocumentEnfantV, Cantine = CantineEnfantV, Contrainte = ContrainteEnfantV, Observ = ObservationEnfantV, Vaccin = VaccinEnfantV, NomRes = ResponsableEnfantV, NomMed = MedecinEnfantV});

            MessageBox.Show("L'enfant a été ajoutée");

            CentreLoisirs.MainWindow.LesEnfants = new List<Enfant>(Cn.Query<Enfant>("SELECT * FROM enfant"));

            TbxNomEnf.Clear();
            TbxPrenomEnf.Clear();
            TbxSecuEnf.Clear();
            TbxObservEnf.Clear();
            TbxVaccinEnf.Clear();
            CbxResponsEnf.Text = " ";
            CbxMedecinEnf.Text = " ";
            CheckCantEnf.IsChecked = false;
            CheckContrEnf.IsChecked = false;
            CheckDocEnf.IsChecked = false;
        }

        private void ConsulEnfant(object sender, RoutedEventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable(); //Table de données en mémoire.
            ds.Tables.Add(dt);
            DataRow dr; // Ligne de données dans un DataTable.
            object[] rowArray = new object[13]; //Tableau contenant deux cases mémoires
            dt.Columns.Add(new DataColumn("N°")); //Ajout de 2 colonnes à la table
            dt.Columns.Add(new DataColumn("Nom"));
            dt.Columns.Add(new DataColumn("Prenom"));
            dt.Columns.Add(new DataColumn("Sexe"));
            dt.Columns.Add(new DataColumn("Date"));
            dt.Columns.Add(new DataColumn("Secu"));
            dt.Columns.Add(new DataColumn("Document"));
            dt.Columns.Add(new DataColumn("Cantine"));
            dt.Columns.Add(new DataColumn("Contrainte"));
            dt.Columns.Add(new DataColumn("Observation"));
            dt.Columns.Add(new DataColumn("Vaccin"));
            dt.Columns.Add(new DataColumn("Responsable"));
            dt.Columns.Add(new DataColumn("Medecin"));
            //Parcours de la collection pour récupérer les salaires f
            for (int i = 0; i < CentreLoisirs.MainWindow.LesEnfants.Count; i++)
            { // Récupération des valeurs de la collection et ajout au tableau
                rowArray[0] = i + 1;
                rowArray[1] = CentreLoisirs.MainWindow.LesEnfants[i].GetNomEnfant();
                rowArray[2] = CentreLoisirs.MainWindow.LesEnfants[i].GetPrenomEnfant();
                rowArray[3] = CentreLoisirs.MainWindow.LesEnfants[i].GetSexeEnfant();
                rowArray[4] = CentreLoisirs.MainWindow.LesEnfants[i].GetDateEnfant();
                rowArray[5] = CentreLoisirs.MainWindow.LesEnfants[i].GetSecuEnfant();
                rowArray[6] = CentreLoisirs.MainWindow.LesEnfants[i].GetDocumentEnfant();
                rowArray[7] = CentreLoisirs.MainWindow.LesEnfants[i].GetCantineEnfant();
                rowArray[8] = CentreLoisirs.MainWindow.LesEnfants[i].GetContrainteEnfant();
                rowArray[9] = CentreLoisirs.MainWindow.LesEnfants[i].GetObservationEnfant();
                rowArray[10] = CentreLoisirs.MainWindow.LesEnfants[i].GetVaccinEnfant();
                rowArray[11] = CentreLoisirs.MainWindow.LesFamilles[i].GetNomFamille1();
                rowArray[12] = CentreLoisirs.MainWindow.LesMedecins[i].GetNomMedecin();
                dr = dt.NewRow(); //Allocation de l’espace mémoire pour une ligne
                dr.ItemArray = rowArray; //Définit toutes les valeurs de cette ligne
                dt.Rows.Add(dr); //Ajout de cette ligne au DataTable
            }
            GridEnfant.ItemsSource = ds.Tables[0].DefaultView; // Source de données du DataGridVie
        }

        private void SupprEnfant(object sender, RoutedEventArgs e)
        {
            int i = 0;
            CbxSupprEnf.Items.Clear();
            while (i < CentreLoisirs.MainWindow.LesEnfants.Count)
            {
                i = i + 1;
                CbxSupprEnf.Items.Add(i);
            }

        }

        private void BtnSupprEnf_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Confirmer?", "Confirmation de suppression", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Cn.Query<Famille>("DELETE FROM enfant WHERE idenfant = @Id", new { Id = CbxSupprEnf.SelectedValue });
                int i = 0;
                MessageBox.Show("L'enfant a été supprimée");
                CbxSupprEnf.Items.Clear();
                CentreLoisirs.MainWindow.LesEnfants = new List<Enfant>(Cn.Query<Enfant>("SELECT * FROM enfant"));
                while (i < CentreLoisirs.MainWindow.LesFamilles.Count)
                {
                    i = i + 1;
                    CbxSupprEnf.Items.Add(i);
                }
                GridEnfant.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Suppression annulée");
            }
        }

        private void ModifierEnfant(object sender, EventArgs e)
        {
            int i = 0;
            CbxModifEnfant.Items.Clear();
            while (i < CentreLoisirs.MainWindow.LesEnfants.Count)
            {
                CbxModifEnfant.Items.Add(CentreLoisirs.MainWindow.LesEnfants[i].GetNomEnfant());
            }
        }

        private void BtnModifEnfant_Click(object sender, RoutedEventArgs e)
        {
            string NomEnfantV, PrenomEnfantV, SexeEnfantV, SecuEnfantV, ObservationEnfantV, VaccinEnfantV, DocumentEnfantV, CantineEnfantV, ContrainteEnfantV, ResponsableEnfantV, MedecinEnfantV;
            DateTime DateEnfantV;

            NomEnfantV = TbxModifNomEnf.Text;
            PrenomEnfantV = TbxModifPrenomEnf.Text;
            SexeEnfantV = TbxModifSexeEnf.Text;
            DateEnfantV = Convert.ToDateTime(TbxModifDateEnf.Text);
            SecuEnfantV = TbxModifSecuEnf.Text;
            ObservationEnfantV = TbxObservEnf.Text;
            VaccinEnfantV = TbxVaccinEnf.Text;
            if (CheckModifDocEnf.IsChecked == true)
            {
                DocumentEnfantV = "Oui";
            }
            else
            {
                DocumentEnfantV = "Non";
            }
            if (CheckModifCantEnf.IsChecked == true)
            {
                CantineEnfantV = "Oui";
            }
            else
            {
                CantineEnfantV = "Non";
            }
            if (CheckModifContrEnf.IsChecked == true)
            {
                ContrainteEnfantV = "Oui";
            }
            else
            {
                ContrainteEnfantV = "Non";
            }

            ResponsableEnfantV = Convert.ToString(CbxModifResponsableEnf.SelectedValue);
            MedecinEnfantV = Convert.ToString(CbxModifMedecinEnf.SelectedValue);

            string NomEnfantSelect = CbxModifEnfant.Text;


            if (MessageBox.Show("Confirmer,", "Confirmation de la modification", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (CbxModifEnfant.SelectedIndex == -1)
                {
                    MessageBox.Show("Erreur de modification");
                }
                else
                {

                    Cn.Query<Enfant>("UPDATE enfant SET(nom, prenom, sexe, date, numsecu, documents, cantine, contrainte, observation, vaccin, nomresponsable, nommedecin) VALUES(@Id, @Nom, @Prenom, @Sexe, @Date, @Secu, @Docs, @Cantine, @Contrainte, @Observ, @Vaccin, @NomRes, @NomMed) WHERE nom1 = @NomSelect", new { Nom = NomEnfantV, Prenom = PrenomEnfantV, Sexe = SexeEnfantV, Date = DateEnfantV, Secu = SecuEnfantV, Docs = DocumentEnfantV, Cantine = CantineEnfantV, Contrainte = ContrainteEnfantV, Observ = ObservationEnfantV, Vaccin = VaccinEnfantV, NomRes = ResponsableEnfantV, NomMed = MedecinEnfantV, NomSelect = NomEnfantSelect });

                    MessageBox.Show("Modification effectuée");
                    CbxModifEnfant.Items.Clear();
                    for (int i = 0; i < CentreLoisirs.MainWindow.LesEnfants.Count; i++)
                    {
                        CbxModifEnfant.Items.Add(CentreLoisirs.MainWindow.LesEnfants[i].GetNomEnfant());
                        CbxModifEnfant.Items.Add(CentreLoisirs.MainWindow.LesEnfants[i].GetPrenomEnfant());
                        CbxModifEnfant.Items.Add(CentreLoisirs.MainWindow.LesEnfants[i].GetSexeEnfant());
                        CbxModifEnfant.Items.Add(CentreLoisirs.MainWindow.LesEnfants[i].GetDateEnfant());
                        CbxModifEnfant.Items.Add(CentreLoisirs.MainWindow.LesEnfants[i].GetSecuEnfant());
                        CbxModifEnfant.Items.Add(CentreLoisirs.MainWindow.LesEnfants[i].GetDocumentEnfant());
                        CbxModifEnfant.Items.Add(CentreLoisirs.MainWindow.LesEnfants[i].GetCantineEnfant());
                        CbxModifEnfant.Items.Add(CentreLoisirs.MainWindow.LesEnfants[i].GetContrainteEnfant());
                        CbxModifEnfant.Items.Add(CentreLoisirs.MainWindow.LesEnfants[i].GetObservationEnfant());
                        CbxModifEnfant.Items.Add(CentreLoisirs.MainWindow.LesEnfants[i].GetVaccinEnfant());
                        CbxModifEnfant.Items.Add(CentreLoisirs.MainWindow.LesEnfants[i].GetResponsableEnfant());
                        CbxModifEnfant.Items.Add(CentreLoisirs.MainWindow.LesEnfants[i].GetMedecinEnfant());
                    }
                }
            }
            else
            {
                MessageBox.Show("Modification annulée");
            }
        }
    } 
}
