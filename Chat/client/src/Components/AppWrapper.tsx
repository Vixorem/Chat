import React, {useContext} from 'react';
import "../Styles/AppWrapper.css"
import UserNavbar from "./UserNavbar";
import ChatHeader from "./ChatHeader";
import ChatContextProvider from "../Contexts/ChatContext";
import ChatPreviewList from "./ChatPreviewList";
import MessageArea from "./MessageArea";
import InputContextProvider from "../Contexts/InputContext";
import MessageInputArea from "./InputArea";
import CreateGroupModal from "./CreateGroupModal/CreateGroupModal";
import {CreateGroupModalContext} from "../Contexts/CreateGroupModal/CreateGroupModalContext";
import ChatNavigationContextProvider from "../Contexts/ChatNavigationContext";

const AppWrapper: React.FC = () => {
    const createGroupModalContext = useContext(CreateGroupModalContext)
    let appStyle = "app"
    if (createGroupModalContext.groupModalState) {
        appStyle += " disableApp"
    }

    return (
        <div>
            <div className={appStyle}>
                <UserNavbar/>
                <ChatContextProvider>
                    <ChatNavigationContextProvider>
                        <ChatHeader/>
                        <MessageArea/>
                    </ChatNavigationContextProvider>
                    <ChatPreviewList/>
                    <InputContextProvider>
                        <MessageInputArea/>
                    </InputContextProvider> 
                </ChatContextProvider>
            </div>
            <CreateGroupModal/>
        </div>
    );
}

export default AppWrapper