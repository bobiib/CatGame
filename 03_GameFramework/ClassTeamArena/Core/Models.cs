namespace ClassTeamArena.Core;

public sealed record GameSettings(
    int TimeLimitSeconds,
    int RoundCostSeconds,
    int TargetProgress
);

public sealed record GameDisplay(
    string StatAName,
    string StatBName,
    string StatCName,
    string RiskName,
    string ProgressName
);

public sealed record GameOutcome(bool IsWin, string Message);
