import { TicketBag } from "./game/ticket-bag";

export interface PlayerState {
  position: number;
  villianRevealedIn: number;
  tickets: TicketBag;
}
