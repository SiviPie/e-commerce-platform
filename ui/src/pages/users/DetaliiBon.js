import React, { useEffect, useState } from "react";
import { useParams } from 'react-router-dom';

import '../../style/DetaliiBon.css';

const DetaliiBon = () => {
    const [detaliiBon, setDetaliiBon] = useState([]);
    const [products, setProducts] = useState([]);
    const { id } = useParams();

    const API_URL = "http://localhost:5239/";

    useEffect(() => {
        const fetchDetaliiBon = async () => {
            try {
                const response = await fetch(`${API_URL}api/DetaliiBon/GetDetaliiBonByBon/${id}`);
                const data = await response.json();
                setDetaliiBon(data);
            } catch (error) {
                console.error('Error fetching detalii bon:', error);
            }
        };

        const fetchProducts = async () => {
            try {
                const response = await fetch(`${API_URL}api/Produse/GetProduseByBon/${id}`);
                const data = await response.json();
                setProducts(data);
            } catch (error) {
                console.error('Error fetching products:', error);
            }
        };

        fetchDetaliiBon();
        fetchProducts();
    }, [id]);

    const [totalPrice, setTotalPrice] = useState(0);

    useEffect(() => {
        // Calculate total price when cart changes
        const newTotalPrice = detaliiBon.reduce((sum, cartItem) => {
            const productPrice = cartItem.Cantitate * cartItem.Pret;
            return sum + productPrice;
        }, 0);

        setTotalPrice(newTotalPrice);
    }, [detaliiBon]);

    return (
        <div className="detalii-bon">
            <h2>Detaliile bonului</h2>
            {detaliiBon.length > 0 && products.length > 0 && detaliiBon.map((detaliu, index) => (
                <div className="produs" key={index}>
                    <img src={products[index].ImagineProdus} alt={products[index].NumeProdus} />
                    <p>{products[index].NumeProdus}</p>
                    <p>Cantitate: {detaliu.Cantitate}</p>
                    <p>Pret: {detaliu.Pret}</p>

                </div>
            ))}
            <div className="total-price">
                <p><span>Preţ total:</span> {totalPrice.toFixed(2)} Lei</p>
            </div>
        </div>
    )
}

export default DetaliiBon;
