import { TicketBag } from "../ticket-bag";
import { ChoosableRoute } from "./choosable-route";
import { IngameEvent } from "./ingame-event";
import { TicketBagForPlayer } from "./ticket-bag-for-player";

export interface TurnVillianEvent extends IngameEvent {
  detectiveTickets: TicketBagForPlayer[];
  routes: ChoosableRoute[];
}
