using BICE.DTO;
using BICE.SRV;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BICE.WPF.Pages
{
    public partial class Vehicule : UserControl
    {
        private TextBox ImmatriculationTextBox;
        private TextBox NumberTextBox;

        public Vehicule()
        {
            InitializeComponent();
            InitializeVehiculePage();
        }

        private void InitializeVehiculePage()
        {
            VehiculeGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            VehiculeGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            VehiculeGrid.ColumnDefinitions.Add(new ColumnDefinition());
            VehiculeGrid.ColumnDefinitions.Add(new ColumnDefinition());

            // Ajout des titres des colonnes
            TextBlock gestionVehiculesTitle = new TextBlock
            {
                Text = "Gestion de véhicules",
                FontWeight = FontWeights.Bold,
                FontSize = 16,
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Center
            };
            VehiculeGrid.Children.Add(gestionVehiculesTitle);
            Grid.SetRow(gestionVehiculesTitle, 0);
            Grid.SetColumn(gestionVehiculesTitle, 0);

            TextBlock gestionMaterielTitle = new TextBlock
            {
                Text = "Gestion matériel véhicules",
                FontWeight = FontWeights.Bold,
                FontSize = 16,
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Center
            };
            VehiculeGrid.Children.Add(gestionMaterielTitle);
            Grid.SetRow(gestionMaterielTitle, 0);
            Grid.SetColumn(gestionMaterielTitle, 1);

            // Configuration du Grid pour les boutons
            Grid buttonsGrid = new Grid();
            buttonsGrid.RowDefinitions.Add(new RowDefinition());
            buttonsGrid.RowDefinitions.Add(new RowDefinition());
            buttonsGrid.RowDefinitions.Add(new RowDefinition());
            buttonsGrid.ColumnDefinitions.Add(new ColumnDefinition());
            buttonsGrid.ColumnDefinitions.Add(new ColumnDefinition());

            // Création des boutons Gestion des véhicules
            Button btnAjouterVehicule = new Button
            {
                Content = "Ajouter véhicule",
                Margin = new Thickness(10),
                Height = 50
            };
            btnAjouterVehicule.Click += BtnAjouterVehicule_Click;
            buttonsGrid.Children.Add(btnAjouterVehicule);
            Grid.SetRow(btnAjouterVehicule, 0);
            Grid.SetColumn(btnAjouterVehicule, 0);

            Button btnModifierVehicule = new Button
            {
                Content = "Modifier véhicule",
                Margin = new Thickness(10),
                Height = 50
            };
            btnModifierVehicule.Click += BtnModifierVehicule_Click;
            buttonsGrid.Children.Add(btnModifierVehicule);
            Grid.SetRow(btnModifierVehicule, 1);
            Grid.SetColumn(btnModifierVehicule, 0);

            Button btnSupprimerVehicule = new Button
            {
                Content = "Supprimer véhicule",
                Margin = new Thickness(10),
                Height = 50
            };
            btnSupprimerVehicule.Click += BtnSupprimerVehicule_Click;
            buttonsGrid.Children.Add(btnSupprimerVehicule);
            Grid.SetRow(btnSupprimerVehicule, 2);
            Grid.SetColumn(btnSupprimerVehicule, 0);

            // Création des boutons Gestion du matériel des véhicules
            Button btnAjouterMateriel = new Button
            {
                Content = "Ajouter liste de matériel",
                Margin = new Thickness(10),
                Height = 50
            };
            btnAjouterMateriel.Click += BtnAjouterMateriel_Click;
            buttonsGrid.Children.Add(btnAjouterMateriel);
            Grid.SetRow(btnAjouterMateriel, 0);
            Grid.SetColumn(btnAjouterMateriel, 1);

            Button btnVoirMateriel = new Button
            {
                Content = "Voir matériel dans véhicules",
                Margin = new Thickness(10),
                Height = 50
            };
            btnVoirMateriel.Click += BtnVoirMateriel_Click;
            buttonsGrid.Children.Add(btnVoirMateriel);
            Grid.SetRow(btnVoirMateriel, 1);
            Grid.SetColumn(btnVoirMateriel, 1);


            VehiculeGrid.Children.Add(buttonsGrid);
            Grid.SetRow(buttonsGrid, 1);
            Grid.SetColumn(buttonsGrid, 0);
            Grid.SetColumnSpan(buttonsGrid, 2);
            // Ajout du Grid principal au UserControl
            this.Content = VehiculeGrid;
        }

        private void BtnAjouterVehicule_Click(object sender, RoutedEventArgs e)
        {
            ShowAddVehicleForm();
        }

        private void BtnModifierVehicule_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSupprimerVehicule_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAjouterMateriel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnVoirMateriel_Click(object sender, RoutedEventArgs e)
        {

        }

        #region AddVehiculeBouton

        private void ShowAddVehicleForm()
        {
            VehiculeGrid.Children.Clear();
            VehiculeGrid.ColumnDefinitions.Clear();
            VehiculeGrid.ColumnDefinitions.Add(new ColumnDefinition());

            StackPanel FormAddVehicule = new StackPanel { Margin = new Thickness(10) };

            FormAddVehicule.Children.Add(new TextBlock { Text = "Numéro d'immatriculation", FontWeight = FontWeights.Bold });
            ImmatriculationTextBox = new TextBox { Name = "ImmatriculationTextBox" };
            ImmatriculationTextBox.PreviewKeyDown += ImmatriculationTextBox_PreviewKeyDown;
            ImmatriculationTextBox.PreviewTextInput += ImmatriculationTextBox_PreviewTextInput;

            FormAddVehicule.Children.Add(ImmatriculationTextBox);

            FormAddVehicule.Children.Add(new TextBlock { Text = "Numéro", FontWeight = FontWeights.Bold, Margin = new Thickness(0, 10, 0, 0) });
            NumberTextBox = new TextBox { Name = "NumberTextBox" };
            NumberTextBox.PreviewTextInput += NumeroTextBoxAddVehicule_PreviewTextInput;
            FormAddVehicule.Children.Add(NumberTextBox);

            Button submitButton = new Button
            {
                Content = "Ajouter le véhicule",
                Margin = new Thickness(0, 20, 0, 0),
                Height = 50
            };
            submitButton.Click += SubmitButtonAddVehicule_Click;
            FormAddVehicule.Children.Add(submitButton);

            Button backButton = new Button
            {
                Content = "Retour au menu",
                Margin = new Thickness(0, 10, 0, 0),
                Height = 50
            };
            backButton.Click += BackButtonAddVehicule_Click;
            FormAddVehicule.Children.Add(backButton);


            // Ajoutez le StackPanel à la grille principale
            VehiculeGrid.Children.Add(FormAddVehicule);
        }

        private void BackButtonAddVehicule_Click(object sender, RoutedEventArgs e)
        {
            VehiculeGrid.Children.Clear();
            VehiculeGrid.ColumnDefinitions.Clear();
            InitializeVehiculePage();
        }

        private void SubmitButtonAddVehicule_Click(object sender, RoutedEventArgs e)
        {
            TextBox emptyField = AreFieldsEmpty();

            if (emptyField != null)
            {
                MessageBox.Show("Veuillez remplir tous les champs correctement.", "Erreur", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                emptyField.Focus();
                return;
            }
            else
            {
                ImmatriculationTextBox = (TextBox)VehiculeGrid.FindName("ImmatriculationTextBox");
                string Immatriculation = ImmatriculationTextBox.Text.ToUpper();
                NumberTextBox = (TextBox)VehiculeGrid.FindName("NumberTextBox");
                string numero = NumberTextBox.Text;

                Vehicule_DTO newVehicle = new Vehicule_DTO
                {
                    Immatriculation = Immatriculation,
                    Numero = numero
                }; // faire l'envoi de json a l'api par des routes
                //if ("retour de la route")
                //{
                //    MessageBox.Show("Le véhicule a été ajouté avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                //}
                //else
                //{
                //    MessageBox.Show("Une erreur s'est produite lors de l'ajout du véhicule. Veuillez réessayer.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                //}
            }
        }

        private void NumeroTextBoxAddVehicule_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsNumber(e.Text);
        }

        private void ImmatriculationTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox immatriculationTextBox = (TextBox)sender;

            if (immatriculationTextBox.Text.Length >= 9)
            {
                e.Handled = true;
                return;
            }

            if (immatriculationTextBox.Text.Length == 2 || immatriculationTextBox.Text.Length == 6)
            {
                immatriculationTextBox.Text += "-";
                immatriculationTextBox.CaretIndex = immatriculationTextBox.Text.Length;
            }

            // Autoriser uniquement les lettres majuscules et les chiffres
            e.Handled = !IsInputValid_Immatriculation(e.Text.ToUpper());

            if (!e.Handled)
            {
                immatriculationTextBox.Text += e.Text.ToUpper();
                immatriculationTextBox.CaretIndex = immatriculationTextBox.Text.Length;
                e.Handled = true;
            }
        }

        private void ImmatriculationTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox immatriculationTextBox = (TextBox)sender;
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
        private static bool IsInputValid_Immatriculation(string text)
        {
            Regex regex = new Regex("^[A-Z0-9-]+$"); // regex pour autoriser uniquement les lettres majuscules, chiffres et le caractère "-"
            return regex.IsMatch(text);
        }

        #endregion

        private static bool IsNumber(string input)
        {
            Regex regex = new Regex("[^0-9]+"); // regex pour autoriser uniquement les chiffres
            return regex.IsMatch(input);
        }

        private TextBox AreFieldsEmpty()
        {
            bool isImmatriculationEmpty = string.IsNullOrWhiteSpace(ImmatriculationTextBox.Text) || ImmatriculationTextBox.Text.Length > 9 || ImmatriculationTextBox.Text.Length < 1;
            bool isNumberEmpty = string.IsNullOrWhiteSpace(NumberTextBox.Text) || NumberTextBox.Text.Length < 1;

            if (isImmatriculationEmpty)
            {
                return ImmatriculationTextBox;
            }
            else if (isNumberEmpty)
            {
                return NumberTextBox;
            }

            return null;
        }
    }
}
