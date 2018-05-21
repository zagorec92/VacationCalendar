import React from 'react';
import Calendar from '../calendar/Calendar';
import '../../App.css';

class Home extends React.Component {
	render() {
		return (
			<div className="App">
				<header className="App-header">
					<h1 className="App-title">Vacation overview</h1>
				</header>
				<Calendar />
			</div>
		);
	}
}

export default Home;
