import React, { useEffect, useState } from "react";
import { GenericTable } from "../../../components/common/GenericTable/GenericTable";
import { BusDto } from "../../../types/Buses";
import { Button } from "../../../ui";
import { deleteBus, getBuses } from "../../../queries/buses";
import classes from './styles.module.css'
import { CreateBusModal } from "../../../components";

export const AdminBusPage: React.FC = ({ }) => {
    const [buses, setBuses] = useState<BusDto[]>([])
    const [currentBus, setCurrentBus] = useState<BusDto | undefined>(undefined)
    const [modalOpen, setModalOpen] = useState(false)

    const columns = [
        {key: "id", title: "ID"},
        {key: "name", title: "Название"},
        {key: "numOfSeats", title: "Количество мест"},
        { 
            key: "actions", 
            title: "Действия",
            render: (bus: BusDto) => (
                <div>
                    <Button 
                        onClick={() => onEditClick(bus)}>
                        Редактировать
                    </Button>
                    <Button onClick={() => removeBus(bus.id)}>
                        Удалить
                    </Button>
                </div>
            )
          },
    ]

    useEffect(() => {
        const fetchBuses = async () => {
            try {
                const response = await getBuses({});
                setBuses(response.data);
            } catch (error) {
                console.error('Error fetching buses:', error);
            }
        };
    
        fetchBuses();
    }, [])

    const removeBus = async (id: number) => {
        try {
            await deleteBus(id)
            const newBusesArr = buses.filter(x => x.id !== id)
            setBuses(newBusesArr)
        } catch(error) {

        }
    }

    const modalSuccess = async () => {
        setModalOpen(false)
        const newBusesArr = await getBuses({})
        setBuses(newBusesArr.data)
    }

    const onEditClick = (bus: BusDto) => {
        setCurrentBus(_ => {
            return bus;
        });
        setModalOpen(true)
    }

    const onCloseClick = () => {
        setCurrentBus(undefined)
        setModalOpen(false)
    }

    return(
        <div className={classes.main}>
            <CreateBusModal isOpen={modalOpen} onClose={() => onCloseClick()} onSuccess={() => modalSuccess()} model={currentBus}/>
            <div className={classes.actions}>
                <Button onClick={() => setModalOpen(true)}>
                    Создать автобус
                </Button>
                <Button>
                    Фильтры
                </Button>
                <Button>
                    Экспорт в Excel
                </Button>
            </div>
            <GenericTable data={buses} columns={columns}/>
        </div>
    )
}