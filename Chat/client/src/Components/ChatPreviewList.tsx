import React, {useContext, useEffect} from 'react';
import {useState} from 'react';
import './ChatPreview'
import '../Styles/ChatPreviewList.css';
import ChatPreview from "./ChatPreview";
import {ServiceResponseGeneric} from "../ServiceResponses/ServiceResponseGeneric"
import {MessagePreviewDto} from "../DtoModels/MessagePreviewDto";
import {getTime} from "../Utils/DateHelper";
import {client, host} from "../Constants/ServerInfo";
import {QueryRepository} from "../Repositories/QueryRepository";
import {HubContext} from "../Contexts/HubContext";
import {ChatContext} from "../Contexts/ChatContext";

const ChatPreviewList: React.FC = () => {
    const hubContext = useContext(HubContext)
    const chatContext = useContext(ChatContext)

    if (chatContext.isError) {
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