import React, {useEffect, useState} from 'react'
import {MessageDto, Status} from "../DtoModels/MessageDto";
import {MessagePreviewDto} from "../DtoModels/MessagePreviewDto";
import {QueryRepository} from "../Repositories/QueryRepository";
import {ServiceResponseGeneric} from "../ServiceResponses/ServiceResponseGeneric";
import {client, host} from "../Constants/ServerInfo";
import {ResultType} from "../ServiceResponses/ResultType";

interface IChatContext {
    updateChat: (message: MessageDto) => void
    loadHistory: (chatId: string) => void
    loadPreviews: () => void
    setOpenedChatId: (chatId: string) => void
    previewClickHandler: (chatId: string) => void
    messages: MessageDto[]
    openedChatId: string
    chatPreviews: MessagePreviewDto[]
    isPreviewLoaded: boolean
    isHistoryLoaded: boolean
    isError: boolean,
    errorMessage: string
}

export const ChatContext = React.createContext<IChatContext>({
    updateChat: (message) => {
        throw Error("Не проинициализирован контекст ChatContext")
    },
    loadHistory: (chatId) => {
        throw Error("Не проинициализирован контекст ChatContext")
    },
    loadPreviews: () => {
        throw Error("Не проинициализирован контекст ChatContext")
    },
    setOpenedChatId: chatId => {
        throw Error("Не проинициализирован контекст ChatContext")
    },
    previewClickHandler: chatId => {
        throw Error("Не проинициализирован контекст ChatContext")
    },
    messages: [],
    openedChatId: "",
    chatPreviews: [],
    isHistoryLoaded: false,
    isPreviewLoaded: false,
    isError: false,
    errorMessage: ""
})

export const ChatContextProvider: React.FC = ({children}) => {
    const [messages, setMessages] = useState<MessageDto[]>([])
    const [chatPreviews, setPreviews] = useState<MessagePreviewDto[]>([])
    const [openedChatId, setOpenedChatId] = useState<string>("")
    const [isHistoryLoaded, setHistoryLoaded] = useState<boolean>(false)
    const [isPreviewLoaded, setPreviewLoaded] = useState<boolean>(false)
    const [isError, setError] = useState<boolean>(false)
    const [errorMessage, SetErrorMessage] = useState<string>("")
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

    const loadHistory = async (chatId: string) => {
        try {
            setOpenedChatId(chatId)
            setHistoryLoaded(false)
            const response = await QueryRepository.getFromServer<ServiceResponseGeneric<MessageDto[]>>(host, "getchathistory",
                {name: "chatId", value: chatId},
                {name: "userId", value: client.id},
                {name: "offset", value: `${historyOffset}`},
                {name: "limit", value: `${historyLimit}`})
            if (response.resultType === ResultType.Ok) {
                setMessages(response.value)
                setError(false)
                SetErrorMessage("")
                setHistoryLoaded(true)
                historyOffset += historyLimit
            } else if (response.resultType === ResultType.Warning) {
                setError(true)
                SetErrorMessage(response.errorMessage)
            } else if (response.resultType === ResultType.Error) {
                setError(true)
                SetErrorMessage("Произошла внутренняя ошибка сервера")
            }
        } catch (e) {
            setError(true)
            SetErrorMessage("Произошла внутренняя ошибка сервера")
        }
    }

    const loadPreviews = async () => {
        try {
            setPreviewLoaded(false)
            const response = await QueryRepository.getFromServer<ServiceResponseGeneric<MessagePreviewDto[]>>(host, "getmessagepreviewsforuser",
                {name: "userId", value: client.id},
                {name: "offset", value: `${previewOffset}`},
                {name: "limit", value: `${previewLimit}`})
            if (response.resultType === ResultType.Ok) {
                setPreviews(prevData => [...response.value, ...prevData])
                setError(false)
                SetErrorMessage("")
                setPreviewLoaded(true)
                previewOffset += previewLimit
            } else if (response.resultType === ResultType.Warning) {
                setError(true)
                SetErrorMessage(response.errorMessage)
            } else if (response.resultType === ResultType.Error) {
                setError(true)
                SetErrorMessage("Произошла внутренняя ошибка сервера")
            }
        } catch (e) {
            setError(true)
            SetErrorMessage("Произошла внутренняя ошибка сервера")
        }
    }

    const previewClickHandler = async (chatId: string) => {
        if (chatId !== openedChatId) {
            historyOffset = 0
        }
        await loadHistory(chatId)
    }

    const updateChat = (message: MessageDto) => {
        if (message.status === Status.Received) {
            const updatingPreview = chatPreviews.filter(preview => preview.chatId === message.receiverId)[0]
            updatingPreview.lastMessage = message.content
            updatingPreview.sentTime = message.sentTime
            const previews = chatPreviews.filter(preview => preview.chatId !== message.receiverId)
            setPreviews([updatingPreview, ...previews])
            if (openedChatId === message.receiverId) {
                const updatingMessage = messages.filter(m => m.clientMessageId === message.clientMessageId)[0]
                if (updatingMessage == null) {
                    setMessages(prev => [message, ...prev])
                    return
                }
                updatingMessage.status = Status.Received
                const oldMessages = messages.filter(m => m.clientMessageId !== message.clientMessageId)
                setMessages([updatingMessage, ...oldMessages])
            }
        } else if (message.status === Status.Sent) {
            setMessages(prev => [message, ...prev])
        } else if (message.status === Status.Error) {
            setMessages(prev => prev.filter(m => {
                if (m.clientMessageId === message.clientMessageId)
                    m.status = Status.Error
                return m
            }))
        }
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
        isError,
        isPreviewLoaded,
        isHistoryLoaded,
        updateChat
    }}>
        {children}
    </ChatContext.Provider>
}

export default ChatContextProvider