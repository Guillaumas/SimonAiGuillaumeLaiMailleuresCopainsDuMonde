using BICE.DTO;
using BICE.SRV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using BICE.WPF.Tools;

namespace BICE.WPF.Pages
{
    public partial class Vehicule : UserControl
    {
        TextBox ImmatriculationTextBox;
        TextBox NumeroTextBox;

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

        #region FonctionsBoutons

        private void BtnAjouterVehicule_Click(object sender, RoutedEventArgs e)
        {
            ShowAddVehicleForm();
        }

        private async void BtnModifierVehicule_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
            await CreateVehiculesDropdown();
        }

        private void BtnAjouterMateriel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnVoirMateriel_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

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
            NumeroTextBox = new TextBox { Name = "NumeroTextBox" };
            NumeroTextBox.PreviewTextInput += NumeroTextBoxAddVehicule_PreviewTextInput;
            FormAddVehicule.Children.Add(NumeroTextBox);

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
            bool AllFieldsFilled = AreFieldsEmpty();

            if (AllFieldsFilled == false)
            {
                MessageBox.Show("Veuillez remplir tous les champs correctement.", "Erreur", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else
            {
                ImmatriculationTextBox = (TextBox)VehiculeGrid.FindName("ImmatriculationTextBox");
                string Immatriculation = ImmatriculationTextBox.Text.ToUpper();
                NumeroTextBox = (TextBox)VehiculeGrid.FindName("NumeroTextBox");
                string numero = NumeroTextBox.Text;

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

        #region EditVehiculeBouton

        private async Task CreateVehiculesDropdown()
        {
            ComboBox vehiculesComboBox = new ComboBox();
            Grid.SetColumn(vehiculesComboBox, 0);
            Grid.SetRow(vehiculesComboBox, 0);
            VehiculeGrid.Children.Add(vehiculesComboBox);

            // Charger la liste des véhicules
            var vehicules = await GetVehicules();
            vehiculesComboBox.ItemsSource = vehicules;
            vehiculesComboBox.DisplayMemberPath = "Immatriculation";
            vehiculesComboBox.SelectionChanged += VehiculesComboBox_SelectionChanged;
        }

        private async Task<List<Vehicule_DTO>> GetVehicules()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("vehicule_API/");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<Vehicule_DTO> vehicules = Tools.DTO_and_JSON_translator.VehiculeDtoFromJson(json);
                    return vehicules;
                }
                else
                {
                    throw new Exception($"Erreur lors de l'appel à l'API : {response.ReasonPhrase}");
                }
            }
        }

        private void VehiculesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Vehicule_DTO selectedVehicule = (Vehicule_DTO)comboBox.SelectedItem;

