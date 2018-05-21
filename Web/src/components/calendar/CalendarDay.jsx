import React from 'react';
import Modal from 'react-responsive-modal';
import enums from '../../global/enums';

class CalendarDay extends React.Component {
	constructor(props) {
		super(props);
		this.state = {
			calendarDay: props.day,
			vacation: props.vacation,
			modalOpen: false
		}
	}

	render() {
		const { vacation, calendarDay, modalOpen } = this.state;
		return (
			<div className={this.setClass()} onClick={this.onOpenModal}>
				<div>{calendarDay.day}</div>
				<Modal open={modalOpen} onClose={this.onCloseModal} showCloseIcon={false} classNames={{ modal: 'modal' }} center>
					<p className="modal-title"><strong>{calendarDay.dayName}</strong></p>
					<div className="horizontal-line"><hr /></div>
					<div className="modal-content">
						<p><strong>Date </strong>{new Date(calendarDay.date).toLocaleDateString()}</p>
						<p>{calendarDay.isHoliday ?
							<span><strong>Holiday </strong>{calendarDay.holidayName}</span>
							: calendarDay.isWeekend ?
								'Weekend'
								: vacation ?
									<div>
										<strong>Vacation </strong>
										<p><span><strong>Type </strong>{vacation.typeName}</span></p>
										<p><span><strong>Status </strong>{vacation.statusName}</span></p>
										<p><span><strong>Availability </strong>{vacation.availabilityName}</span></p>
									</div>
									: 'Work day'
						}</p>
					</div>
				</Modal>
			</div>
		);
	}

	setClass() {
		const { calendarDay, vacation } = this.state;
		let cssClass = 'day';

		if (vacation && !calendarDay.isWeekend && !calendarDay.isHoliday) {
			if (vacation.type === enums.vacationType.VACATION_LEAVE)
				cssClass += ' vacation-leave';
			else
				cssClass += ' vacation-sick';

			if (vacation.status === enums.vacationStatus.ENTERED)
				cssClass += ' vacation-entered';
			else
				cssClass += ' vacation-confirmed';
		}

		if (calendarDay.isWeekend)
			cssClass += ' weekend';

		if (calendarDay.isHoliday)
			cssClass += ' holiday';

		return cssClass;
	}

	onOpenModal = (e) => {
		this.setState({ modalOpen: true });
	}

	onCloseModal = (e) => {
		this.setState({ modalOpen: false });
		e.stopPropagation();
	}
}

export default CalendarDay;