import React, {useContext} from 'react';
import '../Styles/MessageArea.css'
import TextMessage from "./TextMessage";
import {getTime} from "../Utils/DateHelper";
import ChatHistoryContext from "../Contexts/ChatHistoryContext";
import ChatAreaContext from "../Contexts/ChatAreaContext";
import {clientId} from "../Constants/ServerInfo";

const MessageArea: React.FC = () => {
    const historyContext = useContext(ChatHistoryContext)
    const chatArea = useContext(ChatAreaContext)

    return (
        <div className="messageArea">
            {
                historyContext.data.reverse().map(item =>
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