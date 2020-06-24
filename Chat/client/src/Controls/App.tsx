import React, {useState} from 'react';
import '../Styles/App.css';
import UserNavbar from "./UserNavbar";
import ChatList from "./ChatList";
import ChatHeader from "./ChatHeader";
import MessageArea from "./MessageArea";
import MessageInputArea from "./InputArea";
import {MessageDto} from "../DtoModels/MessageDto";
import {ChatItemClickEventProvider} from "../Contexts/ChatItemClickEventContext";
import {ChatHistoryProvider} from "../Contexts/ChatHistoryContext";
import {ChatAreaProvider} from "../Contexts/ChatAreaContext";

const App: React.FC = () => {
    const [chatMessages, setChatMessages] = useState<MessageDto[]>([]);
    const [openedChatId, setOpenedChatId] = useState<string>("");

    return (
        <div className="app">
            <UserNavbar/>
            <ChatHeader/>
            <ChatItemClickEventProvider value={{
                setChatMessages: setChatMessages,
                setOpenedChatId: setOpenedChatId
            }}>
                <ChatList/>
            </ChatItemClickEventProvider>
            <ChatAreaProvider value={{openedChatId: openedChatId}}>
                <ChatHistoryProvider value={{data: chatMessages!}}>
                    <MessageArea/>
                </ChatHistoryProvider>
                <MessageInputArea/>
            </ChatAreaProvider>
        </div>
    );
}

export default App;
