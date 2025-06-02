import React from "react";
import classes from "./styles.module.css";

export const AboutUsPage: React.FC = () => {
    return (
        <div className={classes.aboutPage}>
            <div className={classes.header}>
                <img 
                    src="/BigLogo.png" 
                    alt="BusToursInEurope Logo" 
                    className={classes.logo}
                />
                <h1 className={classes.title}>О BusToursInEurope</h1>
            </div>

            <section className={classes.section}>
                <p className={classes.text}>
                    Автобусные туры – увлекательная возможность получить массу впечатлений в поездке по бюджетной цене. 
                    Путешествие на автобусах позволяет комфортно добраться в соседние страны.
                </p>
            </section>

            <section className={classes.section}>
                <h2 className={classes.sectionTitle}>История автобусных туров</h2>
                <p className={classes.text}>
                    Популярность таких поездок в Беларуси получила развитие с массовым туризмом после 1991 года. 
                    Стоимость их отличается от тура с перелетом и тура на поезде в меньшую сторону.
                </p>
            </section>

            <section className={classes.section}>
                <h2 className={classes.sectionTitle}>Почему это актуально?</h2>
                <p className={classes.text}>
                    Путешествия становятся доступнее, и всё больше людей ищут удобные и недорогие способы увидеть мир. 
                    Автобусные туры идеально подходят для тех, кто хочет исследовать Европу, не переплачивая за перелёты 
                    и не жертвуя своим комфортом.
                </p>
            </section>

            <section className={classes.section}>
                <h2 className={classes.sectionTitle}>Наша миссия</h2>
                <p className={classes.text}>
                    Для многих планирование поездки сопровождается трудностями: недостатком достоверной информации о турах, 
                    путаницей в расписаниях или сложностями при бронировании. Мы создали BusToursInEurope, чтобы сделать 
                    процесс поиска и бронирования туров простым и интуитивно понятным.
                </p>
                <p className={classes.text}>
                    Наше веб-приложение экономит ваше время и помогает находить оптимальные варианты без лишних усилий.
                </p>
            </section>
        </div>
    )
}