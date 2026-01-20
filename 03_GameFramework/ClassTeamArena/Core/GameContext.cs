namespace ClassTeamArena.Core;

public sealed class GameContext
{
    public Random Rng { get; }
    public List<string> Log { get; } = new();

    public GameContext(int seed) => Rng = new Random(seed);
    public void AddLog(string msg) => Log.Add(msg);
}
