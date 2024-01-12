import React, { useEffect, useState } from "react";
import axios from 'axios';
import { useAuth } from "../../AuthContext";
import { useNavigate } from 'react-router-dom';

import CartItem from './CartItem';

import '../../style/CartPage.css'
import Message from "../../components/Message";

const CartPage = () => {

  const { isLoggedIn, userInfo } = useAuth();
  const navigate = useNavigate();

  const [cart, setCart] = useState(() => {
    const storedCart = localStorage.getItem('cart');
    return storedCart ? JSON.parse(storedCart) : [];
  });

  const [ message, setMessage ] = useState("");


  useEffect(() => {
    if (!isLoggedIn) {
      navigate('/');
    }
  }, [isLoggedIn, navigate]);

  const clearCart = () => {
    setCart([]);
    localStorage.removeItem('cart');
  };

  const [totalPrice, setTotalPrice] = useState(0);

  useEffect(() => {
    // Calculate total price when cart changes
    const newTotalPrice = cart.reduce((sum, cartItem) => {
      const productPrice = cartItem.Cantitate * cartItem.Pret;
      return sum + productPrice;
    }, 0);

    setTotalPrice(newTotalPrice);
  }, [cart]);

  const handleCheckout = async () => {
    try {
      // Step 1: Create a new Bon
      const bon = {
        ID_Utilizator: userInfo.UserID,
        DataFacturare: new Date().toISOString(),
        ID_Voucher: 1,
      };

      const formDataBon = new FormData();
      Object.entries(bon).forEach(([key, value]) => {
        formDataBon.append(key, value);
      });

      // Log FormData entries
      const formDataEntries = Array.from(formDataBon.entries());
      console.log('FormData Entries:', formDataEntries);

      const bonResponse = await axios.post('http://localhost:5239/api/Bonuri/AddBon', formDataBon);

      const { ID_Bon } = bonResponse.data;

      // Step 2: Create DetaliuBon for each item in the cart

      for (const cartItem of cart) {
        const detaliuBonData = {
          ID_Bon,
          ID_Produs: cartItem.ID_Produs,
          Cantitate: cartItem.Cantitate,
          Pret: cartItem.Pret,
        };

        const formDataDetaliuBon = new FormData();
        Object.entries(detaliuBonData).forEach(([key, value]) => {
          formDataDetaliuBon.append(key, value);
        });

        // Log FormData entries
        const formDataEntries = Array.from(formDataBon.entries());
        console.log('FormData Entries:', formDataEntries);

        await axios.post('http://localhost:5239/api/DetaliiBon/AddDetaliuBon', formDataDetaliuBon);
      }

      clearCart();

      console.log('Checkout successful');
      setMessage("Comanda trimisa cu succes!");
    } catch (error) {
      console.error('Error during checkout:', error);
      setMessage("Eroare!");
    }
  };



  return (
    <div className="cart-page">
      <h2>Coş de cumpărături</h2>

      {message ? <Message message={message} /> : null}

      {cart.map((cartItem, index) => (
        <CartItem key={cartItem.ID_Produs} cartItem={cartItem} />

      ))}

      <div className="total-price">
        <p>Preţ total: {totalPrice.toFixed(2)} Lei</p>
      </div>

      <button className="clear-cart-button" onClick={clearCart}>Şterge coşul</button>
      <button className="checkout-button" onClick={handleCheckout}>Trimite comanda</button>
    </div>
  );
};

export default CartPage;
