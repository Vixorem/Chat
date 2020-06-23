import React, {useEffect} from 'react';
import {useState} from 'react';
import './ChatListItem'
import '../Styles/ChatList.css';
import ChatListItem from "./ChatListItem";
import {ServiceResponseGeneric} from "../ServiceResponses/ServiceResponseGeneric"
import {MessagePreviewDto} from "../DtoModels/MessagePreviewDto";
import {getTime} from "../Utils/DateHelper";
import {host} from "../Constants/ServerInfo";

interface IProp {
    handleClick: (chatId: string) => void
}

const ChatList: React.FC<IProp> = (props) => {
    const [error, setError] = useState(null);
    const [isLoaded, setLoaded] = useState(false);
    const [response, setState] = useState<ServiceResponseGeneric<MessagePreviewDto[]>>();

    useEffect(() => {
        fetch(`${host}/getmessagepreviewsforuser?userId=d2fd7b4c-4fa9-44ae-97e5-b968700f64bd&offset=0`)
            .then(result => result.json() as Promise<ServiceResponseGeneric<MessagePreviewDto[]>>)
            .then(
                (result) => {
                    setState(result)
                    setLoaded(true)
                },
                (error) => {
                    setError(error)
                }
            )
    }, [])

    if (error) {
        console.log(error)
        return <div>Ошибка</div>
    } else if (!isLoaded) {
        return <div>Загрузка</div>
    } else {
        if (response?.resultType === "2") {
            return <div>Ошибка сервера</div>
        }
        if (response?.resultType === "1") {
            return <div>{response.errorMessage}</div>
        }
        return (
            <div className="chatList">
                <div className="messagesTitle">Последние сообщения</div>
                {
                    response?.value!.map(item =>
                        <ChatListItem
                            chatLoader={props.handleClick}
                            chatId={item.chatId}
                            chatName={item.chatName}
                            messagePreview={item.lastMessage}
                            sentTime={getTime(item.sentTime.toString())}/>)
                }
            </div>
        );
    }
}

export default ChatList