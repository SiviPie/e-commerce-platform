import React, { useEffect, useState, useCallback } from 'react';
import { Link, useParams } from 'react-router-dom';
import '../../style/CategoryDetails.css';
import ProductCompact from './ProductCompact';
import CartWidget from '../../components/CartWidget';

import {useAuth} from '../../AuthContext'

const CategoryDetails = () => {
  const [products, setProducts] = useState([]);
  const [category, setCategory] = useState([]);

  const {isLoggedIn} = useAuth();

  const [cart, setCart] = useState(() => {
    const storedCart = localStorage.getItem('cart');
    return storedCart ? JSON.parse(storedCart) : [];
  });
  const { id } = useParams();

  const API_URL = "http://localhost:5239/";

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const response = await fetch(`${API_URL}api/Produse/GetProduseByCategorie/${id}`);
        const data = await response.json();
        setProducts(data);
      } catch (error) {
        console.error('Error fetching products:', error);
      }
    };

    const fetchCategory = async () => {
      try {
        const response = await fetch(`${API_URL}api/CategoriiProduse/GetCategorieById/${id}`);
        const data = await response.json();
        setCategory(data);
      } catch (error) {
        console.error('Error fetching category:', error);
      }
    };

    fetchProducts();
    fetchCategory();
  }, [id]);

  const addToCart = useCallback((product) => {
    // Check if the product is already in the cart
    const existingItem = cart.find((item) => item.ID_Produs === product.ID_Produs);

    if (existingItem) {
      // If the product is already in the cart, update the quantity
      setCart((prevCart) => {
        const updatedCart = prevCart.map((item) =>
          item.ID_Produs === product.ID_Produs ? { ...item, Cantitate: item.Cantitate + 1 } : item
        );
        return updatedCart;
      });
    } else {
      // If the product is not in the cart, add a new item
      setCart((prevCart) => [...prevCart, { ...product, Cantitate: 1 }]);
    }
  }, [cart]);

  useEffect(() => {
    localStorage.setItem('cart', JSON.stringify(cart));
  }, [cart]);

  if (products.length === 0 || category.length === 0) {
    // Data is still being fetched, you can render a loading spinner or message
    return <div>Loading...</div>;
  }

  return (
    <div className='category-details'>
      <h2>{category[0].NumeCategorie}</h2>
      <ul>
        {products.map((product, index) => (
          <li key={index}>
            <Link to={`/produse/produs/${product.ID_Produs}`} style={{ textDecoration: 'none' }}>
              <ProductCompact product={product} />
            </Link>
            <button onClick={() => addToCart({ ID_Produs: product.ID_Produs, Cantitate: 1, Pret: product.PretProdus })}>
              Adaugă în coş
            </button>
          </li>
        ))}
      </ul>

      { isLoggedIn ? <CartWidget number={ cart.reduce((sum, item) => sum + item.Cantitate, 0)} /> : null }

    </div>
  );
};

export default CategoryDetails;
