using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
class main
{
    // Structure pour stocker des informations concernant le joueur
    public struct Player
    {
        public int id;
        public string pseudo;
        public int scoreFinal;
        public int bonus;
    }

    // Structure pour stocker chaque Round
    public struct ResultatsParRound
    {
        public int id_players;
        public int[] dice;
        public string challenge;
        public int score;
    }
    // Structure pour stocker tout les Rounds
    public struct Rounds
    {
        public int id;
        public ResultatsParRound[] results;
    }
    // Tableaux de 3 dimension pour stocker les challenges et enlever les challenges pour chaque joueur
    public static string[,,] challengesMin = new string[2, 6, 3] {
   { // Joueur 1 Min Challenges
       {"1 : Nombre de 1", "Obtenir le maximum de 1 ", "Somme des dés ayant obtenu 1"},
       {"2 : Nombre de 2", "Obtenir le maximum de 2 ", "Somme des dés ayant obtenu 2"},
       {"3 : Nombre de 3", "Obtenir le maximum de 3 ", "Somme des dés ayant obtenu 3"},
       {"4 : Nombre de 4", "Obtenir le maximum de 4 ", "Somme des dés ayant obtenu 4"},
       {"5 : Nombre de 5", "Obtenir le maximum de 5 ", "Somme des dés ayant obtenu 5"},
       {"6 : Nombre de 6", "Obtenir le maximum de 6 ", "Somme des dés ayant obtenu 6"}
   },
   { // Joueur 2 Min Challenges
       {"1 : Nombre de 1", "Obtenir le maximum de 1 ", "Somme des dés ayant obtenu 1"},
       {"2 : Nombre de 2", "Obtenir le maximum de 2 ", "Somme des dés ayant obtenu 2"},
       {"3 : Nombre de 3", "Obtenir le maximum de 3 ", "Somme des dés ayant obtenu 3"},
       {"4 : Nombre de 4", "Obtenir le maximum de 4 ", "Somme des dés ayant obtenu 4"},
       {"5 : Nombre de 5", "Obtenir le maximum de 5 ", "Somme des dés ayant obtenu 5"},
       {"6 : Nombre de 6", "Obtenir le maximum de 6 ", "Somme des dés ayant obtenu 6"}
   }
};
    public static string[,,] challengesMaj = new string[2, 7, 3] {
   { // Joueur 1 Maj Challenges
       {"7  : Brelan", "Obtenir 3 dés de même valeur", "Somme des 3 dés identiques"},
       {"8  : Carre", "Obtenir 4 dés de même valeur", "Somme des 4 dés identiques"},
       {"9  : Full", "Obtenir 3 dés de même valeur + 2 dés de même valeur", "25 points"},
       {"10 : Petite suite", "Obtenir 1-2-3-4 ou 2-3-4-5 ou 3-4-5-6", "30 points"},
       {"11 : Grande suite", "Obtenir 1-2-3-4-5 ou 2-3-4-5-6", "40 points"},
       {"12 : Yams", "Obtenir 5 dés de même valeur", "50 points"},
       {"13 : Chance", "Obtenir le maximum de points", "Le total des dés obtenus"}
   },
   { // Joueur 2 Maj Challenges
       {"7  : Brelan", "Obtenir 3 dés de même valeur", "Somme des 3 dés identiques"},
       {"8  : Carre", "Obtenir 4 dés de même valeur", "Somme des 4 dés identiques"},
       {"9  : Full", "Obtenir 3 dés de même valeur + 2 dés de même valeur", "25 points"},
       {"10 : Petite suite", "Obtenir 1-2-3-4 ou 2-3-4-5 ou 3-4-5-6", "30 points"},
       {"11 : Grande suite", "Obtenir 1-2-3-4-5 ou 2-3-4-5-6", "40 points"},
       {"12 : Yams", "Obtenir 5 dés de même valeur", "50 points"},
       {"13 : Chance", "Obtenir le maximum de points", "Le total des dés obtenus"}
   }
};

