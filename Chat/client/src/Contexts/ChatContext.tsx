import React, {useEffect, useState} from 'react'
import {MessageDto, Status} from "../DtoModels/MessageDto";
import {MessagePreviewDto} from "../DtoModels/MessagePreviewDto";
import {QueryRepository} from "../Repositories/QueryRepository";
import {ServiceResponseGeneric} from "../ServiceResponses/ServiceResponseGeneric";
import {client, host} from "../Constants/ServerInfo";
import {ResultType} from "../ServiceResponses/ResultType";
import {UserDto} from "../DtoModels/UserDto";
import {IRequestParam} from "../Utils/IRequestParam";

const ErrorMessage = "Не проинициализирован контекст ChatContext"

interface IChatContext {
    updateChat: (message: MessageDto) => void
    updatePreview: (preview: MessagePreviewDto) => void;
    loadHistory: (chatId: string) => void
    loadPreviews: () => void
    setOpenedChatId: (chatId: string) => void
    setOpenedChatName: (name: string) => void
    previewClickHandler: (chatId: string, name: string) => void
    messages: MessageDto[]
    openedChatId: string
    openedChatName: string
    chatPreviews: MessagePreviewDto[]
    isPreviewLoaded: boolean
    isHistoryLoaded: boolean
    errorMessage: string
}

export const ChatContext = React.createContext<IChatContext>({
    updateChat: (message) => {
        throw Error(ErrorMessage)
    },
    updatePreview: (preview: MessagePreviewDto) => {
        throw Error(ErrorMessage)
    },
    loadHistory: (chatId) => {
        throw Error(ErrorMessage)
    },
    loadPreviews: () => {
        throw Error(ErrorMessage)
    },
    setOpenedChatId: chatId => {
        throw Error(ErrorMessage)
    },
    setOpenedChatName: name => {
        throw Error(ErrorMessage)
    },
    previewClickHandler: (chatId, name) => {
        throw Error(ErrorMessage)
    },
    
    messages: [],
    openedChatId: "",
    openedChatName: "",
    chatPreviews: [],
    isHistoryLoaded: false,
    isPreviewLoaded: false,
    errorMessage: "",
})

export const ChatContextProvider: React.FC = ({children}) => {
    const [messages, setMessages] = useState<MessageDto[]>([])
    const [chatPreviews, setPreviews] = useState<MessagePreviewDto[]>([])
    const [openedChatId, setOpenedChatId] = useState<string>("")
    const [openedChatName, setOpenedChatName] = useState<string>("")
    const [isHistoryLoaded, setHistoryLoaded] = useState<boolean>(false)
    const [isPreviewLoaded, setPreviewLoaded] = useState<boolean>(false)
    const [errorMessage, setErrorMessage] = useState<string>("")
    const historyLimit: number = 100
    const previewLimit: number = 100
    let historyOffset: number = 0
    let previewOffset: number = 0

    useEffect(() => {
        const initialize = async () => {
            await loadPreviews()
        }
        initialize()
    }, [])

    const executeFetch = async function <T>(controller: string, onOk: (result: T) => void, ...params: IRequestParam[]) {
        try {
            const response = await QueryRepository.getFromServer<ServiceResponseGeneric<T>>(
                host,
                controller,
                ...params)
            if (response.resultType === ResultType.Ok) {
                setErrorMessage("")
                await onOk(response.value)
            } else if (response.resultType === ResultType.Warning) {
                setErrorMessage(response.errorMessage)
            } else if (response.resultType === ResultType.Error) {
                setErrorMessage("Произошла внутренняя ошибка сервера")
            }
        } catch (e) {
            setErrorMessage("Произошла внутренняя ошибка сервера")
        }
    }

    const loadPreviews = async () => {
        setPreviewLoaded(false)
        await executeFetch<MessagePreviewDto[]>("getmessagepreviewsforuser", (result) => {
                setPreviews(prevData => [...result, ...prevData])
                setPreviewLoaded(true)
                previewOffset += previewLimit
            },
            {name: "userId", value: client.id},
            {name: "offset", value: `${previewOffset}`},
            {name: "limit", value: `${previewLimit}`})
    }

    const loadHistory = async (chatId: string) => {
        setHistoryLoaded(false)
        await executeFetch<MessageDto[]>("getchathistory", (result) => {
                setMessages(result)
                setHistoryLoaded(true)
                historyOffset += historyLimit
            },
            {name: "chatId", value: chatId},
            {name: "userId", value: client.id},
            {name: "offset", value: `${historyOffset}`},
            {name: "limit", value: `${historyLimit}`})
    }

    const previewClickHandler = async (chatId: string, name: string) => {
        if (chatId !== openedChatId) {
            historyOffset = 0
        }
        setOpenedChatId(chatId)
        setOpenedChatName(name)
        await loadHistory(chatId)
    }

    const updatePreview = (preview: MessagePreviewDto) => {
        const updatingPreview = chatPreviews.filter(p => p.chatId === preview.chatId)[0]
        if (updatingPreview == null) {
            pushPreview(preview)
            return
        }
        updatingPreview.lastMessage = preview.lastMessage
        updatingPreview.sentTime = preview.sentTime
        const previews = chatPreviews.filter(p => p.chatId !== preview.chatId)
        setPreviews([updatingPreview, ...previews])
    }

    const pushMessage = (message: MessageDto) => {
        setMessages(prev => [message, ...prev])
    }

    const pushPreview = (preview: MessagePreviewDto) => {
        setPreviews(prev => [preview, ...prev])
    }

    const updateChat = (message: MessageDto) => {
        const updatingMessage = messages.filter(m => m.clientMessageId === message.clientMessageId)[0]
        if (updatingMessage == null) {
            pushMessage(message)
            return
        }
        updatingMessage.status = Status.Received
        const filteredMessages = messages.filter(m => m.clientMessageId !== message.clientMessageId)
        setMessages([updatingMessage, ...filteredMessages])
    }

    return <ChatContext.Provider value={{
        chatPreviews,
        openedChatId,
        messages,
        loadHistory,
        loadPreviews,
        previewClickHandler,
        setOpenedChatId,
        errorMessage,
        isPreviewLoaded,
        isHistoryLoaded,
        updateChat,
        openedChatName,
        setOpenedChatName,
        updatePreview,
    }}>
        {children}
    </ChatContext.Provider>
}

export default ChatContextProvider