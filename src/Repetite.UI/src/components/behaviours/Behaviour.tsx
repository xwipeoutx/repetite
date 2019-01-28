import * as React from "react";

export interface BehaviourListProps {}

export interface BehaviourListState {
  behaviours: Api.Behaviour[];
}

export default class BehaviourList extends React.Component<
  BehaviourListProps,
  BehaviourListState
> {
  constructor(props: BehaviourListProps) {
    super(props);
    this.state = { behaviours: [] };

    this.refresh();
  }

  async refresh() {
    let response = await fetch("https://localhost:5001/api/behaviours");
    let behaviours: Api.Behaviour[] = await response.json();

    this.setState({ behaviours });
  }

  render() {
    return (
      <div>
        {(this.state.behaviours || []).map(b => (
          <div key={b.id}>
            <h2>{b.name}</h2>
            <p>
              <strong>Inputs: </strong> {b.inputs.map(i => i.name).join(",")}
            </p>
            <p>
              <strong>Outputs: </strong> {b.outputs.map(o => o.name).join(",")}
            </p>
          </div>
        ))}
      </div>
    );
  }
}