    public static void initialiserPlayers(Player[] players)
    {
        /* 
        Algorithme:initialiserPlayers
        Idée: Initialiser le joueur en lui demandant son nom et en lui attribuant un identifiant unique
        Entrée: Player[] players: Tableux de structure player
        Local: nom : chaine de caractere 
        Sortie: void 
        */
        string nom;
        // Jouer 1
        Console.Write("👤 Entrez le nom du joueur 1: ");
        nom = Console.ReadLine();
        players[0].pseudo = nom;
        players[0].id = 1;
        // Jouer 2
        Console.Write("👤 Entrez le nom du joueur 2: ");
        nom = Console.ReadLine();
        players[1].pseudo = nom;
        players[1].id = 2;
    }

    public static void afficherTab(int[] tab)
    //Affiche un tableau sous forme d'entier
    {
        for (int i = 0; i < tab.Length; i++)
        {
            Console.Write("{0} ", tab[i]);
        }
        Console.WriteLine();
    }


    /*Fonction qui sert à afficher un tableau d’entier sous forme de dés en utilisant un dictionnaire qui relie 
    un entier à une Liste de chaine de caractères*/
    public static void afficherTabDe(int[] tab)
    //Affiche un tableau sous forme de dés
    {
        Dictionary<int, List<string>> dicoDe = new Dictionary<int, List<string>>
        {
            {1, new List<string> {"┌─────────┐","│         │","│    O    │","│         │","└─────────┘"}},
            {2, new List<string> {"┌─────────┐","│ O       │","│         │","│       O │","└─────────┘"}},
            {3, new List<string> {"┌─────────┐","│ O       │","│    O    │","│       O │","└─────────┘"}},
            {4, new List<string> {"┌─────────┐","│ O     O │","│         │","│ O     O │","└─────────┘"}},
            {5, new List<string> {"┌─────────┐","│ O     O │","│    O    │","│ O     O │","└─────────┘"}},
            {6, new List<string> {"┌─────────┐","│ O     O │","│ O     O │","│ O     O │","└─────────┘"}},
        };
        Console.WriteLine();
        for (int j = 0; j < 5; j++)
        {
            for (int i = 0; i < tab.Length; i++)
            {
                Console.Write("{0} ", dicoDe[tab[i]][j]);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    // lance De
    public static void lanceDe(int len, int[] tabDe)
    //Fonction qui rempli le tableau reçu en paramètre de len element entre 1 et 7 (non inclus)
    {
        Random random = new Random();
        for (int i = 0; i < len; i++)
            tabDe[i] = random.Next(1, len + 2);
    }

    //  Relance De
    

    /*Fonction qui prend le tableau d’entiers en paramètre (le tableau de dés) et remplace les dés que
    le joueur souhaite relancer par des 0 (facilite la tâche pour la fonction lanceEtFusionne)
    De multiples try-catch sont utilisé pour gérer les erreurs de saisies*/
    public static void relance(int[] tabDe)
    {
        string des = "";
        bool isCorrect = false;
        List<int> tempDecList = new List<int>();
        while (isCorrect == false)
        {
            tempDecList.Clear();
            Console.Write("Entrez les valeurs des dés à garder (0 pour ne rien garder) : ");
            des = Console.ReadLine();
            int temp = 0;
            try
            {
                foreach (char c in des.Replace(" ", "").Replace("\t", ""))
                {
                    temp = int.Parse(c.ToString());
                    tempDecList.Add(temp);
                }
                isCorrect = true;
            }
            catch (FormatException fe)
            {
                Console.WriteLine("Entrée incorrecte: " + fe.Message);
                tempDecList.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite: " + ex.Message);
                tempDecList.Clear();
            }
            if (tempDecList.Count > 5)
            {
                Console.WriteLine("Entrée incorrecte: plus valuer que possible");
                tempDecList.Clear();
                isCorrect = false;
            }
            if (des == "0")
            {
                for (int i = 0; i < tabDe.Length; i++)
                {
                    tabDe[i] = 0;
                }
                lanceEtFusionne(tabDe);
            }
            if (isCorrect == true && des != "0")
            {
                int i = 0;
                while (isCorrect == true && i < tempDecList.Count)
                {
                    bool isValid = false;
                    int j = 0;
                    while (j < 5 && isValid == false)
                    {
                        if (tempDecList[i] == tabDe[j])
                            isValid = true;
                        j++;
                    }
                    if (isValid == false)
                    {
                        Console.WriteLine("Entrée incorrecte: Vous n'avez pas " + tempDecList[i] + " dé");
                        isCorrect = false;
                    }
                    i++;
                }
            }
        }
        for (int i = 0; i < 5; i++)
        {
            if (!tempDecList.Contains(tabDe[i]))
                tabDe[i] = 0;
            else
                tempDecList.Remove(tabDe[i]);
        }
        lanceEtFusionne(tabDe);
    }
    // fusionne les dés relancé dans un tableau
    public static void lanceEtFusionne(int[] tabDe)
    //Les 0 dans le tableau (=les dés qu'on souhaite relancer) sont remplacé par des chiffres entre 1 et 6 
    {
        Random random = new Random();
        for (int i = 0; i < 5; i++)
        {
            if (tabDe[i] == 0)
                tabDe[i] = random.Next(1, 7);
        }
    }

    /*La fonction coeur du programme, startGame initialise les variables clés et exécute les 13 rounds en gérant l’affichage 
    en console des interfaces utilisateurs, des challenges et des dés. Cette fonction en appelle d’autres comme afficherChallenges, 
    lanceDe, afficheTabDe, caseChecker et relance.*/
    public static void startGame(Rounds[] rounds, Player[] players)
    {
        int[] tabDe = new int[5];
        bool garder = false;
        string garderOuiNon;
        int roundeNo = 1;
        for (int i = 0; i < 13; i++)
        {
            rounds[i].id = roundeNo;
            roundeNo++;
            rounds[i].results = new ResultatsParRound[2];
            Console.WriteLine();
            Console.WriteLine(" ═══════════ 🕒 Round " + rounds[i].id + " 🕒 ═══════════ ");
            for (int j = 0; j < 2; j++)
            {
                Console.WriteLine();
                Console.WriteLine($"═══════════ 🎮 Joueur: {players[j].pseudo} ═══════════");
                afficherChallenges(j);
                lanceDe(5, tabDe);
                Console.WriteLine();
                Console.Write("═══════════ 1er Lancer ═══════════ ");
                afficherTabDe(tabDe);
                int k = 2;
                garder = false;
                while (k <= 3 && garder == false)
                {
                    Console.WriteLine("✅ Voulez-vous garder ces dés ? ");
                    Console.Write("Tapez « oui » pour les garder, « non » pour les relancer: ");
                    garderOuiNon = Console.ReadLine();
                    if (caseChecker(garderOuiNon) == "OUI")
                        garder = true;
                    else if (caseChecker(garderOuiNon) == "NON")
                    {
                        relance(tabDe);
                        Console.Write(k == 2 ? "═══════════ 2eme Lancer ═══════════" : "═══════════ 3eme Lancer ═══════════");
                        afficherTabDe(tabDe);
                        k++;
                    }
                    else
                        Console.WriteLine("Ecrire 'oui' ou 'non'");
                }
                choixChallenge(rounds, players, tabDe, j, i);
            }
            Console.WriteLine($"Fin du round {rounds[i].id}");
            Console.WriteLine("═══════════════════════════════════════════════════════");

        }
    }
    public static void afficherChallenges(int player)
    // Fonction qui affiche les challenges restant du joueur 
    {
        // Affichage de Challenge min
        Console.WriteLine();
        if (challengesMin[player, 0, 0] != "")
            Console.WriteLine("═══════════ Challenges Mineurs ═══════════");
        for (int i = 0; i < 6; i++)
            if (challengesMin[player, i, 0] != "")
                Console.WriteLine($"{challengesMin[player, i, 0],-20} {challengesMin[player, i, 1],-56} {challengesMin[player, i, 2],-15}");
        // Affichage de Challenge maj
        Console.WriteLine();
        if (challengesMaj[player, 0, 0] != "")
            Console.WriteLine("═══════════ Challenges Majeurs ═══════════");
        for (int i = 0; i < 7; i++)
            if (challengesMaj[player, i, 0] != "")
                Console.WriteLine($"{challengesMaj[player, i, 0],-20} {challengesMaj[player, i, 1],-56} {challengesMaj[player, i, 2],-15}");
    }
    // Choix du challenge
    public static void choixChallenge(Rounds[] rounds, Player[] players, int[] tabDe, int player, int round)
    // S'occupe de toute la partie du choix du challenge
    {
        bool trouve = false;
        string choix = "";
        string typeChoix = "";
        int j = 0;
        string[] numChallenge = new string[2];
        Console.WriteLine();
        while (trouve == false)
        {
            Console.Write("Ecrivez le numéro du challenge que vous souhaitez choisir: ");
            choix = Console.ReadLine();
            string option;
            // check mineure ou majeur
            j = 0;
            while (trouve == false && j < 13)
            {
                if (j < 6)
                {
                    option = challengesMin[player, j, 0].Trim();
                    typeChoix = "min";
                }
                else
                {
                    option = challengesMaj[player, j - 6, 0].Trim();
                    typeChoix = "maj";
                }
                numChallenge = option.Split(':');
                option = numChallenge[0].Trim();
                if (choix != "" && choix == option)
                {
                    trouve = true;
                }
                j++;
            }
        }
        removeChallenge(j - 1, typeChoix, player);

        rounds[round].results[player] = new ResultatsParRound
        {
            id_players = player + 1,
            challenge = numChallenge[1],
            dice = (int[])tabDe.Clone(),
            score = scoreCalcul(choix, typeChoix, tabDe)
        };
        if (typeChoix == "min")
            players[player].bonus += rounds[round].results[player].score;
        players[player].scoreFinal += rounds[round].results[player].score;
    }

    /*Appelle getChallengesRestants pour connaître le nombre de challenge restant du même type que celui qu’on souhaite effacer. 
    On écrase ensuite le challenge qu’on souhaite enlever en déplaçant toutes les lignes par 1. On remplace ensuite 
    la dernière ligne par une chaîne vide pour qu’elle ne soit pas affichée au prochain tour.*/
    public static void removeChallenge(int index, string typeChoix, int player)
    {
        int len = getChallengesRestants(player, typeChoix);
        if (typeChoix == "min")
        {
            for (int i = index; i < len - 1; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    challengesMin[player, i, j] = challengesMin[player, i + 1, j];
                }
            }

            for (int j = 0; j < 3; j++)
            {
                challengesMin[player, len - 1, j] = "";
            }
        }
        else
        {
            for (int i = index - 6; i < len - 1; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    challengesMaj[player, i, j] = challengesMaj[player, i + 1, j];
                }
            }

            for (int j = 0; j < 3; j++)
            {
                challengesMaj[player, len - 1, j] = "";
            }
        }
    }
    public static int getChallengesRestants(int player, string typeChoice)
    //Prends en argument le numéro du joueur et un type de challenge et renvoie le nombre restant de challenges de ce type pour ce joueur
    {
        int count = 0;
        int challengesCount = 0;
        if (typeChoice == "min")
            challengesCount = 6;
        else
            challengesCount = 7;
        for (int i = 0; i < challengesCount; i++)
        {
            if (typeChoice == "min")
            {
                if (!string.IsNullOrWhiteSpace(challengesMin[player, i, 0]))
                    count++;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(challengesMaj[player, i, 0]))
                    count++;
            }
        }
        return count;
    }

