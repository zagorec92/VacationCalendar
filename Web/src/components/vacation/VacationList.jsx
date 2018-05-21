import React from 'react'
import uuid from 'uuid';
import enums from '../../global/enums';
import Vacation from './Vacation';
import VacationService from '../../services/VacationService';
import CalendarService from '../../services/CalendarService';
import Spinner from 'react-spinkit';
import CalendarDays from '../calendar/CalendarDays';
import CalendarPicker from '../calendar/CalendarPicker';
import CalendarIcon from 'react-icons/lib/fa/calendar';
import moment from 'moment';
window['moment'] = moment;

class VacationList extends React.Component {
	constructor(props) {
		super(props);

		this.vacationService = new VacationService();
		this.calendarService = new CalendarService();

		this.state = {
			vacations: [],
			months: [],
			selectedYear: new Date().getFullYear(),
			vacationsCollapsed: false,
			yearCollapsed: false,
			isLoading: false
		}
	}

	componentDidMount() {
		this.reload();
	}

	render() {
		const { vacations, months, selectedYear, vacationsCollapsed, yearCollapsed, isLoading } = this.state;

		return (
			<div>
				<CalendarPicker year={selectedYear} handler={this.handler} className="calendar-picker-user" />
				{isLoading ?
					<Spinner name="line-scale-pulse-out-rapid" className="loader loader-middle" /> :
					<div className="item-grid">
						<div className="item-grid-header" name="vacations" onClick={this.handleExpandCollapse}>
							<p>VACATIONS</p>
							<span>{vacationsCollapsed ? 'expand' : 'collapse'}</span>
						</div>
						{vacationsCollapsed ?
							null :
							<div className="item-grid-content">
								<div className="item-grid-content-header">
									<div>Type</div>
									<div>Status</div>
									<div>Availability</div>
									<div>Date from</div>
									<div>Date to</div>
								</div>
								{vacations && vacations.length > 0 ?
									vacations.map((v) =>
										<Vacation key={uuid.v4()} vacation={v} isNew={false} isAdmin={false} save={(v, vRo) => this.saveVacation(v, vRo)} />
									) :
									<p className="item-grid-content-empty">No results</p>
								}
							</div>
						}
						<Vacation className="item-grid-footer" isNew={true} save={(v) => this.saveVacation(v)} />
					</div>
				}
				{isLoading ?
					<Spinner name="line-scale-pulse-out-rapid" className="loader loader-middle" /> :
					<div>
						<div className="item-grid-header" name="year" onClick={this.handleExpandCollapse}>
							<p>Year overview</p>
							<span>{yearCollapsed ? 'expand' : 'collapse'}</span>
						</div>
						{yearCollapsed ?
							null :
							<div className="calendar-container">
								<div className="calendar-wrapper">
									{months.map((month) =>
										<div key={uuid.v4()} className="calendar-content">
											<div key={uuid.v4()} className="calendar-content-y">
												<CalendarIcon className="icon-calendar" /><p key={month.month} className="vacation-list-month">{moment.months(month.month - 1)}</p>
											</div>
											<CalendarDays month={month} vacations={vacations} />
										</div>
									)}
								</div>
							</div>
						}
					</div>
				}
			</div>
		);
	}

	handleExpandCollapse = (e) => {
		const stateName = `${e.target.parentElement.getAttribute("name")}Collapsed`;
		this.setState({
			[stateName]: !this.state[stateName]
		})
	}

	handler = (type, value) => {
		const selectedYear = this.state.selectedYear + value;
		this.reload(selectedYear);
	}

	reload = (year = this.state.selectedYear) => {
		this.setState({
			isLoading: true
		}, () => {
			this.getData(year)
				.then(([vacations, months]) => {
					this.setState({
						isLoading: false,
						vacations: vacations,
						months: months,
						selectedYear: year
					})
				});
		});
	}

	saveVacation = (vacation, vacationReadOnly) => {
		const vacations = this.state.vacations;
		const isEqual = this.vacationService.isEqual(vacation, vacationReadOnly);
		if (vacations && !isEqual) {
			if (!this.vacationService.hasVacationInRange(vacation, vacations)) {
				if (vacation.active)
					vacation.status = enums.vacationStatus.ENTERED;

				this.vacationService.save(vacation)
					.then((response) => {
						this.getVacations()
							.then((vacations) => {
								this.setState({
									vacations: vacations
								});
							});
					})
					.catch((error) => {
						alert(error);
					});
			}
			else {
				alert('Vacation is already entered in the given date range');
			}
		}
	}

	getMonths(year) {
		return this.calendarService.getMonthsForYear(year)
			.then((response) => {
				return response.json();
			})
			.catch((error) => {
				alert(error);
			});
	}

	getVacations(year = this.state.selectedYear) {
		return this.vacationService.get(year);
	}

	getData(year) {
		return Promise.all([this.getVacations(year), this.getMonths(year)])
	}
}

export default VacationList;