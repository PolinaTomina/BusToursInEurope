import React, { useState } from 'react';
import { Modal } from '../../../ui/Modal';
import { Input } from '../../../ui/Input';
import { Button } from '../../../ui/Button';
import { createRoute } from '../../../queries/routes';
import { CreateRouteBusDto, CreateWayPointDto } from '../../../types/Routes';

interface CreateRouteModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSuccess?: () => void;
}

export const CreateRouteModal: React.FC<CreateRouteModalProps> = ({
  isOpen,
  onClose,
  onSuccess
}) => {
  const [formData, setFormData] = useState<CreateRouteBusDto>({
    distance: 0,
    wayPoints: []
  });

  const [wayPoint, setWayPoint] = useState<CreateWayPointDto>({
    namePlace: '',
    cityId: 0,
    hotelId: 0
  });

  const addWayPoint = () => {
    setFormData({
      ...formData,
      wayPoints: [...(formData.wayPoints || []), wayPoint]
    });
    setWayPoint({ namePlace: '', cityId: 0, hotelId: 0 });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await createRoute(formData);
      onSuccess?.();
      onClose();
    } catch (error) {
      console.error('Failed to create route:', error);
    }
  };

  return (
    <Modal isOpen={isOpen} onClose={onClose} title="Create New Route">
      <form onSubmit={handleSubmit} className="space-y-4">
        <Input
          label="Distance"
          type="number"
          value={formData.distance}
          onChange={(e) => setFormData({ ...formData, distance: parseFloat(e.target.value) })}
          required
          min={0}
        />
        
        <div className="border p-4 rounded-md">
          <h3 className="text-lg font-medium mb-3">Add Waypoint</h3>
          <div className="space-y-3">
            <Input
              label="Place Name"
              value={wayPoint.namePlace || ''}
              onChange={(e) => setWayPoint({ ...wayPoint, namePlace: e.target.value })}
            />
            <Input
              label="City ID"
              type="number"
              value={wayPoint.cityId}
              onChange={(e) => setWayPoint({ ...wayPoint, cityId: parseInt(e.target.value) })}
            />
            <Input
              label="Hotel ID"
              type="number"
              value={wayPoint.hotelId}
              onChange={(e) => setWayPoint({ ...wayPoint, hotelId: parseInt(e.target.value) })}
            />
            <Button type="button" onClick={addWayPoint} variant="outline" className="w-full">
              Add Waypoint
            </Button>
          </div>
        </div>

        {formData.wayPoints && formData.wayPoints.length > 0 && (
          <div className="mt-4">
            <h3 className="text-lg font-medium mb-2">Waypoints</h3>
            <div className="space-y-2">
              {formData.wayPoints.map((wp, index) => (
                <div key={index} className="p-2 bg-gray-50 rounded">
                  {wp.namePlace} (City: {wp.cityId}, Hotel: {wp.hotelId})
                </div>
              ))}
            </div>
          </div>
        )}

        <div className="flex justify-end space-x-2">
          <Button type="button" variant="outline" onClick={onClose}>
            Cancel
          </Button>
          <Button type="submit">
            Create Route
          </Button>
        </div>
      </form>
    </Modal>
  );
};