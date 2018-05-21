import React from 'react';
import moment from 'moment';
import DatePicker from 'react-datepicker';
import Modal from 'react-responsive-modal';
import Select from 'react-select';
import enums from '../../global/enums';
import 'react-select/dist/react-select.css';
import 'react-datepicker/dist/react-datepicker.css';
window['moment'] = moment;

class Vacation extends React.Component {
	constructor(props) {
		super(props);

		const isNew = this.props.isNew;
		const vacation = this.props.vacation;

		let dateFrom = moment(isNew ? new Date() : vacation.dateFrom).startOf('day');
		let dateTo = moment(isNew ? new Date() : vacation.dateTo).startOf('day');

		if (isNew) {
			dateFrom.add(1, 'days');
			dateTo.add(1, 'days');
		}

		if (!this.isWeekday(dateFrom))
			dateFrom = moment().add(1, 'weeks').isoWeekday(1);
		if (!this.isWeekday(dateTo))
			dateTo = moment().add(1, 'weeks').isoWeekday(1);

		this.isEditable = isNew || moment() < dateFrom;

		const vacationState = {
			dateFrom: dateFrom,
			dateTo: dateTo,
			status: isNew ? enums.vacationStatus.ENTERED : vacation.status,
			type: isNew ? enums.vacationType.VACATION_LEAVE : vacation.type,
			availability: isNew ? enums.vacationAvailability.AVAILABLE : vacation.availability,
			statusName: isNew ? null : vacation.statusName,
			typeName: isNew ? null : vacation.typeName,
			availabilityName: isNew ? null : vacation.availabilityName,
			active: isNew ? true : vacation.active,
			id: isNew ? null : vacation.id,
			userFullName: isNew ? null : vacation.userFullName
		};

		this.state = {
			vacationReadOnly: vacationState,
			vacationUpdate: Object.assign({}, vacationState),
			isModalOpen: false
		}
	}

