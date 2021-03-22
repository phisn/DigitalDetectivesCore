import { PlayerColor } from "./game/player-color.enum";
import { TicketBag } from "./game/ticket-bag";

export interface TicketsForPlayer {
  tickets: TicketBag;
  color: PlayerColor;
}