    /*verifie si les dés apparaissent le bon nombre de fois dans le tableau et si c’est le cas il distribue le nombre de points nécessaire
	Entrée : tabDe : tableau d’entier (contient les dés du joueur)
		Times : entier representant le nombre de fois qu’on souhaite retrouver 
	Local : number : le chiffre qui apparaît le nombre de fois nécessaire 
		I : compteur
	Sortie : times * number (correspond au nombre de points à distribuer)*/

    public static int sommeMemeNombre(int[] tabDe, int times)
    {
        int number = 0;
        for (int i = 0; i < 3; i++) // On n'a que besoin d'aller jusqu'à trois car la plus petite somme necessaire est sur le Brelan et s'il y en a en effet 3 meme chiffre alors il y en a au moins 1 sur les 3 premieres case du tableau
        {
            if (compterDe(tabDe, tabDe[i]) >= times)
                number = tabDe[i];
        }
        return (times * number);
    }

    //compte le nombre d'occurrence du dé souhaité dans le tableau de dé reçu en paramètre
    public static int compterDe(int[] tabDe, int num)
    {
        int compteur = 0;
        for (int i = 0; i < 5; i++)
        {
            if (tabDe[i] == num)
            {
                compteur++;
            }
        }
        return compteur;
    }
    /*Appelle les fonctions nécessaires en fonction du challenge choisi pour vérifier si le challenge est validé.
    Si le challenge est validé, la fonction accorde les points gagnés au joueur. Il renvoie ensuite le score gagné par le joueur.*/
    public static int scoreCalcul(string index, string typeChoice, int[] tabDe)
    {
        int score = 0;
        bool isTrue = false;
        int indexInt = int.Parse(index);
        switch (index)
        {
            case "1":
                score = (indexInt) * compterDe(tabDe, indexInt);
                break;
            case "2":
                score = (indexInt) * compterDe(tabDe, indexInt);
                break;
            case "3":
                score = (indexInt) * compterDe(tabDe, indexInt);
                break;
            case "4":
                score = (indexInt) * compterDe(tabDe, indexInt);
                break;
            case "5":
                score = (indexInt) * compterDe(tabDe, indexInt);
                break;
            case "6":
                score = (indexInt) * compterDe(tabDe, indexInt);
                break;
            case "7":
                score = sommeMemeNombre(tabDe, 3);
                break;
            case "8":
                score = sommeMemeNombre(tabDe, 4);
                break;
            case "9":
                isTrue = false;
                isTrue = fullChallenge(tabDe);
                if (isTrue)
                    score = 25;
                break;
            case "10":
                isTrue = false;
                isTrue = petiteSuite(tabDe);
                if (isTrue)
                    score = 30;
                break;
            case "11":
                isTrue = false;
                isTrue = grandeSuite(tabDe);
                if (isTrue)
                    score = 40;
                break;
            case "12":
                score = compterDe(tabDe, tabDe[0]);
                if (score == 5)
                    score = 50;
                else
                    score = 0;
                break;
            case "13":
                for (int j = 0; j < 5; j++)
                    score += tabDe[j];
                break;
            default:
                score = 0;
                break;
        }
        return score;
    }

