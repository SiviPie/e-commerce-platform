import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';

import '../../style/PostDetails.css';

import { useAuth } from '../../AuthContext';

const PostDetails = () => {
    const [posts, setProducts] = useState([]);
    const [replies, setReplies] = useState([]);
    const { id } = useParams(); // Extracts the id from the URL params
    const { isLoggedIn, userInfo } = useAuth();

    const API_URL = "http://localhost:5239/";

    useEffect(() => {
        const fetchProducts = async () => {
            try {
                const response = await fetch(`${API_URL}api/Postari/GetPostareById/${id}`);
                const data = await response.json();
                setProducts(data);
            } catch (error) {
                console.error('Error fetching products:', error);
            }
        };

        const fetchReplies = async () => {
            try {
                const response = await fetch(`${API_URL}api/RaspunsuriPostari/GetRaspunsuriPostare/${id}`);
                const data = await response.json();
                setReplies(data);
            } catch (error) {
                console.error('Error fetching raspunsuri products:', error);
            }
        };

        fetchProducts();
        fetchReplies();

    }, [id]);

    const [replyFormData, setReplyFormData] = useState({
        ID_Utilizator: userInfo.UserID, // Set the actual ID_Utilizator
        ID_Postare: id, // Initialize with a default value
        ContinutRaspuns: '',
        DataRaspuns: new Date().toISOString(),
    });

    const handleReplyChange = (e) => {
        const { name, value } = e.target;

        setReplyFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    };

    const handleReplySubmit = async (e) => {
        e.preventDefault();

        try {
            const formData = new FormData();
            Object.entries(replyFormData).forEach(([key, value]) => {
                formData.append(key, value);
            });

            // Use Axios to make the POST request
            const response = await axios.post(`${API_URL}api/RaspunsuriPostari/AddRaspunsPostare`, formData);

            if (response.status === 200) {
                console.log('Review added successfully');
                // You may want to fetch the updated reviews after adding a new one
                // Refetch the reviews and update the state
                const updatedRepliesResponse = await fetch(`${API_URL}api/RaspunsuriPostari/GetRaspunsuriPostare/${id}`);
                const updatedRepliesData = await updatedRepliesResponse.json();
                setReplies(updatedRepliesData);

                // Clear the form data after successful submission
                setReplyFormData({
                    ID_Utilizator: userInfo.UserID, // Set the actual ID_Utilizator
                    ID_Postare: id, // Initialize with a default value
                    ContinutRaspuns: '',
                    DataRaspuns: new Date().toISOString(),
                });
            } else {
                console.error('Unexpected response status:', response.status);
            }
        } catch (error) {
            console.error('Error:', error);
        }
    };

    const formatReadableDate = (isoDate) => {
        const options = { year: 'numeric', month: 'long', day: 'numeric', hour: 'numeric', minute: 'numeric', second: 'numeric' };
        return new Date(isoDate).toLocaleDateString('ro-RO', options);
    }


    return (
        <div className='post-details'>
            <h2>Postare</h2>

            {posts.map((post, index) => (
                <div key={index} className='post'>
                    <div className='author'>
                        <img src={post.ImagineProfil} alt={post.Username} />
                        <p>{post.Username}</p>
                        <p>{formatReadableDate(post.DataPostarii)}</p>
                    </div>
                    <div className='title'>
                        <p>{post.TitluPostare}</p>
                    </div>
                    <div className='content'>
                        <p>{post.ContinutPostare}</p>
                    </div>
                </div>
            ))}

            <h2>Raspunsuri</h2>

            {isLoggedIn && (
                <form onSubmit={handleReplySubmit}>
                    <label>
                        Continut raspuns:
                    </label>
                    <textarea name="ContinutRaspuns" value={replyFormData.ContinutRaspuns} onChange={handleReplyChange} />

                    <button className="review-button" type="submit">Adaugă Raspuns</button>
                </form>
            )}

            {replies.map((reply, index) => (
                <div key={index} className='reply'>
                    <div className='author'>
                        <img src={reply.ImagineProfil} alt={reply.Username} />
                        <p>{formatReadableDate(reply.DataRaspuns)}</p>
                    </div>
                    <div className='content'>
                        <p>{reply.ContinutRaspuns}</p>
                    </div>


                </div>
            ))}
        </div>
    );
};

export default PostDetails;
