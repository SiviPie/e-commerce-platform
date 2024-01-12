import React, { useEffect } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { useAuth } from '../../AuthContext';
import '../../style/Admin.css';

const AdminPage = () => {
  const { isLoggedIn, isAdmin } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    if ((!isLoggedIn) || (!isAdmin)) {
      navigate("/");
    }
  }, [isLoggedIn, isAdmin, navigate]);

  return (
    <div className="admin-page">
      <h1>Admin</h1>

      <h2>Produse</h2>
      <Link to="/add/produs" style={{ textDecoration: 'none' }}>
        <p>Adauga produs</p>
      </Link>

      <h2>Categorii</h2>
      <Link to="/add/categorie" style={{ textDecoration: 'none' }}>
        <p>Adauga categorie</p>
      </Link>

      <Link to="/manage/categories" style={{ textDecoration: 'none' }}>
        <p>Gestioneaza categoriile existente</p>
      </Link>

      <h2>Specificatii</h2>
      <Link to="/add/specificatie" style={{ textDecoration: 'none' }}>
        <p>Adauga specificatie</p>
      </Link>


      <h2>Statistici</h2>
      <Link to="/statistici" style={{ textDecoration: 'none' }}>
        <p>Statistici</p>
      </Link>
    </div>
  );
};

export default AdminPage;
