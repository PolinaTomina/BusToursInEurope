import React, { useEffect, useState } from "react";
import classes from "./styles.module.css";
import { useNavigate } from "react-router-dom";
import { getProfileQuery, updateProfileQuery, getLikedTours} from "../../../queries/profile";
import { ProfileDto, UpdateProfileDto } from "../../../types/Profile";
import { CircularProgress, Alert, Snackbar, Modal, Box, TextField, Button } from "@mui/material";
import { JwtTokenKey } from "../../../utils/constants/localStorageConstants";
import { isAdmin } from "../../../queries/auth";
import { ShortTourDto } from "../../../types/Tours";
import { BASE_URL } from "../../../utils/constants/urlConstants";

export const UserProfilePage: React.FC = () => {
  const [profile, setProfile] = useState<ProfileDto | null>(null);
  const [likedTours, setLikedTours] = useState<ShortTourDto[]>([]);
  const [userIsAdmin, setIsAdmin] = useState<boolean>(false);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [editModalOpen, setEditModalOpen] = useState(false);
  const [editForm, setEditForm] = useState<UpdateProfileDto>({
    name: "",
    surName: "",
    middleName: "",
    numPhone: "",
    passportNumber: ""
  });
  const navigate = useNavigate();

  useEffect(() => {
    const fetchProfileData = async () => {
      try {
        const [profileResponse, likedToursResponse] = await Promise.all([
          getProfileQuery(),
          getLikedTours()
        ]);
        
        setProfile(profileResponse.data);
        setLikedTours(likedToursResponse.data);
        setEditForm({
          name: profileResponse.data.name || "",
          surName: profileResponse.data.surName || "",
          middleName: profileResponse.data.middleName || "",
          numPhone: profileResponse.data.numPhone || "",
          passportNumber: profileResponse.data.passportNumber || ""
        });

        const admin = await isAdmin(localStorage.getItem(JwtTokenKey) || "");
        if (admin.status !== 401) {
          setIsAdmin(true);
        }
      } catch (err) {
        setError("Не удалось загрузить профиль. Пожалуйста, попробуйте позже.");
        console.error("Error fetching profile:", err);
      } finally {
        setLoading(false);
      }
    };

    fetchProfileData();
  }, []);

  const handleReviewClick = (tourId: number) => {
    navigate(`/tours/${tourId}`);
  };

  const formatDate = (dateString: string) => {
    try {
      const date = new Date(dateString);
      return date.toLocaleDateString();
    } catch {
      return "Неизвестная дата";
    }
  };

  const handleCloseError = () => {
    setError(null);
  };

  const handleEditProfile = () => {
    setEditModalOpen(true);
  };

  const handleExitProfile = () => {
    localStorage.removeItem(JwtTokenKey);
    navigate('/');
  };

  const handleCloseEditModal = () => {
    setEditModalOpen(false);
  };

  const handleFormChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setEditForm(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleSaveProfile = async () => {
    try {
      setLoading(true);
      await updateProfileQuery(editForm);
      setProfile(prev => prev ? { 
        ...prev, 
        ...editForm 
      } : null);
      setEditModalOpen(false);
    } catch (err) {
      setError("Не удалось обновить профиль. Пожалуйста, попробуйте позже.");
      console.error("Error updating profile:", err);
    } finally {
      setLoading(false);
    }
  };

  if (loading) {
    return (
      <div className={classes.loadingContainer}>
        <CircularProgress size={60} />
        <p>Загрузка профиля...</p>
      </div>
    );
  }

  if (!profile || !profile.user) {
    return (
      <div className={classes.errorContainer}>
        <Alert severity="error" sx={{ width: '100%' }}>
          Профиль не найден или произошла ошибка при загрузке.
        </Alert>
      </div>
    );
  }

  return (
    <div className={classes.profileContainer}>
      <Snackbar
        open={!!error}
        autoHideDuration={6000}
        onClose={handleCloseError}
      >
        <Alert onClose={handleCloseError} severity="error" sx={{ width: '100%' }}>
          {error}
        </Alert>
      </Snackbar>

      <div className={classes.profileHeader}>
        <div>
          <h1 className={classes.profileTitle}>Профиль пользователя</h1>
          <p className={classes.profileSubtitle}>Личная информация и активность</p>
        </div>
        <div className={classes.actionButtons}>
          {!userIsAdmin && (
            <Button 
              variant="contained" 
              onClick={handleEditProfile}
              className={classes.editButton}
              startIcon={<span className={classes.editIcon}>✏️</span>}
            >
              Редактировать
            </Button>
          )}
          <Button 
            variant="outlined" 
            onClick={handleExitProfile}
            className={classes.exitButton}
          >
            Выйти
          </Button>
        </div>
      </div>
      
      <div className={classes.profileContent}>
        <div className={classes.mainSection}>
          <div className={classes.profileSection}>
            <h2 className={classes.sectionTitle}>
              <span className={classes.sectionIcon}>👤</span>
              Учетные данные
            </h2>
            <div className={classes.profileInfo}>
              <div className={classes.infoItem}>
                <span className={classes.infoLabel}>Email:</span>
                <span className={classes.infoValue}>{profile.user.email || "Не указан"}</span>
              </div>
              <div className={classes.infoItem}>
                <span className={classes.infoLabel}>Логин:</span>
                <span className={classes.infoValue}>{profile.user.login || "Не указан"}</span>
              </div>
            </div>
          </div>

          {!userIsAdmin && (
            <div className={classes.profileSection}>
              <h2 className={classes.sectionTitle}>
                <span className={classes.sectionIcon}>📝</span>
                Личная информация
              </h2>
              <div className={classes.profileInfo}>
                <div className={classes.infoItem}>
                  <span className={classes.infoLabel}>Имя:</span>
                  <span className={classes.infoValue}>{profile.name || "Не указано"}</span>
                </div>
                <div className={classes.infoItem}>
                  <span className={classes.infoLabel}>Фамилия:</span>
                  <span className={classes.infoValue}>{profile.surName || "Не указано"}</span>
                </div>
                <div className={classes.infoItem}>
                  <span className={classes.infoLabel}>Отчество:</span>
                  <span className={classes.infoValue}>{profile.middleName || "Не указано"}</span>
                </div>
                <div className={classes.infoItem}>
                  <span className={classes.infoLabel}>Телефон:</span>
                  <span className={classes.infoValue}>{profile.numPhone || "Не указано"}</span>
                </div>
                <div className={classes.infoItem}>
                  <span className={classes.infoLabel}>Паспорт:</span>
                  <span className={classes.infoValue}>{profile.passportNumber || "Не указано"}</span>
                </div>
              </div>
            </div>
          )}
        </div>

        <div className={classes.activitySection}>
          {!userIsAdmin && (
            <>
              {/* Блок лайкнутых туров */}
              {likedTours.length > 0 ? (
                <div className={classes.profileSection}>
                  <h2 className={classes.sectionTitle}>
                    <span className={classes.sectionIcon}>❤️</span>
                    Понравившиеся туры ({likedTours.length})
                  </h2>
                  <div className={classes.toursList}>
                    {likedTours.map(tour => (
                      <div 
                        key={tour.id} 
                        className={classes.tourItem}
                        onClick={() => navigate(`/tours/${tour.id}`)}
                      >
                        {tour.firstImageLink && (
                          <div className={classes.tourImage}>
                            <img 
                              src={`${BASE_URL}/${tour.firstImageLink}`} 
                              alt={tour.name || "Тур"} 
                              className={classes.tourImage}
                            />
                          </div>
                        )}
                        <div className={classes.tourInfo}>
                          <h3 className={classes.tourName}>{tour.name || "Без названия"}</h3>
                          <div className={classes.tourDates}>
                            <span>{formatDate(tour.startDate)}</span>
                            <span> - </span>
                            <span>{formatDate(tour.endDate)}</span>
                          </div>
                          <div className={classes.tourPrice}>
                            {tour.price.toLocaleString()} €
                          </div>
                          <div className={classes.tourRating}>
                            Рейтинг: {tour.rating.toFixed(1)}
                          </div>
                        </div>
                      </div>
                    ))}
                  </div>
                </div>
              ) : (
                <div className={classes.profileSection}>
                  <h2 className={classes.sectionTitle}>
                    <span className={classes.sectionIcon}>❤️</span>
                    Понравившиеся туры
                  </h2>
                  <div className={classes.emptySection}>
                    <p>У вас пока нет понравившихся туров.</p>
                  </div>
                </div>
              )}

              {profile.user.reservationsDto && profile.user.reservationsDto.length > 0 ? (
                <div className={classes.profileSection}>
                  <h2 className={classes.sectionTitle}>
                    <span className={classes.sectionIcon}>📅</span>
                    Бронирования ({profile.user.reservationsDto.length})
                  </h2>
                  <div className={classes.reservationsList}>
                    {profile.user.reservationsDto.map(reservation => (
                      <div key={reservation.id} className={classes.reservationItem}>
                        <div className={classes.reservationHeader}>
                          <span className={classes.reservationDate}>
                            {formatDate(reservation.date)}
                          </span>
                          <span className={classes.reservationSeats}>
                            {reservation.numOfSeats} мест
                          </span>
                        </div>
                        <div className={classes.reservationDetails}>
                          <div>
                            <span>Оплата: </span>
                            <span>{formatDate(reservation.paymentDate) || "Не оплачено"}</span>
                          </div>
                          <div>
                            <span>Срок оплаты: </span>
                            <span>{formatDate(reservation.paymentDeadline)}</span>
                          </div>
                        </div>
                      </div>
                    ))}
                  </div>
                </div>
              ) : (
                <div className={classes.profileSection}>
                  <h2 className={classes.sectionTitle}>
                    <span className={classes.sectionIcon}>📅</span>
                    Бронирования
                  </h2>
                  <div className={classes.emptySection}>
                    <p>У вас пока нет бронирований.</p>
                  </div>
                </div>
              )}
            </>
          )}

          {!userIsAdmin && (
            <>
              {profile.user.reviewsDto && profile.user.reviewsDto.length > 0 ? (
                <div className={classes.profileSection}>
                  <h2 className={classes.sectionTitle}>
                    <span className={classes.sectionIcon}>⭐</span>
                    Отзывы ({profile.user.reviewsDto.length})
                  </h2>
                  <div className={classes.reviewsList}>
                    {profile.user.reviewsDto.map(review => (
                      <div 
                        key={review.id} 
                        className={classes.reviewItem}
                        onClick={() => handleReviewClick(review.tourId)}
                      >
                        <div className={classes.reviewHeader}>
                          <span className={classes.reviewRating}>
                            {Array.from({ length: 5 }).map((_, i) => (
                              <span 
                                key={i} 
                                className={i < review.rating ? classes.starFilled : classes.starEmpty}
                              >
                                ★
                              </span>
                            ))}
                          </span>
                          <span className={classes.reviewDate}>
                            {formatDate(review.reviewDate)}
                          </span>
                        </div>
                        <div className={classes.commentContainer}>
                          <div className={classes.commentContent}>
                            {review.comment || "Без комментария"}
                          </div>
                        </div>
                        {review.login && (
                          <div className={classes.reviewAuthor}>
                            Автор: {review.login}
                          </div>
                        )}
                      </div>
                    ))}
                  </div>
                </div>
              ) : (
                <div className={classes.profileSection}>
                  <h2 className={classes.sectionTitle}>
                    <span className={classes.sectionIcon}>⭐</span>
                    Отзывы
                  </h2>
                  <div className={classes.emptySection}>
                    <p>Вы еще не оставляли отзывов.</p>
                  </div>
                </div>
              )}
            </>
          )}
        </div>
      </div>

      {/* Модальное окно редактирования */}
      <Modal
        open={editModalOpen}
        onClose={handleCloseEditModal}
        aria-labelledby="edit-profile-modal"
        aria-describedby="edit-profile-form"
      >
        <Box className={classes.modalContainer}>
          <h2 className={classes.modalTitle}>Редактировать профиль</h2>
          <div className={classes.editForm}>
            <TextField
              label="Имя"
              name="name"
              value={editForm.name}
              onChange={handleFormChange}
              fullWidth
              margin="normal"
              variant="outlined"
            />
            <TextField
              label="Фамилия"
              name="surName"
              value={editForm.surName}
              onChange={handleFormChange}
              fullWidth
              margin="normal"
              variant="outlined"
            />
            <TextField
              label="Отчество"
              name="middleName"
              value={editForm.middleName}
              onChange={handleFormChange}
              fullWidth
              margin="normal"
              variant="outlined"
            />
            <TextField
              label="Телефон"
              name="numPhone"
              value={editForm.numPhone}
              onChange={handleFormChange}
              fullWidth
              margin="normal"
              variant="outlined"
            />
            <TextField
              label="Паспорт"
              name="passportNumber"
              value={editForm.passportNumber}
              onChange={handleFormChange}
              fullWidth
              margin="normal"
              variant="outlined"
            />
            <div className={classes.modalButtons}>
              <Button 
                variant="outlined" 
                onClick={handleCloseEditModal}
                className={classes.cancelButton}
              >
                Отмена
              </Button>
              <Button 
                variant="contained" 
                onClick={handleSaveProfile}
                disabled={loading}
                className={classes.saveButton}
              >
                {loading ? <CircularProgress size={24} /> : "Сохранить"}
              </Button>
            </div>
          </div>
        </Box>
      </Modal>
    </div>
  );
};