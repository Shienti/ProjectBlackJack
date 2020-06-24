using System;

namespace BlackJackLib
{
    public class Card
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public Card(string name, int value)
        {
            Name = name.Trim();
            Value = value;
        }
    }
}
