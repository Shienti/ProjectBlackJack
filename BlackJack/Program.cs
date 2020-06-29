using System;
using BlackJackLib;

namespace BlackJack
{
    class Program
    {
        static void NewLine() => Console.WriteLine();
        public static int balance = 1000;                                                           //Initial player balance
        public static Deck deck = new Deck();                                                       //Creating new deck
        public static Hand playerHand = new Hand();                                                 //Creating players hand
        public static Hand dealerHand = new Hand();                                                 //Creating dealers hand
        static void Main(string[] args)
        {
            Console.WriteLine("*****BLACKJACK*****");
            bool play = true;                                                                       //Preparing game properties
            do
            {

                AddingCardsToDeck();                                                                //Adding cards to deck
                PlayerStartingHand();                                                               //Adding players starting cards
                DealerStartingHand();                                                               //Adding dealers starting cards

                Console.WriteLine($"Your cards: {playerHand.CardsInHand()}");
                Console.WriteLine($"Total Value: {playerHand.HandValue()}");
                Console.WriteLine($"Your account balance: {balance}$");
                int betValue = AddingBet();                                                         //Accepting players bet

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
                        Console.WriteLine($"Your bet: {betValue}$");                                //Players current bet
                        Console.WriteLine($"Your account balance: {balance-betValue}$");            //Players current balance

                        Menu();                                                                     //Print game Menu with possible choices
                        string decision = Console.ReadLine().Trim().ToLower();                      //getting user input
                        Console.Clear();

                        switch (decision)                                                           //Makes action, depending on users choice
                        {

                            //Hit
                            case "h":
                                validInput = true;
                                playerHand.hand.Add(deck.GetCard());                                //Adding card to players deck
                                Console.WriteLine($"You got: {playerHand.LastCardInHand().Name}");  //Presenting players new card
                                if (playerHand.BustCheck() == true)                                 //Checking for Bust
                                {
                                    Console.WriteLine($"You Busted!\nYou lost {betValue}$");
                                    PrintResults();
                                    balance -= betValue;
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
                                    Console.WriteLine($"Dealer Busted!\nYou won {2*betValue}$");
                                    balance += 2 * betValue;
                                    PrintResults();
                                    break;
                                }
                                ResultCheck(betValue);                                              //Checking game result and comparing hands
                                break;
                        }
                        
                    }
                }

                play = PlayAgain();                                                                 //Asking user for next deal
                Console.Clear();
            }
            while (play == true);
        }

        private static void PlayerStartingHand()                                                    //Basic starting hand method
        {
            playerHand.hand.Add(deck.GetCard());
            playerHand.hand.Add(deck.GetCard());
        }
        private static void DealerStartingHand()                                                    //Basic starting hand method
        {
            dealerHand.hand.Add(deck.GetCard());
            dealerHand.hand.Add(deck.GetCard());
        }
        private static void AddingCardsToDeck()                                                     //Adding cards to deck
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
        private static void Menu()                                                                  //Menu display
        {
            Console.WriteLine("What's your decision?");
            Console.WriteLine("[H]it!");
            Console.WriteLine("[S]tay!");
            Console.Write("Decision: ");
        }
        private static void DealerPlay()                                                            //Dealers "logic"
        {
            while (playerHand.HandValue() > dealerHand.HandValue() && dealerHand.HandValue() < 15)
            {
                dealerHand.hand.Add(deck.GetCard());
                Console.WriteLine($"Dealer got: {dealerHand.LastCardInHand().Name}");
            }

        }

        private static void ResultCheck(int bet)                                                    //Checking results after player and dealer finish their turns
        {
            if (playerHand.HandValue() > dealerHand.HandValue())
            {
                Console.WriteLine($"Congratulations! You Won {2*bet}$!");
                balance += 2 * bet;
                PrintResults();
            }
            else if (playerHand.HandValue() < dealerHand.HandValue())
            {
                Console.WriteLine($"Unlucky! You Lost {bet}$!");
                balance -= bet;
                PrintResults();
            }
            else
            {
                Console.WriteLine($"Draw! Bet returned to your account!");
                PrintResults();
            }
        }

        private static bool PlayAgain()                                                             //Setting up for new game
        {
            bool again = true;
            bool validInput = false;

            if (balance <= 0)
            {
                Console.WriteLine("You lost all your money. Gambling is BAD!");
                Console.ReadLine();
                again = false;
            }
            else
            {
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
                            Console.ReadLine();
                            again = false;
                            validInput = true;
                            break;
                    }
                    Console.Clear();
                }
            }

            return again;
        }

        private static void PrintResults()                                                          //Printing results
        {
            Console.WriteLine("*********************************************");
            Console.WriteLine($"Your final cards: {playerHand.CardsInHand()}");
            Console.WriteLine($"Your final score: {playerHand.HandValue()}");
            Console.WriteLine("*********************************************");
            Console.WriteLine($"Dealers final cards: {dealerHand.CardsInHand()}");
            Console.WriteLine($"Dealers final score: {dealerHand.HandValue()}");
        }

        private static int AddingBet()                                                              //Accepting bet
        {
            int result;
            Console.WriteLine("How much would You like to bet?");
            do
            {
                Console.Write("Bet: ");
                while (!int.TryParse(Console.ReadLine(), out result))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid value");
                    Console.WriteLine($"Your balance: {balance}$");
                    Console.Write("Bet: ");
                }
                if (result>balance)
                {
                    Console.Clear();
                    Console.WriteLine("Not enough money!");
                    Console.WriteLine($"Your balance: {balance}$");
                }
                else if (result<0)
                {
                    Console.Clear();
                    Console.WriteLine("Can't bet with negative numbers!");
                    Console.WriteLine($"Your balance: {balance}$");
                }
            } while (result > balance || result<0);
            Console.Clear();

            return result;

        }
    }
}
