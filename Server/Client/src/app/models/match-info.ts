import { PlayerColor } from "src/app/models/game/player-color.enum";
import { MatchSettings } from "./match-settings";

export class MatchInfo {
  public availableColors: PlayerColor[];
  public round: number;
  public settings: MatchSettings;
}