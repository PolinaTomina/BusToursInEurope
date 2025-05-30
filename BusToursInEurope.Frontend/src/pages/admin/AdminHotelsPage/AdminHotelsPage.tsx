import React, { useEffect, useState } from "react";
import { GenericTable } from "../../../components/common/GenericTable/GenericTable";
import { HotelDto } from "../../../types/Hotels";
import { Button } from "../../../ui";
import { deleteHotel, getHotels } from "../../../queries/hotels";
import classes from './styles.module.css'
import { CreateHotelModal } from "../../../components";
import { CityDto } from "../../../types/Cities";
import { getCities } from "../../../queries/cities";

export const AdminHotelsPage: React.FC = ({ }) => {
    const [hotels, setHotels] = useState<HotelDto[]>([])
    const [cities, setCities] = useState<CityDto[]>([])
    const [currentHotel, setCurrentHotel] = useState<HotelDto | undefined>(undefined)
    const [modalOpen, setModalOpen] = useState(false)

    const columns = [
        {key: "id", title: "ID"},
        {key: "name", title: "Название"},
        {key: "rating", title: "Рейтинг"},
        {
            key: "city", 
            title: "Местоположение", 
            render: (hotel: HotelDto) => (
                <div>
                    {getCityName(hotel.id)}
                </div>
            )
        },
        { 
            key: "actions", 
            title: "Действия",
            render: (hotel: HotelDto) => (
                <div>
                    <Button 
                        onClick={() => onEditClick(hotel)}>
                        Редактировать
                    </Button>
                    <Button onClick={() => removeBus(hotel.id)}>
                        Удалить
                    </Button>
                </div>
            )
          },
    ]

    useEffect(() => {
        const fetchHotels = async () => {
            try {
                const response = await getHotels({});
                setHotels(response.data);
            } catch (error) {
                console.error('Error fetching hotels:', error);
            }
        };

        const fecthCities = async () => {
            try {
                const response = await getCities({});
                setCities(response.data)
            } catch (error) {
                console.error('Error fetching cities:', error);
            }
        }
    
        fetchHotels();
        fecthCities();
    }, [])

    const removeBus = async (id: number) => {
        try {
            await deleteHotel(id)
            const newBusesArr = hotels.filter(x => x.id !== id)
            setHotels(newBusesArr)
        } catch(error) {

        }
    }

    const modalSuccess = async () => {
        setModalOpen(false)
        const newBusesArr = await getHotels({})
        setHotels(newBusesArr.data)
    }

    const onEditClick = (hotel: HotelDto) => {
        setCurrentHotel(_ => {
            return hotel;
        });
        setModalOpen(true)
    }

    const onCloseClick = () => {
        setCurrentHotel(undefined)
        setModalOpen(false)
    }

    const getCityName = (cityId: number) : string => {
        const city = cities.find(x => x.id === cityId)

        return `${city?.name}, ${city?.country}`
    }

    return(
        <div className={classes.main}>
            <CreateHotelModal isOpen={modalOpen} onClose={() => onCloseClick()} onSuccess={() => modalSuccess()} model={currentHotel}/>
            <div className={classes.actions}>
                <Button onClick={() => setModalOpen(true)}>
                    Создать отель
                </Button>
                <Button>
                    Фильтры
                </Button>
                <Button>
                    Экспорт в Excel
                </Button>
            </div>
            <GenericTable data={hotels} columns={columns}/>
        </div>
    )
}