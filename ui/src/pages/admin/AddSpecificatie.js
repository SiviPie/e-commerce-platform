import React, { useState, useEffect } from 'react';
import axios from 'axios';

import '../../style/AddSpecification.css';

const AddSpecification = () => {
  const [specification, setSpecification] = useState({
    ValoareSpecificatie: '',
    ID_CategorieSpecificatii: 0,
  });

  const [categories, setCategories] = useState([]);
  const [newCategory, setNewCategory] = useState({
    NumeCategorie: '',
  });

  const fetchCategories = async () => {
    try {
      const response = await axios.get('http://localhost:5239/api/CategoriiSpecificatii/GetCategoriiSpecificatii');
      setCategories(response.data);
    } catch (error) {
      console.error('Error fetching categories:', error);
    }
  };

  useEffect(() => {
    fetchCategories();
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setSpecification((prevSpecification) => ({
      ...prevSpecification,
      [name]: value,
    }));
  };

  const handleNewCategoryChange = (e) => {
    const { name, value } = e.target;
    setNewCategory((prevNewCategory) => ({
      ...prevNewCategory,
      [name]: value,
    }));
  };

  const handleAddCategory = async () => {
    try {
      const newCategoryFormData = new FormData();
      Object.entries(newCategory).forEach(([key, value]) => {
        newCategoryFormData.append(key, value);
      });

      const response = await axios.post('http://localhost:5239/api/CategoriiSpecificatii/AddCategorieSpecificatii', newCategoryFormData);

      if (response.status === 200) {
        console.log('Category added successfully');
        // Fetch updated categories list
        fetchCategories();
        // Clear the new category form
        setNewCategory({
          NumeCategorie: '',
          DescriereCategorie: '',
        });
      } else {
        console.error('Error adding category:', response.data);
      }
    } catch (error) {
      console.error('Error:', error);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const formData = new FormData();
      Object.entries(specification).forEach(([key, value]) => {
        formData.append(key, value);
      });

      const response = await axios.post('http://localhost:5239/api/Specificatii/AddSpecificatie', formData);

      if (response.status === 200) {
        console.log('Specification added successfully');
        // Redirect or perform any other actions upon successful addition
        setSpecification({
          ValoareSpecificatie: '',
          ID_CategorieSpecificatii: 0,
        });
      } else {
        console.error('Error adding specification:', response.data);
        // Display error message to the user
      }
    } catch (error) {
      console.error('Error:', error);
    }
  };

  return (
    <div className="add-specification">
      <h2>Adaugă specificaţie</h2>
      <form onSubmit={handleSubmit}>
        <label>Valoarea specificaţiei:</label>
        <input type="text" name="ValoareSpecificatie" value={specification.ValoareSpecificatie} onChange={handleChange} required />

        <label>Categorie:</label>
        <select name="ID_CategorieSpecificatii" value={specification.ID_CategorieSpecificatii} onChange={handleChange}>
          <option value={0} disabled>
            Selectează o categorie
          </option>
          {categories.map((category) => (
            <option key={category.ID_CategorieSpecificatii} value={category.ID_CategorieSpecificatii}>
              {category.NumeCategorie}
            </option>
          ))}
        </select>

        <button type="submit">Adaugă specificaţie</button>
      </form>

      <h2>Adaugă categorie</h2>
      <form onSubmit={handleAddCategory}>
      <label>Numele Categoriei:</label>
      <input type="text" name="NumeCategorie" value={newCategory.NumeCategorie} onChange={handleNewCategoryChange} required />

      <button type="button" onClick={handleAddCategory}>
        Adaugă categorie
      </button>
      </form>
    </div>
  );
};

export default AddSpecification;
