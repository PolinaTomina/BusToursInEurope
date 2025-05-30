import React, { useEffect, useState } from "react";
import classes from "./styles.module.css"
import { getTopTours } from "../../../../queries/tours";
import { ShortTourDto } from "../../../../types/Tours";
import { ShortTourFrame } from "../ShortTourFrame/ShortTourFrame";

interface TopToursFrameProps {

}

export const TopToursFrame: React.FC<TopToursFrameProps> = ({ }) => {
    const [tours, setTours] = useState<ShortTourDto[]>([]);

    const fetchTopTours = async () => {
        try {
            const {data, status} = await getTopTours()

            if (status === 400 || status === 500) {
                return
            }

            setTours(data)
        } catch(error) {
            console.log(error)
        }
    }

    useEffect(() => {
        fetchTopTours()
    }, [])

    return(
        <div className={classes.topTours}>
            {tours.map((value, index) => (
                index < 4 && <ShortTourFrame key={value.id} {...value} />
            ))}
        </div>
    );
}