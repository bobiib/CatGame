using System.Reflection;

namespace ClassTeamArena.Core;

public static class PackFactory
{
    public static List<IGamePack> DiscoverPacks()
    {
        var packs = new List<IGamePack>();
        var asm = Assembly.GetExecutingAssembly();

        foreach (var t in asm.GetTypes())
        {
            if (t.IsAbstract || t.IsInterface) continue;
            if (!typeof(IGamePack).IsAssignableFrom(t)) continue;

            if (Activator.CreateInstance(t) is IGamePack pack)
                packs.Add(pack);
        }

        return packs.OrderBy(p => p.Title).ToList();
    }
}
