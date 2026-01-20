using System.Diagnostics;

namespace ClassTeamArena.Core;

public sealed class GameEngine
{
    private readonly IGamePack _pack;
    private readonly GameState _state;
    private readonly GameContext _ctx;
    private readonly IReadOnlyList<IGameModule> _modules;
    private readonly IReadOnlyList<IPlayerAction> _actions;

    private readonly TimeSpan _timeLimit;
    private readonly TimeSpan _roundCost;
    private readonly Stopwatch _sw = new();

    public GameEngine(IGamePack pack)
    {
        _pack = pack;

        var seed = (int)DateTime.Now.Ticks;
        _ctx = new GameContext(seed);
        _state = pack.CreateInitialState();

        _modules = pack.CreateModules();
        _actions = pack.CreateActions();

        _timeLimit = TimeSpan.FromSeconds(pack.Settings.TimeLimitSeconds);
        _roundCost = TimeSpan.FromSeconds(pack.Settings.RoundCostSeconds);
    }

    public void Run()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Clear();

        PrintBanner();
        PrintIntro();
        Console.WriteLine("\nEnter zum Start…");
        Console.ReadLine();

        _sw.Start();

        while (true)
        {
            _state.Round++;

            var remaining = _timeLimit - _sw.Elapsed;
            if (remaining <= TimeSpan.Zero)
            {
                var outcome = _pack.EvaluateOutcome(_state, _sw.Elapsed, TimeSpan.Zero)
                              ?? new GameOutcome(false, "Zeit ist um. (Keine Outcome-Regel im Pack definiert.)");
                End(outcome);
                return;
            }

            Console.Clear();
            PrintHUD(remaining);

            var action = AskAction();
            _ctx.AddLog($"[R{_state.Round}] Aktion: {action.Title}");
            action.Apply(_state, _ctx);

            foreach (var m in _modules)
                m.Execute(_state, _ctx);

            BusyWait(_roundCost);

            Console.Clear();
            remaining = _timeLimit - _sw.Elapsed;
            PrintHUD(remaining);
            PrintLogTail(10);

            if (_state.DefaultLoseCheck(out var defaultReason))
            {
                End(new GameOutcome(false, defaultReason));
                return;
            }

            var custom = _pack.EvaluateOutcome(_state, _sw.Elapsed, remaining);
            if (custom != null)
            {
                End(custom);
                return;
            }

            Console.WriteLine("\nWeiter mit Enter… (oder Q zum Abbrechen)");
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Q) return;
        }
    }

    private void PrintIntro()
    {
        Console.WriteLine($"=== {_pack.Title} ===");
        Console.WriteLine(_pack.Tagline);
        Console.WriteLine(new string('-', 72));

        Console.WriteLine("STORY (Kurz):");
        var story = TextLoader.TryLoad(_pack.StoryPath);
        Console.WriteLine(TrimForConsole(story, 40));

        Console.WriteLine("\nREGELN (Kurz):");
        var rules = TextLoader.TryLoad(_pack.RulesPath);
        Console.WriteLine(TrimForConsole(rules, 24));

        Console.WriteLine("\nHinweis: Die Klasse entscheidet pro Runde EINE Aktion (Key).");
    }

    private IPlayerAction AskAction()
    {
        Console.WriteLine("\nWählt eure Aktion (Key):");
        foreach (var a in _actions)
            Console.WriteLine($"  [{a.Key}] {a.Title} – {a.Description}");

        while (true)
        {
            Console.Write("\nEingabe: ");
            var input = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();
            var match = _actions.FirstOrDefault(a => a.Key.ToUpperInvariant() == input);
            if (match != null) return match;

            _state.ChangeRisk(+2);
            _ctx.AddLog("Ungültige Eingabe → Risiko/Chaos +2.");
            Console.WriteLine("Ungültig. (Risiko steigt!)");
        }
    }

    private void PrintHUD(TimeSpan remaining)
    {
        var d = _pack.Display;
        Console.WriteLine($"=== {_pack.Title} === Runde: {_state.Round} | Rest: {remaining.ToString(@"mm\:ss")}");
        Console.WriteLine($"{d.StatAName}: {_state.StatA,3} | {d.StatBName}: {_state.StatB,3} | {d.StatCName}: {_state.StatC,3} | {d.RiskName}: {_state.Risk,3} | {d.ProgressName}: {_state.Progress,3}");
        Console.WriteLine(new string('-', 78));
    }

    private void PrintLogTail(int lastN)
    {
        var items = _ctx.Log.TakeLast(lastN).ToList();
        Console.WriteLine("Ereignisse:");
        foreach (var line in items) Console.WriteLine(" • " + line);
    }

    private void End(GameOutcome outcome)
    {
        Console.Clear();
        Console.WriteLine(outcome.IsWin ? "\n🏁 SIEG! 🏁" : "\n💥 NIEDERLAGE 💥");
        Console.WriteLine(outcome.Message);
        Console.WriteLine($"\nFinal: Runde {_state.Round}, Progress {_state.Progress}, Risiko {_state.Risk}, A {_state.StatA}, B {_state.StatB}, C {_state.StatC}");
    }

    private static void BusyWait(TimeSpan duration)
    {
        var sw = Stopwatch.StartNew();
        while (sw.Elapsed < duration) { }
    }

    private static string TrimForConsole(string txt, int maxLines)
    {
        var lines = txt.Replace("\r\n", "\n").Split('\n');
        var take = lines.Take(maxLines).ToList();
        return string.Join("\n", take);
    }

    private static void PrintBanner()
    {
        Console.WriteLine(@"
  _____ _                 _____                     _                     
 / ____| |               / ____|                   | |                    
| |    | | __ _ ___ ___ | |  __  __ _ _ __ ___   __| | ___  _ __ ___ ___  
| |    | |/ _` / __/ __|| | |_ |/ _` | '_ ` _ \/ _` |/ _ \\| '__/ __/ _ \\ 
| |____| | (_| \__ \__ \| |__| | (_| | | | | | | (_| | (_) | | | (_|  __/ 
 \_____|_|\__,_|___/___/ \_____|\__,_|_| |_| |_|\__,_|\___/|_|  \___\___| 
");
    }
}
