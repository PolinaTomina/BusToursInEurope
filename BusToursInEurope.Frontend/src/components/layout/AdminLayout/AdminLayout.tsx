import React from "react";
import { Outlet } from "react-router-dom";
import { AdminSidebar } from "./Sidebar/AdminSidebar";
import styles from "./styles.module.css";

export const AdminLayout: React.FC = () => {
    return (
        <div>
            <AdminSidebar />
            <div className={styles.mainContent}>
                <Outlet />
            </div>
        </div>
    );
};