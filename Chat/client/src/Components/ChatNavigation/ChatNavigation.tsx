import React, {useContext, useEffect} from 'react';
import {ChatNavigationContext} from "../../Contexts/ChatNavigationContext";
import {ChatContext} from "../../Contexts/ChatContext";
import "../../Styles/ChatNavigation.css"

const ChatNavigation: React.FC = () => {
    const chatNavigationContext = useContext(ChatNavigationContext)
    const chatContext = useContext(ChatContext)

    useEffect(() => {
        const initialize = async () => {
            chatNavigationContext.loadMembers(chatContext.openedChatId)
        }

        initialize()
    }, [chatNavigationContext])

    if (chatNavigationContext.errorMessage !== "")
        return <div className="chatNavigation">
            {chatNavigationContext.errorMessage}
        </div>
    else
        return (
            <div className="chatNavigation">
                <button className="addMemberButton">
                    ДОБАВИТЬ
                </button>
                <div className="memberList">
                    {
                        chatNavigationContext.members.map(m =>
                            <div key={m.id}
                                 className="memberItem">
                                <div className="memberLogin">
                                    {m.login}
                                </div>
                                <div className="kickMemberBtn"
                                     onClick={(e) => chatNavigationContext.kickMember(m.id)}>
                                    УДАЛИТЬ
                                </div>
                            </div>
                        )
                    }
                </div>
            </div>
        )
}

export default ChatNavigation