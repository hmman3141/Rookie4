import { timers } from "jquery";
import axios from "axios";

const requestProductType = 'REQUEST_PRODUCTS';
const receiveProductType = 'RECEIVE_PRODUCTS';
const receiveCategoryType = 'RECEIVE_CATEGORIES';
const addProduct = 'ADD_PRODUCT';
const executeAddProduct = 'EXECUTE_ADD_PRODUCT';
const updateProducts = 'UPDATE_PRODUCTS';
const doneExecute = 'DONE_EXECUTE';

const localhost = 'https://localhost:5011/api/Product';
const initialState = { products: [], categories: [], isLoading: false, isExcuting: false, isAdding: false, isUpdating: false };

function createGuid() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}

export const actionCreators = {
    requestCategories: () => async (dispatch, getState) => {

        const url = `api/Category`;
        const response = await axios(url);
        const categories = response.data;
        dispatch({ type: receiveCategoryType, categories });
    },
    requestProducts: page => async (dispatch, getState) => {
        if (page === getState().products.page) {
            // Don't issue a duplicate request (we already have or are loading the requested data)
            return;
        }

        dispatch({ type: requestProductType, page });

        const url = `api/Product/find?page=${page}`;
        const response = await fetch(url);
        const data = await response.json();
        const products = data.items;

        dispatch({ type: receiveProductType, page, products });
    },
    requestProductsByName: (name, page) => async (dispatch, getState) => {
        if (name !== "") {
            const response = await axios.get(localhost + "/containname/" + name);
            const products = response.data;
            dispatch({ type: receiveProductType, page, products });
        } else {
            dispatch({ type: requestProductType, page });

            const url = `api/Product/find?page=${page}`;
            const response = await fetch(url);
            const data = await response.json();
            const products = data.items;

            dispatch({ type: receiveProductType, page, products });
        }
        
    },
    viewProducts: page => async (dispatch) => {
        dispatch({ type: requestProductType, page });

        const url = `api/Product/find?page=${page}`;
        const response = await fetch(url);
        const data = await response.json();
        const products = data.items;

        dispatch({ type: receiveProductType, page, products });
        dispatch({ type: doneExecute })
    },
    addProducts: () => async (dispatch, getState) => {
        dispatch({ type: addProduct })
    },
    executeAddProduct: (name, desc, price, quantity, file, createid, categoryid) => async (dispatch, getState) => {
        if (name !== "" && desc !== "" && price !== "" && quantity !== "" && file !== null && createid !== "" && categoryid !== "") {
            var response = await axios.get(localhost + "/name/" + name);
            if (Object.keys(response.data).length) {
                alert("Duplicated product name!");
            } else {
                const form = new FormData();
                form.append("file", file)

                await axios.post(localhost + "/File", form,
                    {
                        headers: { "Content-Type": "multipart/form-data" }
                    });

                var productid = createGuid();

                await axios.post(localhost, {
                    "id": productid,
                    "createdDate": new Date().toISOString,
                    "updatedDate": new Date().toISOString,
                    "creatorId": createid,
                    "pubished": true,
                    "productName": name,
                    "isFeatured": true,
                    "categoryId": categoryid,
                    "desc": desc,
                    "price": parseInt(price),
                    "quantity": parseInt(quantity)
                });


                await axios.post('https://localhost:5011/api/ProductPicture', {
                    "id": createGuid(),
                    "createdDate": new Date().toISOString,
                    "updatedDate": new Date().toISOString,
                    "creatorId": createid,
                    "productId": productid,
                    "pubished": true,
                    "pictureUrl": file.name,
                    "title": "string"
                });

                alert("Success!")
            }
        }
        else {
            alert("Please fulfill the input!")
        }
    },
    updateProducts: () => async (dispatch, getState) => {
        dispatch({ type: updateProducts })
    },
    executeUpdateProduct: (id, name, desc, price, quantity, createdate, creatorid, pubished, categoryid) => async (dispatch, getState) => {
        await axios.put(localhost, {
            "id": id,
            "updatedDate": new Date().toISOString,
            "pubished": pubished,
            "createdDate": createdate,
            "creatorId": creatorid,
            "productName": name,
            "desc": desc,
            "price": parseInt(price),
            "quantity": parseInt(quantity),
            "isFeatured": true,
            "categoryId": categoryid
        });
        var page = getState().products.page;
        dispatch({ type: requestProductType, page });

        const url = `api/Product/find?page=${page}`;
        const response = await fetch(url);
        const data = await response.json();
        const products = data.items;
        dispatch({ type: receiveProductType, page, products });
    }
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === requestProductType) {
        return {
            ...state,
            page: action.page,
            isLoading: true
        };
    }

    if (action.type === receiveProductType) {
        return {
            ...state,
            page: action.page,
            products: action.products,
            isLoading: false
        };
    }

    if (action.type === receiveCategoryType) {
        return {
            ...state,
            page: action.page,
            categories: action.categories,
        };
    }

    if (action.type === addProduct) {
        return {
            ...state,
            isAdding: true,
            isExcuting: true,
            isUpdating: false
        }
    }

    if (action.type === updateProducts) {
        return {
            ...state,
            isUpdating: true,
            isExcuting: true,
            isAdding: false
        }
    }

    if (action.type === doneExecute) {
        return {
            ...state,
            isExcuting: false,
            isUpdating: false,
            isAdding: false
        }
    }

    return state;
};
