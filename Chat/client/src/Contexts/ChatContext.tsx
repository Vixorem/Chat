import React, {useEffect, useState} from 'react'
import {MessageDto} from "../DtoModels/MessageDto";

interface IChatContext {
    setMessages: (messages: MessageDto[]) => void
    setOpenedChatId: (chatId: string) => void
    messages: MessageDto[]
    openedChatId: string
}

const ChatContext = React.createContext<IChatContext>({
    setMessages: messages => {
        throw Error("Не проинициализирован контекст ChatPreview")
    },
    setOpenedChatId: chatId => {
        throw Error("Не проинициализирован контекст ChatPreview")
    },
    messages: [],
    openedChatId: ""
})

export const ChatContextProvider: React.FC = ({children}) => {
    const [messages, setMessages] = useState<MessageDto[]>([])
    const [openedChatId, setOpenedChatId] = useState<string>("")

    return <ChatContext.Provider value={{
        messages,
        openedChatId,
        setMessages,
        setOpenedChatId
    }}>
        {children}
    </ChatContext.Provider>
}

export default ChatContext