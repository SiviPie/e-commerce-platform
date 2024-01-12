import { useAuth } from '../AuthContext'

import '../style/Home.css'

const Home = () => {
  const{ isLoggedIn, userInfo } = useAuth();

  return (
    <div className='home'>
      {isLoggedIn ? (
          <h2>Bine ai venit, <span>{userInfo ? `${userInfo.Nume} ${userInfo.Prenume}` : 'User'}</span>!</h2>
      ) : (
        <h2>Bine ai venit!</h2>
      )}
      <h2>Foloseste bara de navigare pentru a parcurge site-ul.</h2>
    </div>
  );
};

export default Home;
