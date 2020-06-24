import React, {useEffect} from 'react';
import {useState} from 'react';
import './ChatListItem'
import '../Styles/ChatList.css';
import ChatListItem from "./ChatListItem";
import {ServiceResponseGeneric} from "../ServiceResponses/ServiceResponseGeneric"
import {MessagePreviewDto} from "../DtoModels/MessagePreviewDto";
import {getTime} from "../Utils/DateHelper";
import {clientId, host} from "../Constants/ServerInfo";
import {MessageDto} from "../DtoModels/MessageDto";
import {QueryRepository} from "../Repositories/QueryRepository";

const ChatList: React.FC = () => {
    const [error, setError] = useState(null);
    const [isLoaded, setLoaded] = useState(false);
    const [response, setResponse] = useState<ServiceResponseGeneric<MessagePreviewDto[]>>();

    useEffect(() => {
        const response = QueryRepository.getFromServer<ServiceResponseGeneric<MessagePreviewDto[]>>(host, "getmessagepreviewsforuser",
            {name: "userId", value: clientId},
            {name: "offset", value: "0"},
            {name: "limit", value: "100"})

        response.then(result => {
            console.log(result)
            setResponse(result)
            setLoaded(true)
        }).catch((error) => {
            console.log(error)
            setLoaded(false)
            setError(error)
        })
    }, [])

    //TODO: проверки на ошибки

    if (!isLoaded) {
        return <div>Загрузка...</div>
    } else
        return (
            <div className="chatList">
                <div className="messagesTitle">Последние сообщения</div>
                {
                    response?.value!.map(item =>
                        <ChatListItem
                            chatId={item.chatId}
                            chatName={item.chatName}
                            messagePreview={item.lastMessage}
                            sentTime={getTime(item.sentTime.toString())}/>)
                }
            </div>
        );
}

export default ChatList