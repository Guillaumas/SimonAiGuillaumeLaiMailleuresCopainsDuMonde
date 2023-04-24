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
using BICE.DTO;
using BICE.SRV;
using Microsoft.Win32;
using System.IO;
using BICE.WPF.Tools;
using Microsoft.VisualBasic.FileIO;
using BICE.DAL;

namespace BICE_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnLoadCsv_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog // Ouvre une fenêtre de dialogue pour choisir le fichier CSV à importer
            {
                Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*", // Filtre pour les fichiers CSV
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var materiels = CSVParser.ParseCsvFile(openFileDialog.FileName);

                Materiel_depot_DAL materielDepot = new Materiel_depot_DAL(); // Crée un objet Materiel_depot_DAL
                
                foreach (var materiel in materiels)
                {
                    Materiel_DAL materialDal = Converter.Materiel_DTO_To_DAL(materiel); // Convertit le DTO en DAL
                    materielDepot.Insert(materialDal); // Insère le matériel dans la liste de matériel du dépôt
                }
            }

            MessageBox.Show("Les données ont été importées avec succès.");

        }
    }
}
