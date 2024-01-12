import React, { useEffect, useState, useCallback } from 'react';
import { Link } from 'react-router-dom';
import { useLocation } from 'react-router-dom';
import ProductCompact from './ProductCompact';

import {useAuth} from '../../AuthContext'

import '../../style/SearchResultPage.css'
import CartWidget from '../../components/CartWidget';

const SearchResultPage = () => {
    const location = useLocation();
    const searchParams = new URLSearchParams(location.search);
    const searchTerm = searchParams.get('term');

    const {isLoggedIn} = useAuth();

    const [cart, setCart] = useState(() => {
        const storedCart = localStorage.getItem('cart');
        return storedCart ? JSON.parse(storedCart) : [];
      });

    // Use searchTerm to fetch search results from the server
    const [products, setProducts] = useState([]);

    const API_URL = "http://localhost:5239/";

    useEffect(() => {
        const fetchProducts = async () => {
            try {
                const response = await fetch(`${API_URL}api/Produse/GetProduseByText/${searchTerm}`);
                const data = await response.json();
                setProducts(data);
            } catch (error) {
                console.error('Error fetching products:', error);
            }
        };

        fetchProducts();
    }, [searchTerm]);

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

    return (
        <div className='search-result-page'>
            <h1>Rezultatele căutării: {searchTerm}</h1>
            <ul>
                {products.map(product => (
                    <li>
                        <Link to={`/produse/produs/${product.ID_Produs}`}>
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

export default SearchResultPage;
