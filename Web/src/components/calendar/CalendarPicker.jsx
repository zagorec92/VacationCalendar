import React from 'react'
import Left from 'react-icons/lib/fa/angle-left';
import Right from 'react-icons/lib/fa/angle-right';

class MonthPicker extends React.Component {
	render() {
		const { year, month, className } = this.props;
		return (
			<div className={`calendar-picker ${className}`}>
				{year ?
					<div>
						<div className="calendar-picker-item">
							<button type="button" title="previous" onClick={() => { this.handleClick("year", -1) }}>{<Left />}</button>
							<div>
								<p>{year}</p>
								<p>year</p>
							</div>
							<button type="button" title="next" onClick={() => { this.handleClick("year", 1) }}>{<Right />}</button>
						</div>
						{this.props.month ?
							<div className="calendar-picker-item">
								<button type="button" title="previous" onClick={() => { this.handleClick("month", -1) }}>{<Left />}</button>
								<div>
									<p>{month}</p>
									<p>month</p>
								</div>
								<button type="button" title="next" onClick={() => { this.handleClick("month", 1) }}>{<Right />}</button>
							</div> : null}
					</div>
					: null
				}
			</div>
		);
	}

	handleClick = (type, value) => {
		this.props.handler(type, value);
	}
}

export default MonthPicker;