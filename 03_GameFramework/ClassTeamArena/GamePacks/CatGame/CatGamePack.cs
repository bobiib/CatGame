using ClassTeamArena.Core;
using ClassTeamArena.GamePacks.CatGame.Actions;
using ClassTeamArena.GamePacks.CatGame.Modules;

namespace ClassTeamArena.GamePacks.CatGame;

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
        StatAName: "Energie",
        StatBName: "Temperatur",
        StatCName: "Atem",
        RiskName: "Feuer",
        ProgressName: "Fluchtweg"
    );

    public GameSettings Settings => new(
        TimeLimitSeconds: 6 * 60,
        RoundCostSeconds: 18,
        TargetProgress: 80
    );

    public string StoryPath => Path.Combine("GamePacks", Id, "story.md");
    public string RulesPath => Path.Combine("GamePacks", Id, "rules.md");

    public GameState CreateInitialState() => new GameState();

    public IReadOnlyList<IPlayerAction> CreateActions()
        => new List<IPlayerAction>
        {
            new Sprint(),
            new KuehlererOrt(),
            new Atmen(),
            new MutSammeln()
        };

    public IReadOnlyList<IGameModule> CreateModules()
        => new List<IGameModule>
        {
            new Feuerausbreitung(),
            new Rauchvergiftung(),
            new PanikDerKatze()
        };

    public GameOutcome? EvaluateOutcome(GameState state, TimeSpan elapsed, TimeSpan remaining)
    {
        if (state.StatA <= 0)
            return new GameOutcome(false, "Die Katze ist zusammengebrochen! Zu viel Rauch eingeatmet.");

        if (state.StatB > 100)
            return new GameOutcome(false, "Das Feuer hat die Katze erreicht!");

        if (remaining <= TimeSpan.Zero)
            return state.Progress >= Settings.TargetProgress
                ? new GameOutcome(true, "🐱 Die Katze hat es geschafft zu entkommen!")
                : new GameOutcome(false, "Das Feuer war zu schnell. Die Katze konnte nicht rechtzeitig fliehen.");

        if (state.Progress >= 100)
            return new GameOutcome(true, "🐱 Vollständige Flucht! Die Katze ist in Sicherheit!");

        return null;
    }
}
