using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BICE.SRV;
using Microsoft.Win32;
using BICE.DTO;
using System.Linq;
using BICE.DAL;
using BICE.BLL;
using BICE.WPF.Tools;

namespace BICE.WPF.Pages
{
    public partial class ImportMateriel : UserControl
    {
        private List<Material_DTO>? materiels;
        private DataGrid? dataGrid;
        private Button? BtnLoadCsv;
        private Button? BtnConfirmer;
        public enum eHeaderColumnsPage1
        {
            CodeBarre,
            Denomination,
            Categorie,
            NombreUtilisations,
            NombreUtilisationsLimite,
            DateExpiration,
            DateControle
        }

        public enum eBindingColumnsPage1
        {
            Code_barre,
            Denomination,
            Categorie,
            Nombre_utilisations,
            Nombre_utilisations_limite,
            Date_expiration,
            Date_prochain_controle
        }

        public ImportMateriel()
        {
            InitializeComponent();
            InitializeImportMateriel();
        }

        private void InitializeImportMateriel()
        {
            // Ajout des rangées
            ImportMaterielGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
            ImportMaterielGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            // Ajout des colonnes
            ImportMaterielGrid.ColumnDefinitions.Add(new ColumnDefinition());
            ImportMaterielGrid.ColumnDefinitions.Add(new ColumnDefinition());

            // Création du DataGrid
            dataGrid = new DataGrid
            {
                AutoGenerateColumns = false,
                IsReadOnly = true,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };

            // Gestion des événements de clic sur les boutons
            BtnLoadCsv = new Button
            {
                Content = "Charger CSV",
            };
            BtnLoadCsv.Click += BtnLoadCsv_Click;
            ImportMaterielGrid.Children.Add(BtnLoadCsv);
            Grid.SetRow(BtnLoadCsv, 0);
            Grid.SetColumn(BtnLoadCsv, 0);

            BtnConfirmer = new Button
            {
                Content = "Confirmer",
            };
            BtnConfirmer.Click += BtnConfirmer_Click;
            ImportMaterielGrid.Children.Add(BtnConfirmer);
            Grid.SetRow(BtnConfirmer, 0);
            Grid.SetColumn(BtnConfirmer, 1);

            // Ajout du DataGrid au Grid principal
            ImportMaterielGrid.Children.Add(dataGrid);
            Grid.SetRow(dataGrid, 1);
            Grid.SetColumn(dataGrid, 0);
            Grid.SetColumnSpan(dataGrid, 2);
        }


        private void BtnLoadCsv_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Fichiers CSV (*.csv)|*.csv|Tous les fichiers (*.*)|*.*",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    materiels = CSVParser.ParseCsvFile(openFileDialog.FileName);

                    UpdateDataGrid(materiels);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de l'importation du fichier : {ex.Message}", "Erreur",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void UpdateDataGrid(List<Material_DTO> data)
        {
            dataGrid.ItemsSource = null;
            dataGrid.AutoGenerateColumns = false;

            // Création des colonnes en fonction des propriétés à afficher

            dataGrid.Columns.Clear(); // Efface toutes les colonnes existantes

            // Création des colonnes pour le DataGrid
            for (int i = 0; i < Enum.GetValues(typeof(eHeaderColumnsPage1)).Length; i++)
            {
                // Récupération des valeurs des deux énumérations
                var headerValue = ((eHeaderColumnsPage1)i).ToString();
                var bindingValue = ((eBindingColumnsPage1)i).ToString();

                // Création de la colonne
                DataGridTextColumn column = new DataGridTextColumn
                {
                    Header = headerValue,
                    Binding = new Binding(bindingValue),
                    Width = new DataGridLength(1, DataGridLengthUnitType.Star)
                };

                // Ajout de la colonne au DataGrid
                dataGrid.Columns.Add(column);
            }

            dataGrid.ItemsSource = data; // Ajout des données au DataGrid
        }

        private void BtnConfirmer_Click(object sender, RoutedEventArgs e)
        {
            var materiel_SRV = new Materiel_SRV();
            materiel_SRV.UpdateByStock(materiels);

            MessageBox.Show("Les données ont été importées avec succès.");

        }
    }
}