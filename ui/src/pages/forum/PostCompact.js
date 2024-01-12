import { useEffect, useState } from 'react';

import '../../style/PostCompact.css'

const PostCompact = ({ post }) => {

    const [author, setAuthor] = useState([]);
    const id = post.ID_Postare;

    const API_URL = "http://localhost:5239/";

    useEffect(() => {
        const fetchAuthor = async () => {
            try {
                const response = await fetch(`${API_URL}api/Postari/GetAuthor/${id}`);
                const data = await response.json();
                setAuthor(data);
            } catch (error) {
                console.error('Error fetching author:', error);
            }
        };


        fetchAuthor();
    }, [id]);

    const formatReadableDate = (isoDate) => {
        const options = { year: 'numeric', month: 'long', day: 'numeric', hour: 'numeric', minute: 'numeric', second: 'numeric' };
        return new Date(isoDate).toLocaleDateString('ro-RO', options);
      }

    return (
        <div className="post-compact">
            <div className="author">
                {author.map((auth, index) => (
                    <p key={index}>Postat de {auth.Username}</p>
                ))}
                <p>{formatReadableDate(post.DataPostarii)}</p>
            </div>
            <div className="title">
                <p>{post.TitluPostare}</p>
            </div>
            <div className="content">
                <p>{post.ContinutPostare}</p>
            </div>

        </div>
    )
}

export default PostCompact;