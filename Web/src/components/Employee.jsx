import React from 'react';
import Modal from 'react-responsive-modal';
import UserIcon from 'react-icons/lib/fa/user';
import uuid from 'uuid';

class Employee extends React.Component {
	constructor(props) {
		super(props);

		this.state = {
			modalOpen: false
		}
	}

	render() {
		const { user } = this.props;
		const modalOpen = this.state.modalOpen;
		return (
			<div title={user.fullName} className="employee">
				<UserIcon className="icon-user" onClick={this.onOpenModal} /> <p onClick={this.onOpenModal}>{user.fullName}</p>
				<Modal open={modalOpen} onClose={this.onCloseModal} showCloseIcon={false} classNames={{ modal: 'modal' }} center>
					<p className="modal-title"><strong>{user.fullName}</strong></p>
					<div className="horizontal-line"><hr /></div>
					<div className="modal-content">
						<p><strong>Email </strong>{user.email}</p>
						<p><strong>Roles </strong>{user.roles.map((role) => <span key={uuid.v4()}>{role.name}</span>)}</p>
					</div> 
				</Modal>
			</div>
		);
	}

	onOpenModal = (e) => {
		this.setState({ modalOpen: true });
	}

	onCloseModal = (e) => {
		this.setState({ modalOpen: false });
		e.stopPropagation();
	}
}

export default Employee;