    //tri Insertion pour un tableau
    public static void triTab(int[] triTab)
    {
        int temp;
        for (int i = 1; i < triTab.Length; i++)
        {
            if (triTab[i - 1] > triTab[i])
            {
                for (int j = i; j > 0; j--)
                {
                    if (triTab[j - 1] > triTab[j])
                    {
                        temp = triTab[j - 1];
                        triTab[j - 1] = triTab[j];
                        triTab[j] = temp;
                    }
                }
            }
        }
    }
    // Full challenge
    public static bool fullChallenge(int[] tabDe)
    //Vérifie si le tableau donnée en paramètre vérifie les requis pour le challenge full (= contient 3 fois un même élément et 2 fois un autre)
    {
        int triple = 0;
        int doub = 0;       //On ne peut pas l'appeler double
        int temp = 0;
        bool isTrue = false;
        for (int i = 0; i < 5; i++)
        {
            temp = compterDe(tabDe, tabDe[i]);
            if (temp == 3)
                triple = 3;
            if (temp == 2)
                doub = 2;
        }
        if (triple == 3 && doub == 2)
            isTrue = true;
        return (isTrue);
    }
    // Petite Suite
    public static bool petiteSuite(int[] tabDe)
    //utilise triTab pour trier le tableau par ordre croissant et ensuite regarde si le tableau des dés contient 4 chiffres qui se suivent
    {
        bool isTrue = false;

        triTab(tabDe);

        int oneToFour = 1;
        int tempOneToFour = 0;


        int twoToFive = 2;
        int tempTwoToFive = 0;


        int threeToSiTx = 3;
        int tempThreeToSix = 0;


        for (int i = 0; i < 5; i++)
        {
            if (oneToFour == tabDe[i] && tempOneToFour < 4)
            {
                tempOneToFour++;
                oneToFour++;
            }
            if (twoToFive == tabDe[i] && tempTwoToFive < 4)
            {
                twoToFive++;
                tempTwoToFive++;
            }
            if (threeToSiTx == tabDe[i] && tempThreeToSix < 4)
            {
                threeToSiTx++;
                tempThreeToSix++;
            }
        }
        if (tempOneToFour == 4 || tempTwoToFive == 4 || tempThreeToSix == 4)
            isTrue = true;
        return isTrue;
    }

