import React, { useEffect, useState } from "react";
import { GenericTable } from "../../../components/common/GenericTable/GenericTable";
import { Button, Input } from "../../../ui";
import classes from './styles.module.css';
import { ReviewDto } from "../../../types/Reviews";
import { ShortTourDto } from "../../../types/Tours";
import { deleteReview, getAll, getReviewsByTourId } from "../../../queries/reviews";
import { getToursByFilters } from "../../../queries/tours";

interface ReviewRow {
    review: ReviewDto,
    reviewId: number,
    tour?: ShortTourDto,
    tourId?: number,
}

export const AdminReviewsPage: React.FC = () => {
    const [reviewRows, setReviewRows] = useState<ReviewRow[]>();
    const [reviews, setReviews] = useState<ReviewDto[]>([]);
    const [tours, setTours] = useState<ShortTourDto[]>([]);
    const [error, setError] = useState<string | null>(null);
    const [tourId, setTourId] = useState<number | undefined>();

    const columns = [
        { key: "reviewId", title: "ID отзыва", width: "100px" },
        { 
            key: "review.login", 
            title: "Логин",
            render: (review: ReviewRow) => (
                <div className={classes.dateCell}>
                    {review.review.login}
                </div>
            )
        },
        { 
            key: "review.comment", 
            title: "Комментарий",
            render: (review: ReviewRow) => (
                <div className={classes.dateCell}>
                    <div className={classes.reviewCard}>
                        <p className={classes.reviewComment}>{review.review.comment}</p>
                    </div>
                </div>
            )
        },
        { 
            key: "review.rating", 
            title: "Рейтинг",
            render: (review: ReviewRow) => (
                <div className={classes.dateCell}>
                    {review.review.rating}
                </div>
            )
        },
        { 
            key: "review.reviewDate", 
            title: "Дата создания",
            render: (review: ReviewRow) => (
                <div className={classes.dateCell}>
                    {new Date(review.review.reviewDate).toLocaleString()}
                </div>
            )
        },
        { key: "tourId", title: "ID тура", width: "100px" },
        { 
            key: "tour.name", 
            title: "Имя тура",
            render: (review: ReviewRow) => (
                <div className={classes.dateCell}>
                    {review.tour?.name}
                </div>
            )
        },
        { 
            key: "actions", 
            title: "Действия",
            align: "right" as const,
            render: (review: ReviewRow) => (
                <div className={classes.actionsCell}>
                    <Button
                        variant="primary"
                        size="sm"
                        onClick={() => removeReview(review.review.id)}
                        className={classes.deleteButton}
                    >
                        Удалить
                    </Button>
                </div>
            )
        },
    ];

    const fetchData = async (tourId?: number) => {
        try {
            const reviewsResponse = tourId ? await getReviewsByTourId(tourId) : await getAll();
            const toursResponse = await getToursByFilters({});

            if (reviewsResponse.data && toursResponse.data) {
                setReviews(reviewsResponse.data);
                setTours(toursResponse.data);

                const newReviewsRows: ReviewRow[] = [];

                reviewsResponse.data.forEach(value => {
                    newReviewsRows.push({
                        review: value,
                        reviewId: value.id,
                        tour: toursResponse.data.find(t => t.id == value.tourId),
                        tourId: value.tourId
                    })
                })
                console.log("new rows: ", newReviewsRows)
                setReviewRows(newReviewsRows)
            }
        } catch {
            setError("Ошибка получения данных")
        }
    }

    useEffect(() => {
        fetchData();
    }, []);

    const removeReview = async (id: number) => {
        try {
            await deleteReview(id);
            await fetchData();
        } catch(error) {
            console.error('Error deleting reservation:', error);
            setError("Не удалось удалить отзыв");
        }
    };

    const handleTourIdChange = (value: number) => {
        console.log("value: ", value)
        setTourId(value)
    }

    return (
        <div className={classes.pageContainer}>
            <div className={classes.header}>
                <h1 className={classes.title}>Управление отзывами</h1>
            </div>

            {error && (
                <div className={classes.errorAlert}>
                    {error}
                </div>
            )}
            <div className={classes.mainActions}>
                <Input type="number" placeholder="ID Тура" onChange={(e) => handleTourIdChange(Number(e.target.value))} value={tourId}/>
                <Button size="sm" onClick={() => fetchData(tourId)}> Поиск </Button>
                <Button size="sm" onClick={() => { fetchData(); setTourId(0)}}> Сбросить </Button>
            </div>
            <div className={classes.tableContainer}>
                {reviewRows && reviewRows?.length > 0 ? (
                    <GenericTable 
                        data={reviewRows}
                        columns={columns}
                        emptyMessage="Нет данных о отзывах"
                    />
                ) : (
                    <div>
                        Данные отсутствуют
                    </div>
                )}

            </div>
        </div>
    );
};