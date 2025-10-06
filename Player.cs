using System.Collections.Generic;

public class Player
{
    public string Name;
    public int Health = 20;
    public int Mana = 1;
    public Queue<Card> Deck = new Queue<Card>();
    public List<Card> Hand = new List<Card>();
    public List<CreatureCard> Board = new List<CreatureCard>();

    public void DrawCard()
    {
        if (Deck.Count > 0)
        {
            Card card = Deck.Dequeue();
            Hand.Add(card);
        }
    }
}
