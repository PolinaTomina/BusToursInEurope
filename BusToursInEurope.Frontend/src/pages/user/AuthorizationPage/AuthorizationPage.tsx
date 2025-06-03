import React, { useState } from 'react';
import { Mail, Lock, User, AlertCircle, Phone, Eye, EyeOff } from 'lucide-react';
import styles from './styles.module.css';
import { AuthorizationDto, RegistrationDto } from '../../../types/Authorization';
import { isAdmin, login, register } from '../../../queries/auth';
import { JwtTokenKey } from '../../../utils/constants/localStorageConstants';
import { useNavigate } from 'react-router-dom';

type AuthMode = 'login' | 'register';

export const AuthorizationPage: React.FC = () => {
  const [mode, setMode] = useState<AuthMode>('login');
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();
  
  const [loginEmail, setLoginEmail] = useState('');
  const [loginPassword, setLoginPassword] = useState('');
  const [showLoginPassword, setShowLoginPassword] = useState(false);
  
  const [registerUsername, setRegisterUsername] = useState('');
  const [registerEmail, setRegisterEmail] = useState('');
  const [registerPassword, setRegisterPassword] = useState('');
  const [showRegisterPassword, setShowRegisterPassword] = useState(false);
  const [registerPhone, setRegisterPhone] = useState('');

  const handleLoginSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!loginEmail || !loginPassword) {
      setError('Заполните все поля!');
      return;
    }

    try {
      const fetchLogin = async (dto: AuthorizationDto) => {
        console.log(dto)
        const response = await login(dto);

        if (response.data) {
          console.log(response.data)
          const token = response.data.token

          localStorage.setItem(JwtTokenKey, `Bearer ${token}`)

          await redirectByToken(`Bearer ${token}`)
        }
      }

      const request : AuthorizationDto = { 
        email: loginEmail,
        password: loginPassword
      }

      fetchLogin(request)
    } catch (error) {
      setError('Ошибка на сервере')
    }

    setError(null);
  };

  const handleRegisterSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!registerUsername || !registerEmail || !registerPassword || !registerPhone) {
      setError('Заполните все поля!');
      return;
    }

    try {
      const fetchRegister = async (dto: RegistrationDto) => {
        const response = await register(dto);

        if (response.data) {
          const token = response.data

          localStorage.setItem(JwtTokenKey, `Bearer ${token}`)
          
          redirectByToken(`Bearer ${token}`)
        }
      }

      const request : RegistrationDto = { 
        email: registerEmail,
        numPhone: registerPhone,
        login: registerUsername,
        password: registerPassword
      }

      fetchRegister(request)
    } catch (error) {
      setError('Ошибка на сервере')
    }
    
    setError(null);
  };  

  const switchMode = (newMode: AuthMode) => {
    setMode(newMode);
    setError(null);
  };

  const redirectByToken = async (bearer: string) => {
    try {
      const response = await isAdmin(bearer)

      if (response.status === 200) {
        navigate('/admin')
      }
    }
    catch {
        navigate('/main')
    }
  }

  const toggleLoginPasswordVisibility = () => {
    setShowLoginPassword(!showLoginPassword);
  };

  const toggleRegisterPasswordVisibility = () => {
    setShowRegisterPassword(!showRegisterPassword);
  };

  return (
    <div className={styles.container}>
      <div className={styles.authCard}>
        <div className={styles.tabsContainer}>
          <button 
            className={`${styles.tabButton} ${mode === 'login' ? styles.activeTab : ''}`}
            onClick={() => switchMode('login')}
          >
            Авторизация
          </button>
          <button 
            className={`${styles.tabButton} ${mode === 'register' ? styles.activeTab : ''}`}
            onClick={() => switchMode('register')}
          >
            Регистрация
          </button>
        </div>
        
        <div className={styles.formContainer}>
          {mode === 'login' ? (
            <form onSubmit={handleLoginSubmit} className={styles.form}>
              <h2 className={styles.formTitle}>Вход</h2>
              
              <div className={styles.inputGroup}>
                <label htmlFor="login-email" className={styles.label}>
                  Email
                </label>
                <div className={styles.inputWithIcon}>
                  <Mail size={18} className={styles.inputIcon} />
                  <input
                    id="login-email"
                    type="email"
                    className={styles.input}
                    placeholder="your@email.com"
                    value={loginEmail}
                    onChange={(e) => setLoginEmail(e.target.value)}
                  />
                </div>
              </div>
              
              <div className={styles.inputGroup}>
                <label htmlFor="login-password" className={styles.label}>
                  Пароль
                </label>
                <div className={styles.inputWithIcon}>
                  <Lock size={18} className={styles.inputIcon} />
                  <input
                    id="login-password"
                    type={showLoginPassword ? "text" : "password"}
                    className={styles.input}
                    placeholder="••••••••"
                    value={loginPassword}
                    onChange={(e) => setLoginPassword(e.target.value)}
                  />
                  <button 
                    type="button" 
                    className={styles.passwordToggle}
                    onClick={toggleLoginPasswordVisibility}
                  >
                    {showLoginPassword ? <EyeOff size={18} /> : <Eye size={18} />}
                  </button>
                </div>
              </div>
              
              {error && (
                <div className={styles.errorMessage}>
                  <AlertCircle size={16} />
                  <span>{error}</span>
                </div>
              )}
              
              <button type="submit" className={styles.submitButton}>
                Войти
              </button>
            </form>
          ) : (
            <form onSubmit={handleRegisterSubmit} className={styles.form}>
              <h2 className={styles.formTitle}>Создать аккаунт</h2>
              
              <div className={styles.inputGroup}>
                <label htmlFor="register-username" className={styles.label}>
                  Имя пользователя
                </label>
                <div className={styles.inputWithIcon}>
                  <User size={18} className={styles.inputIcon} />
                  <input
                    id="register-username"
                    type="text"
                    className={styles.input}
                    placeholder="johndoe"
                    value={registerUsername}
                    onChange={(e) => setRegisterUsername(e.target.value)}
                  />
                </div>
              </div>
              
              <div className={styles.inputGroup}>
                <label htmlFor="register-email" className={styles.label}>
                  Email
                </label>
                <div className={styles.inputWithIcon}>
                  <Mail size={18} className={styles.inputIcon} />
                  <input
                    id="register-email"
                    type="email"
                    className={styles.input}
                    placeholder="your@email.com"
                    value={registerEmail}
                    onChange={(e) => setRegisterEmail(e.target.value)}
                  />
                </div>
              </div>

              <div className={styles.inputGroup}>
                <label htmlFor="register-phone" className={styles.label}>
                  Номер телефона
                </label>
                <div className={styles.inputWithIcon}>
                  <Phone size={18} className={styles.inputIcon} />
                  <input
                    id="register-phone"
                    type="tel"
                    className={styles.input}
                    placeholder="+375-xx-xxx-xx-xx"
                    value={registerPhone}
                    onChange={(e) => setRegisterPhone(e.target.value)}
                  />
                </div>
              </div>
              
              <div className={styles.inputGroup}>
                <label htmlFor="register-password" className={styles.label}>
                  Пароль
                </label>
                <div className={styles.inputWithIcon}>
                  <Lock size={18} className={styles.inputIcon} />
                  <input
                    id="register-password"
                    type={showRegisterPassword ? "text" : "password"}
                    className={styles.input}
                    placeholder="••••••••"
                    value={registerPassword}
                    onChange={(e) => setRegisterPassword(e.target.value)}
                  />
                  <button 
                    type="button" 
                    className={styles.passwordToggle}
                    onClick={toggleRegisterPasswordVisibility}
                  >
                    {showRegisterPassword ? <EyeOff size={18} /> : <Eye size={18} />}
                  </button>
                </div>
              </div>
              
              {error && (
                <div className={styles.errorMessage}>
                  <AlertCircle size={16} />
                  <span>{error}</span>
                </div>
              )}
              
              <button type="submit" className={styles.submitButton}>
                Создать аккаунт
              </button>
            </form>
          )}
        </div>
      </div>
    </div>
  );
};