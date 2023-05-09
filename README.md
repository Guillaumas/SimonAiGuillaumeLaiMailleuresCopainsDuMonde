## Routes
### Materiels

* GET /materiel_API/ : Liste tout les materiel
* GET /materiel_API/AJeter : Liste tout les materiels à jeter
* GET /materiel_API/AController : Liste tout les materiels à controller
* GET /materiel_API/Stock : Liste tout les materiels dans le stock
* POST /materiel_API/ : Envoie de la liste du materiel du stock
* POST /materiel_API/updateVehicule/{numeroVehicule} : Envoie de la liste du materiel dans le vehicule dont le numeros est {numeroVechicule}
* POST /materiel_API/usedMateriel : Envoie de la liste du materiel utilisé au retour d'intervention
* POST /materiel_API/notUsedMateriel : Envoie de la liste du materiel non utilisé au retour d'intervention
* GET /materiel_API/LostMaterial/{numeroVehicule} : Met tout les materiels presentn dans le vehicule {numeroVehicule} en etat "perdu"

### Vehicules

* GET /vehicule_API/ : Liste tout les vehicules
* DELETE /vehicule_API/{numeroVehicule} : Suprime le vehicue {numeroVehicule}
* POST /vehicule_API/ : Ajout d'un vehicule
* PUT /vehicule_API/{numeroVehicule} : Envoi d'un vehicule DTO et le remplace parle vehicule {numeroVehicule} (modifier)