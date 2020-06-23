import React, {useEffect, useState} from 'react';

import '../Styles/MessageArea.css'
import TextMessage from "./TextMessage";
import {ServiceResponseGeneric} from "../ServiceResponses/ServiceResponseGeneric";
import {MessageDto} from "../DtoModels/MessageDto";
import {getTime} from "../Utils/DateHelper";
import {errorAlert} from "../Utils/errorAlert";

interface IProp {
    response: ServiceResponseGeneric<MessageDto[]>
}

const MessageArea: React.FC<IProp> = (props) => {
    //TODO: пока что храним Guid пользователя здесь
    const clientGuid = "d2fd7b4c-4fa9-44ae-97e5-b968700f64bd"

    return (
        <div className="messageArea">
            {
                props.response?.value.reverse().map(item =>
                    <TextMessage
                        isClientMessage={item.sender.id === clientGuid}
                        messageId={item.id}
                        content={item.content}
                        sentTime={getTime(item.sentTime.toString())}/>
                )
            }
        </div>
    );
}

export default MessageArea