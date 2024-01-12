// ProductCompact.js
import React from 'react';

import '../../style/ProductCompact.css'

const ProductCompact = ({ product}) => {
  const { NumeProdus, PretProdus, ImagineProdus } = product;

  const truncatedName = NumeProdus.length > 60
  ? NumeProdus.substring(0, 60) + '...'
  : NumeProdus;

  return (
    <div className="product-compact">
      <img src={ImagineProdus} alt={NumeProdus} />
      <h3>{truncatedName}</h3>
      <p>Pret: {PretProdus.toFixed(2)} RON</p>
    </div>
  );
};

export default ProductCompact;
