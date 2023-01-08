# 3BA-Ecosysteme
## Diagramme de Classe
![Diagramme Classe](https://github.com/OussamaSahib/3BA-Ecosysteme/blob/master/Diagramme%20Classe%20-IMAGE.png)


## Diagramme de Séquence
![Diagramme Séquence](https://github.com/OussamaSahib/3BA-Ecosysteme/blob/b8b434ecd086515bd37b493412d01c8df5edadec/Diagramme%20S%C3%A9quence%20-IMAGE.png)


## Principes SOLID
**Dans le projet, nous pouvons analyser 2 principes SOLID:**


1)Le 1er principe est: **"Open/Closed Principle (=OCP)"**.<br> Ce principe consiste au fait qu'une classe ou un module doit être ouvert pour l'extension, mais fermé pour la modification. Cela signifie que nous devrions être en mesure d'ajouter de nouvelles fonctionnalités à une classe sans avoir à modifier son code existant. Dans notre projet par exemple, si nous voulons ajouter de nouvelles propriétés ou méthodes à la classe "SimulationObjet", nous devrions créer une nouvelle classe dérivée plutôt que de modifier la classe de base. Cela nous permet de maintenir la stabilité et la fiabilité du code existant tout en permettant l'extension de ses fonctionnalités.


2)Le 2ème principe est: **"Liskov Substitution Principle (=LSP)"**.<br> Dans notre projet, il est appliqué dans la définition de la classe "EtreVivant" qui hérite de "SimulationObjet". En effet, la classe "EtreVivant" étend la classe de base "SimulationObjet" en ajoutant des comportements spécifiques aux êtres vivants, tels que le fonctionnement Energie-Vie . Cependant, en utilisant le principe de substitution de Liskov, nous pouvons être sûrs que tout objet de type "EtreVivant" peut être utilisé à la place d'un objet de type "SimulationObjet", sans causer de problèmes de compatibilité ou de fonctionnement. Cela signifie que nous pouvons utiliser des objets de type "EtreVivant" là où nous attendons des objets de type "SimulationObjet", en étant sûrs qu'ils se comporteront de manière prévue.
