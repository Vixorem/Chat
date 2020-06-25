import React, {useContext} from 'react';
import '../Styles/MessageArea.css'
import TextMessage from "./TextMessage";
import {getTime} from "../Utils/DateHelper";
import {clientId} from "../Constants/ServerInfo";
import ChatContext from "../Contexts/ChatContext";

const MessageArea: React.FC = () => {
    const chatContext = useContext(ChatContext)

    return (
        <div className="messageArea">
            {
                chatContext.messages.reverse().map(item =>
                    <TextMessage
                        isClientMessage={item.sender.id === clientId}
                        messageId={item.id}
                        content={item.content}
                        sentTime={getTime(item.sentTime.toString())}/>
                )
            }
        </div>
    );
}

export default MessageArea