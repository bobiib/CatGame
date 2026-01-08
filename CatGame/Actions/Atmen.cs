using ClassTeamArena.Core;

namespace ClassTeamArena.GamePacks.CatGame.Actions;

public sealed class Atmen : IPlayerAction
{
    public string Key => "A";
    public string Title => "Atmen";
    public string Description => "Erhöhe den Atem um +8, aber Feuer +3.";

    public void Apply(GameState state, GameContext ctx)
    {
        state.ChangeStatC(+8);
        state.ChangeRisk(+3);
        ctx.AddLog("Tiefe Atemzüge! Atem +8, Feuer +3.");
    }
}
