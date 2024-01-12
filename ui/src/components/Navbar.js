import React, { useEffect } from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from '../AuthContext';  // Import the AuthContext hook
import './Navbar.css';
import SearchForm from './SearchForm';

const Navbar = () => {
  const { isLoggedIn, isAdmin, userInfo } = useAuth();  // Use the authentication state and functions from the AuthContext

  useEffect(() => {
    // Fetch user information or use stored information
    if (isLoggedIn) {
      // Assuming you store the user ID in the AuthContext, you can check the admin status here
      // Example: checkAdminStatus(userId);
    }
  }, [isAdmin, isLoggedIn, userInfo]); // Add isLoggedIn as a dependency to the useEffect

  return (
    <nav className="navbar">
      <Link to="/"><h1>Proiect BD</h1></Link>
      <SearchForm />
      <ul>
        <Link to="/"><li>Acasa</li></Link>
        <Link to="/produse"><li>Produse</li></Link>
        {isLoggedIn ? <Link to="/forum"><li>Forum</li></Link> : null}
        {(isLoggedIn && isAdmin) ? <Link to="/admin"><li>Admin</li></Link> : null}
        {isLoggedIn ?<Link to="/cart"><li>Cos</li></Link> : null}
        {isLoggedIn ? <Link to="/account"><li>Cont</li></Link> : null}
        {isLoggedIn ? <Link to="/logout"><li>Logout</li></Link> : null}
        {isLoggedIn ? null : <Link to="/login"><li>Login</li></Link>}
        {isLoggedIn ? null : <Link to="/register"><li>Register</li></Link>}
      </ul>
    </nav>
  );
};

export default Navbar;
