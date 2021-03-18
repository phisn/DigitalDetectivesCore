import { PlayerColor } from "../player-color.enum";
import { TicketBag } from "../ticket-bag";

export interface TicketBagForPlayer {
  tickets: TicketBag;
  color: PlayerColor;
}
