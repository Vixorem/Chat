import React, {useContext, useEffect} from 'react';
import './ChatPreview'
import '../Styles/ChatPreviewList.css';
import ChatPreview from "./ChatPreview";
import {getTime} from "../Utils/DateHelper";
import {ChatContext} from "../Contexts/ChatContext";

const ChatPreviewList: React.FC = () => {
    const chatContext = useContext(ChatContext)

    if (chatContext.errorMessage !== "") {
        return <div>{chatContext.errorMessage}</div>
    }
    if (!chatContext.isPreviewLoaded) {
        return <div>Загрузка...</div>
    } else
        return (
            <div className="chatPreviewList">
                <div className="messagesTitle">Последние сообщения</div>
                {
                    chatContext.chatPreviews.map(item =>
                        <ChatPreview
                            chatId={item.chatId}
                            chatName={item.chatName}
                            messagePreview={item.lastMessage}
                            sentTime={getTime(item.sentTime.toString())}/>)
                }
            </div>
        );
}

export default ChatPreviewList