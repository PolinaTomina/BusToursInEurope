import React, { useEffect, useState } from 'react';
import { Modal } from '../../../ui/Modal';
import { Input } from '../../../ui/Input';
import { Button } from '../../../ui/Button';
import { createHotel, updateHotel } from '../../../queries/hotels';
import { HotelDto } from '../../../types/Hotels';
import { Select, SelectOption } from '../../ui/Select';
import { getCities } from '../../../queries/cities';
import { CityDto } from '../../../types/Cities';

interface CreateHotelModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSuccess?: () => void;
  model?: HotelDto;
}

export const CreateHotelModal: React.FC<CreateHotelModalProps> = ({
  isOpen,
  onClose,
  onSuccess,
  model
}) => {
  const [formData, setFormData] = useState<Omit<HotelDto, 'id'>>({
    name: '',
    rating: 0,
    cityId: 0 // изменено на число
  });

  const [cityOptions, setCityOptions] = useState<SelectOption[]>([]);
  const [title, setTitle] = useState('Создать отель');
  const [isLoading, setIsLoading] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsLoading(true);
    try {
      if (model) {
        await updateHotel(model.id, { ...formData, id: model.id });
      } else {
        await createHotel({ ...formData, id: 0 });
      }
      onSuccess?.();
      onClose();
    } catch (error) {
      console.error('Failed to create hotel:', error);
    } finally {
      setIsLoading(false);
    }
  };

  const handleCityChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setFormData({ ...formData, cityId: Number(e.target.value) }); // преобразуем в число
  };

  useEffect(() => {
    const fetchCities = async () => {
      try {
        const response = await getCities({});
        const options: SelectOption[] = response.data.map((city: CityDto) => ({
          id: city.id,
          value: `${city.name}, ${city.country}`
        }));
        setCityOptions(options);
        
        if (!model && options.length > 0 && formData.cityId === 0) {
          setFormData(prev => ({ ...prev, cityId: options[0].id as number }));
        }
      } catch (error) {
        console.error('Error fetching cities:', error);
      }
    };

    if (isOpen) {
      fetchCities();
    }
  }, [isOpen, model]);

  useEffect(() => {
    if (model) {
      setTitle('Редактировать отель');
      setFormData({
        name: model.name,
        rating: model.rating,
        cityId: model.cityId
      });
    } else {
      setTitle('Создать отель');
      setFormData({
        name: '',
        rating: 0,
        cityId: cityOptions[0]?.id as number || 0
      });
    }
  }, [isOpen, model, cityOptions]);

  return (
    <Modal isOpen={isOpen} onClose={onClose} title={title}>
      <form onSubmit={handleSubmit} className="space-y-4">
        <Input
          label="Название"
          value={formData.name || ""}
          onChange={(e) => setFormData({ ...formData, name: e.target.value })}
          required
        />
        <Input
          label="Рейтинг"
          type="number"
          value={formData.rating}
          onChange={(e) => setFormData({ ...formData, rating: parseFloat(e.target.value) || 0 })}
          required
          min={0}
          max={5}
          step={0.1}
        />
        <Select
          label="Город"
          options={cityOptions}
          currentSelectedId={formData.cityId}
          onChange={handleCityChange}
          required
        />
        <div className="flex justify-end space-x-2">
          <Button type="button" variant="outline" onClick={onClose} disabled={isLoading}>
            Отмена
          </Button>
          <Button type="submit" disabled={isLoading}>
            {isLoading ? 'Сохранение...' : 'Сохранить'}
          </Button>
        </div>
      </form>
    </Modal>
  );
};