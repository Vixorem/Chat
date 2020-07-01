import React, {useContext} from 'react';
import '../Styles/UserNavbar.css';
import {CreateGroupModalContext} from "../Contexts/CreateGroupModal/CreateGroupModalContext";

const UserNavbar: React.FC = () => {
    const createGroupModalContext = useContext(CreateGroupModalContext)

    return (
        <div className="userNavbar">
            <div className="options">
                <button
                    onClick={(e) => createGroupModalContext.setGroupModalState(true)}>
                    Создать группу
                </button>
            </div>
            <div className="searchBar">
                <input type="text"/>
            </div>
        </div>
    );
}

export default UserNavbar