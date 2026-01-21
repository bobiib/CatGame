using ClassTeamArena.Core;

namespace ClassTeamArena.GamePacks.CatGame.Modules;

public sealed class Rauchvergiftung : IGameModule
{
    public string Name => "Rauchvergiftung";

    public void Execute(GameState state, GameContext ctx)
    {
        // Wahrscheinlichkeit steigt mit Feuer
        int chance = state.Risk / 2;

        if (ctx.Rng.Next(100) < chance)
        {
            int damage = ctx.Rng.Next(4, 9);
            state.ChangeStatC(-damage);
            state.ChangeStatA(-2);

            ctx.AddLog($"Dichter Rauch! Atem -{damage}, Energie -2.");
        }
        else
        {
            ctx.AddLog("Der Rauch ist erträglich… noch.");
        }
    } 
}