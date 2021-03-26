import { RouteOption } from "./route-option";
import { PlayerState } from "./player-state";
import { TicketBag } from "./game/ticket-bag";

export interface DetectiveState extends PlayerState {
  villianTickets: TicketBag;
  routes: RouteOption[];
}