    // Grande Suite
    public static bool grandeSuite(int[] tabDe)
    // Fonction quasi identique à petiteSuite, elle utilise triTab pour trier le tableau par ordre croissant et ensuite regarde si le tableau des dés contient 5 chiffres qui se suivent
    {
        bool isTrue = false;

        triTab(tabDe);

        int oneToFive = 1;
        int tempOneToFive = 0;


        int twoToSix = 2;
        int tempTwoToSix = 0;


        for (int i = 0; i < 5; i++)
        {
            if (oneToFive == tabDe[i] && tempOneToFive < 5)
            {
                tempOneToFive++;
                oneToFive++;
            }
            if (twoToSix == tabDe[i] && tempTwoToSix < 5)
            {
                twoToSix++;
                tempTwoToSix++;
            }
        }
        if (tempOneToFive == 5 || tempTwoToSix == 5)
            isTrue = true;
        return isTrue;
    }
    // Convertisseur de casse   lowerCase => upperCase

    /*Convertisseur de casse pour tout transformer en majuscule, utile pour corriger des erreurs de saisie, 
    elle est utilisée lorsque l’utilisateur indique s’il veut relancer ses dés ou non. Dès que la fonction détecte 
    une lettre en minuscule elle lui enlève 32 pour avoir l'équivalent en majuscule sur la table ASCII.*/
    public static string caseChecker(string str)
    {
        char[] charTab = str.ToCharArray();
        int len = charTab.Length;
        for (int i = 0; i < len; i++)
        {
            if (charTab[i] >= 'a' && charTab[i] <= 'z')
                charTab[i] = (char)(charTab[i] - 32);
        }
        return new string(charTab);
    }
    
