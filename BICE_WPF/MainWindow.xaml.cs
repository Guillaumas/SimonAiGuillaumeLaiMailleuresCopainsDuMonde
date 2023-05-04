using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BICE.SRV;
using System.Windows.Media.Media3D;
using Microsoft.Win32;
using BICE.DTO;
using System.Linq;
using BICE.DAL;
using BICE.BLL;
using BICE.WPF.Tools;

namespace BICE_WPF
{
    public partial class MainWindow : Window
    {
        private List<Material_DTO>? materiels;

        private List<string> importedData = new List<string>();
        private DataGrid? dataGrid;
        private Button BtnLoadCsv;
        private Button BtnConfirmer;
        private Material_DTO? materialDto;
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



        public MainWindow()
        {
            InitializeComponent();
            InitializePages();
        }

        private void InitializePages()
        {
            // Page 1 - Import matériel
            var page1_grid = new Grid();
            // Ajout des rangées
            page1_grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
            page1_grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            // Ajout des colones
            page1_grid.ColumnDefinitions.Add(new ColumnDefinition());
            page1_grid.ColumnDefinitions.Add(new ColumnDefinition());

            // Ajouter les éléments et les propriétés de la page 1
            // ...

            var tabItem1 = new TabItem
            {
                Header = "Import Materiel",
                Content = page1_grid
            };

            // Page 2
            var page2 = new Grid();
            // Ajouter les éléments et les propriétés de la page 2
            // ...

            var tabItem2 = new TabItem
            {
                Header = "Page 2",
                Content = page2
            };

            // Page 3
            var page3 = new Grid();
            // Ajouter les éléments et les propriétés de la page 3
            // ...

            var tabItem3 = new TabItem
            {
                Header = "Page 3",
                Content = page3
            };

            // Page 4
            var page4 = new Grid();
            // Ajouter les éléments et les propriétés de la page 4
            // ...

            var tabItem4 = new TabItem
            {
                Header = "Page 4",
                Content = page4
            };

            // Ajouter les pages au TabControl
            TabControl mainTabControl = new TabControl();
            mainTabControl.Items.Add(tabItem1);
            mainTabControl.Items.Add(tabItem2);
            mainTabControl.Items.Add(tabItem3);
            mainTabControl.Items.Add(tabItem4);

            // Ajouter le TabControl à la fenêtre principale
            this.Content = mainTabControl;


            // Création du DataGrid
            dataGrid = new DataGrid();
            dataGrid.AutoGenerateColumns = false;
            dataGrid.IsReadOnly = true;
            dataGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            dataGrid.VerticalAlignment = VerticalAlignment.Stretch;




            // Gestion des événements de clic sur les boutons
            BtnLoadCsv = new Button();
            BtnLoadCsv.Content = "Charger CSV";
            BtnLoadCsv.Click += BtnLoadCsv_Click;
            page1_grid.Children.Add(BtnLoadCsv);
            Grid.SetRow(BtnLoadCsv, 0);
            Grid.SetColumn(BtnLoadCsv, 0);

            BtnConfirmer = new Button();
            BtnConfirmer.Content = "Confirmer";
            BtnConfirmer.Click += BtnConfirmer_Click;
            page1_grid.Children.Add(BtnConfirmer);
            Grid.SetRow(BtnConfirmer, 0);
            Grid.SetColumn(BtnConfirmer, 1);

            // Ajout du DataGrid au Grid principal
            page1_grid.Children.Add(dataGrid);
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