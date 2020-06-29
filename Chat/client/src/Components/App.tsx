import React from 'react';
import '../Styles/App.css';
import UserNavbar from "./UserNavbar";
import ChatHeader from "./ChatHeader";
import MessageArea from "./MessageArea";
import MessageInputArea from "./InputArea";
import ChatPreviewList from "./ChatPreviewList";
import HubContextProvider from "../Contexts/HubContext";
import ChatContextProvider from "../Contexts/ChatContext";
import InputContextProvider from "../Contexts/InputContext";

const App: React.FC = () => {
    return (
        <div className="app">
            <HubContextProvider>
                <UserNavbar/>
                <ChatHeader/>
                <ChatContextProvider>
                    <ChatPreviewList/>
                    <MessageArea/>
                    <InputContextProvider>
                        <MessageInputArea/>
                    </InputContextProvider>
                </ChatContextProvider>
            </HubContextProvider>
        </div>
    );
}

export default App;
