import React, {useContext, useState} from 'react';
import {UserDto} from "../DtoModels/UserDto";
import {QueryRepository} from "../Repositories/QueryRepository";
import {ServiceResponseGeneric} from "../ServiceResponses/ServiceResponseGeneric";
import {client, host} from "../Constants/ServerInfo";
import {ChatContext} from "./ChatContext";
import {ResultType} from "../ServiceResponses/ResultType";
import {IRequestParam} from "../Utils/IRequestParam";
import {ServiceResponse} from "../ServiceResponses/ServiceResponse";

const ErrorMessage = "Непроинициализирован контекст ChatNavigationContext"

interface IChatNavigationContext {
    members: UserDto[]
    loadMembers: (chatid: string) => void
    errorMessage: string
    setErrorMessage: (message: string) => void
    isActive: boolean,
    setActive: (active: boolean) => void
    kickMember: (userId: string) => void
}

export const ChatNavigationContext = React.createContext<IChatNavigationContext>({
    loadMembers: chatid => {
        throw Error(ErrorMessage)
    },
    setErrorMessage: message => {
        throw Error(ErrorMessage)
    },
    setActive: (active) => {
        throw Error(ErrorMessage)
    },
    kickMember: userId => {
        throw Error(ErrorMessage)
    },
    errorMessage: "",
    members: [],
    isActive: false,
})

export const ChatNavigationContextProvider: React.FC = ({children}) => {
    const [members, setMembers] = useState<UserDto[]>([])
    const [errorMessage, setErrorMessage] = useState<string>("")
    const [isActive, setActive] = useState<boolean>(false)
    const chatContext = useContext(ChatContext)

    const executeFetch = async function <T>(controller: string, onOk: (result: T) => void, ...params: IRequestParam[]) {
        try {
            const response = await QueryRepository.getFromServer<ServiceResponseGeneric<T>>(host, controller, ...params)
            if (response.resultType === ResultType.Ok) {
                setErrorMessage("")
                await onOk(response.value)
            } else if (response.resultType === ResultType.Warning) {
                setErrorMessage(response.errorMessage)
            } else if (response.resultType === ResultType.Error) {
                setErrorMessage("Произошла внутренняя ошибка сервера")
            }
        } catch (e) {
            setErrorMessage("Не удалось выполнить запрос")
        }
    }

    const executePost = async (controller: string, onOk: () => void, body: string) => {
        try {
            const response = await QueryRepository.postToServer<ServiceResponse>(host, controller, body)
            if (response.resultType === ResultType.Ok) {
                setErrorMessage("")
                await onOk()
            } else if (response.resultType === ResultType.Warning) {
                setErrorMessage(response.errorMessage)
            } else if (response.resultType === ResultType.Error) {
                setErrorMessage("Произошла внутренняя ошибка сервера")
            }
        } catch (e) {
            console.log(e)
            setErrorMessage("Не удалось выполнить запрос")
        }
    }

    const kickMember = async (userId: string) => {
        await executePost("removeuserfromgroup", () => {
                setMembers(members.filter(m => m.id !== userId))
            },
            JSON.stringify({
                groupId: chatContext.openedChatId,
                removerId: client.id,
                removeeId: userId
            }))
    }

    const loadMembers = async (chatId: string) => {
        await executeFetch<UserDto[]>(
            "getusersingroup",
            (result) => {
                setMembers(result)
            },
            {name: "groupId", value: chatId},
            {name: "userId", value: client.id}
        )
    }

    return <ChatNavigationContext.Provider value={{
        members,
        loadMembers,
        errorMessage,
        setErrorMessage,
        setActive,
        isActive,
        kickMember
    }}>
        {children}
    </ChatNavigationContext.Provider>
}

export default ChatNavigationContextProvider