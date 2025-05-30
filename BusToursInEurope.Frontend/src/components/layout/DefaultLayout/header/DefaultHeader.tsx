import React, { useEffect, useState } from "react";
import {Link, useNavigate} from "react-router-dom"
import classes from './styles.module.css'
import { JwtTokenKey } from "../../../../utils/constants/localStorageConstants";
import { isAdmin } from "../../../../queries/auth";

export const DefaultHeader: React.FC = () => {
    const [profileString, setProfileString] = useState("Вход")
    const [profileLink, setProfileLink] = useState("/authentication")
    const navigate = useNavigate()

    useEffect(() => {
        const token = localStorage.getItem(JwtTokenKey)
        if (token) {
            const fecthCheckIsAdmin = async () => {
                try {
                    const response = await isAdmin(token)

                    if (response.status === 200) {
                        navigate('/admin')
                    }
                }
                catch {
                    setProfileString("Личный кабинет")
                    setProfileLink("/profile")
                }
            } 

            fecthCheckIsAdmin()
        }
    }, [])

    return(
        <header className={classes.header}>
            <div className={classes.container}>
                <Link to={"/"} className={classes.logo}>
                    Logo
                </Link>
                <nav className={classes.navigation}>
                    <Link to={"/tours"} className={classes.navLink}>
                        <div className={classes.headerElement}>
                            Туры
                        </div>
                    </Link>
                    <Link to={"/about"} className={classes.navLink}>
                        <div className={classes.headerElement}>
                            О нас
                        </div>
                    </Link>
                    <Link to={profileLink} className={classes.navLink}>
                        <div className={classes.headerElement}>
                            {profileString}
                        </div>
                    </Link>
                </nav>
            </div>
        </header>
    );
};