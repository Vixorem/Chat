import React, {useContext, useEffect} from 'react';
import '../Styles/MessageArea.css'
import TextMessage from "./TextMessage";
import {getTime} from "../Utils/DateHelper";
import {client} from "../Constants/ServerInfo";
import {ChatContext} from "../Contexts/ChatContext";
import {HubContext} from "../Contexts/HubContext";
import {MessageDto, Status} from "../DtoModels/MessageDto";
import {ServiceResponseGeneric} from "../ServiceResponses/ServiceResponseGeneric";
import {ResultType} from "../ServiceResponses/ResultType";
import {MessagePreviewDto} from "../DtoModels/MessagePreviewDto";
import ChatNavigation from "./ChatNavigation/ChatNavigation";
import {ChatNavigationContext} from "../Contexts/ChatNavigationContext";

const MessageArea: React.FC = () => {
    const chatContext = useContext(ChatContext)
    const hubContext = useContext(HubContext)
    const chatNavigationContext = useContext(ChatNavigationContext)
    const ReceiveMessage: string = "ReceiveMessage"

    useEffect(() => {
        const initialize = async () => {
            hubContext.subscribe(ReceiveMessage, async (response) => {
                const serviceResponse = response as unknown as ServiceResponseGeneric<MessageDto>
                if (serviceResponse.resultType === ResultType.Ok) {
                    const message = serviceResponse.value
                    if (chatContext.openedChatId === message.receiverId) {
                        await chatContext.updateChat(serviceResponse.value)
                        const preview = new MessagePreviewDto(
                            message.receiverId,
                            chatContext.openedChatName,
                            message.content,
                            message.sentTime.toString())
                        await chatContext.updatePreview(preview)
                    }
                }
            })
        }
        initialize()

        return () => hubContext.unsubscribe(ReceiveMessage)
    }, [chatContext])

    if (!chatNavigationContext.isActive)
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
    else
        return (
            <div className="messageArea">
                <ChatNavigation/>
            </div>
        )
}

export default MessageArea