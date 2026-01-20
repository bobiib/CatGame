namespace ClassTeamArena.Core;

public static class TextLoader
{
    public static string TryLoad(string relativePath)
    {
        try
        {
            var baseDir = AppContext.BaseDirectory;
            var full = Path.Combine(baseDir, relativePath);
            if (!File.Exists(full)) return $"(Datei nicht gefunden: {relativePath})";
            return File.ReadAllText(full);
        }
        catch (Exception ex)
        {
            return $"(Fehler beim Laden von {relativePath}: {ex.Message})";
        }
    }
}
