import React from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../AuthContext';  // Import the AuthContext hook

import '../../style/Users.css';

const Logout = () => {
  const navigate = useNavigate();
  const { logout } = useAuth();  // Use the logout function from the AuthContext

  const handleLogout = () => {
    // Use the logout function from the AuthContext to log out the user
    logout();

    // Redirect to the login page after logout
    navigate('/login');
  };

  return (
    <div className="logout-container">
      <h2>Logout</h2>
      <p>Sigur doreşti să te deconectezi?</p>
      <button type="button" onClick={handleLogout}>
        Logout
      </button>
    </div>
  );
};

export default Logout;
