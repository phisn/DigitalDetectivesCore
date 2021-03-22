import { RouteOption } from "./route-option";
import { PlayerState } from "./player-state";
import { TicketsForPlayer } from "./tickets-for-player";

export interface VillianState extends PlayerState {
  detectiveTickets: TicketsForPlayer[];
  routes: RouteOption[];
}
