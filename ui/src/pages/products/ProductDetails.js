import React, { useEffect, useState, useCallback } from 'react';
import { Link, useParams } from 'react-router-dom';
import '../../style/ProductDetails.css';

import axios from 'axios';

import Review from '../../components/products/Review';
import ProductComplete from './ProductComplete';

import { useAuth } from '../../AuthContext'
import CartWidget from '../../components/CartWidget';

const ProductDetails = () => {
  const { isLoggedIn, isAdmin, userInfo } = useAuth();

  const { id } = useParams();

  const API_URL = "http://localhost:5239/";

  const [products, setProducts] = useState([]);
  const [specificatii, setSpecificatii] = useState([]);
  const [reviews, setReviews] = useState([]);
  const [loading, setLoading] = useState(true);
  

  const [reviewFormData, setReviewFormData] = useState({
    ID_Utilizator: userInfo.UserID, // Set the actual ID_Utilizator
    ID_Produs: id, // Initialize with a default value
    ContinutRecenzie: '',
    Stele: 0,
    DataRecenzie: new Date().toISOString(),
  });

  const [cart, setCart] = useState(() => {
    const storedCart = localStorage.getItem('cart');
    return storedCart ? JSON.parse(storedCart) : [];
  });


  useEffect(() => {

    const fetchData = async () => {
      try {
        const productResponse = await fetch(`${API_URL}api/Produse/GetProdusById/${id}`);
        const productData = await productResponse.json();
        setProducts(productData);

        const specificatiiResponse = await fetch(`${API_URL}api/Specificatii/GetSpecificatiiProdus/${id}`);
        const specificatiiData = await specificatiiResponse.json();
        setSpecificatii(specificatiiData);

        const reviewsResponse = await fetch(`${API_URL}api/Recenzii/GetRecenziiProdus/${id}`);
        const reviewsData = await reviewsResponse.json();
        setReviews(reviewsData);

        setLoading(false);
      } catch (error) {
        console.error('Error fetching data:', error);
        setLoading(false);
      }
    };

    fetchData();
  }, [id, reviews]);

  const addToCart = useCallback((product) => {
    const existingItem = cart.find((item) => item.ID_Produs === product.ID_Produs);

    if (existingItem) {
      setCart((prevCart) => {
        const updatedCart = prevCart.map((item) =>
          item.ID_Produs === product.ID_Produs ? { ...item, Cantitate: item.Cantitate + 1 } : item
        );
        return updatedCart;
      });
    } else {
      setCart((prevCart) => [...prevCart, { ...product, Cantitate: 1 }]);
    }
  }, [cart]);

  useEffect(() => {
    localStorage.setItem('cart', JSON.stringify(cart));
  }, [cart]);


  const handleReviewChange = (e) => {
    const { name, value } = e.target;

    setReviewFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleReviewSubmit = async (e) => {
    e.preventDefault();

    try {
      const formData = new FormData();
      Object.entries(reviewFormData).forEach(([key, value]) => {
        formData.append(key, value);
      });

      // Use Axios to make the POST request
      const response = await axios.post(`${API_URL}api/Recenzii/AddRecenzie`, formData);

      if (response.status === 200) {
        console.log('Review added successfully');
        // You may want to fetch the updated reviews after adding a new one
        // Refetch the reviews and update the state
        const updatedReviewsResponse = await fetch(`${API_URL}api/Recenzii/GetRecenziiProdus/${id}`);
        const updatedReviewsData = await updatedReviewsResponse.json();
        setReviews(updatedReviewsData);

        // Clear the form data after successful submission
        setReviewFormData({
          ID_Utilizator: userInfo.UserID,
          ID_Produs: id,
          ContinutRecenzie: '',
          Stele: 0,
          DataRecenzie: new Date().toISOString(),
        });
      } else {
        console.error('Unexpected response status:', response.status);
      }
    } catch (error) {
      console.error('Error:', error);
    }
  };


  return (
    <div className='product-details'>
      <h1>Detaliile produsului</h1>

      {loading ? (
        <p>Loading...</p>
      ) : (
        <React.Fragment>
          <Link to={`/produse/produs/${id}`} style={{ textDecoration: 'none' }}>
            <ProductComplete product={products[0]} />
          </Link>
          <button className="add-cart-button" onClick={() => addToCart({ ID_Produs: products[0].ID_Produs, Cantitate: 1, Pret: products[0].PretProdus })}>
            Adaugă în coş
          </button>
          {isLoggedIn && isAdmin ?
            <button className="edit-button">
              <Link to={`/edit/produs/${id}`} style={{ textDecoration: 'none' }}>
                Modifică produs
              </Link>
            </button>
            : null}

          <div className="product-specificatii">
            <h2>Specificatii</h2>
            {specificatii.map((specificatie, index) => (
              <div className="specificatie" key={index}>
                <p>{specificatie.NumeCategorie}</p>
                <p>{specificatie.ValoareSpecificatie}</p>
              </div>
            ))}
          </div>

          <div className="product-reviews">
            <h2>Recenzii</h2>

            {isLoggedIn && (
              <form onSubmit={handleReviewSubmit}>
                <label>
                  Continut recenzie:
                </label>
                <textarea name="ContinutRecenzie" value={reviewFormData.ContinutRecenzie} onChange={handleReviewChange} />

                <label>
                  Stele:
                </label>
                <input type="number" name="Stele" value={reviewFormData.Stele} onChange={handleReviewChange} />

                <button className="review-button" type="submit">Adaugă Recenzie</button>
              </form>
            )}

            {reviews.map(review => (
              <div className="product-review" key={review.ID_Recenzie}>
                <Review review={review} />
              </div>
            ))}
          </div>

          { isLoggedIn ? <CartWidget number={ cart.reduce((sum, item) => sum + item.Cantitate, 0)} /> : null }
        </React.Fragment>
      )}
    </div>
  );
};

export default ProductDetails;
