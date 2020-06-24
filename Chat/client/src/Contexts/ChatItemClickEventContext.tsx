import React from 'react'
import {MessageDto} from "../DtoModels/MessageDto";

interface IChatItemClickEventValue {
    setChatMessages: (messages: MessageDto[]) => void,
    setOpenedChatId: (chatId: string) => void
}

const ChatItemClickEventContext = React.createContext<IChatItemClickEventValue>({
    setChatMessages: messages => {
    },
    setOpenedChatId: chatId => {
    }
})
export const ChatItemClickEventProvider = ChatItemClickEventContext.Provider
export default ChatItemClickEventContext