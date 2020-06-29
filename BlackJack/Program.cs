using System;
using BlackJackLib;

namespace BlackJack
{
    class Program
    {
        static void NewLine() => Console.WriteLine();
        public static Deck deck = new Deck();                                                       //Creating new deck
        public static Hand playerHand = new Hand();                                                 //Creating players hand
        public static Hand dealerHand = new Hand();                                                 //Creating dealers hand
        static void Main(string[] args)
        {
            Console.WriteLine("*****BLACKJACK*****");

            bool play = true;                                                                       //Entering game loop
            do
            {

                AddingCardsToDeck();                                                                //Adding cards to deck
                PlayerStartingHand();                                                               //Adding players starting cards
                DealerStartingHand();                                                               //Adding dealers starting cards

                bool game = true;                                                                   //Main game logic loop       
                while (game == true)
                {
                    bool validInput = false;
                    while (validInput == false)                                                     //Checking if user input is valid. If user unput is invalid, the loop will continue until it's valid
                    {

                        Console.WriteLine($"Your cards: {playerHand.CardsInHand()}");               //Presenting cards in players hand
                        Console.WriteLine($"Total Value: {playerHand.HandValue()}");                //Presenting players hand value
                        NewLine();
                        Console.WriteLine($"Dealers cards: {dealerHand.CardsInHand()}");            //Presenting cards in dealers hand
                        Console.WriteLine($"Total Value: {dealerHand.HandValue()}");                //Presenting dealers hand value
                        NewLine();

                        Menu();                                                                     //Print game Menu with possible choices
                        string decision = Console.ReadLine().Trim().ToLower();                      //getting user input
                        Console.Clear();

                        switch (decision)                                                           //Action taken, depending on users choice
                        {

                            //Hit
                            case "h":
                                validInput = true;
                                playerHand.hand.Add(deck.GetCard());                                //Adding card to players deck
                                Console.WriteLine($"You got: {playerHand.LastCardInHand().Name}");  //Presenting players new card
                                if (playerHand.BustCheck() == true)                                 //Checking for Bust
                                {
                                    Console.WriteLine($"You Busted!");
                                    PrintResults();
                                    game = false;
                                    break;
                                }
                                break;

                            //Stay
                            case "s":
                                validInput = true;
                                game = false;                                                       //Stoping game logic loop, because game ends after player decides to stay with his hand
                                DealerPlay();                                                       //Simulating Dealers "logic"
                                if (dealerHand.BustCheck() == true)                                 //Checking for dealers Bust
                                {
                                    Console.WriteLine("Dealer Busted!");
                                    PrintResults();
                                    break;
                                }
                                ResultCheck();                                                      //Checking game result and comparing hands
                                break;
                        }
                        
                    }
                }

                play = PlayAgain();                                                                 //Asking user for next deal
                Console.Clear();
            }
            while (play == true);
        }

        private static void PlayerStartingHand()
        {
            playerHand.hand.Add(deck.GetCard());
            playerHand.hand.Add(deck.GetCard());
        }
        private static void DealerStartingHand()
        {
            dealerHand.hand.Add(deck.GetCard());
            dealerHand.hand.Add(deck.GetCard());
        }
        private static void AddingCardsToDeck()
        {
            deck.AddNewCard("2", 2);
            deck.AddNewCard("3", 3);
            deck.AddNewCard("4", 4);
            deck.AddNewCard("5", 5);
            deck.AddNewCard("6", 6);
            deck.AddNewCard("7", 7);
            deck.AddNewCard("8", 8);
            deck.AddNewCard("9", 9);
            deck.AddNewCard("10", 10);
            deck.AddNewCard("Jack", 10);
            deck.AddNewCard("Queen", 10);
            deck.AddNewCard("King", 10);
            deck.AddNewCard("Ace", 1);
        }
        private static void Menu()
        {
            Console.WriteLine("What's your decision?");
            Console.WriteLine("[H]it!");
            Console.WriteLine("[S]tay!");
            Console.Write("Decision: ");
        }
        private static void DealerPlay()
        {
            while (playerHand.HandValue() > dealerHand.HandValue() && dealerHand.HandValue() < 15)
            {
                dealerHand.hand.Add(deck.GetCard());
                Console.WriteLine($"Dealer got: {dealerHand.LastCardInHand().Name}");
            }

        }

        private static void ResultCheck()
        {
            if (playerHand.HandValue() > dealerHand.HandValue())
            {
                Console.WriteLine($"Congratulations! You Won!");
                PrintResults();
            }
            else if (playerHand.HandValue() < dealerHand.HandValue())
            {
                Console.WriteLine($"Unlucky! You Lost!");
                PrintResults();
            }
            else
            {
                Console.WriteLine($"Draw!");
                PrintResults();
            }
        }

        private static bool PlayAgain()
        {
            bool again = true;
            bool validInput = false;

            while (validInput == false)
            {
                Console.WriteLine("Would you like to play again? ");
                Console.WriteLine("[Y]es");
                Console.WriteLine("[N]o");
                Console.Write("Decision: ");
                string decision = Console.ReadLine().Trim().ToLower();
                switch (decision)
                {
                    case "y":
                        again = true;
                        validInput = true;
                        deck.cards.Clear();
                        playerHand.hand.Clear();
                        dealerHand.hand.Clear();
                        break;

                    case "n":
                        Console.WriteLine("See you next time!");
                        again = false;
                        validInput = true;
                        break;
                }
                Console.Clear();
            }

            return again;
        }

        private static void PrintResults()
        {
            Console.WriteLine("*********************************************");
            Console.WriteLine($"Your final cards: {playerHand.CardsInHand()}\nYour final score: {playerHand.HandValue()}");
            Console.WriteLine("*********************************************");
            Console.WriteLine($"Dealers final cards: {dealerHand.CardsInHand()}\nDealers final score: {dealerHand.HandValue()}\n");
        }
    }
}
