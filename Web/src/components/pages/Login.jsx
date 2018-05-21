import React from 'react';
import AuthService from '../../services/AuthService';
import Mail from 'react-icons/lib/ti/mail';
import Key from 'react-icons/lib/ti/key';
import Spinner from 'react-spinkit';
import emitter from '../../global/emitter';
import '../../style/login.css';

class Login extends React.Component {
	constructor(props) {
		super(props);
		this.service = new AuthService();
		this.state = {
			emailError: false,
			passwordError: false,
			loginError: false,
			isLoading: false
		}
	}

	componentWillMount() {
		if (this.service.loggedIn())
			this.props.history.replace('/');
	}

	render() {
		const { emailError, passwordError, loginError, isLoading } = this.state;
		return (
			<div className="App">
				<header className="App-header">
					<h1 className="App-title">Login</h1>
				</header>
				<form onSubmit={this.onLogin}>
					<div className="grid login-wrapper">
						<div className="login-title">
							<h3>Welcome</h3>
						</div>
						<div className="error">
							{loginError ? <p className="validator">Wrong email or password</p> : null}
						</div>
						<div>
							<div className="grid login-content">
								<span className="icon"><Mail /></span>
								<input type="text" name="email" autoComplete="username" placeholder="Email" onChange={this.onChange} />
							</div>
							{emailError ? <p className="validator">Type your email</p> : null}
						</div>
						<div>
							<div className="grid login-content">
								<span className="icon"><Key /></span>
								<input type="password" name="password" autoComplete="current-password" placeholder="Password" onChange={this.onChange} />
							</div>
							{passwordError ? <p className="validator">Type your password</p> : null}
						</div>
						<div className="login-submit">
							{isLoading ?
								<Spinner name="line-scale-pulse-out-rapid" className="loader loader-middle" /> :
								<input type="submit" value="Login" onClick={this.onLogin} />
							}
						</div>
					</div>
				</form>
			</div >
		);
	}

	onChange = (e) => {
		this.setState({
			[e.target.name]: e.target.value,
			[`${e.target.name}Error`]: !e.target.value && e.target.value.trim() !== "",
			loginError: false
		});
	}

	onLogin = (e) => {
		e.preventDefault();
		const { email, password } = this.state;
		const emailError = !email || email.trim() === "";
		const passwordError = !password || password.trim() === "";

		if (!emailError && !passwordError) {
			this.setState({ isLoading: true }, () => {
				this.service.login(email, password)
					.then(response => {
						this.props.history.replace('/user');
						emitter.emit('onLogin');
					})
					.catch(error => {
						this.setState({ loginError: true, isLoading: false })
					});
			});
		}
		else {
			this.setState({
				emailError: emailError,
				passwordError: passwordError,
				loginError: false
			})
		}
	}
}

export default Login;