import React from 'react';
import uuid from 'uuid';
import CalendarDay from './CalendarDay';

class CalendarDays extends React.Component {
	render() {
		const { month, vacations } = this.props;
		return (
			<div key={uuid.v4()} className="calendar-content-x">
				{month.days.map((day) =>
					<CalendarDay key={uuid.v4()} day={day} vacation={this.filterVacation(day, vacations)} />
				)}
			</div>
		);
	}

	filterVacation = (day, vacation) => {
		return vacation && vacation.find((v) => {
			return v.dateFrom <= day.date && day.date <= v.dateTo;
		});
	}
}

export default CalendarDays;