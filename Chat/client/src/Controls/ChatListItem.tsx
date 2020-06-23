import React from 'react';
import '../Styles/ChatListItem.css';

interface IProp {
    chatId: string,
    chatName: string,
    messagePreview: string,
    sentTime: string
    chatLoader: (chatId: string) => void
}

const ChatListItem: React.FC<IProp> = (props) => {

    return (
        <div className="chatListItem" key={props.chatId}
             onClick={(e: React.MouseEvent) => props.chatLoader(props.chatId)}>
            <div className="chatName">{props.chatName}</div>
            <div className="time">{props.sentTime}</div>
            <div className="message">{props.messagePreview}</div>
        </div>
    );
}

export default ChatListItem