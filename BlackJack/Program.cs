using System;
using BlackJackLib;

namespace BlackJack
{
    class Program
    {
        static void NewLine() => Console.WriteLine();
        public static Deck deck = new Deck();
        public static Hand playerHand = new Hand();
        public static Hand dealerHand = new Hand();
        static void Main(string[] args)
        {
            Console.WriteLine("*****BLACKJACK*****");
            bool play = true;
            do
            {
                AddingCardsToDeck();
                PlayerStartingHand();
                DealerStartingHand();
                bool game = true;

                while (game == true)
                {
                    bool validInput = false;
                    while (validInput == false)
                    {
                        Console.WriteLine($"Your cards: {playerHand.CardsInHand()}\nTotal Value: {playerHand.HandValue()}");
                        NewLine();
                        Console.WriteLine($"Dealers cards: {dealerHand.CardsInHand()}\nTotal Value: {dealerHand.HandValue()}");
                        NewLine();
                        Menu();
                        string decision = Console.ReadLine().Trim().ToLower();
                        Console.Clear();
                        switch (decision)
                        {
                            case "h":
                                validInput = true;
                                playerHand.hand.Add(deck.GetCard());
                                Console.WriteLine($"You got: {playerHand.LastCardInHand().Name}");
                                if (playerHand.BustCheck() == true)
                                {
                                    Console.WriteLine($"You Busted!\nYour final hand: {playerHand.CardsInHand()}\nYour final score: {playerHand.HandValue()}");
                                    game = false;
                                    break;
                                }
                                break;
                            case "s":
                                validInput = true;
                                game = false;
                                DealerPlay();
                                if (dealerHand.BustCheck() == true)
                                {
                                    Console.WriteLine("Dealer Busted!");
                                    break;
                                }
                                ResultCheck();
                                break;
                        }
                        
                    }
                }

                play = PlayAgain();
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
            deck.AddNewCard("Ace", 11);
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
                Console.WriteLine($"Congratulations! You Won!\n\nYour final cards: {playerHand.CardsInHand()}\nYour final score: {playerHand.HandValue()}\n\nDealers final cards: {dealerHand.CardsInHand()}\nDealers final score: {dealerHand.HandValue()}\n");
            }
            else if (playerHand.HandValue() < dealerHand.HandValue())
            {
                Console.WriteLine($"Unlucky! You Lost!\n\nYour final cards: {playerHand.CardsInHand()}\nYour final score: {playerHand.HandValue()}\n\nDealers final cards: {dealerHand.CardsInHand()}\nDealers final score: {dealerHand.HandValue()}\n");
            }
            else
            {
                Console.WriteLine($"Draw!\n\nYour final cards: {playerHand.CardsInHand()}\nYour final score: {playerHand.HandValue()}\n\nDealers final cards: {dealerHand.CardsInHand()}\nDealers final score: {dealerHand.HandValue()}\n");
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
    }
}
