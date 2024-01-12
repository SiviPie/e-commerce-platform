import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import '../../style/Sediu.css';

import { useAuth } from '../../AuthContext';

const SediiPage = () => {
  const navigate = useNavigate();
  const {isLoggedIn, isAdmin} = useAuth();
  const [sedii, setSedii] = useState([]);

  useEffect(() => {
    if (!isLoggedIn || isAdmin) {
      navigate("/");
    } else {
      fetchSediiData();
    }
  }, [isLoggedIn, isAdmin, navigate]);


  const fetchSediiData = async () => {
    try {
      const sediiResponse = await fetch('http://localhost:5239/api/Sedii/GetSedii');
      const sediiData = await sediiResponse.json();
      setSedii(sediiData);
    } catch (error) {
      console.error('Error fetching sedii data:', error);
      // Handle errors (e.g., redirect to an error page)
    }
  };

  return (
    <div className="sedii-page">
      <h2>Sedii</h2>
      {/* Render sedii data here */}
      <ul>
        {sedii.map((sediu) => (
          <li key={sediu.ID_Sediu}>{sediu.NumeSediu}</li>
        ))}
      </ul>
    </div>
  );
};

export default SediiPage;
