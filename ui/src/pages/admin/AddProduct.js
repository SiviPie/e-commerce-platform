import React, { useState, useEffect } from 'react';
import axios from 'axios';

import '../../style/AddProduct.css';
import Message from '../../components/Message';

const AddProduct = () => {
  const [product, setProduct] = useState({
    NumeProdus: '',
    ID_Categorie: 0, // Initialize with a default value
    CodProdus: '',
    ImagineProdus: '',
    DescriereProdus: '',
    PretProdus: 0,
    ID_Reducere: 1,
  });

  const [categories, setCategories] = useState([]);

  useEffect(() => {
    const fetchCategories = async () => {
      try {
        const response = await axios.get('http://localhost:5239/api/CategoriiProduse/GetCategorii');
        setCategories(response.data);
      } catch (error) {
        console.error('Error fetching categories:', error);
      }
    };

    fetchCategories();
  }, []);

  const [message, setMessage] = useState("");

  const handleChange = (e) => {
    const { name, value } = e.target;

    setProduct((prevProduct) => ({
      ...prevProduct,
      [name]: value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const formData = new FormData();
      Object.entries(product).forEach(([key, value]) => {
        formData.append(key, value);
      });

      // Log FormData entries
      const formDataEntries = Array.from(formData.entries());
      console.log('FormData Entries:', formDataEntries);

      const response = await axios.post('http://localhost:5239/api/Produse/AddProdus', formData);
      console.log('Response:', response);

      if (response.status === 200) {
        setMessage("Produs adaugat cu succes!");
        console.log('Product added successfully');
        // Redirect or perform any other actions upon successful addition
        setProduct({
          NumeProdus: '',
          ID_Categorie: 0, // Initialize with a default value
          CodProdus: '',
          ImagineProdus: '',
          DescriereProdus: '',
          PretProdus: 0,
          ID_Reducere: 1,
        });
      } else {
        console.error('Unexpected response status:', response.status);
        setMessage("Eroare!");
      }
    } catch (error) {
      console.error('Error:', error);
      setMessage("Eroare!");
    }
  };

  return (
    <div className='add-product'>
      <h1>Add Product</h1>
      <form onSubmit={handleSubmit}>
        <label>Denumire:</label>
        <input type='text' name='NumeProdus' value={product.NumeProdus} onChange={handleChange} />

        <label>Categorie:</label>
        <select name='ID_Categorie' value={product.ID_Categorie} onChange={handleChange}>
          <option value={0} disabled>
            Selectează o categorie
          </option>
          {categories.map((category) => (
            <option key={category.ID_Categorie} value={category.ID_Categorie}>
              {category.NumeCategorie}
            </option>
          ))}
        </select>

        <label>Cod:</label>
        <input type='text' name='CodProdus' value={product.CodProdus} onChange={handleChange} />

        <label>URL Imagine:</label>
        <input type='text' name='ImagineProdus' value={product.ImagineProdus} onChange={handleChange} />

        <label>Descriere:</label>
        <input type='text' name='DescriereProdus' value={product.DescriereProdus} onChange={handleChange} />

        <label>Pret:</label>
        <input type='number' step='0.01' name='PretProdus' value={product.PretProdus} onChange={handleChange} />

        <button type='submit'>Adaugă Produs</button>
      </form>
        {message ? <Message message={message}/> : null }
    </div>
  );
};

export default AddProduct;
