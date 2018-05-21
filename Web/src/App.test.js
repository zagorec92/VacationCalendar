import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import Calendar from './components/Calendar';

it('renders without crashing', () => {
	const div = document.createElement('div');
	ReactDOM.render(<App />, div);
	ReactDOM.unmountComponentAtNode(div);
});

it('renders without crashing', () => {
	const div = document.createElement('div');
	ReactDOM.render(<Calendar />, div);
	ReactDOM.unmountComponentAtNode(div);
});
