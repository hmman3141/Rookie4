import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { actionCreators } from '../store/Category';

class Category extends Component {
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
        this.props.requestCategories(page);
    }

    onUpdateCategoryClick() {
        const page = parseInt(this.props.match.params.page, 5) || 0;
        this.props.updateCategories();
    }

    onAddCategoryClick() {
        this.props.addCategories();
    }

    onViewCategoryClick() {
        this.props.viewCategories();
    }

    render() {
        const { categories, user } = this.props;
        return (
            <div>
                <h1>Category</h1>
                <p>This component demonstrates fetching data from the server and working with URL parameters.</p>
                <button onClick={() => this.onAddCategoryClick()}>Add category</button>
                <button>Remove category</button>
                <button onClick={() => this.onUpdateCategoryClick()}>Update categories</button>
                <button onClick={() => this.onViewCategoryClick()}>View categories</button>
                <br />

                {!categories.isExcuting && renderCategoryTable(categories)}
                {!categories.isExcuting && renderPagination(categories)}
                {categories.isAdding && renderAddCategory(this.props, user)}
                {categories.isUpdating && renderUpdateCategoryTable(categories, user, this.props)}
                {categories.isUpdating && renderPagination(categories, user)}
            </div>
        );
    }
}

function renderCategoryTable(props) {
    return (
        <table className='table table-striped'>
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Name</th>
                    <th>Desc</th>
                </tr>
            </thead>
            <tbody>
                {props.categories.map(cat =>
                    <tr key={cat.id}>
                        <td>{cat.id}</td>
                        <td>{cat.categoryName}</td>
                        <td>{cat.desc}</td>
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
        <Link className='btn btn-default pull-left' to={`/category/${prevStartDateIndex}`}>Previous</Link>
        <Link className='btn btn-default pull-right' to={`/category/${nextStartDateIndex}`}>Next</Link>
        {props.isLoading ? <span>Loading...</span> : []}
    </p>;
}

function renderAddCategory(props, user) {
    if (user) {
        if (user.profile['role'] === 'Admin')
            return <div className="mb-3">
                <p>Name</p>
                <input type="text" id="name" />

                <p className="mt-3">Description</p>
                <textarea id="desc" name="comment" cols="30" rows="5" className="form-control"></textarea>

                <p className="mt-3">Fontawesome icon name</p>
                <input type="text" id="iconname" />
                <br />
                <button className="btn btn-dark mt-3" onClick={() => props.executeAddCategory(document.getElementById("name").value, document.getElementById("desc").value, document.getElementById("iconname").value, user.profile["sub"])}>Create</button>
            </div>
        else 
            return <span>You are not allowed to do this!</span>
    }
    else
        return <span>Please login!</span>
}

function renderUpdateCategoryTable(props, user, action) {
    if (user) {
        if (user.profile['role'] === 'Admin')
            return (
                <table className='table table-striped'>
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Name</th>
                            <th>Desc</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {props.categories.map(cat =>
                            <tr key={cat.id}>
                                <td>{cat.id}</td>
                                <td>
                                    <input type="text" id={`${cat.id}-name`} className="form-control" placeholder={cat.categoryName} onChange={() => { document.getElementById(`${cat.id}-btn`).hidden = (document.getElementById(`${cat.id}-name`).value.length + document.getElementById(`${cat.id}-desc`).value.length === 0) ? true:false }} />
                                </td>
                                <td>
                                    <input type="text" id={`${cat.id}-desc`} className="form-control" placeholder={cat.desc} onChange={() => { document.getElementById(`${cat.id}-btn`).hidden = (document.getElementById(`${cat.id}-name`).value.length + document.getElementById(`${cat.id}-desc`).value.length === 0) ? true : false }}/>
                                </td>
                                <td>
                                    <button className="btn btn-dark" id={`${cat.id}-btn`} onClick={() =>
                                    {
                                        action.executeUpdateCategory(cat.id, (document.getElementById(`${cat.id}-name`).value.length !== 0) ? document.getElementById(`${cat.id}-name`).value : cat.categoryName,
                                            (document.getElementById(`${cat.id}-desc`).value.length !== 0) ? document.getElementById(`${cat.id}-desc`).value : cat.desc,
                                            cat.createdDate,
                                            cat.creatorId,
                                            cat.pubished,
                                            cat.imageUrl);
                                        document.getElementById(`${cat.id}-name`).value = "";
                                        document.getElementById(`${cat.id}-desc`).value = "";
                                        document.getElementById(`${cat.id}-btn`).hidden = true;
                                    }} hidden>Update</button>
                                </td>
                            </tr>
                            )}
                    </tbody>
                </table>
            );
        else 
            return <span>You are not allowed to do this!</span>
    }
    else
        return <span>Please login!</span>
}
export default connect(
    state => ({ categories: state.categories, user: state.oidc.user}),
    dispatch => bindActionCreators(actionCreators, dispatch)
)(Category);