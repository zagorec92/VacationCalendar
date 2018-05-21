import React from 'react';
import auth from '../authentication/Auth';
import uuid from 'uuid';
import AuthService from '../../services/AuthService';
import VacationList from '../vacation/VacationList';
import AdminPanel from '../admin/AdminPanel';

class User extends React.Component {
	constructor(props) {
		super(props);
		this.service = new AuthService();
	}

	render() {
		const user = this.service.getProfile();
		const roles = this.service.getRoles(user);
		const isAdmin = this.service.isAdmin(roles);
		return (
			<div className="App">
				<header className="App-header">
					<h1 className="App-title">{user.sub}</h1>
					<p>{user.email}</p>
					{roles ?
						Array.isArray(roles) ?
							roles.map((role) =>
								<p key={uuid.v4()}>{role}</p>
							)
							: <p>{roles}</p>
						: null
					}
				</header>
				{isAdmin ?
					<AdminPanel /> :
					<VacationList />
				}
			</div>
		);
	}
}

export default auth(User);