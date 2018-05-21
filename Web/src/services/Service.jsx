class Service {
	constructor(domain) {
		this.domain = domain || 'http://localhost:56864';
	}

	encodeQueryParams = (params) => {
		let encodedQuery = [];

		for (const p in params) {
			if (p !== '' && params[p]) {
				encodedQuery.push(encodeURIComponent(p) + '=' + encodeURIComponent(params[p]));
			}
		}

		return encodedQuery.join('&');
	}

	//beware of the order of properties
	isEqual = (obj1, obj2) => {
		return JSON.stringify(obj1) === JSON.stringify(obj2);
	}
}

export default Service;