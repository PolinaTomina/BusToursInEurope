import React from "react";
import { NavLink } from "react-router-dom";
import styles from "./styles.module.css";

export const AdminSidebar: React.FC = () => {
    return (
        <div className={styles.sidebar}>
            <div className={styles.sidebarHeader}>
                <h1 className={styles.sidebarTitle}>Администратор</h1>
            </div>
            <nav>
                <ul className={styles.navList}>
                    <li className={styles.navItem}>
                        <NavLink 
                            to="/admin/buses" 
                            className={({ isActive }) => 
                                isActive ? `${styles.navLink} ${styles.active}` : styles.navLink
                            }
                        >
                            Автобусы
                        </NavLink>
                    </li>
                    <li className={styles.navItem}>
                        <NavLink 
                            to="/admin/hotels" 
                            className={({ isActive }) => 
                                isActive ? `${styles.navLink} ${styles.active}` : styles.navLink
                            }
                        >
                            Отели
                        </NavLink>
                    </li>
                    <li className={styles.navItem}>
                        <NavLink 
                            to="/admin/orders" 
                            className={({ isActive }) => 
                                isActive ? `${styles.navLink} ${styles.active}` : styles.navLink
                            }
                        >
                            Брони
                        </NavLink>
                    </li>
                    <li className={styles.navItem}>
                        <NavLink 
                            to="/admin/tours" 
                            className={({ isActive }) => 
                                isActive ? `${styles.navLink} ${styles.active}` : styles.navLink
                            }
                        >
                            Туры
                        </NavLink>
                    </li>
                    <li className={styles.navItem}>
                        <NavLink 
                            to="/admin/profile" 
                            className={({ isActive }) => 
                                isActive ? `${styles.navLink} ${styles.active}` : styles.navLink
                            }
                        >
                            Профиль
                        </NavLink>
                    </li>
                    <li className={styles.navItem}>
                        <NavLink 
                            to="/admin/users" 
                            className={({ isActive }) => 
                                isActive ? `${styles.navLink} ${styles.active}` : styles.navLink
                            }
                        >
                            Пользователи
                        </NavLink>
                    </li>
                </ul>
            </nav>
        </div>
    );
};