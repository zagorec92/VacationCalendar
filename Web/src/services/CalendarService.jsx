import Service from './Service';

class CalendarService extends Service {
	getMonth = (year, month) => {
		return fetch(`${this.domain}/api/calendar/${year}/${month}`, {
			method: 'GET'
		}).then(response => {
			return Promise.resolve(response);
		});
	}

	getMonthsForYear = (year) => {
		return fetch(`${this.domain}/api/calendar/${year}`, {
			method: 'GET'
		}).then(response => {
			return Promise.resolve(response);
		});
	}
}

export default CalendarService;