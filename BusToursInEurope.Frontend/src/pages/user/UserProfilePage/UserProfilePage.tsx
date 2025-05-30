import React, { useEffect, useState } from "react";
import classes from "./styles.module.css";
import { useNavigate } from "react-router-dom";
import { getProfileQuery, updateProfileQuery } from "../../../queries/profile";
import { ProfileDto, UpdateProfileDto } from "../../../types/Profile";
import { CircularProgress, Alert, Snackbar, Modal, Box, TextField, Button } from "@mui/material";
import { JwtTokenKey } from "../../../utils/constants/localStorageConstants";

export const UserProfilePage: React.FC = () => {
  const [profile, setProfile] = useState<ProfileDto | null>(null);
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
    const fetchProfile = async () => {
      try {
        const response = await getProfileQuery();
        const profileData = response.data;
        setProfile(profileData);
        setEditForm({
          name: profileData.name || "",
          surName: profileData.surName || "",
          middleName: profileData.middleName || "",
          numPhone: profileData.numPhone || "",
          passportNumber: profileData.passportNumber || ""
        });
      } catch (err) {
        setError("Не удалось загрузить профиль. Пожалуйста, попробуйте позже.");
        console.error("Error fetching profile:", err);
      } finally {
        setLoading(false);
      }
    };

    fetchProfile();
  }, []);

  const handleReviewClick = (tourId: number) => {
    navigate(`/tour/${tourId}`);
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
    localStorage.removeItem(JwtTokenKey)
    navigate('/')
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
        <CircularProgress />
        <p>Загрузка профиля...</p>
      </div>
    );
  }

  if (!profile || !profile.user) {
    return (
      <div className={classes.errorContainer}>
        <Alert severity="error">
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
        <h1 className={classes.profileTitle}>Профиль пользователя</h1>
        <div className={classes.actionButtons}>
          <Button 
            variant="contained" 
            onClick={handleEditProfile}
            className={classes.editButton}
          >
            Редактировать профиль
          </Button>
          <Button 
            variant="contained" 
            onClick={handleExitProfile}
            className={classes.editButton}
          >
            Выйти из аккаунта
          </Button>
        </div>
      </div>
      
      <div className={classes.profileSection}>
        <h2>Личная информация</h2>
        <div className={classes.profileInfo}>
          <p><strong>Имя:</strong> {profile.name || "Не указано"}</p>
          <p><strong>Фамилия:</strong> {profile.surName || "Не указано"}</p>
          <p><strong>Отчество:</strong> {profile.middleName || "Не указано"}</p>
          <p><strong>Телефон:</strong> {profile.numPhone || "Не указано"}</p>
          <p><strong>Идентификационный паспорта:</strong> {profile.passportNumber || "Не указано"}</p>
        </div>
      </div>

      <div className={classes.profileSection}>
        <h2>Учетные данные</h2>
        <div className={classes.profileInfo}>
          <p><strong>Email:</strong> {profile.user.email || "Не указан"}</p>
          <p><strong>Логин:</strong> {profile.user.login || "Не указан"}</p>
          <p><strong>Роль:</strong> {profile.user.role || "Не указана"}</p>
        </div>
      </div>

      {profile.user.reservationsDto && profile.user.reservationsDto.length > 0 ? (
        <div className={classes.profileSection}>
          <h2>Бронирования ({profile.user.reservationsDto.length})</h2>
          <div className={classes.reservationsList}>
            {profile.user.reservationsDto.map(reservation => (
              <div key={reservation.id} className={classes.reservationItem}>
                <p><strong>Дата бронирования:</strong> {formatDate(reservation.date)}</p>
                <p><strong>Дата оплаты:</strong> {formatDate(reservation.paymentDate)}</p>
                <p><strong>Крайний срок оплаты:</strong> {formatDate(reservation.paymentDeadline)}</p>
                <p><strong>Количество мест:</strong> {reservation.numOfSeats}</p>
              </div>
            ))}
          </div>
        </div>
      ) : (
        <div className={classes.profileSection}>
          <h2>Бронирования</h2>
          <p>У вас пока нет бронирований.</p>
        </div>
      )}

      {profile.user.reviewsDto && profile.user.reviewsDto.length > 0 ? (
        <div className={classes.profileSection}>
          <h2>Отзывы ({profile.user.reviewsDto.length})</h2>
          <div className={classes.reviewsList}>
            {profile.user.reviewsDto.map(review => (
              <div 
                key={review.id} 
                className={classes.reviewItem}
                onClick={() => handleReviewClick(review.tourId)}
              >
                <p><strong>Имя:</strong> {review.username || "Аноним"}</p>
                <p><strong>Оценка:</strong> {review.rating}/5</p>
                <p><strong>Комментарий:</strong> {review.comment || "Без комментария"}</p>
                <p><strong>Дата отзыва:</strong> {formatDate(review.reviewDate)}</p>
              </div>
            ))}
          </div>
        </div>
      ) : (
        <div className={classes.profileSection}>
          <h2>Отзывы</h2>
          <p>Вы еще не оставляли отзывов.</p>
        </div>
      )}

      {/* Модальное окно редактирования */}
      <Modal
        open={editModalOpen}
        onClose={handleCloseEditModal}
        aria-labelledby="edit-profile-modal"
        aria-describedby="edit-profile-form"
      >
        <Box className={classes.modalContainer}>
          <h2>Редактировать профиль</h2>
          <div className={classes.editForm}>
            <TextField
              label="Имя"
              name="name"
              value={editForm.name}
              onChange={handleFormChange}
              fullWidth
              margin="normal"
            />
            <TextField
              label="Фамилия"
              name="surName"
              value={editForm.surName}
              onChange={handleFormChange}
              fullWidth
              margin="normal"
            />
            <TextField
              label="Отчество"
              name="middleName"
              value={editForm.middleName}
              onChange={handleFormChange}
              fullWidth
              margin="normal"
            />
            <TextField
              label="Телефон"
              name="numPhone"
              value={editForm.numPhone}
              onChange={handleFormChange}
              fullWidth
              margin="normal"
            />
            <TextField
              label="Идентификационный паспорта"
              name="passportNumber"
              value={editForm.passportNumber}
              onChange={handleFormChange}
              fullWidth
              margin="normal"
            />
            <div className={classes.modalButtons}>
              <Button 
                variant="outlined" 
                onClick={handleCloseEditModal}
              >
                Отмена
              </Button>
              <Button 
                variant="contained" 
                onClick={handleSaveProfile}
                disabled={loading}
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