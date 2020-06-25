import React, {useEffect, useState} from "react";
import {QueryRepository} from "../Repositories/QueryRepository";
import {ServiceResponse} from "../ServiceResponses/ServiceResponse";
import {clientId, host} from "../Constants/ServerInfo";

type InputEventHandler = () => void

const errMessage: string = "Непроинициализирован контекст Input"

interface IInputContext {
    setEventHandler: React.Dispatch<React.SetStateAction<InputEventHandler>>
    setContent: React.Dispatch<React.SetStateAction<string>>
    setReceiverId: React.Dispatch<React.SetStateAction<string>>
    eventHandler: InputEventHandler
    content: string
    receiverId: string
}

const InputContext = React.createContext<IInputContext>({
    setEventHandler: eventHandler => {
        throw Error(errMessage)
    },
    setContent: value => {
        throw Error(errMessage)
    },
    setReceiverId: () => {
        throw Error(errMessage)
    },
    eventHandler: () => {
        throw Error(errMessage)
    },
    receiverId: "",
    content: ""
})

const InputContextProvider: React.FC = ({children}) => {
    const [eventHandler, setEventHandler] = useState<InputEventHandler>(() => {
    })
    const [content, setContent] = useState<string>("")
    const [receiverId, setReceiverId] = useState<string>("")

    useEffect(() => {
        const initialize = async () => {
            await setEventHandler(() => {
                const response = QueryRepository.postToServer<ServiceResponse>(host, content, JSON.stringify({
                    clientId,
                    receiverId,
                    content
                }))
                //response.then(response => setResponse(response))
            })
        }
    })

    return <InputContext.Provider value={{
        content,
        receiverId,
        eventHandler,
        setEventHandler,
        setContent,
        setReceiverId
    }}>
        {children}
    </InputContext.Provider>
}

export default InputContext