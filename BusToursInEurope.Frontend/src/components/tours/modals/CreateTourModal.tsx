import React, { useEffect, useState, useRef } from 'react';
import { Modal } from '../../../ui/Modal';
import { Input } from '../../../ui/Input';
import { Button } from '../../../ui/Button';
import { createTour, updateTour, getTour } from '../../../queries/tours';
import { CreateTourDto, UpdateTourDto } from '../../../types/Tours';
import { XMarkIcon } from '@heroicons/react/24/outline';

interface CreateTourModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSuccess?: () => void;
  id?: number; // Изменено с model?: FullTourDto на id?: number
}

export const CreateTourModal: React.FC<CreateTourModalProps> = ({
  isOpen,
  onClose,
  onSuccess,
  id
}) => {
  const [formData, setFormData] = useState<CreateTourDto>({
    id: 0,
    name: '',
    price: 0,
    startDate: '',
    endDate: '',
    numOfSeats: 0,
    description: '',
    images: [],
    busId: 0,
    routeBusId: 0
  });

  const [updateData, setUpdateData] = useState<UpdateTourDto>({
    name: '',
    price: null,
    startDate: null,
    endDate: null,
    route: null,
    numOfSeats: null,
    description: null,
    images: []
  });

  const [previewImages, setPreviewImages] = useState<Array<{url: string, isExisting: boolean}>>([]);
  const fileInputRef = useRef<HTMLInputElement>(null);
  const [title, setTitle] = useState('Создать тур');
  const [isLoading, setIsLoading] = useState(false);

  const formatDate = (dateString: string) => {
    return dateString ? dateString.split('T')[0] : '';
  };

  // Загрузка данных тура при открытии модального окна
  useEffect(() => {
    const loadTourData = async () => {
      if (isOpen && id) {
        setIsLoading(true);
        try {
          const response = await getTour(id);
          const tourData = response.data;
          
          setTitle('Редактировать тур');
          setUpdateData({
            name: tourData.name || '',
            price: tourData.price,
            startDate: formatDate(tourData.startDate),
            endDate: formatDate(tourData.endDate),
            route: null,
            numOfSeats: tourData.numOfSeats,
            description: tourData.description || '',
            images: []
          });
          
          if (tourData.fullImageLink?.length) {
            loadExistingImages(tourData.fullImageLink);
          } else {
            setPreviewImages([]);
          }
        } catch (error) {
          console.error('Error loading tour data:', error);
        } finally {
          setIsLoading(false);
        }
      } else if (isOpen) {
        // Режим создания нового тура
        setTitle('Создать тур');
        setFormData({
          id: 0,
          name: '',
          price: 0,
          startDate: '',
          endDate: '',
          numOfSeats: 0,
          description: '',
          images: [],
          busId: 0,
          routeBusId: 0
        });
        setPreviewImages([]);
      }
    };

    loadTourData();
  }, [isOpen, id]);

  // Функция для загрузки существующих изображений
  const loadExistingImages = (imageLinks: string[]) => {
    const backendBasePath = 'D:/Projects/BusToursInEurope/BusToursInEurope';
    const images = imageLinks.map(link => {
      const fullPath = `${backendBasePath}/${link}`;
      return {
        url: fullPath,
        isExisting: true
      };
    });
    
    setPreviewImages(images);
    setUpdateData(prev => ({
      ...prev,
      images: []
    }));
  };

  const handleImageChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files && e.target.files.length > 0) {
      const files = Array.from(e.target.files);
      const newPreviews = files.map(file => ({
        url: URL.createObjectURL(file),
        isExisting: false
      }));

      if (id) {
        setUpdateData(prev => ({ 
          ...prev, 
          images: [...(prev.images || []), ...files] 
        }));
      } else {
        setFormData(prev => ({ 
          ...prev, 
          images: [...prev.images, ...files] 
        }));
      }

      setPreviewImages(prev => [...prev, ...newPreviews]);
    }
  };

  const removeImage = (index: number) => {
    const imageToRemove = previewImages[index];
    if (!imageToRemove.isExisting) {
      URL.revokeObjectURL(imageToRemove.url);
    }

    const newPreviews = [...previewImages];
    newPreviews.splice(index, 1);
    setPreviewImages(newPreviews);

    if (id) {
      setUpdateData(prev => {
        const newImages = [...(prev.images || [])];
        if (!imageToRemove.isExisting) {
          newImages.splice(index, 1);
        }
        return { ...prev, images: newImages };
      });
    } else {
      setFormData(prev => {
        const newImages = [...prev.images];
        newImages.splice(index, 1);
        return { ...prev, images: newImages };
      });
    }
  };

  const triggerFileInput = () => {
    fileInputRef.current?.click();
  };

  useEffect(() => {
    return () => {
      previewImages.forEach(image => {
        if (!image.isExisting) {
          URL.revokeObjectURL(image.url);
        }
      });
    };
  }, [previewImages]);

  const handleCommonFieldChange = (field: keyof CreateTourDto | keyof UpdateTourDto, value: any) => {
    if (id) {
      setUpdateData({ ...updateData, [field]: value });
    } else {
      setFormData({ ...formData, [field]: value });
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      if (id) {
        await updateTour(id, updateData);
      } else {
        await createTour(formData);
      }
      onSuccess?.();
      onClose();
    } catch (error) {
      console.error('Failed to save tour:', error);
    }
  };

  const currentData = id ? updateData : formData;

  if (isLoading) {
    return (
      <Modal isOpen={isOpen} onClose={onClose} title={title}>
        <div className="flex justify-center items-center h-40">
          <p>Загрузка данных тура...</p>
        </div>
      </Modal>
    );
  }

  return (
    <Modal isOpen={isOpen} onClose={onClose} title={title}>
      <form onSubmit={handleSubmit} className="space-y-4">
        <Input
          label="Название тура"
          value={currentData.name || ''}
          onChange={(e) => handleCommonFieldChange('name', e.target.value)}
          required
        />
        <Input
          label="Цена"
          type="number"
          value={id ? updateData.price || '' : formData.price}
          onChange={(e) => handleCommonFieldChange('price', parseFloat(e.target.value))}
          required
          min={0}
        />
        <Input
          label="Дата начала"
          type="date"
          value={id ? updateData.startDate || '' : formData.startDate}
          onChange={(e) => handleCommonFieldChange('startDate', e.target.value)}
          required
        />
        <Input
          label="Дата окончания"
          type="date"
          value={id ? updateData.endDate || '' : formData.endDate}
          onChange={(e) => handleCommonFieldChange('endDate', e.target.value)}
          required
        />
        <Input
          label="Количество мест"
          type="number"
          value={id ? updateData.numOfSeats || '' : formData.numOfSeats}
          onChange={(e) => handleCommonFieldChange('numOfSeats', parseInt(e.target.value))}
          required
          min={1}
        />
        <Input
          label="Описание"
          value={currentData.description || ''}
          onChange={(e) => handleCommonFieldChange('description', e.target.value)}
        />
        
        {id && (
          <Input
            label="Маршрут"
            value={updateData.route || ''}
            onChange={(e) => setUpdateData({ ...updateData, route: e.target.value })}
          />
        )}

        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">Изображения</label>
          
          <input
            type="file"
            multiple
            accept="image/*"
            onChange={handleImageChange}
            ref={fileInputRef}
            className="hidden"
          />
          
          <Button
            type="button"
            variant="outline"
            onClick={triggerFileInput}
            className="mb-2"
          >
            Добавить изображения
          </Button>
          
          {previewImages.length > 0 && (
            <div className="mt-2">
              <div className="flex flex-wrap gap-2">
                {previewImages.map((preview, index) => (
                  <div key={index} className="relative group">
                    <img
                      src={preview.url}
                      alt={`Preview ${index}`}
                      className="h-20 w-20 object-cover rounded border border-gray-200"
                    />
                    <button
                      type="button"
                      onClick={() => removeImage(index)}
                      className="absolute -top-2 -right-2 bg-red-500 rounded-full p-1 opacity-0 group-hover:opacity-100 transition-opacity"
                    >
                      <XMarkIcon className="h-4 w-4 text-white" />
                    </button>
                  </div>
                ))}
              </div>
              <p className="text-xs text-gray-500 mt-1">
                Существующие изображения будут заменены новыми
              </p>
            </div>
          )}
        </div>

        {!id && (
          <>
            <Input
              label="ID автобуса"
              type="number"
              value={formData.busId}
              onChange={(e) => setFormData({ ...formData, busId: parseInt(e.target.value) })}
              required
            />
            <Input
              label="ID маршрута"
              type="number"
              value={formData.routeBusId}
              onChange={(e) => setFormData({ ...formData, routeBusId: parseInt(e.target.value) })}
              required
            />
          </>
        )}

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