import React, { useEffect, useState } from "react";
import { ShortTourFrame } from "../../../components/tours/frames/ShortTourFrame/ShortTourFrame";
import { getToursByFilters } from "../../../queries/tours";
import { ShortTourDto } from "../../../types/Tours";
import classes from "./styles.module.css";
import { FiChevronDown, FiChevronUp, FiFilter } from "react-icons/fi";
import { motion, AnimatePresence } from "framer-motion";

interface TourFilter {
  Country?: string;
  MinPrice?: number;
  MaxPrice?: number;
  StartDate?: string;
  EndDate?: string;
}

export const ToursPage: React.FC = () => {
  const [tours, setTours] = useState<ShortTourDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [filters, setFilters] = useState<TourFilter>({});
  const [filtersPanelOpen, setFiltersPanelOpen] = useState(false);
  const [tempFilters, setTempFilters] = useState<TourFilter>({});

  const fetchTours = async (appliedFilters: TourFilter) => {
    try {
      setLoading(true);
      const { data } = await getToursByFilters(appliedFilters);
      setTours(data);
    } catch (error) {
      console.error("Error fetching tours:", error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchTours({});
  }, []);

  const handleFilterChange = (key: keyof TourFilter, value: any) => {
    setTempFilters(prev => ({ ...prev, [key]: value }));
  };

  const applyFilters = () => {
    setFilters(tempFilters);
    fetchTours(tempFilters);
    setFiltersPanelOpen(false);
  };

  const resetFilters = () => {
    setTempFilters({});
    setFilters({});
    fetchTours({});
  };

  return (
    <div className={classes.toursPage}>
      <div className={classes.filtersSection}>
        <motion.button 
          className={`${classes.filtersToggle} ${filtersPanelOpen ? classes.active : ''}`}
          onClick={() => setFiltersPanelOpen(!filtersPanelOpen)}
          whileHover={{ scale: 1.02 }}
          whileTap={{ scale: 0.98 }}
        >
          <FiFilter />
          <span>Фильтры</span>
          {filtersPanelOpen ? <FiChevronUp /> : <FiChevronDown />}
        </motion.button>

        <AnimatePresence>
          {filtersPanelOpen && (
            <motion.div
              className={classes.filtersPanel}
              initial={{ opacity: 0, height: 0 }}
              animate={{ opacity: 1, height: 'auto' }}
              exit={{ opacity: 0, height: 0 }}
              transition={{ duration: 0.3 }}
            >
              <div className={classes.filterGroup}>
                <label>Страна</label>
                <input
                  type="text"
                  placeholder="Введите страну"
                  value={tempFilters.Country || ''}
                  onChange={(e) => handleFilterChange('Country', e.target.value)}
                  className={classes.filterInput}
                />
              </div>

              <div className={classes.filterGroup}>
                <label>Цена, €</label>
                <div className={classes.rangeInputs}>
                  <input
                    type="number"
                    placeholder="От"
                    value={tempFilters.MinPrice || ''}
                    onChange={(e) => handleFilterChange('MinPrice', Number(e.target.value))}
                    className={classes.filterInput}
                  />
                  <input
                    type="number"
                    placeholder="До"
                    value={tempFilters.MaxPrice || ''}
                    onChange={(e) => handleFilterChange('MaxPrice', Number(e.target.value))}
                    className={classes.filterInput}
                  />
                </div>
              </div>

              <div className={classes.filterGroup}>
                <label>Даты</label>
                <div className={classes.rangeInputs}>
                  <input
                    type="date"
                    value={tempFilters.StartDate || ''}
                    onChange={(e) => handleFilterChange('StartDate', e.target.value)}
                    className={classes.filterInput}
                  />
                  <input
                    type="date"
                    value={tempFilters.EndDate || ''}
                    onChange={(e) => handleFilterChange('EndDate', e.target.value)}
                    className={classes.filterInput}
                  />
                </div>
              </div>

              <div className={classes.filterActions}>
                <motion.button 
                  className={classes.resetButton}
                  onClick={resetFilters}
                  whileHover={{ scale: 1.03 }}
                  whileTap={{ scale: 0.97 }}
                >
                  Сбросить
                </motion.button>
                <motion.button 
                  className={classes.applyButton}
                  onClick={applyFilters}
                  whileHover={{ scale: 1.03 }}
                  whileTap={{ scale: 0.97 }}
                >
                  Применить
                </motion.button>
              </div>
            </motion.div>
          )}
        </AnimatePresence>
      </div>

      <div className={classes.toursList}>
        {loading ? (
          <div className={classes.loading}>Загрузка...</div>
        ) : tours.length > 0 ? (
          tours.map((tour) => (
            <ShortTourFrame key={tour.id} {...tour} />
          ))
        ) : (
          <div className={classes.noResults}>Ничего не найдено</div>
        )}
      </div>
    </div>
  );
};