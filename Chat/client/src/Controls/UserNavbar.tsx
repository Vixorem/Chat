import React from 'react';
import '../Styles/UserNavbar.css';

const UserNavbar: React.FC = () => {
    return (
        <div className="userNavbar">
            <div className="options">
                <button className="bp3-button bp3-minimal bp3-icon-user "/>
                <button className="bp3-button bp3-minimal bp3-icon-cog"/>
                <button>Создать группу</button>
                <button>Написать</button>
            </div>
            <div className="bp3-input-group searchBar">
                <span className="bp3-icon bp3-icon-search"/>
                <input type="text" className="bp3-input"/>
            </div>
        </div>
    );
}

export default UserNavbar