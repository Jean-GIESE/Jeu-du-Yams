# Jeu du Yam's

- Membres du goupe : [Jean GIESE](https://git.unistra.fr/jgiese), [Abdullah NEZAMI](https://git.unistra.fr/nezami), [Thomas BARSEGHIAN](https://git.unistra.fr/tbarseghian)

Étant donné que le jeu n'ait pas été partagé via git, aucun commit n'est présent.

## Installation / lancement du jeu

Ouvrez un terminal et mettez-vous dans un répertoire où vous placerez le projet

### Mise en place du dépôt Git

1. Installer git sur votre ordinateur personnel (rien à faire si vous avez déjà Git) :
```sh
$ sudo apt install git
```

2. Configurer vos informations d'utilisateur :
```sh
$ git config --global user.name "[Prenom] [Nom]"
$ git config --global user.email "[email]"
```

3. Cloner ce dépôt sur votre ordinateur personnel :
```sh
$ git clone https://github.com/Jean-GIESE/Jeu-du-Yams.git
```
Si vous n'avez pas le language C# sur votre ordinateur, installez-le.
De même pour l'HTML, CSS et JavaScript.

### Lancement de la simulation

Allez dans le répertoire `Jeu-du-Yams` et tapez la commande `./yams`
Le jeu se lancera alors dans votre terminal et un partie commencera

Une fois la partie terminée, un fichier .json apparaîtra (yams.json) il faudra convertir ce json en code via le site `https...` afin de pouvoir voir les statistiques sur la page html.
La page html se situe dans le répertoire `Yams_Web`, elle se nomme **index.html**. Ouvrez la page sur votre navigateur et insérez le code fournit via le .json pour voir le déroulé de la partie.

## Description
Le but est de reproduire le jeu du Yam's ([Description du jeu](https://www.agoralude.com/blog/-la-regle-du-yams-ou-du-jeu-du-yahtzee-n43)) en C# et de faire un visuel du résultat sur une page web.
Pour plus d'information sur le code, voir le document **[Description.pdf](https://github.com/Jean-GIESE/Jeu-du-Yams/blob/master/Description.pdf)**.

### Captures d'écran

A compléter
