import React, { useState } from "react";
import classes from "./styles.module.css";
import { ShortTourDto } from "../../../../types/Tours";
import { Link } from "react-router-dom";
import { BASE_URL } from "../../../../utils/constants/urlConstants";
import { addLikeToTour } from "../../../../queries/profile";

export const ShortTourFrame: React.FC<ShortTourDto> = (value) => {
    const imageUrl = `${BASE_URL}/${value.firstImageLink}`;
    const formatDate = (dateString: string) => dateString.substring(0, dateString.indexOf('T'));
    const [isLiked, setIsLiked] = useState(value.isLiked || false);
    
    const handleLikeClick = async (e: React.MouseEvent) => {
        e.preventDefault();
        e.stopPropagation();
        if (isLiked){
            return;
        }
        try {
            await addLikeToTour(value.id);
            setIsLiked(!isLiked);
        } catch (error) {
            console.error("Error adding like to tour:", error);
        }
    };

    const renderRatingStars = () => {
        if (!value.rating || value.rating === 0) return null;
        
        return (
            <div className={classes.ratingContainer}>
                {[...Array(5)].map((_, i) => (
                    <svg
                        key={i}
                        className={classes.starIcon}
                        viewBox="0 0 24 24"
                        fill={i < Math.round(value.rating) ? "#FFD700" : "none"}
                        stroke="#FFD700"
                    >
                        <path d="M12 17.27L18.18 21l-1.64-7.03L22 9.24l-7.19-.61L12 2 9.19 8.63 2 9.24l5.46 4.73L5.82 21z"/>
                    </svg>
                ))}
                <span className={classes.ratingValue}>{value.rating.toFixed(1)}</span>
            </div>
        );
    };

    return (
        <Link to={`/tours/${value.id}`} className={classes.tourLink}>
            <div className={classes.tourCard}>
                <div className={classes.imageContainer}>
                    <img src={imageUrl} alt={value.name || ":("} className={classes.tourImage} />
                    <div className={classes.priceTag}>{value.price}€</div>
                </div>
                
                <div className={classes.tourContent}>
                    <h3 className={classes.tourTitle}>{value.name}</h3>
                    
                    {renderRatingStars()}
                    
                    <div className={classes.dateRange}>
                        <span className={classes.dateIcon}>📅</span>
                        {formatDate(value.startDate)} - {formatDate(value.endDate)}
                        <button 
                            className={`${classes.likeButton} ${isLiked || value.isLiked ? classes.liked : ''}`}
                            onClick={handleLikeClick}
                            aria-label={isLiked || value.isLiked ? "Unlike this tour" : "Like this tour"}
                            disabled={value.isLiked}
                        >
                            <svg 
                                xmlns="http://www.w3.org/2000/svg" 
                                viewBox="0 0 24 24" 
                                fill="none" 
                                stroke="currentColor" 
                                strokeWidth="2"
                            >
                                <path d="M12 21.35l-1.45-1.32C5.4 15.36 2 12.28 2 8.5 2 5.42 4.42 3 7.5 3c1.74 0 3.41.81 4.5 2.09C13.09 3.81 14.76 3 16.5 3 19.58 3 22 5.42 22 8.5c0 3.78-3.4 6.86-8.55 11.54L12 21.35z"/>
                            </svg>
                        </button>
                    </div>
                    
                    {value.reservationCount > 0 && (
                        <div className={classes.reservationBadge}>
                            <span className={classes.reservationIcon}>📌</span>
                            {value.reservationCount} {value.reservationCount === 1 ? 'бронь' : 
                              value.reservationCount < 5 ? 'брони' : 'броней'}
                        </div>
                    )}
                </div>
            </div>
        </Link>
    );
};