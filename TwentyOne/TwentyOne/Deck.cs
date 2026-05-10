using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyOne
{
    public class Deck
    {
        private List<Card> _cards = new List<Card>();
        public List<Card> Cards { get { return _cards; } set { _cards = value; } }

        public Deck()
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Face face in Enum.GetValues(typeof(Face)))
                {
                    Cards.Add(new Card { Suit = suit, Face = face });
                }
            }
        }

        public void Shuffle()
        {
            Random rng = new Random();
            Cards = Cards.OrderBy(x => rng.Next()).ToList();
        }
    }
}