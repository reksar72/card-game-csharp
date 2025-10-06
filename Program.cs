using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Game game = new Game();

        Player player1 = new Player { Name = "Роберт" };
        Player player2 = new Player { Name = "Никита" };

        List<Card> deck1 = MakeDeck();
        List<Card> deck2 = MakeDeck();

        player1.Deck = new Queue<Card>(deck1);
        player2.Deck = new Queue<Card>(deck2);

        game.Player1 = player1;
        game.Player2 = player2;
        game.CurrentPlayer = player1;

        for (int i = 0; i < 3; i++)
        {
            player1.DrawCard();
            player2.DrawCard();
        }

        PlayGame(game);
    }

    static List<Card> MakeDeck()
    {
        List<Card> deck = new List<Card>();

        deck.Add(new CreatureCard { Name = "Воин", Cost = 1, Attack = 10, Health = 2 });
        deck.Add(new CreatureCard { Name = "Лучник", Cost = 2, Attack = 10, Health = 2 });
        deck.Add(new CreatureCard { Name = "Рыцарь", Cost = 3, Attack = 10, Health = 4 });

        return deck;
    }

    static void PlayGame(Game game)
    {
        for (int turn = 1; turn <= 3; turn++)
        {
            game.Log("ХОД " + turn);

            Player player = game.CurrentPlayer;
            Player opponent = (player == game.Player1) ? game.Player2 : game.Player1;

            player.Mana = turn;
            player.DrawCard();

            game.Log("ХП: " + player.Name + " - " + player.Health + ", " + opponent.Name + " - " + opponent.Health);

            if (player.Hand.Count > 0)
            {
                Card card = player.Hand[0];

                if (card is IPlayable playableCard)
                {
                    try
                    {
                        playableCard.Play(player, game);
                        player.Hand.Remove(card);
                    }
                    catch (Exception ex)
                    {
                        game.Log("Ошибка: " + ex.Message);
                    }
                }
            }

            if (player.Board.Count > 0)
            {
                foreach (CreatureCard creature in player.Board)
                {
                    opponent.Health -= creature.Attack;
                    game.Log(creature.Name + " атакует " + opponent.Name);
                    game.Log("ХП: " + player.Name + " - " + player.Health + ", " + opponent.Name + " - " + opponent.Health);

                    if (opponent.Health <= 0)
                    {
                        game.Log(opponent.Name + " убит!");
                        game.Log("Победитель: " + player.Name);
                        return;
                    }
                }
            }

            if (game.CurrentPlayer == game.Player1)
                game.CurrentPlayer = game.Player2;
            else
                game.CurrentPlayer = game.Player1;
        }
    }
}
