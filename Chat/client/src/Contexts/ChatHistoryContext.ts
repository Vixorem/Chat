import React from 'react'
import {MessageDto} from "../DtoModels/MessageDto";

interface IChatItemClickEventValue {
    data: MessageDto[]
}

const ChatHistoryContext = React.createContext<IChatItemClickEventValue>({data: []})
export const ChatHistoryProvider = ChatHistoryContext.Provider
export default ChatHistoryContext