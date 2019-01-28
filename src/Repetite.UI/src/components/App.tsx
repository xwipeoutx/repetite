import * as React from "react";
import "./../assets/scss/App.scss";
import BehaviourList from "./behaviours/Behaviour";

const reactLogo = require("./../assets/img/react_logo.svg");

export interface AppProps {
}

export default class App extends React.Component<AppProps, undefined> {
    render() {
        return (
            <div className="app">
                <h1>Hello World!</h1>
                <BehaviourList />
            </div>
        );
    }
}
