using ClassTeamArena.Core;

namespace ClassTeamArena.GamePacks.CatGame.Actions;

public sealed class Sprint : IPlayerAction
{
    public string Key => "S";
    public string Title => "Sprint";
    public string Description => "Erhöhe die Fluchtweg um +15, aber Energie -8.";

    public void Apply(GameState state, GameContext ctx)
    {
        state.ChangeProgress(+15);
        state.ChangeStatA(-8);
        ctx.AddLog("Sprint! Fluchtweg +15, Energie -8.");
    }
}
