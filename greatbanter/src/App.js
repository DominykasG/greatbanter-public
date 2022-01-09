import logo from './logo.svg';
import { Route, Switch } from 'react-router-dom'
import { BrowserRouter as Router, Redirect } from "react-router-dom";
import {useSelector} from 'react-redux';
import 'bootstrap/dist/css/bootstrap.min.css';
import Navbar from './features/Navbar';
import Random from './pages/random';
import Greatest from './pages/greatest';
import Add from './pages/add';
import Login from './pages/login';
import Register from './pages/register';
import bg from './images/bg.jpeg';

function App() {
  const loggedIn = useSelector(state => state.user.isLoggedIn)

  return (
      <Router> 
        <Navbar></Navbar>
        <Switch>
          <Route exact path="/">
            <Redirect to="/random"/>
          </Route>
          <Route exact path="/random" component={Random}></Route>
          <Route exact path="/greatest" component={Greatest}></Route>
          <Route exact path="/add" component={Add}></Route>
          <Route exact path="/login" component={Login}>
            {loggedIn ? <Redirect to="/random" component={Navbar}/> : <Route to="/login" component={Login}/>}
          </Route>
          <Route exact path="/register" component={Register}></Route>
        </Switch>
      </Router>  
  );
}

export default App;
