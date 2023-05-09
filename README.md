## Routes
### Materiels

* GET /materiel_API/ : Liste tout les 
* GET /materiel_API/AJeter : Liste tout les materiels à jeter
*    POST /materiel_API/ : Envoie de la liste du materiel du stock
*   POST /materiel_API/updateVehicule/{numeroVehicule} : Envoie de la liste du materiel dans le vehicule dont le numeros est {numeroVechicule}
*    POST /materiel_API/usedMateriel : Envoie de la liste du materiel utilisé au retour d'intervention
*    POST /materiel_API/notUsedMateriel : Envoie de la liste du materiel non utilisé au retour d'intervention
*    GET /materiel_API/LostMaterial/{numeroVehicule} : Met tout les materiels presentn dans le vehicule {numeroVehicule} en etat "perdu"

### Vehicules

*    GET /vehicule_API/ : Liste tout les vehicules
*    GET /vehicule_API/{numeroVehicule} : Suprime le vehicue {numeroVehicule}
*    POST /vehicule_API/ : Ajout d'un vehicule
*    POST /vehicule_API/updateVehicule/{numeroVehicule} : Envoi d'un vehicule DTO et le remplace parle vehicule {numeroVehicule} (modifier)