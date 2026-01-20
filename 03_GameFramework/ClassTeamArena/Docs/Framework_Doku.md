# Framework-Doku – ClassTeamArena

## Grundidee
- Engine kennt nur Interfaces → **Polymorphismus**
- Packs liefern Module + Actions + Story/Rules → **lose Kopplung**
- GameState kapselt Werte → **Kapselung**
- GamePack ist Abstraktion: Thema/Regeln austauschbar

## Ordnerstruktur
- `Core/` Engine, State, Contracts
- `GamePacks/<PackId>/` pro Gruppe ein Spiel
  - `<PackId>Pack.cs`
  - `Actions/*.cs`
  - `Modules/*.cs`
  - `story.md`, `rules.md`

## Regeln (für Gruppen)
- Engine/Core NICHT ändern
- Alles wirkt nur über `GameState`
- Actions: 2–4 Keys (A/B/C/D empfohlen)
- Modules: 2–4 (Pressure + Event + Stabilisierung)

## Winnable
- In rules.md: Win/Lose + 3 Tipps + Beispiel-Run
