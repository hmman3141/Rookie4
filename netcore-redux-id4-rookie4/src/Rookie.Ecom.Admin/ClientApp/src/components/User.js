import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { actionCreators } from '../store/User';

class User extends Component {
    componentDidMount() {
        // This method is called when the component is first added to the document
        this.ensureDataFetched();
    }

    componentDidUpdate() {
        // This method is called when the route parameters change
        this.ensureDataFetched();
    }

    ensureDataFetched() {
        const page = parseInt(this.props.match.params.page, 5) || 0;
        this.props.requestUsers(page);
    }


    render() {
        const { users, user } = this.props;
        return (
            <div>
                <h1>User</h1>
                <p>This component demonstrates fetching data from the server and working with URL parameters.</p>
                
                {user ? renderUserTable(users) : <span>You are not allowed to do this!</span>}
                {user ? renderPagination(users) : []}
            </div>
        );
    }
}

function renderUserTable(props) {
    return (
        <table className='table table-striped'>
            <thead>
                <tr>
                    <th>ID</th>
                    <th>First name</th>
                    <th>Last name</th>
                    <th>Created date</th>
                </tr>
            </thead>
            <tbody>
                {props.users.map(user =>
                    <tr key={user.id}>
                        <td>{user.id}</td>
                        <td>{user.firstName}</td>
                        <td>{user.lastName}</td>
                        <td>{new Date(user.createdDate).toUTCString()}</td>
                    </tr>
                )}
            </tbody>
        </table>
    );
}

function renderPagination(props, user = "") {
    const prevStartDateIndex = (props.page || 0) - 5;
    const nextStartDateIndex = (props.page || 0) + 5;
    if (user === "" || (user !== null && user.profile['role']==='Admin'))
    return <p className='clearfix text-center'>
        <Link className='btn btn-default pull-left' to={`/user/${prevStartDateIndex}`}>Previous</Link>
        <Link className='btn btn-default pull-right' to={`/user/${nextStartDateIndex}`}>Next</Link>
        {props.isLoading ? <span>Loading...</span> : []}
    </p>;
}

export default connect(
    state => ({ users: state.users, user: state.oidc.user}),
    dispatch => bindActionCreators(actionCreators, dispatch)
)(User);