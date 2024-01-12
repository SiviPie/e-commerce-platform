import { useNavigate, Link } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { useAuth } from '../../AuthContext';

import axios from 'axios';

import '../../style/ManageCategories.css'

const ManageCategories = () => {
    const [loading, setLoading] = useState(true);
    const { isLoggedIn, isAdmin } = useAuth();
    const navigate = useNavigate();

    useEffect(() => {
        if ((!isLoggedIn) || (!isAdmin)) {
            navigate("/");
        }
    }, [isLoggedIn, isAdmin, navigate]);

    const [categorii, setCategorii] = useState([]);

    const API_URL = "http://localhost:5239/";

    useEffect(() => {
        const fetchCategorii = async () => {
            try {
                const categorieResponse = await axios.get(`${API_URL}api/CategoriiProduse/GetCategorii/`);
                setCategorii(categorieResponse.data);
                setLoading(false);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        }

        fetchCategorii();
    });

    if (loading) {
        return <div>Loading...</div>;
    }

    return (
        <div className="manage-categories">
            <h2>Gestioneaza categoriile</h2>
            <ul>
                {categorii.map((categorie, index) => (
                    <li key={index}>
                        <Link to={`/edit/categorie/${categorie.ID_Categorie}`} style={{ textDecoration: 'none' }}>
                            <div className='content'>
                                <p>{categorie.NumeCategorie}</p>
                            </div>
                        </Link>
                    </li>
                ))}

            </ul>


        </div>
    )
}

export default ManageCategories;