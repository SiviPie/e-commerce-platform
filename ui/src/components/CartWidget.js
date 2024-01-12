import { Link } from "react-router-dom"

import '../style/CartWidget.css';

const CartWidget = ({ number }) => {
    return (

        <div className="cart-widget">
            <Link to="/cart" style={{ textDecoration: 'none' }}>
                <div className="cart-widget-content">
                    <i className="fas fa-shopping-cart"></i>
                    <p>{number}</p>
                </div>
            </Link>
        </div >

    )
}

export default CartWidget;