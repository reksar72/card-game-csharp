using System;

public class SpellCard : Card, IPlayable
{
    public string Effect;

    public void Play(Player player, Game game)
    {
        if (player.Mana < Cost)
        {
            throw new Exception("Мало маны для " + Name);
        }

        player.Mana -= Cost;
        game.Log(player.Name + " играет " + Name);
    }
}
