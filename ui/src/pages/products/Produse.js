import { Component } from 'react';
import { Link } from 'react-router-dom';

import '../../style/Products.css'

class Produse extends Component {

  constructor(props) {
    super(props);
    this.state = {
      categorii: [],
    }
  }

  API_URL = "http://localhost:5239/";

  componentDidMount() {
    this.refreshCategorii();
  }


  async refreshCategorii() {
    fetch(this.API_URL + "api/CategoriiProduse/GetCategorii").then(response => response.json()).then(data => {
      this.setState({ categorii: data });
    })
  }

  render() {
    const { categorii } = this.state;
    return (
      <div className='categories'>

        <h2>Categorii</h2>
        <ul className="categorii-list">

          {categorii.map(categorie =>
            <Link to={`/produse/categorie/${categorie.ID_Categorie}`} style={{ textDecoration: 'none' }} key={categorie.ID_Categorie}>
              <li>
                {categorie.NumeCategorie}
              </li>
            </Link>
          )}
        </ul>
      </div>
    )
  }
}
export default Produse;