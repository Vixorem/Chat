import React, {RefObject, useContext, useRef, useState} from "react";
import {client, host} from "../Constants/ServerInfo";
import {MessageDto, Status} from "../DtoModels/MessageDto";
import {ChatContext} from "./ChatContext";
import {HubContext} from "./HubContext";

type InputButtonClickHandler = (inputRef: RefObject<HTMLTextAreaElement>) => void
type InputEnterKeyHandler = (e: React.KeyboardEvent<HTMLTextAreaElement>, inputRef: RefObject<HTMLTextAreaElement>) => void

const errMessage: string = "Непроинициализирован контекст Input"

interface IInputContext {
    sendHandler: InputButtonClickHandler
    enterKeyHandler: InputEnterKeyHandler
}

export const InputContext = React.createContext<IInputContext>({
    sendHandler: (inputRef) => {
        throw Error(errMessage)
    },
    enterKeyHandler: () => {
        throw Error(errMessage)
    }
})

const InputContextProvider: React.FC = ({children}) => {
    const inputContext = useContext(InputContext)
    const chatContext = useContext(ChatContext)
    const hubContext = useContext(HubContext)

    const sendHandler = async (inputRef: RefObject<HTMLTextAreaElement>) => {
        if (inputRef.current?.value !== "") {
            let message = new MessageDto(
                inputRef.current?.value ?? "",
                client,
                chatContext.openedChatId,
                `${Date.now()}`)
            chatContext.updateChat(message)
            console.log(JSON.stringify(message))
            const res: boolean = await hubContext.send("SendMessage", JSON.stringify(message))
            if (!res) {
                message.status = Status.Error
                chatContext.updateChat(message)
            }
        }
    }

    const enterKeyHandler = (e: React.KeyboardEvent<HTMLTextAreaElement>, inputRef: RefObject<HTMLTextAreaElement>) => {
        if (e.key === "Enter")
            inputContext.sendHandler(inputRef)
        inputRef.current?.scrollIntoView()
    }

    return <InputContext.Provider value={{sendHandler, enterKeyHandler}}>
        {children}
    </InputContext.Provider>
}

export default InputContextProvider