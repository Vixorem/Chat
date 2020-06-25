import React from 'react';

import '../Styles/TextMessage.css'

interface IProps {
    messageId: string,
    content: string,
    sentTime: string,
    isClientMessage: boolean
}

const TextMessage: React.FC<IProps> = ({messageId, content, sentTime, isClientMessage}) => {
    let messageType = "dialogTextMessage "
    if (isClientMessage)
        messageType += "clientTextMessage"
    else
        messageType += "someonesTextMessage"
    
    return (
        <div className={messageType} key={messageId}>
            <div className="dialogTextContent">{content}</div>
            <div className="textMessageTime">{sentTime}</div>
        </div>
    );
}

export default TextMessage