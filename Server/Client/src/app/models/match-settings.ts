import { TicketBag } from "./game/ticket-bag";

export interface MatchSettings {
  playerCount: number;
  rounds: number;
  showVillianAfter: number;
  showVillianEvery: number;
  detectiveTickets: TicketBag;
  villianTickets: TicketBag;
  villianBlackTicketMulti: number;
}
