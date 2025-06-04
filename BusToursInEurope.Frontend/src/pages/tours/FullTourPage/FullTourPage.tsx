import React, { useEffect, useState } from "react";
import { FullTourDto } from "../../../types/Tours";
import { getTour } from "../../../queries/tours";
import { useParams, useNavigate } from "react-router-dom";
import classes from "./styles.module.css";
import { BASE_URL } from "../../../utils/constants/urlConstants";
import { createReservation } from "../../../queries/reservations";
import { Input } from "../../../ui";
import { createReview } from "../../../queries/reviews";
import { JwtTokenKey } from "../../../utils/constants/localStorageConstants";

interface FullTourProps {}

export const FullTourPage: React.FC<FullTourProps> = ({}) => {
  const [tour, setTour] = useState<FullTourDto>();
  const { id } = useParams<{ id: string }>();
  const [numOfSeats, setNumOfSeats] = useState(1);
  const [isBooked, setIsBooked] = useState(false);
  const [reviewRating, setReviewRating] = useState(5);
  const [reviewComment, setReviewComment] = useState("");
  const [isAuthorized, setIsAuthorized] = useState(false);
  const [isReviewFormOpen, setIsReviewFormOpen] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    if (id) {
      setTourDataById(parseInt(id));
    }
    checkAuth();
  }, [id]);

  const checkAuth = () => {
    const token = localStorage.getItem(JwtTokenKey);
    setIsAuthorized(!!token);
  };

  const setTourDataById = async (tourId: number) => {
    const response = await getTour(tourId);
    if (response) {
      setTour(response.data);
    }
  };

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString();
  };

  const handleBookTour = async () => {
    if (!tour?.id) return;
    
    try {
      const response = await createReservation({
        numOfSeats: numOfSeats,
        tourId: tour.id
      });
      
      if (response.status >= 200 && response.status < 300) {
        setIsBooked(true);
        setTourDataById(tour.id);
      }
    } catch (error) {
      console.error("Ошибка при бронировании:", error);
    }
  };

  const handleBookingClick = () => {
    if (!isAuthorized) {
      navigate("/authentication");
      return;
    }
    handleBookTour();
  };

  const handleSubmitReview = async () => {
    if (!tour?.id) return;
    
    try {
      const response = await createReview({
        tourId: tour.id,
        rating: reviewRating,
        comment: reviewComment
      });
      
      if (response.status >= 200 && response.status < 300) {
        setTourDataById(tour.id);
        setReviewComment("");
        setReviewRating(5);
        setIsReviewFormOpen(false);
      }
    } catch (error) {
      console.error("Ошибка при отправке отзыва:", error);
    }
  };

  const averageRating = tour?.reviewsDto && tour.reviewsDto.length > 0 
    ? (tour.reviewsDto.reduce((sum, review) => sum + review.rating, 0)) / tour.reviewsDto.length
    : 0;

  return (
    <div className={classes.tourContainer}>
      {tour ? (
        <>
          <div className={classes.tourHeader}>
            <h1 className={classes.tourTitle}>{tour.name}</h1>
            <div className={classes.tourPrice}>{tour.price} €</div>
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
                <li key={index}>{point.description}</li>
              ))}
            </ul>
          </div>

          <div className={classes.sectionDivider}></div>

          {/* Перемещённый блок бронирования */}
          <div className={classes.bookingSection}>
            <div className={classes.bookingContainer}>
              <div className={classes.bookingHeader}>
                <h3>Бронирование тура</h3>
                <div className={classes.priceBadge}>
                  {tour.price} € <span className={classes.perPerson}>/ чел</span>
                </div>
              </div>
              
              <div className={classes.bookingControls}>
                <div className={classes.seatsControl}>
                  <label htmlFor="seats-input" className={classes.seatsLabel}>
                    Количество мест
                  </label>
                  <Input
                    id="seats-input"
                    type="number"
                    min="1"
                    max={tour.numOfSeats}
                    value={numOfSeats}
                    onChange={(e) => setNumOfSeats(Math.max(1, Math.min(tour.numOfSeats, Number(e.target.value))))}
                    className={classes.seatsInput}
                    disabled={!isAuthorized || isBooked}
                  />
                </div>
                
                <button
                  onClick={handleBookingClick}
                  className={`${classes.bookButton} ${
                    !isAuthorized ? classes.bookButtonDisabled : ""
                  }`}
                  disabled={isBooked}
                >
                  {isBooked ? (
                    <>
                      <svg className={classes.checkIcon} viewBox="0 0 24 24">
                        <path d="M20 6L9 17l-5-5" stroke="currentColor" strokeWidth="2" fill="none"/>
                      </svg>
                      Забронировано
                    </>
                  ) : (
                    <>
                      <svg className={classes.bookIcon} viewBox="0 0 24 24">
                        <path d="M4 19.5A2.5 2.5 0 016.5 17H20M6.5 2H20v20H6.5A2.5 2.5 0 014 19.5v-15A2.5 2.5 0 016.5 2z" stroke="currentColor" strokeWidth="2" fill="none"/>
                      </svg>
                      {isAuthorized ? "Забронировать тур" : "Войти для бронирования"}
                    </>
                  )}
                </button>
              </div>
              
              {!isAuthorized && (
                <div className={classes.authPrompt}>
                  <span>Для бронирования необходимо </span>
                  <a href="/authentication" className={classes.authLink}>
                    войти в аккаунт
                  </a>
                </div>
              )}
            </div>
          </div>

          <div className={classes.tourReviews}>
            <h2>Отзывы {averageRating > 0 && `(Средняя оценка: ${averageRating.toFixed(1)})`}</h2>
            
            {isAuthorized && (
              <div className={classes.reviewFormContainer}>
                <button 
                  onClick={() => setIsReviewFormOpen(!isReviewFormOpen)}
                  className={classes.toggleReviewFormButton}
                >
                  {isReviewFormOpen ? (
                    <>
                      <span>Скрыть форму</span>
                      <svg className={classes.arrowIcon} viewBox="0 0 24 24">
                        <path d="M7 15l5-5 5 5" stroke="currentColor" strokeWidth="2" fill="none"/>
                      </svg>
                    </>
                  ) : (
                    <>
                      <span>Написать отзыв</span>
                      <svg className={classes.arrowIcon} viewBox="0 0 24 24">
                        <path d="M7 10l5 5 5-5" stroke="currentColor" strokeWidth="2" fill="none"/>
                      </svg>
                    </>
                  )}
                </button>
                
                <div className={`${classes.reviewForm} ${isReviewFormOpen ? classes.formVisible : ''}`}>
                  <div className={classes.formHeader}>
                    <h3>Ваш отзыв</h3>
                    <div className={classes.ratingContainer}>
                      <label>Оценка:</label>
                      <div className={classes.starsContainer}>
                        {[1, 2, 3, 4, 5].map((star) => (
                          <span
                            key={star}
                            className={`${classes.star} ${star <= reviewRating ? classes.starSelected : ''}`}
                            onClick={() => setReviewRating(star)}
                          >
                            ★
                          </span>
                        ))}
                      </div>
                    </div>
                  </div>
                  <Input
                    type="text"
                    value={reviewComment}
                    onChange={(e) => setReviewComment(e.target.value)}
                    placeholder="Поделитесь своими впечатлениями..."
                    className={classes.reviewTextInput}
                  />
                  <button 
                    onClick={handleSubmitReview}
                    className={classes.submitReviewButton}
                    disabled={!reviewComment.trim()}
                  >
                    <svg className={classes.sendIcon} viewBox="0 0 24 24">
                      <path d="M22 2L11 13M22 2l-7 20-4-9-9-4 20-7z" stroke="currentColor" strokeWidth="2" fill="none"/>
                    </svg>
                    Отправить отзыв
                  </button>
                </div>
              </div>
            )}

            {tour.reviewsDto && tour.reviewsDto.length > 0 ? (
              <div className={classes.reviewsGrid}>
                {tour.reviewsDto.map((review) => (
                  <div key={review.id} className={classes.reviewCard}>
                    <div className={classes.reviewHeader}>
                      <span className={classes.reviewUsername}>{review.login}</span>
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
              <p className={classes.noReviews}>Пока нет отзывов</p>
            )}
          </div>
        </>
      ) : (
        <p>Загрузка тура...</p>
      )}
    </div>
  );
};