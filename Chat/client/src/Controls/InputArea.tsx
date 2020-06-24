import React, {useContext, useState} from 'react';
import '../Styles/InputArea.css'
import ChatAreaContext from "../Contexts/ChatAreaContext";
import {QueryRepository} from "../Repositories/QueryRepository";
import {ServiceResponse} from "../ServiceResponses/ServiceResponse";
import {clientId, host} from "../Constants/ServerInfo";

const InputArea: React.FC = () => {
    const [textContent, setTextContent] = useState<string>("")
    const [response, setResponse] = useState<ServiceResponse>()
    const chatAreaContext = useContext(ChatAreaContext)

    function sendMessageButtonClickHandler(content: string, receiverId: string) {
        const response = QueryRepository.postToServer<ServiceResponse>(host, "sendtextmessage", JSON.stringify({
            clientId,
            receiverId,
            content
        }))
        response.then(response => setResponse(response))
    }

    if (chatAreaContext.openedChatId === "")
        return <div/>
    else
        return (
            <div className="writingArea">
            <textarea placeholder="Введите сообщение" className="textArea"
                      onChange={(e: React.ChangeEvent) => setTextContent(e.target.textContent ?? "")}/>
                <button className="sendButton"
                        onClick={
                            (e: React.MouseEvent) => {
                                if (textContent !== "")
                                    sendMessageButtonClickHandler(textContent, chatAreaContext.openedChatId)
                            }}/>
            </div>
        );
}

export default InputArea