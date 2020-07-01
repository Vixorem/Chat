import React, {useContext, useEffect, useRef} from 'react'
import "../../Styles/CreateGroupModal/CreateGroupModal.css"
import {CreateGroupModalContext} from "../../Contexts/CreateGroupModal/CreateGroupModalContext";
import {HubContext} from "../../Contexts/HubContext";
import {ServiceResponseGeneric} from "../../ServiceResponses/ServiceResponseGeneric";
import {ChatContext} from "../../Contexts/ChatContext";
import {MessagePreviewDto} from "../../DtoModels/MessagePreviewDto";
import {getTime} from "../../Utils/DateHelper";
import {ResultType} from "../../ServiceResponses/ResultType";
import {GroupDto} from "../../DtoModels/GroupDto";

const CreateGroupModal: React.FC = () => {
    const AcceptGroupCreation = "AcceptGroupCreation"
    const createGroupModalContext = useContext(CreateGroupModalContext)
    const chatContext = useContext(ChatContext)
    const errorRef = useRef<HTMLDivElement>(null)
    const inputRef = useRef<HTMLInputElement>(null)

    const hubContext = useContext(HubContext)

    useEffect(() => {
        const initialize = async () => {
            hubContext.subscribe(AcceptGroupCreation, async (response) => {
                let serviceResponse = response as unknown as ServiceResponseGeneric<GroupDto>
                if (serviceResponse.resultType === ResultType.Ok) {
                    const group = serviceResponse.value
                    const preview = new MessagePreviewDto(
                        group.id,
                        group.name,
                        "",
                        getTime(Date.now().toString()))
                    await chatContext.updatePreview(preview)
                }
            })
        }
        initialize()

        return () => hubContext.unsubscribe(AcceptGroupCreation)
    }, [])

    if (!createGroupModalContext.groupModalState)
        return (
            <div/>
        );
    else
        return (
            <div className="createGroupModal">
                <div className="createGroupTitile">Создать новую группу</div>
                <input ref={inputRef} className="inputGroupName" placeholder="Название группы"/>
                <button className="createGroupButton"
                        onClick={(e) => createGroupModalContext.acceptButtonHandler(errorRef, inputRef.current?.value ?? "")}>Создать
                </button>
                <button className="cancelCreateGroupButton"
                        onClick={(e) => createGroupModalContext.cancelButtonHandler()}>Закрыть
                </button>
                <div ref={errorRef} className="errorMessage"/>
            </div>
        );
}

export default CreateGroupModal