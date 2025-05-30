import React, { Suspense } from "react";
import { Outlet } from "react-router-dom";
import { DefaultHeader } from "./header/DefaultHeader";
import { DefaultFooter } from "./footer/DefaultFooter";
import { Loader } from "../Loader/Loader";


export const DefaultLayout: React.FC = () => {
    return(
        <div>
            <DefaultHeader />
                <Suspense fallback={<Loader/>}>
                    <Outlet />
                </Suspense>
            <DefaultFooter />
        </div>
    )
}