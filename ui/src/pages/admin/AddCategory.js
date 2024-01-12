import React, { useState } from 'react';

import Message from '../../components/Message.js'

import '../../style/AddCategory.css'

const AddCategory = () => {
  const [category, setCategory] = useState({
    NumeCategorie: '',
    DescriereCategorie: '',
  });

  const [message, setMessage] = useState('');

  const handleChange = (e) => {
    const { name, value } = e.target;
    setCategory((prevCategory) => ({
      ...prevCategory,
      [name]: value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const formData = new FormData();
      Object.entries(category).forEach(([key, value]) => {
        formData.append(key, value);
      });

      const response = await fetch('http://localhost:5239/api/CategoriiProduse/AddCategorii', {
        method: 'POST',
        body: formData,
      });

      if (response.ok) {
        console.log('Category added successfully');
        
        setMessage('Categorie adaugata cu succes!');

        setCategory({
          NumeCategorie: '',
          DescriereCategorie: ''})
      } else {
        const errorData = await response.json();
        console.error('Error adding category:', errorData);

        setMessage('Eroare!');
      }
    } catch (error) {
      console.error('Error:', error);
      setMessage('Eroare!');
    }
  };

  return (
    <div className='add-category'>
      <h1>Adaugă categorie</h1>
      <form onSubmit={handleSubmit}>
        <label>Nume Categorie:</label>
        <input type="text" name="NumeCategorie" value={category.NumeCategorie} onChange={handleChange} required />

        <label>Descriere Categorie:</label>
        <textarea name="DescriereCategorie" value={category.DescriereCategorie} onChange={handleChange} required />

        <button type="submit">Adauga Categorie</button>
      </form>
      {message ? <Message message={message} /> : null}
    </div>
  );
};

export default AddCategory;
