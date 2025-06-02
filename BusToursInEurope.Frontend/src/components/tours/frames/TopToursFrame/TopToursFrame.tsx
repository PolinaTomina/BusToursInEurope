import React, { useEffect, useState } from "react";
import classes from "./styles.module.css";
import { getTopTours } from "../../../../queries/tours";
import { ShortTourDto } from "../../../../types/Tours";
import { ShortTourFrame } from "../ShortTourFrame/ShortTourFrame";

interface TopToursFrameProps {}

export const TopToursFrame: React.FC<TopToursFrameProps> = ({}) => {
    const [tours, setTours] = useState<ShortTourDto[]>([]);
    const [loading, setLoading] = useState(true);

    const fetchTopTours = async () => {
        try {
            setLoading(true);
            const { data, status } = await getTopTours();

            if (status === 400 || status === 500) {
                return;
            }

            setTours(data || []);
        } catch (error) {
            console.error("Ошибка при загрузке топовых туров:", error);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchTopTours();
    }, []);

    const getTitle = () => {
        if (tours.length === 0) return "Лучшие туры";
        if (tours.length >= 10) return "Топ 10 туров";
        return `Топ ${tours.length} туров`;
    };

    if (loading) {
        return <div className={classes.loading}>Загрузка лучших туров...</div>;
    }

    if (tours.length === 0) {
        return <div className={classes.noTours}>Нет доступных туров</div>;
    }

    return (
        <section className={classes.topToursSection}>
            <h2 className={classes.sectionTitle}>{getTitle()}</h2>
            <div className={classes.toursGrid}>
                {tours.map((tour) => (
                    <ShortTourFrame key={tour.id} {...tour} />
                ))}
            </div>
        </section>
    );
};