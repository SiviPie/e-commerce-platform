import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import '../../style/Users.css';
import Message from '../../components/Message';

const Register = () => {
  const [user, setUser] = useState({
    Nume: '',
    Prenume: '',
    Username: '',
    Email: '',
    Telefon: '',
    Parola: '',
    ImagineProfil: '',
    Adresa: '',
    Puncte: 0,
    DataCreate: new Date().toISOString(),
    UltimaAutentificare: new Date().toISOString(),
  });

  const [message, setMessage] = useState('');

  const navigate = useNavigate();

  const handleChange = (e) => {
    const { name, value } = e.target;
    setUser((prevUser) => ({
      ...prevUser,
      [name]: value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const formData = new FormData();

      Object.entries(user).forEach(([key, value]) => {
        formData.append(key, value);
      });

      const response = await axios.post('http://localhost:5239/api/Utilizatori/AddUtilizator', formData);

      if (response.status === 200) {
        console.log('User added successfully');
        // Redirect or perform any other actions upon successful addition
      } else {
        console.error('Unexpected response status:', response.status);
        setMessage("Te rugam sa incerci din nou!");
      }
    } catch (error) {
      if (error.response && error.response.status === 400) {
        // Server returned validation errors
        console.error('Validation errors:', error.response.data);
      } else {
        console.error('Error:', error);
        setMessage("Te rugam sa incerci din nou!");
      }
    }
  }

  useEffect(() => {
    const storedToken = localStorage.getItem('token');
    const isLoggedIn = !!storedToken;

    if (isLoggedIn) {
      navigate('/');
    }
  }, [navigate]);

  return (
    <div className="register-container">
      <div className="wrapper">
        <div className="title"><span>Înregistrare</span></div>
        <form onSubmit={handleSubmit}>
          <div className="row">
            <i className="fas fa-user"></i>
            <input type="text" placeholder="Nume" name="Nume" value={user.Nume} onChange={handleChange} required />
          </div>
          <div className="row">
            <i className="fas fa-user"></i>
            <input type="text" placeholder="Prenume" name="Prenume" value={user.Prenume} onChange={handleChange} required />
          </div>
          <div className="row">
            <i className="fas fa-user"></i>
            <input type="text" placeholder="Username" name="Username" value={user.Username} onChange={handleChange} required />
          </div>
          <div className="row">
            <i className="fas fa-envelope"></i>
            <input type="text" placeholder="Email" name="Email" value={user.Email} onChange={handleChange} required />
          </div>
          <div className="row">
            <i className="fas fa-phone"></i>
            <input type="text" placeholder="Telefon" name="Telefon" value={user.Telefon} onChange={handleChange} required />
          </div>
          <div className="row">
            <i className="fas fa-lock"></i>
            <input type="password" placeholder="Parola" name="Parola" value={user.Parola} onChange={handleChange} required />
          </div>
          <div className="row">
            <i className="fas fa-user"></i>
            <input type="text" placeholder="URL Imagine Profil" name="ImagineProfil" value={user.ImagineProfil} onChange={handleChange} required />
          </div>
          <div className="row">
            <i className="fas fa-map-marker-alt"></i>
            <input type="text" placeholder="Adresa" name="Adresa" value={user.Adresa} onChange={handleChange} required />
          </div>
          <div className="row button">
            <button type="submit">
              Înregistrare
            </button>
          </div>
          <div className="signup-link">Ai deja cont? <a href="/login">Autentifică-te aici</a></div>
        </form>
      </div>
      {message ? <Message message={message} /> : null}
    </div>
  );
};

export default Register;
