import { Component } from 'react';
import { Link } from 'react-router-dom';

import SubjectCompact from './SubjectCompact.js'

import '../../style/Forum.css'

class Forum extends Component {

  constructor(props) {
    super(props);
    this.state = {
      subiecte: [],
    }
  }

  API_URL = "http://localhost:5239/";

  componentDidMount() {
    this.refreshSubiecte();
  }

  async refreshSubiecte() {
    fetch(this.API_URL + "api/Subiecte/GetSubiecte").then(response => response.json()).then(data => {
      this.setState({ subiecte: data });
    })
  }

  render() {
    const { subiecte } = this.state;
    return (
      <div className='forum'>
        <h2>Forum</h2>
        <ul>
          {subiecte.map((subiect, index) =>
            <li key={index}>
              <Link to={`/forum/subject/${subiect.ID_Subiect}`}>
                <SubjectCompact subject={subiect} />
              </Link>
            </li>
          )}
        </ul>
      </div>
    )
  }
}
export default Forum;