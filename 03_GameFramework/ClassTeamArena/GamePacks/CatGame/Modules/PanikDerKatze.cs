using ClassTeamArena.Core;

namespace ClassTeamArena.GamePacks.CatGame.Modules;

public sealed class PanikDerKatze : IGameModule
{
    public string Name => "Panik der Katze";

    public void Execute(GameState state, GameContext ctx)
    {
        if (state.Risk < 40) return;

        int panic = (state.Risk - 30) / 10;

        state.ChangeStatA(-panic);

        // Kleine Chance auf komplette Panik
        if (ctx.Rng.Next(100) < 15)
        {
            state.ChangeProgress(-5);
            ctx.AddLog("Die Katze gerät in Panik und rennt im Kreis! (-5 Fluchtweg)");
        }
        else
        {
            ctx.AddLog($"Die Katze ist panisch. Energie -{panic}.");
        }
    }
}