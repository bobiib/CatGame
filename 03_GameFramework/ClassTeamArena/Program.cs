using ClassTeamArena.Core;

namespace ClassTeamArena;

public static class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        var packs = PackFactory.DiscoverPacks();

        if (packs.Count == 0)
        {
            Console.WriteLine("Keine GamePacks gefunden. Füge mindestens ein IGamePack hinzu.");
            return;
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Class-Team Game Arena ===");
            Console.WriteLine("Wähle ein Spiel (Pack):\n");

            for (int i = 0; i < packs.Count; i++)
            {
                var p = packs[i];
                Console.WriteLine($"  [{i+1}] {p.Title} — {p.Tagline}");
            }

            Console.WriteLine("\n  [Q] Beenden");
            Console.Write("\nEingabe: ");
            var input = (Console.ReadLine() ?? "").Trim();

            if (input.Equals("Q", StringComparison.OrdinalIgnoreCase))
                return;

            if (!int.TryParse(input, out int idx) || idx < 1 || idx > packs.Count)
                continue;

            var pack = packs[idx - 1];
            var engine = new GameEngine(pack);
            engine.Run();

            Console.WriteLine("\nNochmal zurück zum Menü? (Enter) oder Q zum Beenden");
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Q) return;
        }
    }
}
