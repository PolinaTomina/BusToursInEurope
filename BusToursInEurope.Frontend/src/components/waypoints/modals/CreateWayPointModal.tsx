import React, { useState } from 'react';
import { Modal } from '../../../ui/Modal';
import { Input } from '../../../ui/Input';
import { Button } from '../../../ui/Button';
import { createWayPoint } from '../../../queries/waypoints';
import { CreateWPDto } from '../../../types/WayPoints';

interface CreateWayPointModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSuccess?: () => void;
  routeId: number;
}

export const CreateWayPointModal: React.FC<CreateWayPointModalProps> = ({
  isOpen,
  onClose,
  onSuccess,
  routeId
}) => {
  const [formData, setFormData] = useState<CreateWPDto>({
    namePlace: '',
    cityDtoId: 0,
    routeBusDtoId: routeId,
    hotelDtoId: 0
  });

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await createWayPoint(formData);
      onSuccess?.();
      onClose();
    } catch (error) {
      console.error('Failed to create waypoint:', error);
    }
  };

  return (
    <Modal isOpen={isOpen} onClose={onClose} title="Create New Waypoint">
      <form onSubmit={handleSubmit} className="space-y-4">
        <Input
          label="Place Name"
          value={formData.namePlace || ''}
          onChange={(e) => setFormData({ ...formData, namePlace: e.target.value })}
          required
        />
        <Input
          label="City ID"
          type="number"
          value={formData.cityDtoId}
          onChange={(e) => setFormData({ ...formData, cityDtoId: parseInt(e.target.value) })}
          required
        />
        <Input
          label="Hotel ID"
          type="number"
          value={formData.hotelDtoId}
          onChange={(e) => setFormData({ ...formData, hotelDtoId: parseInt(e.target.value) })}
          required
        />
        <div className="flex justify-end space-x-2">
          <Button type="button" variant="outline" onClick={onClose}>
            Cancel
          </Button>
          <Button type="submit">
            Create Waypoint
          </Button>
        </div>
      </form>
    </Modal>
  );
};