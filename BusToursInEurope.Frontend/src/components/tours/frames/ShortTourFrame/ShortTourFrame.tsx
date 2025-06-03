import React from "react";
import classes from "./styles.module.css";
import { ShortTourDto } from "../../../../types/Tours";
import { Link } from "react-router-dom";
import { BASE_URL } from "../../../../utils/constants/urlConstants";

export const ShortTourFrame: React.FC<ShortTourDto> = (value) => {
    const imageUrl = `${BASE_URL}/${value.firstImageLink}`;
    const formatDate = (dateString: string) => dateString.substring(0, dateString.indexOf('T'));
    
    return (
        <Link to={`/tours/${value.id}`} className={classes.tourLink}>
            <div className={classes.tourCard}>
                <div className={classes.imageContainer}>
                    <img src={imageUrl} alt={value.name || ":("} className={classes.tourImage} />
                    <div className={classes.priceTag}>{value.price}€</div>
                </div>
                
                <div className={classes.tourContent}>
                    <h3 className={classes.tourTitle}>{value.name}</h3>
                    <div className={classes.dateRange}>
                        <span className={classes.dateIcon}>📅</span>
                        {formatDate(value.startDate)} - {formatDate(value.endDate)}
                    </div>
                </div>
            </div>
        </Link>
    );
};