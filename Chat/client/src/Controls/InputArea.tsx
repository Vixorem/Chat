import React, {useState} from 'react';
import '../Styles/InputArea.css'
import {MessageDto} from "../DtoModels/MessageDto";

interface IProp {
    handleSendMessage: (content: string, receiverId: string) => void
    currentChatId: string
}

const InputArea: React.FC<IProp> = (props) => {
    const [textContent, setTextContent] = useState<string>("")


    return (
        <div className="writingArea">
            <textarea placeholder="Введите сообщение" className="textArea"
                      onChange={(e: React.ChangeEvent) => setTextContent(e.target.textContent ?? "")}/>
            <button className="sendButton"
                    onClick={
                        (e: React.MouseEvent) => {
                            if (textContent !== "") props.handleSendMessage(textContent, props.currentChatId)
                        }}/>
        </div>
    );
}

export default InputArea