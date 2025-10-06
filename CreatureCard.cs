using System;

public class CreatureCard : Card, IPlayable
{
    public int Attack;
    public int Health;

    public void Play(Player player, Game game)
    {
        if (player.Mana < Cost)
        {
            throw new Exception("Мало маны для " + Name);
        }

        player.Mana -= Cost;
        player.Board.Add(this);
        game.Log(player.Name + " играет " + Name);
    }
}
