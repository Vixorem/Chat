﻿import React, {useContext} from 'react';
import '../Styles/ChatListItem.css';
import {ServiceResponseGeneric} from "../ServiceResponses/ServiceResponseGeneric";
import {MessageDto} from "../DtoModels/MessageDto";
import {clientId, host} from "../Constants/ServerInfo";
import {QueryRepository} from "../Repositories/QueryRepository";
import ChatContext from "../Contexts/ChatContext";

interface IProp {
    chatId: string,
    chatName: string,
    messagePreview: string,
    sentTime: string
}

const ChatPreview: React.FC<IProp> = (props) => {
    const context = useContext(ChatContext)

    function chatListItemClickHandler(chatId: string) {
        const response = QueryRepository.getFromServer<ServiceResponseGeneric<MessageDto[]>>(host, "getchathistory",
            {name: "chatId", value: chatId},
            {name: "userId", value: clientId},
            {name: "offset", value: "0"},
            {name: "limit", value: "100"})
        response.then(response => {
            context.setMessages(response.value);
            context.setOpenedChatId(chatId)
        })
    }

    return (
        <div className="chatListItem" key={props.chatId}
             onClick={(e) => {
                 chatListItemClickHandler(props.chatId)
             }}>
            <div className="chatName">{props.chatName}</div>
            <div className="time">{props.sentTime}</div>
            <div className="message">{props.messagePreview}</div>
        </div>
    );
}

export default ChatPreview