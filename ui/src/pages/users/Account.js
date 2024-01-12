import React from 'react';
import { useState, useEffect } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import '../../style/Users.css';

import { useAuth } from '../../AuthContext'

const Account = () => {
  const [user, setUser] = useState([]);
  const [bonuri, setBonuri] = useState([]);
  const navigate = useNavigate();

  const { isLoggedIn, userInfo } = useAuth();

  useEffect(() => {
    if (isLoggedIn) {
      fetchUserData(userInfo.UserID);
      fetchBonuriData(userInfo.UserID);
    } else {
      navigate('/');
    }
  }, [isLoggedIn, userInfo.UserID, navigate]);

  const fetchUserData = async (id) => {
    try {
      const response = await fetch(`http://localhost:5239/api/Utilizatori/GetUtilizatorById/${id}`);
      const userData = await response.json();
      setUser(userData);
    } catch (error) {
      console.error('Error fetching user data:', error);
      // Handle errors (e.g., redirect to an error page)
    }
  };

  const fetchBonuriData = async (id) => {
    try {
      const response = await fetch(`http://localhost:5239/api/Bonuri/GetBonuriByUtilizator/${id}`);
      const bonuriData = await response.json();
      setBonuri(bonuriData);
    } catch (error) {
      console.error('Error fetching bonuri data:', error);
      // Handle errors (e.g., redirect to an error page)
    }
  };

  const formatReadableDate = (isoDate) => {
    const options = { year: 'numeric', month: 'long', day: 'numeric', hour: 'numeric', minute: 'numeric', second: 'numeric' };
    return new Date(isoDate).toLocaleDateString('ro-RO', options);
  }

  return (
    <div className="account">
      {user.map(u => (
        <div key={u.ID_Utilizator} className="account-info">
          <img src={u.ImagineProfil} alt={`${u.Username}`} />
          <div className='data'>
            <p><span>Nume:</span> {u.Nume} {u.Prenume}</p>
            <p><span>Username:</span> {u.Username}</p>
            <p><span>Telefon:</span> {u.Telefon}</p>
            <p><span>Email:</span> {u.Email}</p>
          </div>
        </div>
      ))}
      <h2>Comenzile mele</h2>
      {bonuri.map((bon, index) => (
        <Link key={index} to={`/account/bon/${bon.ID_Bon}`} style={{ textDecoration: 'none' }}>
          <div className="bonuri">
            <p>{formatReadableDate(bon.DataFacturare)}</p>
          </div>
        </Link>
      ))}
    </div>
  );
};

export default Account;
