import React, { useEffect, useState } from "react";
import { GenericTable } from "../../../components/common/GenericTable/GenericTable";
import { Button } from "../../../ui";
import { 
  getAllReservations, 
  deleteReservation, 
  updatePayment,
  getUsersForReservations
} from "../../../queries/reservations";
import classes from './styles.module.css';

export interface ReservationDto {
  id: number;
  date: string;
  paymentDate?: string;
  paymentDeadline: string;
  numOfSeats: number;
  userId: number;
}

export interface ShortUserDto {
  id: number;
  email: string;
}

export const AdminOrdersPage: React.FC = () => {
    const [reservations, setReservations] = useState<ReservationDto[]>([]);
    const [users, setUsers] = useState<ShortUserDto[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    const columns = [
        { key: "id", title: "ID брони", width: "100px" },
        { 
            key: "date", 
            title: "Дата бронирования",
            render: (reservation: ReservationDto) => (
                <div className={classes.dateCell}>
                    {new Date(reservation.date).toLocaleString()}
                </div>
            )
        },
        { 
            key: "paymentDate", 
            title: "Дата оплаты",
            render: (reservation: ReservationDto) => (
                <div className={classes.dateCell}>
                    {reservation.paymentDate 
                        ? new Date(reservation.paymentDate).toLocaleString() 
                        : 'Не оплачено'}
                </div>
            )
        },
        { 
            key: "paymentDeadline", 
            title: "Срок оплаты",
            render: (reservation: ReservationDto) => (
                <div className={classes.dateCell}>
                    {new Date(reservation.paymentDeadline).toLocaleString()}
                </div>
            )
        },
        {
            key: "numOfSeats", 
            title: "Количество мест",
            render: (reservation: ReservationDto) => (
                <div className={classes.seatsCell}>
                    {reservation.numOfSeats}
                </div>
            )
        },
        {
            key: "userEmail", 
            title: "Email пользователя",
            render: (reservation: ReservationDto) => {
                const user = Array.isArray(users) ? users.find(u => u.id === reservation.userId) : null;
                return (
                    <div className={classes.emailCell}>
                        {user?.email || `ID: ${reservation.userId}`}
                    </div>
                );
            }
        },
        { 
            key: "actions", 
            title: "Действия",
            align: "right" as const,
            render: (reservation: ReservationDto) => (
                <div className={classes.actionsCell}>
                    <Button
                        variant={reservation.paymentDate ? "primary" : "outline"}
                        size="sm"
                        onClick={() => handlePaymentStatus(reservation.id, !reservation.paymentDate)}
                        className={classes.paymentButton}
                    >
                        {reservation.paymentDate ? 'Отменить оплату' : 'Подтвердить оплату'}
                    </Button>
                    <Button
                        variant="primary"
                        size="sm"
                        onClick={() => removeReservation(reservation.id)}
                        className={classes.deleteButton}
                    >
                        Удалить
                    </Button>
                </div>
            )
        },
    ];

    useEffect(() => {
        fetchData();
    }, []);

    const fetchData = async () => {
        try {
            setIsLoading(true);
            const reservationsResponse = await getAllReservations();
            const usersResponse = await getUsersForReservations();
            
            setReservations(reservationsResponse.data);
            
            if (usersResponse?.data) {
                setUsers(usersResponse.data.result);
            } else {
                console.warn('Unexpected users data format:', usersResponse);
                setUsers([]);
            }
            
            setError(null);
        } catch (error) {
            console.error('Error fetching data:', error);
            setError("Не удалось загрузить данные");
            setUsers([]);
        } finally {
            setIsLoading(false);
        }
    };

    const removeReservation = async (id: number) => {
        try {
            await deleteReservation(id);
            setReservations(prev => prev.filter(x => x.id !== id));
        } catch(error) {
            console.error('Error deleting reservation:', error);
            setError("Не удалось удалить бронирование");
        }
    };

    const handlePaymentStatus = async (id: number, isPaid: boolean) => {
        try {
            await updatePayment({ id, isPaid });
            setReservations(prev => prev.map(res => 
                res.id === id 
                    ? { ...res, paymentDate: isPaid ? new Date().toISOString() : undefined }
                    : res
            ));
        } catch(error) {
            console.error('Error updating payment status:', error);
            setError("Не удалось изменить статус оплаты");
        }
    };

    const handleExportToExcel = () => {

    };

    return (
        <div className={classes.pageContainer}>
            <div className={classes.header}>
                <h1 className={classes.title}>Управление бронированиями</h1>
                <div className={classes.controls}>
                    <Button
                        variant="primary"
                        onClick={handleExportToExcel}
                        className={classes.exportButton}
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
                    data={reservations}
                    columns={columns}
                    emptyMessage="Нет данных о бронированиях"
                />
            </div>
        </div>
    );
};