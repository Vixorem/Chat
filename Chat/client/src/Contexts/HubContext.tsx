import React, {useEffect, useState} from 'react'
import {HubConnection, HubConnectionBuilder} from '@microsoft/signalr';
import {host} from "../Constants/ServerInfo";

interface IHubContext {
    connection: HubConnection | undefined
    setConnection: React.Dispatch<React.SetStateAction<HubConnection | undefined>>
}

export const HubContext = React.createContext<IHubContext>({
    connection: undefined,
    setConnection: value => {
        throw Error("Не проинициализирован контекст хаба")
    }
})

export const HubContextProvider: React.FC = ({children}) => {
    const [connection, setConnection] = useState<HubConnection | undefined>()

    useEffect(() => {
        const initialize = async () => {
            const hubConnection = new HubConnectionBuilder()
                .withUrl("/chat")
                .build()
            hubConnection.baseUrl = host + "/chat"
            try {
                //await hubConnection.start()
                setConnection(hubConnection)
            } catch (e) {
                throw e
            }
        }

        initialize()
    })

    return <HubContext.Provider value={{connection, setConnection}}>
        {children}
    </HubContext.Provider>
}

export default HubContext
