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
                Header = "Vehicule",
                Content = new Vehicule()
            };
            
            var tabItem4 = new TabItem
            {
                Header = "Materiel",
                Content = new Materiel()
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
//todo :  autre methode : insertion des 2 listes dans wpf puis envoyer une
//todo : fois que les 2 listes sont ok avec le meme numero de vehicule (securite check) appui
//todo : bouton confirmer et envoie sur 3 routes distinctes pour materiel utilisé, materiel non utilisé et
//todo : enfin numéro de vehicule et api fais le reste (le materiel restant dans le vehicule est jeté).


//import de materiel - import materiel



//    Exporter en format csv la liste de smateriel du stcok - materiel
//Voir les materiels - materiel
//Ajouter une liste de materiel du stock - materiel



//Ajouter une liste de materiel utilisé au retour d'intervention - retour interv
//    Ajouter une liste de materiel NON utilisé au retour d'intervention - retour interv
//    Voir la liste des materiel a jetter - retour interv
//    Exporter la liste des materiel a jetter - retour interv





//    Ajouter un vehicule - vehicule
//modifier - vehicule
//suprimer - vehicule
//Ajouter une liste de materiel dans un vehicule - vehicule
//Voir les materiel dans un vehicule - vehicule

