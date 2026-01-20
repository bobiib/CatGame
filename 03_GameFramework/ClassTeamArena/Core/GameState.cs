namespace ClassTeamArena.Core;

public sealed class GameState
{
    public int StatA { get; private set; } = 70;
    public int StatB { get; private set; } = 70;
    public int StatC { get; private set; } = 70;
    public int Risk  { get; private set; } = 20;
    public int Progress { get; private set; } = 10;

    public int Round { get; internal set; } = 0;

    public bool Flag1 { get; private set; } = false;
    public bool Flag2 { get; private set; } = false;

    public void ChangeStatA(int delta) => StatA = Clamp(StatA + delta);
    public void ChangeStatB(int delta) => StatB = Clamp(StatB + delta);
    public void ChangeStatC(int delta) => StatC = Clamp(StatC + delta);
    public void ChangeRisk(int delta) => Risk = Clamp(Risk + delta);
    public void ChangeProgress(int delta) => Progress = Clamp(Progress + delta);

    public void SetFlag1(bool value) => Flag1 = value;
    public void SetFlag2(bool value) => Flag2 = value;

    public bool DefaultLoseCheck(out string reason)
    {
        if (StatA <= 0) { reason = "StatA ist 0. Das System kollabiert."; return true; }
        if (StatB <= 0) { reason = "StatB ist 0. Das System kollabiert."; return true; }
        if (StatC <= 0) { reason = "StatC ist 0. Das System kollabiert."; return true; }
        if (Risk >= 100) { reason = "Risiko 100. Game Over."; return true; }
        reason = "";
        return false;
    }

    private static int Clamp(int x) => Math.Max(0, Math.Min(100, x));
}
