import React from 'react';
import VacationService from '../../services/VacationService';
import CalendarPicker from '../calendar/CalendarPicker';
import Vacation from '../vacation/Vacation';
import Spinner from 'react-spinkit';
import uuid from 'uuid';

class AdminPanel extends React.Component {
	constructor(props) {
		super(props);

		this.vacationService = new VacationService();
		this.state = {
			vacations: [],
			selectedYear: new Date().getFullYear(),
			isLoading: false
		}
	}

	componentDidMount() {
		this.getVacationsForYear();
	}

	render() {
		const { vacations, selectedYear, isLoading } = this.state;
		return (
			<div>
				<CalendarPicker year={selectedYear} handler={this.handler} className="calendar-picker-user" />
				{isLoading ?
					<Spinner name="line-scale-pulse-out-rapid" className="loader loader-middle" /> :
					<div className="item-grid">
						<div className="item-grid-header" name="vacations" onClick={this.handleExpandCollapse}>
							<p>ENTERED VACATIONS</p>
						</div>
						<div className="item-grid-content">
							<div className="item-grid-content-header">
								<div>Name</div>
								<div>Type</div>
								<div>Status</div>
								<div>Availability</div>
								<div>Date from</div>
								<div>Date to</div>
							</div>
							{vacations && vacations.length > 0 ?
								vacations.map((v) =>
									<Vacation key={uuid.v4()} vacation={v} isNew={false} isAdmin={true} save={(v) => this.saveVacation(v)} />
								) :
								<p className="item-grid-content-empty">No results</p>
							}
							<div className="item-grid-footer default-cursor"></div>
						</div>
					</div>
				}
			</div>
		)
	}

	handler = (type, value) => {
		const selectedYear = this.state.selectedYear + value;
		this.getVacationsForYear(selectedYear);
	}

	getVacationsForYear = (year = this.state.selectedYear) => {
		this.setState({
			isLoading: true
		}, () => {
			this.vacationService.getAllForYear(year)
				.then((vacations) => {
					this.setState({
						isLoading: false,
						vacations: vacations,
						selectedYear: year
					})
				})
				.catch((error) => {
					alert(error);
				});
		});
	}

	saveVacation = (vacation) => {
		const vacations = this.state.vacations;
		if (vacations) {
			this.vacationService.save(vacation)
				.then((response) => {
					this.getVacationsForYear();
				})
				.catch((error) => {
					alert(error);
				});
		}
	}
}

export default AdminPanel;