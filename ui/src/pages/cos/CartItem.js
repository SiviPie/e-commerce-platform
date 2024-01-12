import React, { useState, useEffect } from 'react';
import axios from 'axios';

import '../../style/CartPage.css';

const CartItem = ({ cartItem }) => {
  const [productDetails, setProductDetails] = useState([]);

  useEffect(() => {
    const fetchProductDetails = async () => {
      try {
        const response = await axios.get(`http://localhost:5239/api/Produse/GetProdusById/${cartItem.ID_Produs}`);
        setProductDetails(response.data);
      } catch (error) {
        console.error(`Error fetching product details for ID ${cartItem.ID_Produs}:`, error);
      }
    };

    fetchProductDetails();
  }, [cartItem.ID_Produs]);

  if (productDetails.length === 0) {
    // Data is still being fetched, you can render a loading state or return null
    return null;
  }

  // Use substring to show only the first 20 characters of NumeProdus
  const truncatedName = productDetails[0]?.NumeProdus?.length > 150
    ? productDetails[0]?.NumeProdus?.substring(0, 100) + '...'
    : productDetails[0]?.NumeProdus;

  return (
    <div key={cartItem.ID_Produs} className='cart-item'>
      <img src={productDetails[0]?.ImagineProdus ?? 'default-image-url'} alt={productDetails[0].NumeProdus} />
      <p>{truncatedName}</p>
      <p>Pret: {productDetails[0].PretProdus} Lei</p>
      <p>Cantitate: {cartItem.Cantitate}</p>
    </div>
  );
};

export default CartItem;
