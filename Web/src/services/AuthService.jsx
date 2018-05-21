import Service from './Service';
import decode from 'jwt-decode';

class AuthService extends Service {
	constructor(domain) {
		super(domain);
		// this.domain = domain || 'http://localhost:56864';
		this.roleIdentifier = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
		// this.fetch = this.fetch.bind(this);
		// this.login = this.login.bind(this);
		// this.getProfile = this.getProfile.bind(this);
	}

	login = (email, password) => {
		return this.fetch(`${this.domain}/api/users/login`, {
			method: 'POST',
			body: JSON.stringify({ "email": email, "password": password })
		}).then(response => {
			this.setToken(response);
			return Promise.resolve(response);
		});
	}

	logout = () => {
		localStorage.removeItem('token');
	}

	getProfile = () => {
		return decode(this.getToken());
	}

	getRoles = (t) => {
		const token = t || this.getProfile();
		return token && token[this.roleIdentifier];
	}

	isAdmin = (r) => {
		let retVal = false;

		const roles = r || this.getRoles();
		if (roles) {
			const rolesArray = Array.isArray(roles) ? roles : [roles];
			retVal = rolesArray.every((value) => {
				return value === "Admin";
			});
		}

		return retVal;
	}

	getHeader = () => {
		return { 'Authorization': 'Bearer ' + this.getToken() };
	}

	loggedIn = () => {
		const token = this.getToken();
		return !!token && !this.isTokenExpired(token);
	}

	isTokenExpired = (token) => {
		let retVal = true;

		try {
			const decodedToken = decode(token);
			if (!(decodedToken.exp < Date.now() / 1000))
				retVal = false;
		}
		catch (error) {
			retVal = false;
		}
		finally {
			return retVal;
		}
	}

	setToken = (token) => {
		localStorage.setItem('token', token);
	}

	getToken = () => {
		return localStorage.getItem('token');
	}

	fetch = (url, options) => {
		const headers = {
			'Accept': 'application/json',
			'Content-Type': 'application/json'
		};

		if (this.loggedIn()) {
			headers['Authorization'] = 'Bearer ' + this.getToken();
		}

		return fetch(url, { headers, ...options })
			.then(this.checkStatus)
			.then(response => response.json());
	}

	checkStatus = (response) => {
		return response;
		// TODO
	}
}

export default AuthService;