import './App.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';

import Navbar from './components/Navbar.js';
import Home from './pages/Home.js';

import { AuthProvider } from './AuthContext';

// ADMIN
import AddCategory from './pages/admin/AddCategory.js';

// USERS
import Login from './pages/users/Login.js';

// PRODUSE
import Produse from './pages/products/Produse.js';
import CategoryDetails from './pages/products/CategoryDetails.js';
import ProductDetails from './pages/products/ProductDetails.js';

// FORUM
import Forum from './pages/forum/Forum.js';
import SubjectDetails from './pages/forum/SubjectDetails.js';
import PostDetails from './pages/forum/PostDetails.js';
import SearchResultPage from './pages/products/SearchResultPage.js';
import Register from './pages/users/Register.js';
import AdminPage from './pages/admin/AdminPage.js';
import Logout from './pages/users/Logout.js';
import Account from './pages/users/Account.js';
import SediiPage from './pages/sediu/SediiPage.js';
import AddProduct from './pages/admin/AddProduct.js';
import CartPage from './pages/cos/CartPage.js';
import DetaliiBon from './pages/users/DetaliiBon.js';
import EditProduct from './pages/admin/EditProduct.js';
import AddSpecification from './pages/admin/AddSpecificatie.js';
import ManageCategories from './pages/admin/ManageCategories.js';
import EditCategory from './pages/admin/EditCategory.js';
import Statistici from './pages/admin/Statistici.js';

const App = () => {

  return (
    <AuthProvider>
      <Router>
        <div className="App">
          <Navbar />
          <div className="Content">
            <Routes>
              <Route path='/' element={<Home />} />

              {/* USERS */}
              <Route path='/register' element={<Register />} />
              <Route path='/login' element={<Login />} />
              <Route path='/logout' element={<Logout />} />
              <Route path='/account' element={<Account />} />
              <Route path='/account/bon/:id' element={<DetaliiBon />} />

              {/* ADMIN */}
              <Route path='/admin' element={<AdminPage />} />
              <Route path='/add/categorie' element={<AddCategory />} />
              <Route path='/add/specificatie' element={<AddSpecification />} />
              <Route path='/add/produs' element={<AddProduct />} />
              <Route path='/edit/produs/:id' element={<EditProduct />} />
              <Route path='/edit/categorie/:id' element={<EditCategory />} />
              <Route path='/manage/categories' element={<ManageCategories />} />
              <Route path='/statistici' element={<Statistici />} />

              {/* SEDII */}
              <Route path='/sediu' element={<SediiPage />} />

              {/* PRODUSE */}
              <Route path='/produse' element={<Produse />} />
              <Route path='/produse/categorie/:id' element={<CategoryDetails />} />
              <Route path='/produse/produs/:id' element={<ProductDetails />} />
              <Route path='/search' element={<SearchResultPage />} />

              {/* COS */}
              <Route path='/cart' element={<CartPage />} />

              {/* FORUM */}
              <Route path='/forum' element={<Forum />} />
              <Route path='/forum/subject/:id' element={<SubjectDetails />} />
              <Route path='/forum/post/:id' element={<PostDetails />} />


            </Routes>

          </div>
        </div>
      </Router>
    </AuthProvider>
  )
}

export default App;
