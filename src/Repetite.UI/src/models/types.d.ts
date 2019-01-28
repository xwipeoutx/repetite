declare namespace Api {
  interface Input {
    name: string;
    type: string;
  }
  interface Output {
    name: string;
    type: string;
  }
  interface Behaviour {
    id: string;
    name: string;
    outputs: Output[];
    inputs: Input[];
  }
  interface Node {
    id: string;
    name: string;
    behaviourId: string;
  }
}
