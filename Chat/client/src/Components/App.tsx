import React from 'react';
import HubContextProvider from "../Contexts/HubContext";
import AppWrapper from "./AppWrapper";
import CreateGroupModalContextProvider from "../Contexts/CreateGroupModal/CreateGroupModalContext";

const App: React.FC = () => {
    return (
        <HubContextProvider>
            <CreateGroupModalContextProvider>
                <AppWrapper/>
            </CreateGroupModalContextProvider>
        </HubContextProvider>
    );
}

export default App;
