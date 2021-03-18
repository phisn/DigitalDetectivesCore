import { TicketBag } from "../ticket-bag";

export interface IngameEvent {
  position: number;
  villianRevealedIn: number;
  tickets: TicketBag;
}
