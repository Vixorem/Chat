import React, {useContext, useEffect, useRef} from 'react';
import '../Styles/InputArea.css'
import {InputContext} from "../Contexts/InputContext";
import {ChatContext} from "../Contexts/ChatContext";

const InputArea: React.FC = () => {
    const inputRef = useRef<HTMLTextAreaElement>(null)
    const chatContext = useContext(ChatContext)
    const inputContext = useContext(InputContext)

    useEffect(() => {
        inputRef.current?.focus()
    }, [chatContext.openedChatId])

    if (chatContext.openedChatId === "")
        return <div/>
    else
        return (
            <div className="writingArea">
                <textarea ref={inputRef} placeholder="Введите сообщение" className="textArea" id="HELLO"
                          onKeyDown={(e) => inputContext.enterKeyHandler(e, inputRef)}/>
                <button className="sendButton"
                        onClick={(e) => inputContext.sendHandler(inputRef)}
                />
            </div>
        );
}

export default InputArea