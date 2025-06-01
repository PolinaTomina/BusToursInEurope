import React, { useEffect, useState } from "react";
import { FullTourDto } from "../../../types/Tours";
import { getTour } from "../../../queries/tours";
import { useParams } from "react-router-dom";
import classes from "./styles.module.css";
import { BASE_URL } from "../../../utils/constants/urlConstants";
import { createReservation } from "../../../queries/reservations";
import { Input } from "../../../ui";

interface FullTourProps {}

export const FullTourPage: React.FC<FullTourProps> = ({}) => {
  const [tour, setTour] = useState<FullTourDto>();
  const { id } = useParams<{ id: string }>();
  const [numOfSeats, setNumOfSeats] = useState(0)
  const [isBooked, setIsBooked] = useState(false)

  useEffect(() => {
    if (id) {
      setTourDataById(parseInt(id));
    }
  }, [id]);

  const setTourDataById = async (tourId: number) => {
    const response = await getTour(tourId);
    if (response) {
      setTour(response.data);
    }
  };

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString();
  };

  const handleBookTour = () => {
    const fetchBookAsync = async () => {
      console.log(tour?.id)
      if (!tour?.id) {
        return;
      }

      var response = await createReservation({
        numOfSeats: numOfSeats,
        tourId: tour?.id
      })
      console.log(response)
      if (response.status >= 200 && response.status < 300){
        setIsBooked(true)
      }
    }

    fetchBookAsync()
    console.log("Бронирование тура с ID:", tour?.id);
  };

  return (
    <div className={classes.tourContainer}>
      {tour ? (
        <>
          <div className={classes.tourHeader}>
            <h1 className={classes.tourTitle}>{tour.name}</h1>
            <div className={classes.tourPrice}>{tour.price} ₽</div>
          </div>

          <div className={classes.tourGallery}>
            {tour.fullImageLink.map((image, index) => (
              <img
                key={index}
                src={`${BASE_URL}/${image}`}
                alt={`Тур ${tour.name} ${index + 1}`}
                className={classes.tourImage}
              />
            ))}
          </div>

          <div className={classes.actionZone}>
            <div className={classes.inputButtonGroup}>
              <Input 
                type="number" 
                value={numOfSeats} 
                onChange={(value) => setNumOfSeats(Number(value.target.value))}
                className={classes.seatsInput}
              />
              <button 
                onClick={handleBookTour}
                className={classes.bookButton}
                disabled={isBooked}
              >
                Забронировать тур
              </button>
            </div>
          </div>

          <div className={classes.tourDetails}>
            <div className={classes.detailCard}>
              <h3>Даты</h3>
              <p>
                {formatDate(tour.startDate)} - {formatDate(tour.endDate)}
              </p>
            </div>

            <div className={classes.detailCard}>
              <h3>Свободные места</h3>
              <p>{tour.numOfSeats}</p>
            </div>

            <div className={classes.detailCard}>
              <h3>Автобус</h3>
              <p>{tour.busDto.name} ({tour.busDto.numOfSeats} мест)</p>
            </div>

            <div className={classes.detailCard}>
              <h3>Расстояние маршрута</h3>
              <p>{tour.routeBusDto.distance} км</p>
            </div>
          </div>

          <div className={classes.tourDescription}>
            <h2>Описание</h2>
            <p>{tour.description}</p>
          </div>

          <div className={classes.tourWaypoints}>
            <h2>Точки маршрута</h2>
            <ul>
              {tour.routeBusDto.wayPointsDto?.map((point, index) => (
                <li key={index}>{point.namePlace}</li>
              ))}
            </ul>
          </div>

          <div className={classes.tourReviews}>
            <h2>Отзывы</h2>
            {tour.reviewsDto && tour.reviewsDto.length > 0 ? (
              <div className={classes.reviewsGrid}>
                {tour.reviewsDto.map((review) => (
                  <div key={review.id} className={classes.reviewCard}>
                    <div className={classes.reviewHeader}>
                      <span className={classes.reviewUsername}>{review.username}</span>
                      <span className={classes.reviewRating}>
                        {Array(5).fill(0).map((_, i) => (
                          <span key={i} className={i < review.rating ? classes.starFilled : classes.starEmpty}>★</span>
                        ))}
                      </span>
                    </div>
                    <p className={classes.reviewComment}>{review.comment}</p>
                    <div className={classes.reviewDate}>
                      {formatDate(review.reviewDate)}
                    </div>
                  </div>
                ))}
              </div>
            ) : (
              <p>Пока нет отзывов</p>
            )}
          </div>
        </>
      ) : (
        <p>Загрузка тура...</p>
      )}
    </div>
  );
};