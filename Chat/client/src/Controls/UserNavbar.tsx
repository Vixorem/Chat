import React from 'react';
import '../Styles/UserNavbar.css';

const UserNavbar: React.FC = () => {
    return (
        <div className="userNavbar">
            <div className="options">
                <button>Создать группу</button>
                <button>Написать</button>
            </div>
            <div className="searchBar">
                <input type="text"/>
            </div>
        </div>
    );
}

export default UserNavbar