    // La partie JSON
    
    //Fonction utilisée pour créer le fichier JSON et le remplir des informations récoltées durant la partie.
    static void fichierJSON(Player[] players, Rounds[] rounds)
    {
        string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
        //les paramètres
        string parametreJ = "{\n \"parameters\":{\n      \"code\":\"groupe1-TP5\",\n     \"date\":\"" + currentDate + "\"\n   },\n";
        StreamWriter json = new StreamWriter("yams.json");
        json.Write(parametreJ); // Écrire la chaîne dans le fichier

        //affichage des joueurs
        string playersJ = $" \"players\": [\n    {{\n        \"id\": 1,\n        \"pseudo\": \"{players[0].pseudo}\"\n   }},\n   {{\n        \"id\": 2,\n        \"pseudo\": \"{players[1].pseudo}\"\n   }}\n    ],\n";
        json.Write(playersJ);

        //affichage des rounds
        string roundsJ = "    \"rounds\": [\n";
        json.Write(roundsJ);

        // tout les rounds dans la boucle
        string roundJ;
        string joueursJ;
        for (int i = 0; i < 13; i++)
        {
            roundJ = $"    {{\n        \"id\": {rounds[i].id},\n        \"results\": [\n";
            json.Write(roundJ);

            for (int j = 0; j < 2; j++)
            {
                joueursJ = $"     {{\n            \"id_player\": {players[j].id},\n           \"dice\": [{rounds[i].results[j].dice[0]}, {rounds[i].results[j].dice[1]}, {rounds[i].results[j].dice[2]}, {rounds[i].results[j].dice[3]}, {rounds[i].results[j].dice[4]}],\n           \"challenge\": \"{rounds[i].results[j].challenge}\",\n          \"score\": {rounds[i].results[j].score}\n       }}";
                json.Write(joueursJ);
                if (j == 0)
                    json.Write(",\n");   //mettre une virgule si on est au joueur 1
                else
                    json.Write("\n");
            }

            json.Write("     ]\n");  //mettre fin au round i

            if (i == 12)
            {
                json.Write(" }\n");  //pas de virgule si on est au dernier round
            }

            else
                json.Write(" },\n");

        }
        json.Write("    ],\n");

        //affichage du resultat finale
        json.Write(" \"final_result\": [\n");
        string resFinale;
        for (int i = 0; i < 2; i++)
        {
            resFinale = $"    {{\n       \"id_player\": {players[i].id},\n       \"bonus\": {players[i].bonus},\n        \"score\": {players[i].scoreFinal}\n    }}";
            json.Write(resFinale);
            if (i == 0)
                json.Write(",\n");   //virgule si c'est le joueur 1
            else
                json.Write("\n");
        }

        json.Write(" ]\n}");

        json.Close();    //obligatoire
    }
    /*La fonction Main affiche certains éléments de l’interface utilisateur sur le terminal, elle initialise aussi 
    des structures clés au programme. Elle appelle ensuite startGame pour lancer les 13 rounds. Une fois startGame 
    fini la fonction Main donne le bonus aux joueurs qui ont rempli les contraintes et enfin il annonce le gagnant 
    de la partie. Et à la toute fin la fonction appelle FichierJSON pour créer le fichier JSON.*/
    static void Main()
    {
        Console.WriteLine();
        Console.WriteLine("═══════════════════════════════════════════════");
        Console.WriteLine("              🎲 Bienvenue au Jeu de Yam's! 🎲");
        Console.WriteLine("═══════════════════════════════════════════════");
        Console.WriteLine();
        Player[] players = new Player[2];
        Rounds[] rounds = new Rounds[13];
        initialiserPlayers(players);
        startGame(rounds, players);
        players[0].bonus = players[0].bonus >= 63 ? 35 : 0;
        players[1].bonus = players[1].bonus >= 63 ? 35 : 0;
        Console.WriteLine();
        Console.WriteLine("Les joueurs :");
        Console.WriteLine();
        Console.WriteLine($"ID: {players[0].id}, Pseudo: {players[0].pseudo}, Score Finale : {players[0].scoreFinal}, Bonus: {players[0].bonus}");
        Console.WriteLine($"ID: {players[1].id}, Pseudo: {players[1].pseudo}, Score Finale : {players[1].scoreFinal}, Bonus: {players[1].bonus}");
        Console.WriteLine();
        if (players[0].scoreFinal < players[1].scoreFinal)
            Console.WriteLine("Bravo! " + players[1].pseudo + " tu as gagné !!!");
        else
            Console.WriteLine("Bravo! " + players[0].pseudo + " tu as gagné !!!");
        Console.WriteLine();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Merci d'avoir joué au Yam's ! À la prochaine !");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        fichierJSON(players, rounds);
    }

}
