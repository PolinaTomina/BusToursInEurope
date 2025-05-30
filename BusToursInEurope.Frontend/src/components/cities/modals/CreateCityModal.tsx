import React, { useState } from 'react';
import { Modal } from '../../../ui/Modal';
import { Input } from '../../../ui/Input';
import { Button } from '../../../ui/Button';
import { createCity } from '../../../queries/cities';
import { CityDto } from '../../../types/Cities';

interface CreateCityModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSuccess?: () => void;
}

export const CreateCityModal: React.FC<CreateCityModalProps> = ({
  isOpen,
  onClose,
  onSuccess
}) => {
  const [formData, setFormData] = useState<Omit<CityDto, 'id' | 'hotelIds' | 'wayPointIds'>>({
    name: '',
    country: '',
    visa: false
  });

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await createCity({
        ...formData,
        id: 0,
        hotelIds: [],
        wayPointIds: []
      });
      onSuccess?.();
      onClose();
    } catch (error) {
      console.error('Failed to create city:', error);
    }
  };

  return (
    <Modal isOpen={isOpen} onClose={onClose} title="Create New City">
      <form onSubmit={handleSubmit} className="space-y-4">
        <Input
          label="City Name"
          value={formData.name || ''}
          onChange={(e) => setFormData({ ...formData, name: e.target.value })}
          required
        />
        <Input
          label="Country"
          value={formData.country || ''}
          onChange={(e) => setFormData({ ...formData, country: e.target.value })}
          required
        />
        <div className="flex items-center space-x-2">
          <input
            type="checkbox"
            id="visa"
            checked={formData.visa}
            onChange={(e) => setFormData({ ...formData, visa: e.target.checked })}
            className="rounded border-gray-300"
          />
          <label htmlFor="visa" className="text-sm text-gray-700">
            Visa Required
          </label>
        </div>
        <div className="flex justify-end space-x-2">
          <Button type="button" variant="outline" onClick={onClose}>
            Cancel
          </Button>
          <Button type="submit">
            Create City
          </Button>
        </div>
      </form>
    </Modal>
  );
};