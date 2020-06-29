import React, {useContext, useEffect, useState} from 'react';
import '../Styles/MessageArea.css'
import TextMessage from "./TextMessage";
import {getTime} from "../Utils/DateHelper";
import {client} from "../Constants/ServerInfo";
import {ChatContext} from "../Contexts/ChatContext";
import {HubContext} from "../Contexts/HubContext";
import {MessageDto} from "../DtoModels/MessageDto";
import {ServiceResponseGeneric} from "../ServiceResponses/ServiceResponseGeneric";

const MessageArea: React.FC = () => {
    const chatContext = useContext(ChatContext)
    const hubContext = useContext(HubContext)
    const ReceiveMessageMethodName: string = "ReceiveMessage"

    useEffect(() => {
        const initialize = async () => {
            hubContext.subscribe(ReceiveMessageMethodName, async (response) => {
                let serviceResponse: ServiceResponseGeneric<MessageDto> = response as unknown as ServiceResponseGeneric<MessageDto>
                //TODO: проверка респонса на ошибки
                await chatContext.updateChat(serviceResponse.value)
            })
        }
        initialize()
        
        return () => {hubContext.unsubscribe(ReceiveMessageMethodName)}
    }, [chatContext])

    return (
        <div className="messageArea">
            {
                [...chatContext.messages].reverse().map(item =>
                    <TextMessage
                        key={item.id}
                        isClientMessage={item.sender.id === client.id}
                        messageId={item.id}
                        status={item.status}
                        content={item.content}
                        sentTime={getTime(item.sentTime.toString())}/>
                )
            }
        </div>
    );
}

export default MessageArea