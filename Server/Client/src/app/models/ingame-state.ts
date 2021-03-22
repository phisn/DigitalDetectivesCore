import { DetectiveState } from "./detective-state";
import { StateType } from "./state-type.enum";
import { VillianState } from "./villian-state";

export class IngameState {
  constructor(
    public type: StateType,
    public state: VillianState | DetectiveState) {
  }

  assertVillian(): VillianState {
    console.assert(this.type == StateType.Villian);
    return this.state as VillianState;
  }

  assertDetective(): VillianState {
    console.assert(this.type == StateType.Villian);
    return this.state as VillianState;
  }
}