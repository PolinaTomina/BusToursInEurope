// context/AuthContext.tsx
import { createContext, useContext, useEffect, useState } from 'react';
import { JwtTokenKey } from '../../../utils/constants/localStorageConstants';
import { isAdmin } from '../../../queries/auth';

const AuthContext = createContext({
  token: null as string | null,
  admin: false,
  isLoading: true,
});

export const AuthProvider = ({ children }: { children: React.ReactNode }) => {
  const [token, setToken] = useState<string | null>(null);
  const [admin, setAdmin] = useState(false);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const checkAuth = async () => {
      const storedToken = localStorage.getItem(JwtTokenKey);
      setToken(storedToken);

      if (storedToken) {
        try {
          const response = await isAdmin(storedToken)
          
          if (response.status === 200) {
            setAdmin(true);
          }
        } catch (error) {
          console.error('Error checking admin status:', error);
        }
      }
      
      setIsLoading(false);
    };

    checkAuth();

    // Слушаем изменения в localStorage
    const handleStorageChange = (e: StorageEvent) => {
      if (e.key === JwtTokenKey) {
        setToken(e.newValue);
      }
    };

    window.addEventListener('storage', handleStorageChange);
    return () => window.removeEventListener('storage', handleStorageChange);
  }, []);

  return (
    <AuthContext.Provider value={{ token, admin, isLoading }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);