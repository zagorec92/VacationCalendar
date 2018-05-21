import AuthService from './AuthService';
import Service from './Service';

class UserService extends Service {
	constructor(domain) {
		super(domain);
		// this.domain = domain || 'http://localhost:56864';
		this.authService = new AuthService();
	}

	getUser = () => {
		const userToken = this.authService.getProfile();
		return this.authService.fetch(`${this.domain}/api/users/${userToken.jti}`, {
			method: 'GET'
		}).then(response => {
			return Promise.resolve(response);
		});
	}

	getUsers = (params) => {
		var url = `${this.domain}/api/users`;
		const queryString = this.encodeQueryParams(params);

		if (queryString !== '')
			url += `?${queryString}`;

		return fetch(url)
			.then((response) => {
				return Promise.resolve(response);
			});
	}
}

export default UserService;