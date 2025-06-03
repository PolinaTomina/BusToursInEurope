// src/components/layout/DefaultLayout/DefaultLayout.tsx
import React, { Suspense, useEffect, useRef } from "react";
import { Outlet } from "react-router-dom";
import { DefaultHeader } from "./header/DefaultHeader";
import { DefaultFooter } from "./footer/DefaultFooter";
import { Loader } from "../Loader/Loader";
import classes from "./styles.module.css";

export const DefaultLayout: React.FC = () => {
    const floatingShapesRef = useRef<HTMLDivElement[]>([]);
    const bgRef = useRef<HTMLDivElement>(null);

    useEffect(() => {
        // Анимация плавающих элементов
        floatingShapesRef.current.forEach((shape) => {
            if (!shape) return;
            
            const startX = Math.random() * 80;
            const startY = Math.random() * 80;
            const size = 40 + Math.random() * 120;
            
            shape.style.setProperty('--size', `${size}px`);
            shape.style.setProperty('--start-x', `${startX}%`);
            shape.style.setProperty('--start-y', `${startY}%`);
            shape.style.setProperty('--delay', `${Math.random() * 20}s`);
        });

        // Анимация градиента фона
        if (bgRef.current) {
            let hue = 0;
            const animateBg = () => {
                hue = (hue + 0.2) % 360;
                bgRef.current!.style.background = `linear-gradient(
                    160deg, 
                    hsl(${hue}, 10%, 92%) 0%, 
                    hsl(${(hue + 20) % 360}, 15%, 88%) 100%
                )`;
                requestAnimationFrame(animateBg);
            };
            animateBg();
        }
    }, []);

    const addToRefs = (el: HTMLDivElement | null, index: number) => {
        if (el) floatingShapesRef.current[index] = el;
    };

    return (
        <div className={classes.layout}>
            <div ref={bgRef} className={classes.animatedBackground}>
                {[...Array(12)].map((_, i) => (
                    <div 
                        key={i} 
                        className={classes.floatingShape}
                        ref={(el) => addToRefs(el, i)}
                    />
                ))}
            </div>
            <DefaultHeader />
            <div className={classes.contentWrapper}>
                <main className={classes.mainContent}>
                    <Suspense fallback={<Loader />}>
                        <Outlet />
                    </Suspense>
                </main>
            </div>
            <DefaultFooter />
        </div>
    );
};