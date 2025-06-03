import React, { useEffect, useState } from "react";
import { GenericTable } from "../../../components/common/GenericTable/GenericTable";
import { BusDto } from "../../../types/Buses";
import { Button } from "../../../ui";
import { deleteBus, getBuses } from "../../../queries/buses";
import classes from './styles.module.css';
import { CreateBusModal } from "../../../components";

export const AdminBusPage: React.FC = () => {
    const [buses, setBuses] = useState<BusDto[]>([]);
    const [currentBus, setCurrentBus] = useState<BusDto | undefined>(undefined);
    const [modalOpen, setModalOpen] = useState(false);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    const columns = [
        { key: "id", title: "ID", width: "80px" },
        { key: "name", title: "Название автобуса" },
        { key: "numOfSeats", title: "Количество мест", align: "center" as const },
        { 
            key: "actions", 
            title: "Действия",
            align: "right" as const,
            render: (bus: BusDto) => (
                <div className={classes.actionsCell}>
                    <Button
                        variant="primary"
                        size="sm"
                        onClick={(e) => {
                            e.stopPropagation();
                            onEditClick(bus);
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
                            removeBus(bus.id);
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
        const fetchBuses = async () => {
            try {
                setIsLoading(true);
                const response = await getBuses({});
                setBuses(response.data);
                setError(null);
            } catch (error) {
                console.error('Error fetching buses:', error);
                setError("Не удалось загрузить список автобусов");
            } finally {
                setIsLoading(false);
            }
        };
    
        fetchBuses();
    }, []);

    const removeBus = async (id: number) => {
        try {
            await deleteBus(id);
            setBuses(prev => prev.filter(x => x.id !== id));
        } catch(error) {
            console.error('Error deleting bus:', error);
            setError("Не удалось удалить автобус");
        }
    };

    const handleRowClick = (bus: BusDto) => {
        onEditClick(bus);
    };

    const onEditClick = (bus: BusDto) => {
        setCurrentBus(bus);
        setModalOpen(true);
    };

    const onCloseClick = () => {
        setCurrentBus(undefined);
        setModalOpen(false);
    };

    const modalSuccess = async () => {
        try {
            setIsLoading(true);
            const response = await getBuses({});
            setBuses(response.data);
            setError(null);
        } catch (error) {
            console.error('Error refreshing buses:', error);
            setError("Не удалось обновить список автобусов");
        } finally {
            setIsLoading(false);
            setModalOpen(false);
        }
    };

    return (
        <div className={classes.pageContainer}>
            <div className={classes.header}>
                <h1 className={classes.title}>Управление автобусами</h1>
                <div className={classes.controls}>
                    <Button 
                        onClick={() => setModalOpen(true)}
                        className={classes.addButton}
                    >
                        + Добавить автобус
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
                    data={buses}
                    columns={columns}
                    emptyMessage="Нет данных об автобусах"
                    onRowClick={handleRowClick}
                />
            </div>

            <CreateBusModal 
                isOpen={modalOpen} 
                onClose={onCloseClick} 
                onSuccess={modalSuccess} 
                model={currentBus}
            />
        </div>
    );
};