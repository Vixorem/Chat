import React, {useEffect, useState} from 'react'
import {HubConnection, HubConnectionBuilder, HubConnectionState} from '@microsoft/signalr';

interface IHubContext {
    unsubscribe: (name: string) => void
    subscribe: (name: string, callback: (data: string) => void) => void
    send: (name: string, data: string) => Promise<boolean>
    isConnected: boolean
}

export const HubContext = React.createContext<IHubContext>({
    subscribe: name => {
        throw Error("Не проинициализирован контекст хаба")
    },
    unsubscribe: name => {
        throw Error("Не проинициализирован контекст хаба")
    },
    send: name => {
        throw Error("Не проинициализирован контекст хаба")
    },
    isConnected: false
})

export const HubContextProvider: React.FC = ({children}) => {
    const [connection, setConnection] = useState<HubConnection>(new HubConnectionBuilder()
        .withUrl("https://localhost:5001/chat")
        .build())
    const [isConnected, setConnected] = useState<boolean>(false)

    const subscribe = (name: string, callback: (data: string) => void) => {
        if (connection != null && isConnected)
            connection.on(name, callback)
    }

    const unsubscribe = (name: string) => {
        if (connection != null && isConnected)
            connection.off(name)
    }

    const send = async (name: string, data: string): Promise<boolean> => {
        try {
            if (connection != null && isConnected) {
                await connection.invoke(name, data)
                return true
            }
            return false
        } catch (e) {
            return false
        }
    }

    useEffect(() => {
        const initialize = async () => {
            try {
                await connection.start()
                setConnected(connection.state === HubConnectionState.Connected)
            } catch (e) {
                throw e
            }
        }

        initialize()
    }, [])

    return <HubContext.Provider
        value={{
            subscribe,
            unsubscribe,
            send,
            isConnected: connection.state === HubConnectionState.Connected
        }}>
        {children}
    </HubContext.Provider>
}

export default HubContextProvider
