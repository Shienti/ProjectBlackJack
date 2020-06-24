using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BlackJackLib
{
    public class Hand
    {
        public List<Card> hand = new List<Card>();

        public int HandValue()
        {
            int result = 0;
            foreach (Card c in hand)
            {
                result += c.Value;
            }
            return result;
        }

        public string CardsInHand()
        {
            StringBuilder s = new StringBuilder();
            foreach (Card c in hand)
            {
                s.Append(c.Name + " ");
            }
            return s.ToString();
        }

        public bool BustCheck()
        {
            bool bust = false;

            if(HandValue()>21)
            {
                bust = true;
            }

            return bust;
        }
        public bool BlackJackCheck()
        {
            bool play = true;

            if (HandValue() > 21)
            {
                Console.WriteLine("BUSTED!");
                play = false;
            }

            return play;
        }
        public Card LastCardInHand()
        {
            var lastCard = hand.Last();
            return lastCard;
        }
    }
}
