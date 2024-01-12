import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import axios from 'axios';

import '../../style/EditProduct.css';

const EditProduct = () => {
  const [loading, setLoading] = useState(true);
  const [product, setProduct] = useState({
    NumeProdus: '',
    ID_Categorie: 0,
    CodProdus: '',
    ImagineProdus: '',
    DescriereProdus: '',
    PretProdus: 0,
    ID_Reducere: 1,
  });

  const [specificatii, setSpecificatii] = useState([]);
  const [selectedSpecificatie, setSelectedSpecificatie] = useState(0);

  const [categorii, setCategorii] = useState([]);

  const navigate = useNavigate();
  const { id } = useParams();
  const API_URL = "http://localhost:5239/";

  useEffect(() => {
    const fetchData = async () => {
      try {
        const productResponse = await axios.get(`${API_URL}api/Produse/GetProdusById/${id}`);
        setProduct(productResponse.data[0]);

        const specificatiiResponse = await axios.get(`${API_URL}api/Specificatii/GetSpecificatii`);
        setSpecificatii(specificatiiResponse.data);

        const categoriiResponse = await axios.get(`${API_URL}api/CategoriiProduse/GetCategorii`);
        setCategorii(categoriiResponse.data);

        setLoading(false);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    fetchData();
  }, [id, API_URL]);

  const handleUpdate = async () => {
    try {
      const formData = new FormData();
      Object.entries(product).forEach(([key, value]) => {
        formData.append(key, value);
      });

      const response = await axios.put(`${API_URL}api/Produse/UpdateProdus/${id}`, formData);

      if (response.status === 200) {
        console.log('Product updated successfully!');
        navigate(`/produse/produs/${id}`);
      } else {
        console.error('Failed to update product:', response.status);
      }
    } catch (error) {
      console.error('Error updating product:', error);
    }
  };

  const handleDelete = async () => {
    try {
      const response = await axios.delete(`${API_URL}api/Produse/DeleteProdus/${id}`);

      if (response.status === 200) {
        console.log('Product deleted successfully!');
        navigate('/produse');
      } else {
        console.error('Failed to delete product:', response.status);
      }
    } catch (error) {
      console.error('Error deleting product:', error);
    }
  };

  const handleSelectSpecificatii = (e) => {
    const selectedValue = e.target.value;
    setSelectedSpecificatie(selectedValue);
  };

  const handleAddProdusSpecificatie = async () => {
    try {
      const formData = new FormData();
      formData.append('ID_Produs', id);
      formData.append('ID_Specificatie', selectedSpecificatie);

      const response = await axios.post(`${API_URL}api/ProdusSpecificatii/AddProdusSpecificatie`, formData);

      if (response.status === 200) {
        navigate(`/produse/produs/${id}`);
        console.log('ProdusSpecificatii added successfully!');
      } else {
        console.error('Failed to add ProdusSpecificatii:', response.status);
      }
    } catch (error) {
      console.error('Error adding ProdusSpecificatii:', error);
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setProduct((prevProduct) => ({
      ...prevProduct,
      [name]: value,
    }));
  };

  if (loading) {
    return <div>Loading...</div>;
  }

  return (
    <div className="edit-product">
      <h2>Editează Produsul: {product.NumeProdus}</h2>
      <form>
        <label>Nume Produs:</label>
        <input type="text" name="NumeProdus" value={product.NumeProdus} onChange={handleChange} />

        <label>Categorie:</label>
        <select name="ID_Categorie" value={product.ID_Categorie} onChange={handleChange}>
          <option value={0} disabled>
            Selectează o categorie
          </option>
          {categorii.map((categorie) => (
            <option key={categorie.ID_Categorie} value={categorie.ID_Categorie}>
              {categorie.NumeCategorie}
            </option>
          ))}
        </select>

        <label>Cod Produs:</label>
        <input type="text" name="CodProdus" value={product.CodProdus} onChange={handleChange} />

        <label>URL Imagine Produs:</label>
        <input type="text" name="ImagineProdus" value={product.ImagineProdus} onChange={handleChange} />

        <label>Descriere Produs:</label>
        <input type="text" name="DescriereProdus" value={product.DescriereProdus} onChange={handleChange} />

        <label>Preţ:</label>
        <input type="number" step="0.01" name="PretProdus" value={product.PretProdus} onChange={handleChange} />



        <button className="submit-button" type="button" onClick={handleUpdate}>
          Modifică
        </button>
        <button className="delete-button" type="button" onClick={handleDelete}>
          Şterge produs
        </button>
      </form>

      <form onSubmit={handleAddProdusSpecificatie}>
        <label>Specificatii:</label>
        <select name="ID_Specificatii" value={selectedSpecificatie} onChange={handleSelectSpecificatii}>
          <option value={0} disabled>
            Selectează o specificatie
          </option>
          {specificatii.map((specificatie) => (
            <option key={specificatie.ID_Specificatie} value={specificatie.ID_Specificatie}>
              {specificatie.ValoareSpecificatie}
            </option>
          ))}
        </select>
        <button className="add-spec-button" type="button" onClick={handleAddProdusSpecificatie}>
          Adaugă specificaţie
        </button>
      </form>
    </div>
  );
};

export default EditProduct;
