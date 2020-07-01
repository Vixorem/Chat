import React, {RefObject, useContext, useEffect, useState} from 'react'
import {HubContext} from "../HubContext";
import {GroupDto} from "../../DtoModels/GroupDto";
import {client, host} from "../../Constants/ServerInfo";

const ErrorMessage = "Непроинициализирован контекст AppContext"

interface ICreateGroupModalContext {
    acceptButtonHandler: (errorRef: RefObject<HTMLDivElement>, name: string) => void
    cancelButtonHandler: () => void
    groupModalState: boolean
    setGroupModalState: (s: boolean) => void
}

export const CreateGroupModalContext = React.createContext<ICreateGroupModalContext>({
    acceptButtonHandler: (errorRef, name) => {
        throw Error(ErrorMessage)
    },
    cancelButtonHandler: () => {
        throw Error(ErrorMessage)
    },
    setGroupModalState: s => {
        throw Error(ErrorMessage)
    },
    groupModalState: false
})

export const CreateGroupModalContextProvider: React.FC = ({children}) => {
    const [groupModalState, setGroupModalState] = useState(false)
    const hubContext = useContext(HubContext)


    const acceptButtonHandler = async (errorRef: RefObject<HTMLDivElement>, name: string) => {
        const res = await hubContext.send("CreateGroup", JSON.stringify({
            "group": new GroupDto("", name),
            "creatorId": client.id
        }))
        if (res) {
            setGroupModalState(false)
        } else {
            errorRef.current!.innerText = "Ошибка: не удалось создать группу"
        }
    }

    const cancelButtonHandler = () => {
        setGroupModalState(false)
    }

    return <CreateGroupModalContext.Provider
        value={{
            acceptButtonHandler,
            cancelButtonHandler,
            groupModalState,
            setGroupModalState
        }}>
        {children}
    </CreateGroupModalContext.Provider>
}

export default CreateGroupModalContextProvider