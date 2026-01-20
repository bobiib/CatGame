namespace ClassTeamArena.Core;

public interface IGameModule
{
    string Name { get; }
    void Execute(GameState state, GameContext ctx);
}

public interface IPlayerAction
{
    string Key { get; }
    string Title { get; }
    string Description { get; }
    void Apply(GameState state, GameContext ctx);
}

public interface IGamePack
{
    string Id { get; }
    string Title { get; }
    string Tagline { get; }
    GameDisplay Display { get; }

    GameSettings Settings { get; }
    GameState CreateInitialState();

    IReadOnlyList<IPlayerAction> CreateActions();
    IReadOnlyList<IGameModule> CreateModules();

    GameOutcome? EvaluateOutcome(GameState state, TimeSpan elapsed, TimeSpan remaining);

    string StoryPath { get; }
    string RulesPath { get; }
}
