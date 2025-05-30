import React from "react"
import classes from "./styles.module.css"
import { ShortTourDto } from "../../../../types/Tours"
import { Link } from "react-router-dom"

export const ShortTourFrame: React.FC<ShortTourDto> = (value) => {
    return(
        <Link to={`/tours/${value.id}`}>
            <div className={classes.block}>
                <div className={classes.imageTour}></div>
                <div className={classes.tourInfo}>
                    <div className={classes.nameAndPrice}>
                        <div className={classes.name}>
                            {value.name}
                        </div>
                        <div className={classes.price}>
                            {value.price}
                        </div>
                    </div>
                    <div className={classes.startDate}>
                        {value.startDate.substring(0, value.startDate.indexOf('T'))}
                    </div>
                </div>
            </div>
        </Link>
    )
}