import React from 'react';
import AuthService from '../../services/AuthService';

function auth(RenderComponent) {
	const service = new AuthService();
	return class AuthWrapper extends React.Component {
		componentDidUpdate() {
			if(!service.loggedIn()) {
				this.props.history.replace('/login');
			}
		}

		componentWillMount() {
			if(!service.loggedIn()) {
				this.props.history.replace('/login');
			}
			else {
				try {
					const profile = service.getProfile();
					this.setState({
						user: profile
					});
				}
				catch(error) {
					service.logout();
					this.props.history.replace('/login');
				}
			}
		}

		render() {
			if(service.loggedIn()) {
				return (
					<RenderComponent history={this.props.history} user={service.getProfile()} />
				)
			}
			else {
				return null;
			}
		}
	}
}


export default auth;