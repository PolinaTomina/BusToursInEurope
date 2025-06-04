import React, { useEffect, useState } from "react";
import { GenericTable } from "../../../components/common/GenericTable/GenericTable";
import { FullTourDto } from "../../../types/Tours";
import { Button } from "../../../ui";
import { deleteTour, downloadTopToursExcel, getToursByFilters } from "../../../queries/tours";
import classes from './styles.module.css';
import { CreateTourModal } from "../../../components";

export const AdminToursPage: React.FC = () => {
    const [tours, setTours] = useState<FullTourDto[]>([]);
    const [currentTour, setCurrentTour] = useState<FullTourDto | undefined>(undefined);
    const [modalOpen, setModalOpen] = useState(false);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    const columns = [
        { key: "id", title: "ID", width: "80px" },
        { key: "name", title: "Название тура" },
        { 
            key: "price", 
            title: "Цена",
            render: (tour: FullTourDto) => (
                <div className={classes.priceCell}>
                    {tour.price.toLocaleString()}
                </div>
            )
        },
        { 
            key: "startDate", 
            title: "Дата начала",
            render: (tour: FullTourDto) => (
                <div className={classes.dateCell}>
                    {new Date(tour.startDate).toLocaleDateString()}
                </div>
            )
        },
        { 
            key: "endDate", 
            title: "Дата окончания",
            render: (tour: FullTourDto) => (
                <div className={classes.dateCell}>
                    {new Date(tour.endDate).toLocaleDateString()}
                </div>
            )
        },
        {
            key: "numOfSeats", 
            title: "Места",
            render: (tour: FullTourDto) => (
                <div className={classes.seatsCell}>
                    {tour.numOfSeats}
                </div>
            )
        },
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
            align: "right" as const,
            render: (tour: FullTourDto) => (
                <div className={classes.actions}>
                    <Button
                        variant="secondary"
                        size="sm"
                        onClick={(e) => {
                            e.stopPropagation();
                            onEditClick(tour);
                        }}
                        className={classes.editButton}
                    >
                        Редактировать
                    </Button>
                    <Button
                        variant="primary"
                        size="sm"
                        onClick={(e) => {
                            e.stopPropagation();
                            removeTour(tour.id);
                        }}
                        className={classes.deleteButton}
                    >
                        Удалить
                    </Button>
                </div>
            )
        },
    ];

    useEffect(() => {
        const fetchTours = async () => {
            try {
                setIsLoading(true);
                const response = await getToursByFilters({});
                setTours(response.data);
                setError(null);
            } catch (error) {
                console.error('Error fetching tours:', error);
                setError("Не удалось загрузить список туров");
            } finally {
                setIsLoading(false);
            }
        };

        fetchTours();
    }, []);

    const removeTour = async (id: number) => {
        try {
            await deleteTour(id);
            setTours(prev => prev.filter(x => x.id !== id));
        } catch(error) {
            console.error('Error deleting tour:', error);
            setError("Не удалось удалить тур");
        }
    };

    const modalSuccess = async () => {
        try {
            setIsLoading(true);
            const response = await getToursByFilters({});
            setTours(response.data);
            setError(null);
        } catch (error) {
            console.error('Error refreshing tours:', error);
            setError("Не удалось обновить список туров");
        } finally {
            setIsLoading(false);
            setModalOpen(false);
        }
    };

    const onEditClick = (tour: FullTourDto) => {
        setCurrentTour(tour);
        setModalOpen(true);
    };

    const onCloseClick = () => {
        setCurrentTour(undefined);
        setModalOpen(false);
    };

    const handleDownloadExcel = async () => {
        try {
            await downloadTopToursExcel()
        } catch {

        }
    }

    return (
        <div className={classes.pageContainer}>
            <div className={classes.header}>
            <h1 className={classes.title}>Управление турами</h1>
            <div className={classes.controls}>
                <Button 
                onClick={() => setModalOpen(true)}
                className={classes.addButton}
                >
                    Создать тур
                </Button>
                <Button
                variant="primary"
                className={classes.exportButton}
                onClick={() => handleDownloadExcel()}
                >
                    Экспорт в Excel
                </Button>
            </div>
            </div>

            {error && (
                <div className={classes.errorAlert}>
                    {error}
                </div>
            )}

            <div className={classes.tableContainer}>
                <GenericTable 
                    data={tours}
                    columns={columns}
                    emptyMessage="Нет данных о турах"
                />
            </div>

            <CreateTourModal 
                isOpen={modalOpen} 
                onClose={onCloseClick} 
                onSuccess={modalSuccess} 
                id={currentTour?.id}
            />
        </div>
    );
};