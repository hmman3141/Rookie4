import { timers } from "jquery";
import axios from "axios";

const receiveProduct = 'RECEIVE_PRODUCT';
const receiveCategories = 'RECEIVE_CATEGORIES';
const receivePictures = 'RECEIVE_PICTURES';

const doneExecute = 'DONE_EXECUTE';

const localhost = 'https://localhost:5011/api/Product';
const initialState = { productedit:[] ,categories: [], productpicture:[] };

function createGuid() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}

export const actionCreators = {
    requestProduct: (id) => async (dispatch, getState) => {
        const response = await axios.get(localhost + "/all/" + id);
        const productedit = response.data;
        dispatch({ type: receiveProduct, productedit });
    },

    requestCategories: () => async (dispatch, getState) => {

        const response = await axios.get('https://localhost:5011/api/Category');
        const categories = response.data;
        dispatch({ type: receiveCategories, categories });
    },

    requestPicture: id => async (dispatch, getState) => {
        const response = await axios.get('https://localhost:5011/api/ProductPicture/product/'+id);
        const productpicture = response.data;
        dispatch({ type: receivePictures, productpicture });
    },

    executeUpdateProductDetail: (id, name, desc, price, quantity, file, categoryid, productcreatedDate, productcreatorId, productpiccreatorId, productpiccreatedDate) => async (dispatch, getState) => {
            if (typeof (file) != "undefined") {
            const form = new FormData();
            form.append("file", file)

            await axios.post(localhost + "/File", form,
                {
                    headers: { "Content-Type": "multipart/form-data" }
                });

            await axios.put('https://localhost:5011/api/ProductPicture', {
                "id": createGuid(),
                "createdDate": productpiccreatedDate,
                "updatedDate": new Date().toISOString,
                "creatorId": productpiccreatorId,
                "productId": id,
                "pubished": true,
                "pictureUrl": file.name,
                "title": "string"
            });
        }
        
        await axios.put(localhost, {
            "id": id,
            "createdDate": productcreatedDate,
            "updatedDate": new Date().toISOString,
            "creatorId": productcreatorId,
            "pubished": true,
            "productName": name,
            "isFeatured": true,
            "categoryId": categoryid,
            "desc": desc,
            "price": parseInt(price),
            "quantity": parseInt(quantity)
        });

        alert("Success!");
        window.location.href = "/Product";
    }
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === receiveCategories) {
        return {
            ...state,
            categories: action.categories,
        };
    }

    if (action.type === receiveProduct) {
        return {
            ...state,
            productedit: action.productedit,
        };
    }

    if (action.type === receivePictures) {
        return {
            ...state,
            productpicture: action.productpicture,
        };
    }

    return state;
};
