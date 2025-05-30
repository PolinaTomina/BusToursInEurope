import React from "react"
import classes from "./styles.module.css"
import { ShortTourDto } from "../../../../types/Tours"
import { Link } from "react-router-dom"
import { BASE_URL } from "../../../../utils/constants/urlConstants"

export const ShortTourFrame: React.FC<ShortTourDto> = (value) => {
    const imageUrl = `${BASE_URL}/${value.firstImageLink}`
    return(
        <Link to={`/tours/${value.id}`}>
            <div className={classes.block}>
                <img src={imageUrl} className={classes.imageTour}></img>
                <div className={classes.tourInfo}>
                    <div className={classes.nameAndPrice}>
                        <div className={classes.name}>
                            {value.name}
                        </div>
                        <div className={classes.price}>
                            {value.price}$
                        </div>
                    </div>
                    <div className={classes.startDate}>
                        {value.startDate.substring(0, value.startDate.indexOf('T'))} - {value.endDate.substring(0, value.startDate.indexOf('T'))}
                    </div>
                </div>
            </div>
        </Link>
    )
}