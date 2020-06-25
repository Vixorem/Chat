import React, {useContext, useState} from 'react';
import '../Styles/InputArea.css'
import {QueryRepository} from "../Repositories/QueryRepository";
import {ServiceResponse} from "../ServiceResponses/ServiceResponse";
import {clientId, host} from "../Constants/ServerInfo";
import ChatContext from "../Contexts/ChatContext";
import InputContext from "../Contexts/InputContext";

const InputArea: React.FC = () => {
    const [textContent, setTextContent] = useState<string>("")
    const [response, setResponse] = useState<ServiceResponse>()
    const chatContext = useContext(ChatContext)
    const inputContext = useContext(InputContext)


    if (chatContext.openedChatId === "")
        return <div/>
    else
        return (
            <div className="writingArea">
            <textarea placeholder="Введите сообщение" className="textArea"
                      onChange={(e: React.ChangeEvent) => inputContext.setContent(e.target.textContent ?? "")}/>
                <button className="sendButton"
                        onClick={
                            (e: React.MouseEvent) => {
                                if (textContent !== "")
                                    inputContext.setReceiverId(chatContext.openedChatId)
                                inputContext.eventHandler()
                            }}/>
            </div>
        );
}

export default InputArea