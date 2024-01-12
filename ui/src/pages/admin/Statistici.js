import { useEffect, useState } from 'react';
import { useAuth } from '../../AuthContext';
import { useNavigate } from 'react-router-dom';

import axios from 'axios';

import '../../style/Statistici.css'

const Statistici = () => {
    const [loading, setLoading] = useState(true);
    const { isLoggedIn, isAdmin } = useAuth();
    const navigate = useNavigate();

    useEffect(() => {
        if ((!isLoggedIn) || (!isAdmin)) {
            navigate("/");
        }
    }, [isLoggedIn, isAdmin, navigate]);

    const API_URL = "http://localhost:5239/";

    const [topReviewers, setTopReviewers] = useState([]);
    const [topPosters, setTopPosters] = useState([]);

    const [topDistinctSpecProduct, setTopDistinctSpecProduct] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const topReviewersResponse = await axios.get(`${API_URL}api/Utilizatori/GetTopReviewers`);
                setTopReviewers(topReviewersResponse.data);

                const topPostersResponse = await axios.get(`${API_URL}api/Utilizatori/GetTopPosters`);
                setTopPosters(topPostersResponse.data);

                const topDistinctSpecProductResponse = await axios.get(`${API_URL}api/Produse/GetTopDistinctSpecProduct`);
                setTopDistinctSpecProduct(topDistinctSpecProductResponse.data[0]);

                setLoading(false);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchData();
    }, [API_URL]);

    if (loading) {
        return <div>Loading...</div>;
    }

    return (
        <div className="statistici">
            <h2>Statistici</h2>

            <h3>Top utilizatori dupa numarul de recenzii:</h3>
            {topReviewers.map((reviewer, index) => (
                <div key={index} className='result'>
                    <p><span>Utilizator</span> {reviewer.Nume} {reviewer.Prenume}</p>
                    <p><span>Recenzii:</span> {reviewer.NumarRecenzii}</p>
                    <p><span>Medie stele:</span> {reviewer.MediaStele}</p>
                </div>
            ))}

            <h3>Utilizatori care au postat cel puțin o postare pe forum și numărul total de postări ale fiecărui utilizator:</h3>
            {topPosters.map((poster, index) => (
                <div key={index} className='result'>
                <p><span>Utilizator:</span> {poster.Nume} {poster.Prenume}</p>
                <p><span>Postari:</span> {poster.NumarTotalPostari}</p>
            </div>
            ))}

            <h3>Produsul care au cel mai mare număr de specificații distincte:</h3>
                <div className='result'>
                    <p><span>Produs: </span>{topDistinctSpecProduct.NumeProdus}</p>
                    <p><span>Specificatii distincte: </span>{topDistinctSpecProduct.NumarSpecDistincte}</p>
                </div>
        </div>
    )
}

export default Statistici;