	render() {
		const { className, isNew, isAdmin } = this.props;
		const { vacationReadOnly, vacationUpdate, isModalOpen } = this.state;
		return (
			<div className={className} onClick={this.onOpenModal}>
				{!isNew ?
					<div className="item-grid-content-item">
						{isAdmin ? <div title="Name">{vacationReadOnly.userFullName}</div> : null}
						<div title="Vacation type">{vacationReadOnly.typeName}</div>
						<div title="Vacation status">{vacationReadOnly.statusName}</div>
						<div title="Vacation availability">{vacationReadOnly.availabilityName}</div>
						<div title="Date from">{vacationReadOnly.dateFrom.format("MM/DD/YYYY")}</div>
						<div title="Date to">{vacationReadOnly.dateTo.format("MM/DD/YYYY")}</div>
					</div>
					: <div>Add vacation</div>
				}
				<Modal open={isModalOpen} onClose={this.onCloseModal} showCloseIcon={false} classNames={{ overlay: 'overlay', modal: 'modal' }} center>
					<div className="vacation-picker">
						<h3>VACATION DATA</h3>
						<div className="vacation-picker-row">
							<span>Date from</span>
							<DatePicker
								selectsStart
								selected={vacationUpdate.dateFrom}
								startDate={vacationUpdate.dateFrom}
								endDate={vacationUpdate.dateTo}
								filterDate={this.excludeDates}
								disabled={isAdmin}
								className="vacation-picker-date"
								onChange={(e) => this.dateChanged(e, 'from')}
								withPortal />

						</div>
						<div className="vacation-picker-row">
							<span>Date to</span>
							<DatePicker
								selectsEnd
								selected={vacationUpdate.dateTo < vacationUpdate.dateFrom ? vacationUpdate.dateFrom : vacationUpdate.dateTo}
								startDate={vacationUpdate.dateFrom}
								endDate={vacationUpdate.dateTo}
								filterDate={this.excludeDates}
								disabled={isAdmin}
								className="vacation-picker-date"
								onChange={(e) => this.dateChanged(e, 'to')}
								withPortal />
						</div>
						<div className="vacation-picker-row">
							<span>Type</span>
							<Select
								clearable={false}
								onChange={(e) => this.vacationOptionsChanged(e, 'type')}
								value={vacationUpdate.type}
								disabled={isAdmin}
								options={[
									{ value: enums.vacationType.VACATION_LEAVE, label: 'Vacation leave' },
									{ value: enums.vacationType.SICK_LEAVE, label: 'Sick leave' },
								]} />
						</div>
						<div className="vacation-picker-row">
							<span>Availability</span>
							<Select
								clearable={false}
								onChange={(e) => this.vacationOptionsChanged(e, 'availability')}
								value={vacationUpdate.availability}
								disabled={isAdmin}
								options={[
									{ value: enums.vacationAvailability.AVAILABLE, label: 'Available' },
									{ value: enums.vacationAvailability.PARTIALLY_AVAILABLE, label: 'Partially available' },
									{ value: enums.vacationAvailability.UNAVAILABLE, label: 'Unavailable' }
								]} />
						</div>
						{isAdmin ?
							<div className="vacation-picker-row">
								<span>Status</span>
								<Select
									clearable={false}
									onChange={(e) => this.vacationOptionsChanged(e, 'status')}
									value={vacationUpdate.status}
									options={[
										{ value: enums.vacationStatus.ENTERED, label: 'Entered', disabled: true },
										{ value: enums.vacationStatus.CONFIRMED, label: 'Confirmed' },
										{ value: enums.vacationStatus.REJECTED, label: 'Rejected' }
									]} />
							</div> : null
						}

						<div className="vacation-picker-row">
							<label htmlFor="myonoffswitch">Active</label>
							<div className="onoffswitch">
								<input type="checkbox" disabled={isNew || isAdmin} checked={vacationUpdate.active} onChange={(e) => this.vacationOptionsChanged(e, 'active')}
									name="onoffswitch" className="onoffswitch-checkbox" id="myonoffswitch" />
								<label className="onoffswitch-label" htmlFor="myonoffswitch">
									<span className="onoffswitch-inner"></span>
									<span className="onoffswitch-switch"></span>
								</label>
							</div>
						</div>

						<button className="button button-submit" onClick={this.confirm}>Confirm</button>
						<button className="button button-cancel" onClick={this.onCloseModal}>Cancel</button>
					</div>
				</Modal>
			</div>
		);
	}

	isWeekday = (e) => {
		const weekday = e.isoWeekday();
		return weekday !== 6 && weekday !== 7;
	}

	excludeDates = (e) => {
		return this.isWeekday(e) && e.toDate() >= moment().toDate();
	}

	vacationOptionsChanged = (e, type) => {
		let vacation = this.state.vacationUpdate;
		if (type === 'active') {
			vacation.active = e.target.checked;
		}
		else {
			vacation[type] = e.value;
		}

		this.setState({
			vacationUpdate: vacation
		});
	}

	dateChanged = (e, type) => {
		let vacation = this.state.vacationUpdate;
		if (type === 'from') {
			vacation.dateFrom = this.getDate(e);
			if (vacation.dateTo < vacation.dateFrom)
				vacation.dateTo = this.getDate(e);
			this.setState({
				vacationUpdate: vacation
			});
		}
		else if (type === 'to') {
			vacation.dateTo = this.getDate(e);
			if (vacation.dateTo < vacation.dateFrom)
				vacation.dateFrom = this.getDate(e);
			this.setState({
				vacationUpdate: vacation
			});
		}
	}

	getDate = (e) => {
		return e.startOf('day');
	}

	confirm = (e) => {
		const { vacationUpdate, vacationReadOnly } = this.state;
		this.onCloseModal(e);
		this.props.save(vacationUpdate, vacationReadOnly);
	}

	onOpenModal = (e) => {
		if (this.isEditable)
			this.setState({ isModalOpen: true });
		else
			alert('Modifying of passed or in progress vacations is not allowed.');
	}

	onCloseModal = (e) => {
		this.setState({ isModalOpen: false });
		e.stopPropagation();
	}
}

export default Vacation;