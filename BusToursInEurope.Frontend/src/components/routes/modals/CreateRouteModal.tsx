import React, { useState, useEffect } from 'react';
import { Modal } from '../../../ui/Modal';
import { Input } from '../../../ui/Input';
import { Button } from '../../../ui/Button';
import { createRoute, updateRoute } from '../../../queries/routes';
import { CreateRouteBusDto, RouteBusDto, UpdateRouteBusDto } from '../../../types/Routes';
import { CreateWayPointDto, WayPointDto } from '../../../types/WayPoints';

interface CreateRouteModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSuccess?: () => void;
  route?: RouteBusDto;
}

export const CreateRouteModal: React.FC<CreateRouteModalProps> = ({
  isOpen,
  onClose,
  onSuccess,
  route
}) => {
  const [name, setName] = useState("");
  const [distance, setDistance] = useState(0);
  const [wayPoints, setWayPoints] = useState<CreateWayPointDto[]>([]);
  const [currentWayPoint, setCurrentWayPoint] = useState<CreateWayPointDto>({
    namePlace: '',
    cityId: null,
    hotelId: null
  });
  const [editingIndex, setEditingIndex] = useState<number | null>(null);

  // Инициализация формы
  useEffect(() => {
    if (route) {
      setName(route.name);
      setDistance(route.distance);
      setWayPoints(
        route.wayPointsDto?.map(wp => ({
          namePlace: wp.namePlace || '',
          cityId: wp.cityDtoId || null,
          hotelId: wp.hotelDtoId || null
        })) || []
      );
    } else {
      resetForm();
    }
  }, [route, isOpen]);

  const resetForm = () => {
    setName("");
    setDistance(0);
    setWayPoints([]);
    setCurrentWayPoint({ namePlace: '', cityId: null, hotelId: null });
    setEditingIndex(null);
  };

  const handleAddWayPoint = () => {
    if (editingIndex !== null) {
      // Обновляем существующую точку
      const updated = [...wayPoints];
      updated[editingIndex] = currentWayPoint;
      setWayPoints(updated);
      setEditingIndex(null);
    } else {
      // Добавляем новую точку
      setWayPoints([...wayPoints, currentWayPoint]);
    }
    setCurrentWayPoint({ namePlace: '', cityId: null, hotelId: null });
  };

  const handleEditWayPoint = (index: number) => {
    setCurrentWayPoint(wayPoints[index]);
    setEditingIndex(index);
  };

  const handleRemoveWayPoint = (index: number) => {
    setWayPoints(wayPoints.filter((_, i) => i !== index));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const routeData = {
        name,
        distance,
        wayPoints,
        ...(route && { id: route.id })
      };

      if (route) {
        await updateRoute(routeData as UpdateRouteBusDto);
      } else {
        await createRoute(routeData as CreateRouteBusDto);
      }

      onSuccess?.();
      onClose();
      resetForm();
    } catch (error) {
      console.error('Failed to save route:', error);
    }
  };

  return (
    <Modal 
      isOpen={isOpen} 
      onClose={() => {
        onClose();
        resetForm();
      }} 
      title={route ? 'Редактирование маршрута' : 'Создание маршрута'}
    >
      <form onSubmit={handleSubmit} className="space-y-4">
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          <Input
            label="Название"
            value={name}
            onChange={(e) => setName(e.target.value)}
            required
          />
          <Input
            label="Растояние (км)"
            type="number"
            value={distance}
            onChange={(e) => setDistance(parseFloat(e.target.value) || 0)}
            required
            min={0}
            step="0.1"
          />
        </div>

        <div className="border p-4 rounded-md">
          <h3 className="text-lg font-medium mb-3">
            {editingIndex !== null ? 'Редактирование точки маршрута' : 'Добавление точки маршрута'}
          </h3>
          <div className="grid grid-cols-1 md:grid-cols-3 gap-3">
            <Input
              label="Место"
              value={currentWayPoint.namePlace || ''}
              onChange={(e) => setCurrentWayPoint({
                ...currentWayPoint,
                namePlace: e.target.value
              })}
              placeholder="--------------"
            />
            <Input
              label="ID Города"
              type="number"
              value={currentWayPoint.cityId || ''}
              onChange={(e) => setCurrentWayPoint({
                ...currentWayPoint,
                cityId: e.target.value ? parseInt(e.target.value) : null
              })}
              placeholder="Не указано"
            />
            <Input
              label="ID Отеля"
              type="number"
              value={currentWayPoint.hotelId || ''}
              onChange={(e) => setCurrentWayPoint({
                ...currentWayPoint,
                hotelId: e.target.value ? parseInt(e.target.value) : null
              })}
              placeholder="Не указано"
            />
          </div>
          <div className="flex gap-2 mt-3">
            <Button 
              type="button" 
              onClick={handleAddWayPoint} 
              variant="outline"
            >
              {editingIndex !== null ? 'Обновить точку' : 'Добавить точку'}
            </Button>
            {editingIndex !== null && (
              <Button 
                type="button" 
                onClick={() => {
                  setCurrentWayPoint({ namePlace: '', cityId: null, hotelId: null });
                  setEditingIndex(null);
                }} 
                variant="outline"
              >
                Отмена
              </Button>
            )}
          </div>
        </div>

        {wayPoints.length > 0 && (
          <div className="mt-4">
            <h3 className="text-lg font-medium mb-2">Точки маршрута ({wayPoints.length})</h3>
            <div className="space-y-2 max-h-60 overflow-y-auto">
              {wayPoints.map((wp, index) => (
                <div key={index} className="p-3 bg-gray-50 rounded-md flex justify-between items-center">
                  <div>
                    <span className="font-medium">
                      {wp.namePlace || `Точка ${index + 1}`}
                    </span>
                    <span className="text-gray-600 ml-2">
                      (Город: {wp.cityId || 'не указан'}, Отель: {wp.hotelId || 'не указан'})
                    </span>
                  </div>
                  <div className="flex space-x-2">
                    <Button 
                      type="button" 
                      size="sm" 
                      variant="outline"
                      onClick={() => handleEditWayPoint(index)}
                    >
                      Редактировать
                    </Button>
                    <Button 
                      type="button" 
                      size="sm" 
                      variant="primary"
                      onClick={() => handleRemoveWayPoint(index)}
                    >
                      Удалить
                    </Button>
                  </div>
                </div>
              ))}
            </div>
          </div>
        )}

        <div className="flex justify-end space-x-2 pt-4">
          <Button type="button" variant="outline" onClick={() => {
            onClose();
            resetForm();
          }}>
            Отмена
          </Button>
          <Button 
            type="submit" 
            disabled={!name || !distance}
          >
            {route ? 'Обновить маршрут' : 'Создать маршрут'}
          </Button>
        </div>
      </form>
    </Modal>
  );
};