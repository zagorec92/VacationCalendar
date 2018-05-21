import AuthService from './AuthService';
import Service from './Service';
import moment from 'moment';
window['moment'] = moment;

class VacationService extends Service {
	constructor(domain) {
		super(domain);
		this.authService = new AuthService();
		// this.domain = domain || 'http://localhost:56864';
	}

	save = (vacation) => {
		return this.authService.fetch(`${this.domain}/api/vacation/save`, {
			method: 'POST',
			body: JSON.stringify({
				"dateFrom": vacation.dateFrom.format("YYYY-MM-DD"),
				"dateTo": vacation.dateTo.format("YYYY-MM-DD"),
				"status": vacation.status,
				"type": vacation.type,
				"availability": vacation.availability,
				"active": vacation.active,
				"ID": vacation.id
			})
		}).then(response => {
			return Promise.resolve(response);
		}).catch((error) => {
			alert(error);
		});
	}

	hasVacationInRange = (vacation, vacations) => {
		let retVal = false;

		if (vacation.active && vacations.length > 0) {
			retVal = vacations.some((value) => {
				const dateTo = moment(value.dateTo).startOf('day');
				const dateFrom = moment(value.dateFrom).startOf('day');
				return value.id !== vacation.id &&
					(
						(
							vacation.dateFrom <= dateTo
							&& vacation.dateFrom >= dateFrom
						) || (
							vacation.dateFrom < dateFrom
							&& vacation.dateTo >= dateFrom
						)
					);
			});
		}

		return retVal;
	}

	get = (year) => {
		const userToken = this.authService.getProfile();
		return this.authService.fetch(`${this.domain}/api/vacation/${userToken.jti}/${year}`, {
			method: 'GET'
		}).then(response => {
			return Promise.resolve(response);
		}).catch((error) => {
			alert(error);
		});
	}

	getAllForYear = (year) => {
		return this.authService.fetch(`${this.domain}/api/vacation/year/${year}`, {
			method: 'GET'
		}).then(response => {
			return Promise.resolve(response);
		});
	}
}

export default VacationService;