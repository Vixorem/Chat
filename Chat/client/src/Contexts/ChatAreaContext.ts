import React, {createContext} from 'react'

interface IChatAreaValue {
    openedChatId: string
}

const ChatAreaContext = createContext<IChatAreaValue>({
    openedChatId: ""
})

export const ChatAreaProvider = ChatAreaContext.Provider
export default ChatAreaContext