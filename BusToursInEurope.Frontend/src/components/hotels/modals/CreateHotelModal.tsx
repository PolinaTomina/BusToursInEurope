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
  model?: HotelDto
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
    cityDtoId: 0
  });

  const [cityOptions, setCityOptions] = useState<SelectOption[]>([])

  const [title, setTitle] = useState('Создать отель')

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      if (model) {
        const updatedHotelData: HotelDto = {
          id: model.id,
          ...formData
        };
        await updateHotel(model.id, updatedHotelData);
      } else {
        await createHotel({ ...formData, id: 0 });
      }
      onSuccess?.();
      onClose();
    } catch (error) {
      console.error('Failed to create hotel:', error);
    }
  };

  useEffect(() => {
    const fecthCities = async () => {
        try {
          const response = await getCities({});
          const options: SelectOption[] = response.data.map((city: CityDto) => ({
              id: city.id,
              value: `${city.name}, ${city.country}`
          }));

          setCityOptions(options)
        } catch (error) {
          console.error('Error fetching buses:', error);
      }
    }

    fecthCities()
  }, [])

  useEffect(() => {
    if (model) {
      const { id, ...modelWithoutId } = model;
      setTitle('Редактировать отель')
      setFormData(modelWithoutId);
    } else {
      setTitle('Создать отель')
      setFormData({
        name: '',
        rating: 0,
        cityDtoId: 0
      });
    }
  }, [isOpen, model])

  return (
    <Modal isOpen={isOpen} onClose={onClose} title={title}>
      <form onSubmit={handleSubmit} className="space-y-4">
        <Input
          label="Название"
          value={formData.name || ''}
          onChange={(e) => setFormData({ ...formData, name: e.target.value })}
          required
        />
        <Input
          label="Рейтинг"
          type="number"
          value={formData.rating}
          onChange={(e) => setFormData({ ...formData, rating: parseFloat(e.target.value) })}
          required
          min={0}
          max={5}
          step={0.1}
        />
        <Select label='Город' options={cityOptions} currentSelectedId={model?.cityDtoId}/>
        <div className="flex justify-end space-x-2">
          <Button type="button" variant="outline" onClick={onClose}>
            Отмена
          </Button>
          <Button type="submit">
            Сохранить
          </Button>
        </div>
      </form>
    </Modal>
  );
};