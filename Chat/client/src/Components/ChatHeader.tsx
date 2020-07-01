import React, {useContext} from 'react';
import '../Styles/ChatHeader.css'
import {ChatContext} from "../Contexts/ChatContext";
import {ChatNavigationContext} from "../Contexts/ChatNavigationContext";

const ChatHeader: React.FC = () => {
    const chatContext = useContext(ChatContext)
    const chatNavigationContext = useContext(ChatNavigationContext)

    if (chatContext.openedChatId !== "")
        return (
            <div className="chatHeader">
                <div className="chatTitle">
                    {chatContext.openedChatName}
                </div>
                <button className="settings"
                        onClick={(e) => chatNavigationContext.setActive(true)}>
                    Подробнее
                </button>
            </div>
        )
    else
        return (
            <div className="chatHeader">

            </div>
        )
}

export default ChatHeader