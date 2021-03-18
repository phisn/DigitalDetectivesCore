import { TicketBag } from "../ticket-bag";
import { ChoosableRoute } from "./choosable-route";
import { IngameEvent } from "./ingame-event";

export interface TurnDetectiveEvent extends IngameEvent {
  villianTickets: TicketBag;
  routes: ChoosableRoute[];
}
