using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJackLib
{
    public class Deck
    {

        Random index = new Random();
        public List<Card> cards = new List<Card> ();

        public void AddNewCard(string name, int value)
        {
            Card newCard = new Card(name, value);
            cards.Add(newCard);
        }
        public void AddNewCard(Card newCard)
        {
            cards.Add(newCard);
        }

        public Card GetCard()
        {
            int x = index.Next(0, 13);
            Card result = cards[x];
            return result;
        }
    }
}
