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
using BICE.WPF.Pages;

namespace BICE_WPF
{
    public partial class MainWindow : Window
    {
        private List<string> importedData = new List<string>();
        private Material_DTO? materialDto;

        public MainWindow()
        {
            InitializeComponent();
            InitializePages();
        }

        private void InitializePages()
        {
            var tabItem1 = new TabItem
            {
                Header = "Import Materiel",
                Content = new ImportMateriel()
            };

            var tabItem2 = new TabItem
            {
                Header = "Retour Intervention",
                Content = new RetourIntervention()
            };

            var tabItem3 = new TabItem
            {
                Header = "Page 3",
                Content = new Vehicule()
            };

            // Ajouter les pages au TabControl
            TabControl mainTabControl = new TabControl();
            mainTabControl.Items.Add(tabItem1);
            mainTabControl.Items.Add(tabItem2);
            mainTabControl.Items.Add(tabItem3);

            // Ajouter le TabControl à la fenêtre principale
            this.Content = mainTabControl;
        }
    }
}
//todo : pour la mise a jour du materiel en perdu, pour le differentiel 2 post par simon et
 //todo : moi je fais differentiel via comparaison et faire un update
 //todo : avec une liste materieldto et tant que pas de reponse pas possible de fermer le wpf
 ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
 //todo :  autre methode : insertion des 2 listes dans wpf puis envoyer une
 //todo : fois que les 2 listes sont ok avec le meme numero de vehicule (securite check) appui
 //todo : bouton confirmer et envoie sur 3 routes distinctes pour materiel utilisé, materiel non utilisé et
 //todo : enfin numéro de vehicule et api fais le reste (le materiel restant dans le vehicule est jeté).