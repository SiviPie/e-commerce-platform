import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../AuthContext';  // Import the AuthContext hook
import '../../style/Users.css';
import Message from '../../components/Message';

const Login = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();
  const { login } = useAuth();  // Use the login function from the AuthContext

  const [message, setMessage] = useState('');

  const handleLogin = async () => {
    try {
      const formData = new FormData();
      formData.append('Username', username);
      formData.append('Parola', password);
      const response = await axios.post('http://localhost:5239/api/Utilizatori/Login', formData);
    
      if (response.status === 200) {
        const { Token, UserID, Nume, Prenume } = response.data;
        login(Token, { UserID, Nume, Prenume });
        setMessage("Autentificarea a avut succes!");
        navigate('/');
        console.log('Login successful!', Token, UserID, Nume, Prenume);
      } else {
        console.error('Login failed:', response.data);
        setMessage("Autentificarea a esuat!");
      }
    } catch (error) {
      console.error('Login failed:', error.message);
      setMessage("Autentificarea a esuat!");
    }
    
  };

  const handleKeyDown = (e) => {
    if (e.key === 'Enter') {
      handleLogin();
    }
  };

  return (
    <div className="login-container">
      <div className="wrapper">
        <div className="title"><span>Autentificare</span></div>
        <form>
          <div className="row">
            <i className="fas fa-user"></i>
            <input type="text" placeholder="Username" value={username} onChange={(e) => setUsername(e.target.value)} onKeyDown={handleKeyDown} required />
          </div>
          <div className="row">
            <i className="fas fa-lock"></i>
            <input type="password" placeholder="Parola" value={password} onChange={(e) => setPassword(e.target.value)} onKeyDown={handleKeyDown} required />
          </div>
          <div className="row button">
            <button type="button" onClick={handleLogin}>
              Autentificare
            </button>
          </div>
          <div className="signup-link">Nu ai cont? <a href="/register">Inregistreaza-te aici</a></div>
        </form>

      </div>
      {message ? <Message message={message} /> : null}
    </div>
  );
};

export default Login;
