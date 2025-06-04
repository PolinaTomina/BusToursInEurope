import React from "react";
import classes from "./styles.module.css";

export const DefaultFooter: React.FC = () => {
    return (
        <footer className={classes.footer}>
            <div className={classes.footerContent}>
                <div className={classes.footerSection}>
                    <h3>BusToursInEurope</h3>
                    <p>Лучшие автобусные туры по Европе</p>
                </div>
                
                <div className={classes.footerSection}>
                    <h3>Контакты</h3>
                    <p>Email: info@bustours.com</p>
                    <p>Телефон: +375 (XX) XXX-XX-XX</p>
                </div>
                
                <div className={classes.footerSection}>
                    <h3>Быстрые ссылки</h3>
                    <ul>
                        <li><a href="/">Главная</a></li>
                        <li><a href="/tours">Туры</a></li>
                        <li><a href="/about">О нас</a></li>
                        <li><a href="/rules">Правила</a></li>
                    </ul>
                </div>
            </div>
            
            <div className={classes.copyright}>
                © {new Date().getFullYear()} BusToursInEurope. Все права защищены.
            </div>
        </footer>
    );
};