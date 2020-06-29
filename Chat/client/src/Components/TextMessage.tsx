import React from 'react';

import '../Styles/TextMessage.css'
import {Status} from "../DtoModels/MessageDto";

interface IProps {
    messageId: string,
    content: string,
    sentTime: string,
    status: Status
    isClientMessage: boolean
}

const TextMessage: React.FC<IProps> = ({messageId, status, content, sentTime, isClientMessage}) => {
    let messageType = "dialogTextMessage "
    let messageStatus = ""
    if (isClientMessage) {
        messageType += "clientTextMessage"
        messageStatus = "status "
        if (status === Status.Sent) {
            messageStatus += "sent"
        } else if (status === Status.Received) {
            messageStatus += "received"
        } else if (status === Status.Error) {
            messageStatus += "error"
        }
    }
    else
        messageType += "someonesTextMessage"

    return (
        <div className={messageType} key={messageId}>
            <div className="dialogTextContent">{content}</div>
            <div className="textMessageTime">{sentTime}</div>
            <div className={messageStatus}/>
        </div>
    );
}

export default TextMessage