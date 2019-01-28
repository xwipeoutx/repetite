import * as React from "react";

export interface BehaviourListProps {
}

export default class BehaviourList extends React.Component<BehaviourListProps, undefined> {
    constructor(props: BehaviourListProps) {
        super(props);

        this.refresh();
    }

    async refresh() {
        let response = await fetch("https://localhost:5001/api/behaviours");
        let behaviours = response.json();
    }

    render() {
        return (
            <div>
                Asd
            </div>
        );
    }
}
