using ClassTeamArena.Core;

namespace ClassTeamArena.GamePacks.CatGame.Actions;

public sealed class KuehlererOrt : IPlayerAction
{
    public string Key => "K";
    public string Title => "Kühlerer Ort";
    public string Description => "Reduziere die Temperatur um -12, aber Fluchtweg +5.";

    public void Apply(GameState state, GameContext ctx)
    {
        state.ChangeStatB(-12);
        state.ChangeProgress(+5);
        ctx.AddLog("Kühlerer Ort gefunden! Temperatur -12, Fluchtweg +5.");
    }
}
