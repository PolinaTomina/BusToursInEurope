import React, { useEffect, useState } from 'react';
import { Modal } from '../../../ui/Modal';
import { Input } from '../../../ui/Input';
import { Button } from '../../../ui/Button';
import { createBus, updateBus } from '../../../queries/buses';
import { BusDto } from '../../../types/Buses';

interface CreateBusModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSuccess?: () => void;
  model?: BusDto
}

export const CreateBusModal: React.FC<CreateBusModalProps> = ({
  isOpen,
  onClose,
  onSuccess,
  model
}) => {
  const [formData, setFormData] = useState<Omit<BusDto, 'id'>>({
    name: '',
    numOfSeats: 0
  });

  const [title, setTitle] = useState('Создать автобус')

  useEffect(() => {
    if (model) {
      const { id, ...modelWithoutId } = model;
      setTitle('Редактировать автобус')
      setFormData(modelWithoutId);
    } else {
      setTitle('Создать автобус')
      setFormData({
        name: '',
        numOfSeats: 0
      });
    }
  }, [isOpen, model])

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      if (model) {
        const updatedBusData: BusDto = {
          id: model.id,
          ...formData
        };
        await updateBus(model.id, updatedBusData);
      } else {
        await createBus({ ...formData, id: 0 });
      }
      onSuccess?.();
      onClose();
    } catch (error) {
      console.error('Failed to create bus:', error);
    }
  };

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
          label="Количество мест"
          type="number"
          value={formData.numOfSeats}
          onChange={(e) => setFormData({ ...formData, numOfSeats: parseInt(e.target.value) })}
          required
          min={1}
        />
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