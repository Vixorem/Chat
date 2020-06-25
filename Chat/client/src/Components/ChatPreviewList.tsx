import React, {useContext, useEffect} from 'react';
import {useState} from 'react';
import './ChatPreview'
import '../Styles/ChatList.css';
import ChatPreview from "./ChatPreview";
import {ServiceResponseGeneric} from "../ServiceResponses/ServiceResponseGeneric"
import {MessagePreviewDto} from "../DtoModels/MessagePreviewDto";
import {getTime} from "../Utils/DateHelper";
import {clientId, host} from "../Constants/ServerInfo";
import {QueryRepository} from "../Repositories/QueryRepository";
import HubContext from "../Contexts/HubContext";

const ChatPreviewList: React.FC = () => {
    const loadChatPreviews: string = "LoadChatPreviews"
    const [error, setError] = useState(null);
    const [isLoaded, setLoaded] = useState(false);
    const [response, setResponse] = useState<ServiceResponseGeneric<MessagePreviewDto[]>>();
    const [preview, setPreview] = useState<MessagePreviewDto>()
    const hubContext = useContext(HubContext)

    useEffect(() => {
        const get = async () => {
            await hubContext.connection!.on("UpdatePreview", (preview) =>
                setPreview(preview)
            )
            try {
                const response = await QueryRepository.getFromServer<ServiceResponseGeneric<MessagePreviewDto[]>>(host, "getmessagepreviewsforuser",
                    {name: "userId", value: clientId},
                    {name: "offset", value: "0"},
                    {name: "limit", value: "100"})
                setResponse(response)
                setLoaded(true)
            } catch (e) {
                console.log(error)
                setLoaded(false)
                setError(error)
            }
        }

        get()
    }, [])

    //TODO: проверки на ошибки
    if (!isLoaded) {
        return <div>Загрузка...</div>
    } else
        return (
            <div className="chatList">
                <div className="messagesTitle">Последние сообщения{preview}</div>
                {
                    response?.value!.map(item =>
                        <ChatPreview
                            chatId={item.chatId}
                            chatName={item.chatName}
                            messagePreview={item.lastMessage}
                            sentTime={getTime(item.sentTime.toString())}/>)
                }
            </div>
        );
}

export default ChatPreviewList