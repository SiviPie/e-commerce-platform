import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

import '../style/SearchBar.css'

const SearchForm = () => {
  const [searchTerm, setSearchTerm] = useState('');
  const navigate = useNavigate();

  const handleSearch = () => {
    // Check if the search term is not empty
    if (searchTerm.trim() !== '') {
      // Redirect to the search results page with the search term
      navigate(`/search?term=${encodeURIComponent(searchTerm)}`);
    } else {
      // Optionally show an error message or perform other actions
      alert('Te rugăm să introduci un termen valid.');
    }
  };

  const handleKeyDown = (e) => {
    // Check if the pressed key is Enter (key code 13)
    if (e.key === 'Enter') {
      handleSearch();
    }
  };

  return (
    <div className="search-bar">
      <input
        type="text"
        value={searchTerm}
        onChange={(e) => setSearchTerm(e.target.value)}
        onKeyDown={handleKeyDown}
      />
      <button onClick={handleSearch}>
        <i className="fas fa-search"></i>
      </button>
    </div>
  );
};

export default SearchForm;
