import React, { useState, useEffect } from 'react';
import { Button } from '../../../ui/Button';
import { blockUser, unblockUser, getUsers } from '../../../queries/admin';
import { ShortUserDto } from '../../../types/Users';

export const AdminUserPage: React.FC = () => {
  const [users, setUsers] = useState<ShortUserDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const fetchUsers = async () => {
    try {
      setLoading(true);
      const response = await getUsers();
      // Извлекаем массив пользователей из поля result
      const usersData = response.data.result || [];
      setUsers(usersData.map((user: { id: number; email: string; login: string; isLocked: boolean }) => ({
        Id: user.id,
        Email: user.email,
        Login: user.login,
        IsBlocked: user.isLocked
      })));
      setError(null);
    } catch (err) {
      setError('Не удалось загрузить пользователей');
      console.error('Failed to fetch users:', err);
      setUsers([]);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchUsers();
  }, []);

  const handleToggleBlock = async (userId: number, isBlocked: boolean) => {
    try {
      if (isBlocked) {
        await unblockUser(userId.toString());
      } else {
        await blockUser(userId.toString());
      }
      fetchUsers(); // Обновляем список после изменения
    } catch (err) {
      setError(`Не удалось ${isBlocked ? 'разблокировать' : 'заблокировать'} пользователя`);
      console.error('Failed to toggle block:', err);
    }
  };

  if (loading) {
    return <div className="flex justify-center py-8">Загрузка...</div>;
  }

  if (error) {
    return (
      <div className="flex flex-col items-center py-8">
        <div className="text-red-500 mb-4">{error}</div>
        <Button onClick={fetchUsers}>Повторить попытку</Button>
      </div>
    );
  }

  if (users.length === 0) {
    return <div className="flex justify-center py-8">Нет данных о пользователях</div>;
  }

  return (
    <div className="container mx-auto px-4 py-8">
      <h1 className="text-2xl font-bold mb-6">Управление пользователями</h1>
      
      <div className="bg-white shadow rounded-lg overflow-hidden">
        <div className="overflow-x-auto">
          <table className="min-w-full divide-y divide-gray-200">
            <thead className="bg-gray-50">
              <tr>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">ID</th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Логин</th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Email</th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Статус</th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Действия</th>
              </tr>
            </thead>
            <tbody className="bg-white divide-y divide-gray-200">
              {users.map((user) => (
                <tr key={user.Id}>
                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">{user.Id}</td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">{user.Login}</td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">{user.Email}</td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                    {user.IsBlocked ? (
                      <span className="px-2 inline-flex text-xs leading-5 font-semibold rounded-full bg-red-100 text-red-800">
                        Заблокирован
                      </span>
                    ) : (
                      <span className="px-2 inline-flex text-xs leading-5 font-semibold rounded-full bg-green-100 text-green-800">
                        Активен
                      </span>
                    )}
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                    <Button
                      variant={user.IsBlocked ? 'primary' : 'outline'}
                      onClick={() => handleToggleBlock(user.Id, user.IsBlocked)}
                    >
                      {user.IsBlocked ? 'Разблокировать' : 'Заблокировать'}
                    </Button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
};