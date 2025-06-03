import React, { useEffect, useState } from "react";
import { GenericTable } from "../../../components/common/GenericTable/GenericTable";
import { Button } from "../../../ui";
import { getAll, deleteRoute } from "../../../queries/routes";
import { CreateRouteModal } from "../../../components";
import classes from './styles.module.css';
import { RouteBusDto } from "../../../types/Routes";

export const AdminRoutesBusPage: React.FC = () => {
    const [routes, setRoutes] = useState<RouteBusDto[]>([]);
    const [currentRoute, setCurrentRoute] = useState<RouteBusDto | undefined>(undefined);
    const [modalOpen, setModalOpen] = useState(false);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    const columns = [
        { key: "id", title: "ID", width: "80px" },
        { 
            key: "name", 
            title: "Название маршрута",
            render: (route: RouteBusDto) => (
                <div className={classes.nameCell}>
                    {route.name}
                </div>
            )
        },
        { 
            key: "distance", 
            title: "Расстояние (км)",
            render: (route: RouteBusDto) => (
                <div className={classes.distanceCell}>
                    {route.distance}
                </div>
            )
        },
        { 
            key: "waypoints", 
            title: "Количество точек",
            render: (route: RouteBusDto) => (
                <div className={classes.waypointsCell}>
                    {route.wayPointsDto?.length || 0}
                </div>
            )
        },
        { 
            key: "actions", 
            title: "Действия",
            align: "right" as const,
            render: (route: RouteBusDto) => (
                <div className={classes.actionsCell}>
                    <Button
                        variant="outline"
                        size="sm"
                        onClick={() => onEditClick(route)}
                        className={classes.editButton}
                    >
                        Редактировать
                    </Button>
                    <Button
                        variant="primary"
                        size="sm"
                        onClick={() => removeRoute(route.id)}
                        className={classes.deleteButton}
                    >
                        Удалить
                    </Button>
                </div>
            )
        },
    ];

    useEffect(() => {
        fetchRoutes();
    }, []);

    const fetchRoutes = async () => {
        try {
            setIsLoading(true);
            const response = await getAll();
            setRoutes(response.data);
            setError(null);
        } catch (error) {
            console.error('Error fetching routes:', error);
            setError("Не удалось загрузить список маршрутов");
        } finally {
            setIsLoading(false);
        }
    };

    const removeRoute = async (id: number) => {
        try {
            await deleteRoute(id);
            setRoutes(prev => prev.filter(x => x.id !== id));
        } catch(error) {
            console.error('Error deleting route:', error);
            setError("Не удалось удалить маршрут");
        }
    };

    const onEditClick = (route: RouteBusDto) => {
        setCurrentRoute(route);
        setModalOpen(true);
    };

    const onCloseClick = () => {
        setCurrentRoute(undefined);
        setModalOpen(false);
    };

    const handleSuccess = async () => {
        try {
            setIsLoading(true);
            const response = await getAll();
            setRoutes(response.data);
            setError(null);
        } catch (error) {
            console.error('Error refreshing routes:', error);
            setError("Не удалось обновить список маршрутов");
        } finally {
            setIsLoading(false);
            setModalOpen(false);
        }
    };

    return (
        <div className={classes.pageContainer}>
            <div className={classes.header}>
                <h1 className={classes.title}>Управление маршрутами автобусов</h1>
                <div className={classes.controls}>
                    <Button 
                        onClick={() => setModalOpen(true)}
                        className={classes.addButton}
                    >
                        + Создать маршрут
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
                    data={routes}
                    columns={columns}
                    emptyMessage="Нет данных о маршрутах"
                />
            </div>

            <CreateRouteModal 
                isOpen={modalOpen} 
                onClose={onCloseClick} 
                onSuccess={handleSuccess} 
                route={currentRoute}
            />
        </div>
    );
};