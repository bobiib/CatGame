using ClassTeamArena.Core;

namespace ClassTeamArena.GamePacks.CatGame.Actions;

public sealed class MutSammeln : IPlayerAction
{
    public string Key => "M";
    public string Title => "Mut Sammeln";
    public string Description => "Erhöhe Fluchtweg +10, aber Energie -5 und Temperatur +6.";

    public void Apply(GameState state, GameContext ctx)
    {
        state.ChangeProgress(+10);
        state.ChangeStatA(-5);
        state.ChangeStatB(+6);
        ctx.AddLog("Mut gesammelt! Fluchtweg +10, Energie -5, Temperatur +6.");
    }
}
