import React, {useContext} from 'react';
import '../Styles/ChatPreview.css';
import {ChatContext} from "../Contexts/ChatContext";

interface IProp {
    chatId: string,
    chatName: string,
    messagePreview: string,
    sentTime: string
}

const ChatPreview: React.FC<IProp> = (props) => {
    const context = useContext(ChatContext)

    return (
        <div className="chatPreview" key={props.chatId}
             onClick={(e) => {
                 context.previewClickHandler(props.chatId, props.chatName)
             }}>
            <div className="chatName">{props.chatName}</div>
            <div className="time">{props.sentTime}</div>
            <div className="message">{props.messagePreview}</div>
        </div>
    );
}

export default ChatPreview