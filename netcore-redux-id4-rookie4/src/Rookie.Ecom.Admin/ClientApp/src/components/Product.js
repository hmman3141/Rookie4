import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { actionCreators } from '../store/Product';

class Category extends Component {
    componentDidMount() {
        // This method is called when the component is first added to the document
        this.ensureDataFetched();
        this.props.requestCategories();
    }

    componentDidUpdate() {
        // This method is called when the route parameters change
        this.ensureDataFetched();
    }

    ensureDataFetched() {
        const page = parseInt(this.props.match.params.page, 5) || 0;
        this.props.requestProducts(page);
    }

    onUpdateCategoryClick() {
        const page = parseInt(this.props.match.params.page, 5) || 0;
        this.props.updateProducts();
    }

    onAddCategoryClick() {
        this.props.addProducts();
    }

    onViewCategoryClick() {
        const page = parseInt(this.props.match.params.page, 5) || 0;
        this.props.viewProducts(page);
    }

    render() {
        const { products, user, categories } = this.props;
        return (
            <div>
                <h1>Product</h1>
                <p>This component demonstrates fetching data from the server and working with URL parameters.</p>
                <button onClick={() => this.onAddCategoryClick()}>Add product</button>
                <button>Remove product</button>
                <button onClick={() => this.onUpdateCategoryClick()}>Update product</button>
                <button onClick={() => this.onViewCategoryClick()}>View products</button>
                <br />
                {!products.isExcuting && renderCategoryTable(products)}
                {!products.isExcuting && renderPagination(products)}
                {products.isAdding && renderAddCategory(this.props, user, categories)}
                {products.isUpdating && renderUpdateCategoryTable(products, user, this.props)}
                {products.isUpdating && renderPagination(products, user)}
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
                    <th>Price</th>
                    <th>Created date</th>
                    <th>Updated date</th>
                </tr>
            </thead>
            <tbody>
                {props.products.map(pro =>
                    <tr key={pro.id}>
                        <td>{pro.id}</td>
                        <td>{pro.productName}</td>
                        <td>{pro.desc}</td>
                        <td>{pro.price}</td>
                        <td>{pro.createdDate}</td>
                        <td>{pro.updatedDate}</td>
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
        <Link className='btn btn-default pull-left' to={`/product/${prevStartDateIndex}`}>Previous</Link>
        <Link className='btn btn-default pull-right' to={`/product/${nextStartDateIndex}`}>Next</Link>
        {props.isLoading ? <span>Loading...</span> : []}
    </p>;
}

function renderAddCategory(props, user, categories) {
    if (user) {
        if (user.profile['role'] === 'Admin')
            return <div className="container mt-3">
                <div className="row">
                    <div className="col-sm">
                        <p>Name</p>
                        <input type="text" id="name" />
                        <p className="mt-3">Description</p>
                        <textarea id="desc" name="comment" cols="30" rows="5" className="form-control"></textarea>
                        <p className="mt-3">Price</p>
                        <input type="text" id="price" />
                        <p className="mt-3">Quantity</p>
                        <input type="text" id="quantity" />
                        <p className="mt-3">Image</p>
                        <input type="file" id="file" name="file" />
                        <br />
                    </div>
                    <div className="col-sm ml-5">
                        <p>Category</p>
                        {
                            categories.categories.map(cat => <div key={cat.categoryName} className="form-check pl-5 pb-2 nav">
                                <input className="form-check-input align-self-center mb-0" type="radio" id={cat.categoryName} name="category" value={cat.id} onClick={() => { document.getElementById("category").value = document.getElementById(cat.categoryName).value }} />
                                <label className="form-check-label align-self-center" htmlFor={cat.categoryName} onClick={() => { document.getElementById("category").value = document.getElementById(cat.categoryName).value }}>
                                    {cat.categoryName}
                                </label>
                            </div>)
                        }
                        <input type="text" id="category" hidden/>
                        
                    </div>
                    
                </div>
                <button className="btn btn-dark mt-3" onClick={() => props.executeAddProduct(document.getElementById("name").value, document.getElementById("desc").value, document.getElementById("price").value, document.getElementById("quantity").value, document.getElementById("file").files[0], user.profile["sub"], document.getElementById("category").value)}>Create</button>
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
                <div>
                    <label htmlFor="productname" className="mr-3">Name:</label>
                    <input type="text" id="productname" name="productname" />
                    <button className="btn-secondary" onClick={() => action.requestProductsByName(document.getElementById("productname").value, parseInt(action.match.params.page, 5) || 0)}>Find</button>
                    <table className='table table-striped'>
                        <thead>
                            <tr>
                                <th></th>
                                <th>Name</th>
                                <th>Price</th>
                                <th>Quantity</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            {props.products.map(pro =>
                                <tr key={pro.id}>
                                    <td>
                                        <button className="btn btn-info" onClick={() => { window.location.href = "/ProductEdit?id=" + pro.id}}>Detail update</button>
                                    </td>
                                    <td>
                                        <input type="text" id={`${pro.id}-name`} className="form-control" placeholder={pro.productName} onChange={() => { document.getElementById(`${pro.id}-btn`).hidden = (document.getElementById(`${pro.id}-name`).value.length + document.getElementById(`${pro.id}-price`).value.length + document.getElementById(`${pro.id}-quantity`).value.length === 0) ? true : false }} />
                                    </td>
                                    <td>
                                        <input type="text" id={`${pro.id}-price`} className="form-control" placeholder={pro.price} onChange={() => { document.getElementById(`${pro.id}-btn`).hidden = (document.getElementById(`${pro.id}-name`).value.length + document.getElementById(`${pro.id}-quantity`).value.length + document.getElementById(`${pro.id}-price`).value.length === 0) ? true : false }} />
                                    </td>
                                    <td>
                                        <input type="text" id={`${pro.id}-quantity`} className="form-control" placeholder={pro.quantity} onChange={() => { document.getElementById(`${pro.id}-btn`).hidden = (document.getElementById(`${pro.id}-name`).value.length + document.getElementById(`${pro.id}-quantity`).value.length + document.getElementById(`${pro.id}-price`).value.length === 0) ? true : false }} />
                                    </td>
                                    
                                    <td>
                                        <button className="btn btn-dark" id={`${pro.id}-btn`} onClick={() => {
                                            action.executeUpdateProduct(pro.id, (document.getElementById(`${pro.id}-name`).value.length !== 0) ? document.getElementById(`${pro.id}-name`).value : pro.productName,
                                                pro.desc,
                                                (document.getElementById(`${pro.id}-price`).value.length !== 0) ? document.getElementById(`${pro.id}-price`).value : pro.price,
                                                (document.getElementById(`${pro.id}-quantity`).value.length !== 0) ? document.getElementById(`${pro.id}-quantity`).value : pro.quantity,
                                                pro.createdDate,
                                                pro.creatorId,
                                                pro.pubished,
                                                pro.categoryId);
                                            document.getElementById(`${pro.id}-name`).value = "";
                                            document.getElementById(`${pro.id}-quantity`).value = "";
                                            document.getElementById(`${pro.id}-price`).value = "";
                                            document.getElementById(`${pro.id}-btn`).hidden = true;
                                        }} hidden>Update</button>
                                    </td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </div>
            );
        else 
            return <span>You are not allowed to do this!</span>
    }
    else
        return <span>Please login!</span>
}
export default connect(
    state => ({ products: state.products, user: state.oidc.user, categories: state.categories }),
    dispatch => bindActionCreators(actionCreators, dispatch)
)(Category);