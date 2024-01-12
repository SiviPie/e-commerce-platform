import React, { useEffect, useState } from 'react';
import { Link, useParams } from 'react-router-dom';
import PostCompact from './PostCompact.js';
import axios from 'axios';

import '../../style/SubjectDetails.css';

import { useAuth } from '../../AuthContext';

const SubjectDetails = () => {
  const [posts, setPosts] = useState([]);
  const [subject, setSubject] = useState([]);
  const { id } = useParams();
  const { isLoggedIn, userInfo } = useAuth();

  const API_URL = "http://localhost:5239/";

  useEffect(() => {
    const fetchPosts = async () => {
      try {
        const response = await fetch(`${API_URL}api/Postari/GetPostariBySubiect/${id}`);
        const data = await response.json();
        setPosts(data);
      } catch (error) {
        console.error('Error fetching posts:', error);
      }
    };

    const fetchSubject = async () => {
      try {
        const response = await fetch(`${API_URL}api/Subiecte/GetSubiectById/${id}`);
        const data = await response.json();
        setSubject(data);
      } catch (error) {
        console.error('Error fetching subject:', error);
      }
    };

    fetchPosts();
    fetchSubject();
  }, [id]);

  const [postFormData, setPostFormData] = useState({
    ID_Subiect: id,
    ID_Utilizator: userInfo.UserID,
    TitluPostare: '',
    ContinutPostare: '',
    DataPostarii: new Date().toISOString(),
  });

  const handlePostChange = (e) => {
    const { name, value } = e.target;

    setPostFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handlePostSubmit = async (e) => {
    e.preventDefault();

    try {
      const formData = new FormData();
      Object.entries(postFormData).forEach(([key, value]) => {
        formData.append(key, value);
      });

      // Use Axios to make the POST request
      const response = await axios.post(`${API_URL}api/Postari/AddPostare`, formData);

      console.log('Stringified FormData:', JSON.stringify(formData));

      if (response.status === 200) {
        console.log('Post added successfully');
        // Refetch the posts and update the state
        const updatedPostsResponse = await fetch(`${API_URL}api/Postari/GetPostariBySubiect/${id}`);
        const updatedPostsData = await updatedPostsResponse.json();
        setPosts(updatedPostsData);

        // Clear the form data after successful submission
        setPostFormData({
          ID_Subiect: id,
          ID_Utilizator: userInfo.UserID,
          TitluPostare: '',
          ContinutPostare: '',
          DataPostarii: new Date().toISOString(),
        });
      } else {
        console.error('Unexpected response status:', response.status);
      }
    } catch (error) {
      console.error('Error:', error);
    }
  };

  return (
    <div className='subject-details'>
      {subject.map((sub, index) => (
        <h2 key={index}>{sub.NumeSubiect}</h2>
      ))}

      {isLoggedIn && (
        <form onSubmit={handlePostSubmit}>
          <label>
            Titlu postare:
          </label>
          <input type="text" name="TitluPostare" value={postFormData.TitluPostare} onChange={handlePostChange} />

          <label>
            Continut postare:
          </label>
          <textarea name="ContinutPostare" value={postFormData.ContinutPostare} onChange={handlePostChange} />

          <button type="submit">Adaugă Postare</button>
        </form>
      )}

      <ul className="post-list">
        {posts.map((post, index) => (
          <li key={index}>
            <Link to={`/forum/post/${post.ID_Postare}`} style={{ textDecoration: 'none' }}>
              <PostCompact post={post} />
            </Link>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default SubjectDetails;
