import React, {useContext, useState} from 'react';
import '../Styles/App.css';
import UserNavbar from "./UserNavbar";
import ChatHeader from "./ChatHeader";
import MessageArea from "./MessageArea";
import MessageInputArea from "./InputArea";
import {HubContextProvider} from '../Contexts/HubContext';
import {ChatContextProvider} from "../Contexts/ChatContext";
import {InputContext} from "../Contexts/InputContext";
import ChatPreviewList from "./ChatPreviewList";

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
