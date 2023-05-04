using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BICE.SRV;
using System.Windows.Media.Media3D;
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
        private List<Material_DTO>? _materials;
        private DataGrid? _dataGrid;
        private Button _btnLoadCsv;
        private Button _btnConfirm;

        public enum EHeaderColumnsPage1
        {
            CodeBarre,
            Denomination,
            Categorie,
            NombreUtilisations,
            NombreUtilisationsLimite,
            DateExpiration,
            DateControle
        }

        public enum EBindingColumnsPage1
        {
            Code_barre,
            Denomination,
            Categorie,
            Nombre_utilisations,
            Nombre_utilisations_limite,
            Date_expiration,
            Date_prochain_controle
        }

        }



        }

        private void InitializePages()
        {
            var mainTabControl = new TabControl();
            mainTabControl.Items.Add(CreateImportMaterialPage());
            mainTabControl.Items.Add(CreatePage("Page 2"));
            mainTabControl.Items.Add(CreatePage("Page 3"));
            mainTabControl.Items.Add(CreatePage("Page 4"));

            Content = mainTabControl;
        }

        private TabItem CreateImportMaterialPage()
        {
            var pageGrid = CreatePageGrid();

            _btnLoadCsv = CreateButton("Charger CSV", BtnLoadCsv_Click);
            pageGrid.Children.Add(_btnLoadCsv);
            Grid.SetRow(_btnLoadCsv, 0);
            Grid.SetColumn(_btnLoadCsv, 0);

            _btnConfirm = CreateButton("Confirmer", BtnConfirm_Click);
            pageGrid.Children.Add(_btnConfirm);
            Grid.SetRow(_btnConfirm, 0);
            Grid.SetColumn(_btnConfirm, 1);

            _dataGrid = CreateDataGrid();
            pageGrid.Children.Add(_dataGrid);
            Grid.SetRow(_dataGrid, 1);
            Grid.SetColumn(_dataGrid, 0);
            Grid.SetColumnSpan(_dataGrid, 2);

            return CreateTabItem("Import Materiel", pageGrid);
        }

        private TabItem CreatePage(string title)
        {
            var pageGrid = CreatePageGrid();
            return CreateTabItem(title, pageGrid);
        }

        private Grid CreatePageGrid()
        {
            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            return grid;
        }

        private Button CreateButton(string text, RoutedEventHandler clickHandler)
        {
            var button = new Button();
            button.Content = text;
            button.Click += clickHandler;

            return button;
        }

        private DataGrid CreateDataGrid()
        {
            var dataGrid = new DataGrid();
            dataGrid.AutoGenerateColumns = false;
            dataGrid.IsReadOnly = true;
            dataGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            dataGrid.VerticalAlignment = VerticalAlignment.Stretch;

            return dataGrid;
        }

        private TabItem CreateTabItem(string title, UIElement content)
        {
            return new TabItem
            {
                Header = title,
                Content = content
            };
        }

            BtnLoadCsv.Content = "Charger CSV";
            BtnLoadCsv.Click += BtnLoadCsv_Click;
            page1_grid.Children.Add(BtnLoadCsv);
            Grid.SetRow(BtnLoadCsv, 0);
            Grid.SetColumn(BtnLoadCsv, 0);

            BtnConfirmer = new Button();
            BtnConfirmer.Content = "Confirmer";
            BtnConfirmer.Click += BtnConfirmer_Click;
            page1_grid.Children.Add(BtnConfirmer);
                try
                {
                    _materials = CSVParser.ParseCsvFile(openFileDialog.FileName);
                    UpdateDataGrid(_materials);
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
            _dataGrid.ItemsSource = null;
            _dataGrid.AutoGenerateColumns = false;

            _dataGrid.Columns.Clear();

            for (int i = 0; i < Enum.GetValues(typeof(EHeaderColumnsPage1)).Length; i++)
            {
                var headerValue = ((EHeaderColumnsPage1)i).ToString();
                var bindingValue = ((EBindingColumnsPage1)i).ToString();

                DataGridTextColumn column = new DataGridTextColumn
                {
                    Header = headerValue,
                    Binding = new Binding(bindingValue),
                    Width = new DataGridLength(1, DataGridLengthUnitType.Star)
                };

                _dataGrid.Columns.Add(column);
            }

            _dataGrid.ItemsSource = data;
        }
        private void UpdateDataGrid(List<Material_DTO> data)
        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            var materialService = new Materiel_SRV();
            materialService.UpdateByStock(_materials);



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
        {


        private void BtnConfirmer_Click(object sender, RoutedEventArgs e)
        {
            var materiel_SRV = new Materiel_SRV();
            materiel_SRV.UpdateByStock(materiels);

            for (int i = 0; i < Enum.GetValues(typeof(EHeaderColumnsPage1)).Length; i++)
            
                var headerValue = ((EHeaderColumnsPage1)i).ToString();
                var bindingValue = ((EBindingColumnsPage1)i).ToString();

                DataGridTextColumn column = new DataGridTextColumn
                {
                    Header = headerValue,
                    Binding = new Binding(bindingValue),
                    Width = new DataGridLength(1, DataGridLengthUnitType.Star)
                };

                _dataGrid.Columns.Add(column);
            }

            _dataGrid.ItemsSource = data;
        }
            _dataGrid.Columns.Clear();


        private void BtnConfirmer_Click(object sender, RoutedEventArgs e)
        {
            var materiel_SRV = new Materiel_SRV();
            materiel_SRV.UpdateByStock(materiels);


            
                DataGridTextColumn column = new DataGridTextColumn
                {
                    Header = headerValue,
                    Binding = new Binding(bindingValue),
                    Width = new DataGridLength(1, DataGridLengthUnitType.Star)
                };

                _dataGrid.Columns.Add(column);
            }

            _dataGrid.ItemsSource = data;
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            var materialService = new Materiel_SRV();
            materialService.UpdateByStock(_materials);

            MessageBox.Show("Les données ont été importées avec succès.");
        }
    }
}
