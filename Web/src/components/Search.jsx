import React from 'react';
import DatePicker from 'react-datepicker';
import moment from 'moment';
import emitter from '../global/emitter';
import '../style/search.css';
window['moment'] = moment;

class Search extends React.Component {
	constructor(props) {
		super(props);

		emitter.removeAllListeners('onCalendarPickerChanged');
		emitter.addListener('onCalendarPickerChanged', this.onCalendarPickerChanged.bind(this));

		const { year, month } = this.props;
		const momentFrom = moment([year, month - 1]);
		const momentTo = moment([year, month - 1]).endOf('month');
		this.state = {
			name: '',
			minDate: momentFrom,
			maxDate: momentTo,
			dateFrom: null,
			dateTo: null,
			isCollapsed: true
		}
	}

	render() {
		const { name, minDate, maxDate, dateFrom, dateTo, isCollapsed } = this.state;
		return (
			<div>
				<div className="content-header content-header-stretch" name="search" onClick={this.handleExpandCollapse}>
					<p>SEARCH</p>
					<span>{isCollapsed ? 'expand' : 'collapse'}</span>
				</div>
				{isCollapsed ?
					null :
					<div className="search-wrapper">
						<div>
							<input type="search" name="name" className="input" value={name} onChange={this.onSearchChanged} placeholder="Name" />
						</div>
						<DatePicker
							className="input input-date"
							selectsStart
							selected={dateFrom}
							startDate={dateFrom}
							endDate={dateTo}
							minDate={minDate}
							maxDate={maxDate}
							filterDate={this.isWeekday}
							isClearable={true}
							readOnly={true}
							todayButton={"Today"}
							placeholderText="Date from"
							onChange={(e) => this.onSearchChanged(e, "dateFrom")}
							withPortal />
						<DatePicker
							className="input input-date"
							selectsEnd
							selected={dateTo}
							startDate={dateFrom}
							endDate={dateTo}
							minDate={minDate}
							maxDate={maxDate}
							filterDate={this.isWeekday}
							isClearable={true}
							readOnly={true}
							todayButton={"Today"}
							placeholderText="Date to"
							onChange={(e) => this.onSearchChanged(e, "dateTo")}
							withPortal />
						<div className="search-actions">
							<button className="button button-submit" type="submit" onClick={(e) => this.onSearch(e, "search")}>Submit</button>
							<button className="button button-cancel" type="submit" onClick={(e) => this.onSearch(e, "clear")}>Clear</button>
						</div>
					</div>
				}
			</div>
		)
	}

	isWeekday = (e) => {
		const weekday = e.isoWeekday();
		return weekday !== 7 && weekday !== 6;
	}

	onSearchChanged = (e, stateName) => {
		const name = e ? (e.target ? e.target.name : stateName) : stateName;
		const value = e ? (e.target ? e.target.value : e) : e;
		this.setState({
			[`${name}`]: value
		});
	}

	onSearch = (e, type) => {
		const { name, dateFrom, dateTo } = this.state;
		let params = {};
		if (type === "search") {
			params.name = name;
			params.dateFrom = dateFrom ? dateFrom.format("YYYY-MM-DD") : null;
			params.dateTo = dateTo ? dateTo.format("YYYY-MM-DD") : null;
		}
		else if (type === "clear") {
			this.setState({
				name: '',
				dateFrom: null,
				dateTo: null
			});
		}

		this.props.onSearch(params);
	}

	onCalendarPickerChanged() {
		const { year, month } = this.props;
		const momentFrom = moment([year, month - 1]);
		const momentTo = moment([year, month - 1]).endOf('month');
		this.setState({
			minDate: momentFrom,
			maxDate: momentTo,
			dateFrom: null,
			dateTo: null
		});
	}

	handleExpandCollapse = () => {
		this.setState({
			isCollapsed: !this.state.isCollapsed
		})
	}
}

export default Search;