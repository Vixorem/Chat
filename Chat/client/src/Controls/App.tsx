import React, {useState} from 'react';
import '../Styles/App.css';
import UserNavbar from "./UserNavbar";
import ChatList from "./ChatList";
import ChatHeader from "./ChatHeader";
import MessageArea from "./MessageArea";
import MessageInputArea from "./InputArea";
import {ServiceResponseGeneric} from "../ServiceResponses/ServiceResponseGeneric";
import {MessageDto} from "../DtoModels/MessageDto";
import {host} from "../Constants/ServerInfo";
import {ServiceResponse} from "../ServiceResponses/ServiceResponse";
import {errorAlert, errorAlertGeneric} from "../Utils/errorAlert";

const App: React.FC = () => {
    const clientId: string = "d2fd7b4c-4fa9-44ae-97e5-b968700f64bd"
    const [chatMessages, setChatMessages] = useState<ServiceResponseGeneric<MessageDto[]>>();
    const [currentChatId, setCurrentChatId] = useState<string>("");
    const [postResponse, setPostResponse] = useState<ServiceResponse>();

    function chatListItemClickHandler(chatId: string) {
        setCurrentChatId(chatId)
        fetch(`${host}/getchathistory?chatId=${chatId}&userId=${clientId}&offset=0`)
            .then(result =>
                result.json() as Promise<ServiceResponseGeneric<MessageDto[]>>)
            .then((result: ServiceResponseGeneric<MessageDto[]>) => {
                    errorAlertGeneric<MessageDto[]>(result)
                    setChatMessages(result)
                }
            )
    }

    function sendMessageButtonClickHandler(content: string, receiverId: string = currentChatId) {
        fetch(`${host}/sendtextmessage`, {
            method: "POST",
            body: JSON.stringify({clientId, receiverId, content})
        })
            .then((response) =>
                response.json() as Promise<ServiceResponse>)
            .then((response: ServiceResponse) => {
                errorAlert(response)
                setPostResponse(response)
            })
    }

    return (
        <div className="app">
            <UserNavbar/>
            <ChatHeader/>
            <ChatList handleClick={chatListItemClickHandler}/>
            <MessageArea response={chatMessages!}/>
            <MessageInputArea currentChatId={currentChatId} handleSendMessage={sendMessageButtonClickHandler}/>
        </div>
    );
}

export default App;
