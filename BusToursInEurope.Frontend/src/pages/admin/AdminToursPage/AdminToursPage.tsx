import React, { useEffect, useState } from "react";
import { GenericTable } from "../../../components/common/GenericTable/GenericTable";
import { FullTourDto } from "../../../types/Tours";
import { Button } from "../../../ui";
import { deleteTour, getToursByFilters } from "../../../queries/tours";
import classes from './styles.module.css';
import { CreateTourModal } from "../../../components";

export const AdminToursPage: React.FC = () => {
    const [tours, setTours] = useState<FullTourDto[]>([]);
    const [currentTour, setCurrentTour] = useState<FullTourDto | undefined>(undefined);
    const [modalOpen, setModalOpen] = useState(false);

    const columns = [
        { key: "id", title: "ID" },
        { key: "name", title: "Название тура" },
        { 
            key: "price", 
            title: "Цена",
            render: (tour: FullTourDto) => `${tour.price} ₽`
        },
        { key: "startDate", title: "Дата начала" },
        { key: "endDate", title: "Дата окончания" },
        { key: "numOfSeats", title: "Количество мест" },
        { 
            key: "description", 
            title: "Описание",
            render: (tour: FullTourDto) => (
                <div className={classes.description}>
                    {tour.description || 'Нет описания'}
                </div>
            )
        },
        { 
            key: "actions", 
            title: "Действия",
            render: (tour: FullTourDto) => (
                <div className={classes.actions}>
                    <Button onClick={() => onEditClick(tour)}>
                        Редактировать
                    </Button>
                    <Button onClick={() => removeTour(tour.id)}>
                        Удалить
                    </Button>
                </div>
            )
        },
    ];

    useEffect(() => {
        const fetchTours = async () => {
            try {
                // Используем getToursByFilters с пустыми фильтрами
                const response = await getToursByFilters({});
                setTours(response.data);
            } catch (error) {
                console.error('Error fetching tours:', error);
            }
        };

        fetchTours();
    }, []);

    const removeTour = async (id: number) => {
        try {
            await deleteTour(id);
            const newToursArr = tours.filter(x => x.id !== id);
            setTours(newToursArr);
        } catch(error) {
            console.error('Error deleting tour:', error);
        }
    };

    const modalSuccess = async () => {
        setModalOpen(false);
        // После успешного создания/редактирования обновляем список
        const response = await getToursByFilters({});
        setTours(response.data);
    };

    const onEditClick = (tour: FullTourDto) => {
        setCurrentTour(tour);
        setModalOpen(true);
    };

    const onCloseClick = () => {
        setCurrentTour(undefined);
        setModalOpen(false);
    };

    return (
        <div className={classes.main}>
            <CreateTourModal 
                isOpen={modalOpen} 
                onClose={onCloseClick} 
                onSuccess={modalSuccess} 
                id={currentTour?.id}
            />
            
            <div className={classes.actions}>
                <Button onClick={() => setModalOpen(true)}>
                    Создать тур
                </Button>
                <Button>
                    Фильтры
                </Button>
                <Button>
                    Экспорт в Excel
                </Button>
            </div>
            
            <GenericTable 
                data={tours} 
                columns={columns}
                className={classes.table}
            />
        </div>
    );
};