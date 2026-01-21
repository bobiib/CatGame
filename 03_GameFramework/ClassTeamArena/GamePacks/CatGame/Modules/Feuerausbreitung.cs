using ClassTeamArena.Core;

namespace ClassTeamArena.GamePacks.CatGame.Modules;

public sealed class Feuerausbreitung : IGameModule
{
    public string Name => "Feuerausbreitung";

    public void Execute(GameState state, GameContext ctx)
    {
        int baseIncrease = 3;

        // Je weiter die Katze kommt, desto mehr Feuer um sie herum
        if (state.Progress >= 40) baseIncrease += 2;
        if (state.Progress >= 70) baseIncrease += 2;

        state.ChangeRisk(baseIncrease);
        ctx.AddLog($"Das Feuer breitet sich aus (+{baseIncrease} Feuer).");
    }
}