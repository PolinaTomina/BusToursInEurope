import { BrowserRouter, Routes, Route, useNavigate, useLocation } from 'react-router-dom'
import { AboutUsPage, MainPage } from './pages/common';
import { FullTourPage, ToursPage } from './pages/tours';
import { DefaultLayout } from './components/layout/DefaultLayout/DefaultLayout';
import { AdminLayout } from './components/layout/AdminLayout/AdminLayout';
import { AdminBusPage, AdminOrdersPage, AdminToursPage } from './pages/admin';
import { AuthorizationPage, UserProfilePage } from './pages/user';
import classes from './app.module.css'
import { AuthProvider } from './components/common/Authentication/AuthenticationProvider';
import { AdminRoutesBusPage } from './pages/admin/AdminRoutesBusPage/AdminRoutesBusPage';
import { AdminUserPage } from './pages/admin/AdminUsersPage/AdminUsersPage';
import { useEffect } from 'react';
import { isAdmin, isAuthenticated } from './queries/auth';
import { JwtTokenKey } from './utils/constants/localStorageConstants';
import { RulesPage } from './pages/common/RulesPage/RulesPage';
import { AdminReviewsPage } from './pages/admin/AdminReviewsPage/AdminReviewsPage';

function App() {
  return (
    <div className={classes.app}>
      <AuthProvider>
        <BrowserRouter>
          <RouterContent />
        </BrowserRouter>
      </AuthProvider>
    </div>
  );
}

function RouterContent() {
  const navigate = useNavigate();
  const location = useLocation();

  // Маршруты, не требующие авторизации
  const publicRoutes = [
    '/main', 
    '/', 
    '/about', 
    '/authentication', 
    '/tours',
    '/tours/:id',
    '/rules'
  ];

  const isPublicRoute = (path: string) => {
    if (publicRoutes.some(route => path === route || path.startsWith(route.replace(':id', '')))) {
      return true;
    }
    const tourIdPattern = /^\/tours\/\d+$/;
    return tourIdPattern.test(path);
  };

  useEffect(() => {
    const checkAuth = async () => {
      try {
        const auth = await isAuthenticated();
        const isAdminRoute = location.pathname.startsWith('/admin');
        
        // Если пользователь не авторизован и пытается попасть на защищенный маршрут
        if (!auth.data && !isPublicRoute(location.pathname)) {
          navigate('/authentication');
          return;
        }

        // Если пользователь пытается попасть в админку
        if (isAdminRoute) {
          const token = localStorage.getItem(JwtTokenKey);
          if (!token) {
            navigate('/authentication');
            return;
          }
          
          try {
            const adminResponse = await isAdmin(token);
            if (adminResponse.status !== 200) {
              navigate('/authemtication');
              return;
            }
          } catch (error) {
            console.error('Admin check failed:', error);
            navigate('/');
            return;
          }
        }
      } catch (error) {
        console.error('Auth check failed:', error);
        if (!isPublicRoute(location.pathname)) {
          navigate('/authentication');
        }
      }
    };

    checkAuth();
    const intervalId = setInterval(checkAuth, 10 * 60 * 1000);
    return () => clearInterval(intervalId);
  }, [navigate, location.pathname]);

  return (
    <Routes>
      <Route element={<DefaultLayout />}>
        <Route path='/' element={<MainPage />} />
        <Route path='main' element={<MainPage />} />
        <Route path='about' element={<AboutUsPage />}/>
        <Route path='authentication' element={<AuthorizationPage/>}/>
        <Route path='tours' element={<ToursPage />} />
        <Route path='tours/:id' element={<FullTourPage />} />
        <Route path='profile' element={<UserProfilePage/>} />
        <Route path='rules' element={<RulesPage/>} />
      </Route>
      <Route path='/admin' element={<AdminLayout />}>
        <Route path='buses' element={<AdminBusPage />} />
        <Route path='routes' element={<AdminRoutesBusPage/>} />
        <Route path='orders' element={<AdminOrdersPage />} />
        <Route path='tours' element={<AdminToursPage />} />
        <Route path='profile' element={<UserProfilePage/>} />
        <Route path='users' element={<AdminUserPage/>} />
        <Route path='reviews' element={<AdminReviewsPage/>} />
      </Route>
    </Routes>
  );
}

export default App;