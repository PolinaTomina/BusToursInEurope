// DefaultHeader.tsx
import React, { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import classes from './styles.module.css';
import { JwtTokenKey } from "../../../../utils/constants/localStorageConstants";
import { isAdmin } from "../../../../queries/auth";

export const DefaultHeader: React.FC = () => {
    const [profileString, setProfileString] = useState("Вход");
    const [profileLink, setProfileLink] = useState("/authentication");
    const navigate = useNavigate();

    useEffect(() => {
        const token = localStorage.getItem(JwtTokenKey);
        if (token) {
            const fetchCheckIsAdmin = async () => {
                try {
                    const response = await isAdmin(token);
                    if (response.status === 200) {
                        navigate('/admin');
                    }
                } catch {
                    setProfileString("Личный кабинет");
                    setProfileLink("/profile");
                }
            };
            fetchCheckIsAdmin();
        }
    }, [navigate]);

    return (
        <header className={classes.header}>
            <div className={classes.container}>
                <Link to="/" className={classes.logo}>
                    <img src="public/logo.jpg" alt="Логотип" className={classes.logoImage} />
                    <span className={classes.logoText}>Автобусные туры</span>
                </Link>
                <nav className={classes.navigation}>
                    <Link to="/tours" className={classes.navLink}>
                        <span className={classes.linkText}>Туры</span>
                    </Link>
                    <Link to="/about" className={classes.navLink}>
                        <span className={classes.linkText}>О нас</span>
                    </Link>
                    <Link to="/rules" className={classes.navLink}>
                        <span className={classes.linkText}>Правила</span>
                    </Link>
                    <Link to={profileLink} className={classes.navLink}>
                        <span className={classes.linkText}>{profileString}</span>
                    </Link>
                </nav>
            </div>
        </header>
    );
};