import React from 'react';
import { BrowserRouter, Route, NavLink } from 'react-router-dom';
import Home from './components/pages/Home';
import User from './components/pages/User';
import Login from './components/pages/Login';
import AuthService from './services/AuthService';
import MenuIcon from 'react-icons/lib/fa/align-justify';
import emitter from './global/emitter';
import './App.css';
import './style/menu.css';

class App extends React.Component {

	constructor(props) {
		super(props);
		this.nav = React.createRef();
		this.service = new AuthService();
		emitter.removeAllListeners('onLogin');
		emitter.addListener('onLogin', this.onLoginChanged);
		this.state = {
			isLoggedIn: this.service.loggedIn()
		}
	}

	render() {
		const isLoggedIn = this.state.isLoggedIn;
		return (
			<BrowserRouter>
				<div className="grid">
					<div ref={this.nav} className='navbar'>
						<a onClick={this.onClick} className="hamburger"><MenuIcon /></a>
						<NavLink to={'/'} exact activeClassName='active'>
							<span className='glyphicon glyphicon-home'></span> Home
						</NavLink>
						{isLoggedIn ?
							<NavLink to={'/user'} activeClassName='active'>
								<span className='glyphicon glyphicon-education'></span> User
							</NavLink> :
							null
						}
						{isLoggedIn ?
							<a onClick={(e) => this.onLoginChanged(e, true)} className="menu-logout">Logout</a> :
							<NavLink to={'/login'} activeClassName='active'>
								<span className='glyphicon glyphicon-th-list'></span> Login
							</NavLink>
						}
					</div>

					<Route exact path="/" component={Home} />
					<Route exact path="/user" component={User} />
					<Route exact path="/login" component={Login} />
				</div>
			</BrowserRouter>
		);
	}

	onClick = () => {
		const x = this.nav.current;
		if (x.className === "navbar") {
			x.className += " responsive";
		} else {
			x.className = "navbar";
		}
	}

	onLoginChanged = (e, isLogout) => {
		if (isLogout) {
			this.service.logout();
		}

		this.setState({
			isLoggedIn: this.service.loggedIn()
		})
	}
}

export default App;
