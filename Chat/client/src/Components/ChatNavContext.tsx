import {UserDto} from "../DtoModels/UserDto";
import React, {useState} from "react";

const ErrorMessage = "Не проинициализирован контекст ChatNavContext"

interface IChatNavContext {
    isGroup: boolean
    setIsGroup: (b: boolean) => void
    members: UserDto[]
}

export const ChatNavContext = React.createContext<IChatNavContext>({
    isGroup: false,
    setIsGroup: b => {
        throw Error(ErrorMessage)
    },
    members: []
})

export const ChatNavContextProvider: React.FC = ({children}) => {
    const [members, setMembers] = useState<UserDto[]>([])
    const [isGroup, setIsGroup] = useState<boolean>(false)
    
    const LoadMembers = async () => {
        if (members.length === 0) {
            //TODO запрос
        }
    }

    return <ChatNavContext.Provider value={{
        members,
        setIsGroup,
        isGroup
    }}>
        {children}
    </ChatNavContext.Provider>
}