            if (selectedVehicule != null)
            {
                // Initialiser les champs du formulaire
                AddFormFields();

                // Mettre à jour les champs du formulaire avec les informations du véhicule sélectionné
                NumeroTextBox.Text = selectedVehicule.Numero;
                ImmatriculationTextBox.Text = selectedVehicule.Immatriculation;
            }
        }

        private void ClearAll()
        {
            VehiculeGrid.Children.Clear();
            VehiculeGrid.RowDefinitions.Clear();
            VehiculeGrid.ColumnDefinitions.Clear();
            CreateGridStructure();
        }

        private void CreateGridStructure()
        {
            for (int i = 0; i < 5; i++)
            {
                VehiculeGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < 2; i++)
            {
                VehiculeGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        private void AddFormFields()
        {
            // Ajouter le champ "Numéro"
            Label numeroLabel = new Label { Content = "Numéro" };
            Grid.SetColumn(numeroLabel, 0);
            Grid.SetRow(numeroLabel, 1);
            VehiculeGrid.Children.Add(numeroLabel);

            NumeroTextBox = new TextBox();
            Grid.SetColumn(NumeroTextBox, 1);
            Grid.SetRow(NumeroTextBox, 1);
            VehiculeGrid.Children.Add(NumeroTextBox);

            // Ajouter le champ "Immatriculation"
            Label immatriculationLabel = new Label { Content = "Immatriculation" };
            Grid.SetColumn(immatriculationLabel, 0);
            Grid.SetRow(immatriculationLabel, 2);
            VehiculeGrid.Children.Add(immatriculationLabel);

            ImmatriculationTextBox = new TextBox();
            Grid.SetColumn(ImmatriculationTextBox, 1);
            Grid.SetRow(ImmatriculationTextBox, 2);
            VehiculeGrid.Children.Add(ImmatriculationTextBox);

            // Ajouter le bouton "Confirmer"
            Button btnConfirmer = new Button { Content = "Confirmer" };
            Grid.SetColumn(btnConfirmer, 0);
            Grid.SetRow(btnConfirmer, 3);
            btnConfirmer.Click += BtnConfirmer_Click;
            VehiculeGrid.Children.Add(btnConfirmer);

            // Ajouter le bouton "Supprimer"
            Button btnSupprimer = new Button { Content = "Supprimer" };
            Grid.SetColumn(btnSupprimer, 1);
            Grid.SetRow(btnSupprimer, 3);
            btnSupprimer.Click += BtnSupprimer_Click;
            VehiculeGrid.Children.Add(btnSupprimer);
        }

        private async void BtnConfirmer_Click(object sender, RoutedEventArgs e)
        {
            // Valider les informations du formulaire
            if (IsValidForm())
            {
                // Envoyer les modifications à l'API
                Vehicule_DTO vehiculeToUpdate = new Vehicule_DTO { Numero = NumeroTextBox.Text, Immatriculation = ImmatriculationTextBox.Text };
                string json = vehiculeToUpdate.ToJson();
                await UpdateVehicule(json);
            }
            else
                MessageBox.Show("Veuillez vérifier les informations saisies.");
        }

        private async void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce véhicule ?", "Confirmation", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                // Envoyer la suppression à l'API
                await DeleteVehicule(NumeroTextBox.Text);
            }
        }


        private bool IsValidForm()
        {
            return !string.IsNullOrEmpty(NumeroTextBox.Text) && !string.IsNullOrEmpty(ImmatriculationTextBox.Text);
        }

        private async Task UpdateVehicule(string jsonVehiculeToUpdate)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpContent content = new StringContent(jsonVehiculeToUpdate, Encoding.UTF8, "application/json");

                Vehicule_DTO vehiculeToUpdate = JsonConvert.DeserializeObject<Vehicule_DTO>(jsonVehiculeToUpdate);
                HttpResponseMessage response = await client.PostAsync($"vehicule_API/updateVehicule/{vehiculeToUpdate.Numero}", content);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Erreur lors de la mise à jour du véhicule : {response.ReasonPhrase}");
                }
            }
        }

        private async Task DeleteVehicule(string numeroVehicule)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync($"vehicule_API/{numeroVehicule}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Erreur lors de la suppression du véhicule : {response.ReasonPhrase}");
                }
            }
        }

        private void ImmatriculationTextBoxEditVehicule_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void ImmatriculationTextBoxEditVehicule_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox immatriculationTextBox = (TextBox)sender;
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private static bool IsInputValidEditVehicule_Immatriculation(string text)
        {
            Regex regex = new Regex("^[A-Z0-9-]+$"); // regex pour autoriser uniquement les lettres majuscules, chiffres et le caractère "-"
            return regex.IsMatch(text);
        }


        #endregion
        private bool AreFieldsEmpty()
        {
            return string.IsNullOrWhiteSpace(NumeroTextBox.Text) || string.IsNullOrWhiteSpace(ImmatriculationTextBox.Text);
        }

        private static bool IsNumber(string text)
        {
            Regex regex = new Regex("[^0-9]+"); // Expression régulière pour autoriser uniquement les chiffres
            return regex.IsMatch(text);
        }
    }
}
