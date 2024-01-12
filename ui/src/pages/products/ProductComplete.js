import React from 'react';

const ProductComplete = ({ product }) => {
  const { NumeProdus, PretProdus, ImagineProdus, DescriereProdus } = product;

  return (
    <div className="product-complete">
      <img src={ImagineProdus} alt={NumeProdus} />
      <h3>{NumeProdus}</h3>
      <p>Pret: {PretProdus} RON</p>
      <p>{DescriereProdus}</p>
    </div>
  );
};

export default ProductComplete;
