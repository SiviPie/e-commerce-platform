import React from 'react';
import '../../style/Review.css';

const Review = ({ review }) => {
  const formattedDate = new Date(review.DataRecenzie).toLocaleDateString();
  const formattedTime = new Date(review.DataRecenzie).toLocaleTimeString();

  return (
    <div className="review">
      <div className="user">
        <img src={review.ImagineProfil} alt={review.Username} />
        <p>{review.Username}</p>
      </div>
      <div className="content">
        <p>{review.ContinutRecenzie}</p>
      </div>
      <div className="stats">
        <p>Stele: {review.Stele}</p>
        <p>{formattedDate} {formattedTime}</p>
      </div>
    </div>
  );
};

export default Review;
