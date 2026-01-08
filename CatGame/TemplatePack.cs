using ClassTeamArena.Core;

namespace ClassTeamArena.GamePacks._TEMPLATE;

// Kopiere den Ordner, benenne ihn um und passe Id/Title/Display/Settings an.
public sealed class CatGame : IGamePack
{
    public string Id => "CatGame"; // muss dem Ordnernamen entsprechen
    public string Title => "Katzen Spiel";
    public string Tagline => @"Helfe der Katze dem Feuer zu entkommen!
 _._     _,-'""`-._
(,-.`._,'(       |\`-/|
    `-.-' \ )-`( , o o)
          `-    \`_`""'-";

    public GameDisplay Display => new(
        StatAName: "Ressource A",
        StatBName: "Ressource B",
        StatCName: "Ressource C",
        RiskName: "Risiko",
        ProgressName: "Fortschritt"
    );

    public GameSettings Settings => new(
        TimeLimitSeconds: 6 * 60,
        RoundCostSeconds: 18,
        TargetProgress: 80
    );

    public string StoryPath => Path.Combine("GamePacks", Id, Id, "story.md");
    public string RulesPath => Path.Combine("GamePacks", Id, Id, "rules.md");

    public GameState CreateInitialState() => new GameState();

    public IReadOnlyList<IPlayerAction> CreateActions()
        => new List<IPlayerAction>
        {
            // TODO: eigene Actions
        };

    public IReadOnlyList<IGameModule> CreateModules()
        => new List<IGameModule>
        {
            // TODO: eigene Modules
        };

    public GameOutcome? EvaluateOutcome(GameState state, TimeSpan elapsed, TimeSpan remaining)
    {
        if (remaining <= TimeSpan.Zero)
            return state.Progress >= Settings.TargetProgress
                ? new GameOutcome(true, "Siegtext hier.")
                : new GameOutcome(false, "Losetext hier.");

        if (state.Progress >= 100)
            return new GameOutcome(true, "Sofort-Sieg bei 100% Progress!");

        return null;
    }
}
