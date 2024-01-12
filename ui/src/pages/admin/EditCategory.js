import { useParams, useNavigate } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { useAuth } from '../../AuthContext';

import axios from 'axios';

import '../../style/EditCategory.css'

const EditCategory = () => {
    const [loading, setLoading] = useState(true);
    const { isLoggedIn, isAdmin } = useAuth();
    const navigate = useNavigate();

    const { id } = useParams();
    const API_URL = "http://localhost:5239/";

    useEffect(() => {
        if ((!isLoggedIn) || (!isAdmin)) {
            navigate("/");
        }
    }, [isLoggedIn, isAdmin, navigate]);

    const [formCategorie, setFormCategorie] = useState({
        NumeCategorie: '',
        DescriereCategorie: '',
    });

    useEffect(() => {
        const fetchData = async () => {
            try {
                const categorieResponse = await axios.get(`${API_URL}api/CategoriiProduse/GetCategorieById/${id}`);
                setFormCategorie(categorieResponse.data[0]);

                setLoading(false);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };

        fetchData();
    }, [id, API_URL]);

    const handleUpdate = async () => {
        try {
            const formData = new FormData();
            Object.entries(formCategorie).forEach(([key, value]) => {
                formData.append(key, value);
            });

            const response = await axios.put(`${API_URL}api/CategoriiProduse/UpdateCategorie/${id}`, formData);

            if (response.status === 200) {
                console.log('Category updated successfully!');
                navigate(`/produse/categorie/${id}`);
            } else {
                console.error('Failed to update category:', response.status);
            }
        } catch (error) {
            console.error('Error updating category:', error);
        }
    };

    const handleDelete = async () => {
        try {
            const response = await axios.delete(`${API_URL}api/CategoriiProduse/DeleteCategorie/${id}`);

            if (response.status === 200) {
                console.log('Category deleted successfully!');
                navigate('/produse');
            } else {
                console.error('Failed to delete categoriy:', response.status);
            }
        } catch (error) {
            console.error('Error deleting categorie:', error);
        }
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormCategorie((prevProduct) => ({
            ...prevProduct,
            [name]: value,
        }));
    };

    if (loading) {
        return <div>Loading...</div>;
    }

    return (
        <div className="edit-category">
            <h2>Editeaza categoria</h2>
            <form>
                <label>Nume Categorie:</label>
                <input type="text" name="NumeCategorie" value={formCategorie.NumeCategorie} onChange={handleChange} />

                <label>Descriere Categorie:</label>

                <input type="text" name="DescriereCategorie" value={formCategorie.DescriereCategorie} onChange={handleChange} />

                <button className="submit-button" type="button" onClick={handleUpdate}>
                    Modifică
                </button>
                <button className="delete-button" type="button" onClick={handleDelete}>
                    Şterge categorie
                </button>
            </form>
        </div>
    )
}

export default EditCategory;