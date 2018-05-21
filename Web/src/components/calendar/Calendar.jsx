import React from 'react';
import uuid from 'uuid';
import emitter from '../../global/emitter';
import CalendarPicker from './CalendarPicker';
import CalendarDays from './CalendarDays';
import Employee from '../Employee';
import Spinner from 'react-spinkit';
import UserService from '../../services/UserService';
import CalendarService from '../../services/CalendarService';
import Search from '../Search';

class Calendar extends React.Component {
	constructor() {
		super();
		this.userService = new UserService();
		this.calendarService = new CalendarService();
		this.state = {
			contentLoading: false,
			error: false,
			calendarLoading: true,
			users: [],
			month: {},
			selectedYear: 0,
			selectedMonth: 0
		}
	}

	componentDidMount() {
		this.setState({ calendarLoading: true, error: false }, () => {
			this.getData()
				.then(([users, month]) => {
					if (users && month) {
						this.setState({
							calendarLoading: false,
							users: users,
							month: month,
							selectedYear: month.year,
							selectedMonth: month.month
						});
					}
				})
				.catch((error) => {
					this.setState({
						error: true
					});
				});
		});
	}

	render() {
		const { calendarLoading, contentLoading, users, month, error } = this.state;
		if (!error) {
			if (calendarLoading) {
				return (<Spinner name="line-scale-pulse-out-rapid" className="loader loader-middle" />);
			}
			else {
				return (
					<div className="calendar-container">
						<Search onSearch={this.onSearch} year={month.year} month={month.month} />
						<div>
							<CalendarPicker month={month.month} year={month.year} handler={this.handler.bind(this)} className="calendar-picker-margin" />
						</div>
						<div className="calendar-wrapper">
							{users.length > 0 ?
								users.map((user) =>
									<div key={uuid.v4()} className="calendar-content">
										<div key={uuid.v4()} className="calendar-content-y">
											<Employee user={user} />
										</div>
										{contentLoading ?
											<Spinner name="line-scale-pulse-out-rapid" className="loader" />
											: <CalendarDays month={month} vacations={user.vacations} />
										}
									</div>) : <p>No results</p>}
						</div>
					</div>
				);
			}
		} else {
			return (<p>Error occured</p>);
		}
	}

	onSearch = (params) => {
		this.setState({ contentLoading: true }, () => {
			this.getUsers(params)
				.then((users) => {
					this.setState({
						users: users,
						contentLoading: false
					});
				});
		});
	}

	handler(type, value) {
		let refresh = false;
		let selectedMonth = this.state.selectedMonth;
		let selectedYear = this.state.selectedYear;

		if (type === "month") {
			selectedMonth = selectedMonth + value;
			if (selectedMonth === 0) {
				selectedMonth = 12;
				selectedYear -= 1;
			}
			else if (selectedMonth > 12) {
				selectedMonth = 1;
				selectedYear += 1;
			}

			refresh = true;
		}
		else if (type === "year") {
			selectedYear = selectedYear + value
			refresh = true;
		}

		if (refresh) {
			this.setState({
				selectedYear: selectedYear,
				selectedMonth: selectedMonth,
				contentLoading: true
			}, () => {
				this.getMonth(this.state.selectedYear, this.state.selectedMonth)
					.then((month) => {
						this.setState({
							contentLoading: false,
							month: month
						});
						emitter.emit('onCalendarPickerChanged');
					});
			});
		}
	}

	getUsers(params) {
		return this.userService.getUsers(params)
			.then(response => {
				return response.json();
			})
			.catch((error) => {
				alert(error);
			});
	}

	getMonth(year, month) {
		return this.calendarService.getMonth(year, month)
			.then((response) => {
				return response.json();
			})
			.catch((error) => {
				alert(error);
			});
	}

	getData() {
		const date = new Date();
		return Promise.all([this.getUsers({}), this.getMonth(date.getFullYear(), date.getMonth() + 1)])
	}
}

export default Calendar;