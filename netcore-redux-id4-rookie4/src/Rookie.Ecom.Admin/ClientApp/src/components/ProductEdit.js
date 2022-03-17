import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { actionCreators } from '../store/ProductEdit';

class ProductEdit extends Component {
    componentDidMount() {
        // This method is called when the component is first added to the document
        this.ensureDataFetched();
    }

    componentDidUpdate() {
        // This method is called when the route parameters change
        /*this.ensureDataFetched();*/
    }

    ensureDataFetched() {
        const queryUrl = this.props.location.search;
        if (queryUrl.split("?id=").length === 2) {
            const id = queryUrl.split("?id=")[1];
            this.props.requestCategories();
            this.props.requestProduct(id);
            this.props.requestPicture(id);
        }
        
        /*this.props.requestCategories(page);*/
    }

    render() {
        const { categories, productedit, productpicture, user } = this.props;
        return (
            <div>
                <h1>Product detail edit</h1>
                <p>This component demonstrates fetching data from the server and working with URL parameters.</p>
                {user ? renderProductEditTable(productedit, categories, productpicture, this.props) : <span>You are not allowed to do this!</span>}
            </div>
        );
    }
}

function checkButton(categoryid) {
    document.getElementById("button").hidden = (document.getElementById("category").value !== categoryid || document.getElementById("price").value.length + document.getElementById("name").value.length + document.getElementById("desc").value.length + document.getElementById("quantity").value.length + document.getElementById("file").value.length !== 0) ? false : true;
}

function renderProductEditTable(product, categories, productpicture, props) {
    return (
        <div>
            {product.productedit.map(pro =>
                <div key={pro.id}>
                    <div className="container">
                        <div className="row">
                            <div className="col-sm border-right pr-4">
                                <span>Product name</span>
                                <br />
                                <input type="text" id="name" placeholder={pro.productName} className="mb-3" onChange={() => checkButton(pro.categoryId)} />
                                <br />
                                <span>Description</span>
                                <br />
                                <textarea id="desc" name="comment" cols="30" rows="5" placeholder={pro.desc} className="form-control" onChange={() => checkButton(pro.categoryId)}></textarea>
                                <br />
                                <div className="container p-0">
                                    <div className="row">
                                        <div className="col-sm">
                                            <span>Price</span>
                                            <br />
                                            <input type="text" id="price" placeholder={pro.price} onChange={() => checkButton(pro.categoryId)} />
                                        </div>
                                        <div className="col-sm">
                                            <span>Quantity</span>
                                            <br />
                                            <input type="text" id="quantity" placeholder={pro.quantity} onChange={() => checkButton(pro.categoryId)}/>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div className="col-sm ml-2">
                                <span>Category name</span>
                                <br />
                                <select className="custom-select border shadow-sm mb-3" id="category" onChange={() => checkButton(pro.categoryId)}>
                                    {categories.categories.map(cat => cat.id === pro.categoryId ?
                                        <option selected key={cat.categoryName} value={cat.id}>{cat.categoryName}</option> :
                                        <option key={cat.categoryName} value={cat.id}>{cat.categoryName}</option>
                                    )}
                                </select>
                                <span>Picture:</span>
                                <input type="file" id="file" name="file" className="ml-3 mb-2" onChange={() => checkButton(pro.categoryId)}/>
                                <br />
                                {productpicture.productpicture.map(pic => <div key={pic.id}>
                                    <img src={"https://localhost:5011/product/" + pic.pictureUrl} style={{ width: 200 + "px", height: 200 + "px" }}></img>
                                    <input type="text" defaultValue={pic.creatorId} id="pictureCreatorId" hidden />
                                    <input type="text" defaultValue={pic.createdDate} id="pictureCreatedDate" hidden />
                                </div>
                                )}
                                    
                            </div>
                        </div>
                    </div>
                    <div className="text-center mt-3">
                        <button id="button" className="btn btn-dark" onClick={() => props.executeUpdateProductDetail(props.location.search.split("?id=")[1],
                            document.getElementById("name").value !== "" ? document.getElementById("name").value : pro.productName,
                            document.getElementById("desc").value !== "" ? document.getElementById("desc").value : pro.desc,
                            document.getElementById("price").value !== "" ? document.getElementById("price").value : pro.price,
                            document.getElementById("quantity").value !== "" ? document.getElementById("quantity").value: pro.quantity,
                            document.getElementById("file").files[0],
                            document.getElementById("category").value,
                            pro.createdDate,
                            pro.creatorId,
                            document.getElementById("pictureCreatorId").value,
                            document.getElementById("pictureCreatedDate").value)} hidden>Update</button>
                    </div>
                </div>
            )}
        </div>
    );
}

export default connect(
    state => ({ categories: state.categories, productedit: state.productedit, productpicture: state.productpicture, user: state.oidc.user }),
    dispatch => bindActionCreators(actionCreators, dispatch)
)(ProductEdit);