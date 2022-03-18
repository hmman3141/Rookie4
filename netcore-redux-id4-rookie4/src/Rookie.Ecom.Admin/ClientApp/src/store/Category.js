import { timers } from "jquery";
import axios from "axios";

const requestCategoryType = 'REQUEST_CATEGORIES';
const receiveCategoryType = 'RECEIVE_CATEGORIES';
const addCategory = 'ADD_CATEGORY';
const executeAddCategory = 'EXECUTE_ADD_CATEGORY';
const updateCategories = 'UPDATE_CATEGORIES';
const doneExecute = 'DONE_EXECUTE';

const localhost = 'https://localhost:5011/api/Category';
const initialState = { categories: [], isLoading: false, isExcuting: false, isAdding: false, isUpdating: false };

function createGuid() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}

export const actionCreators = {
    requestCategories: page => async (dispatch, getState) => {
        if (page === getState().categories.page) {
            // Don't issue a duplicate request (we already have or are loading the requested data)
            return;
        }

        dispatch({ type: requestCategoryType, page });

        const url = `api/Category/find?page=${page}`;
        const response = await fetch(url);
        const data = await response.json();
        const categories = data.items;
        dispatch({ type: receiveCategoryType, page, categories });
    },
    viewCategories: () => async (dispatch) => {
        dispatch({ type: doneExecute })
    },
    addCategories: () => async (dispatch, getState) => {
        dispatch({ type: addCategory })
    },
    executeAddCategory: (name, desc, iconname, createid) => async (dispatch, getState) => {
        if (name !== "" && desc !== "" && iconname !== "" && createid !== "") {
            const response = await axios.get(localhost+"/name/"+name);

            if (Object.keys(response.data).length) {
                alert("Duplicated category name!");
            } else {
                await axios.post(localhost, {
                    "id": createGuid(),
                    "createdDate": new Date().toISOString,
                    "updatedDate": new Date().toISOString,
                    "creatorId": createid,
                    "pubished": true,
                    "categoryName": name,
                    "desc": desc,
                    "imageUrl": iconname
                });
                alert("Success!");
                const url = `api/Category/find?page=0`;
                const responseGet = await fetch(url);
                const data = await responseGet.json();
                const categories = data.items;
                var page = 0;
                dispatch({ type: receiveCategoryType, page, categories });
            }
        }
        else {
            alert("Please fulfill the input!")
        }
    },
    updateCategories: () => async (dispatch, getState) => {
        dispatch({ type: updateCategories })
    },
    executeUpdateCategory: (id, name, desc, createdate, creatorid, pubished, imageurl) => async (dispatch, getState) => {
        const response = await axios.put(localhost, {
            "id": id,
            "updatedDate": new Date().toISOString,
            "pubished": pubished,
            "createdDate": createdate,
            "creatorId": creatorid,
            "imageUrl": imageurl,
            "categoryName": name,
            "desc": desc
        });
        var page = getState().categories.page;
        dispatch({ type: requestCategoryType, page });

        const url = `api/Category/find?page=${page}`;
        const responseGet = await fetch(url);
        const data = await responseGet.json();
        const categories = data.items;
        dispatch({ type: receiveCategoryType, page, categories });
    }
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === requestCategoryType) {
        return {
            ...state,
            page: action.page,
            isLoading: true
        };
    }

    if (action.type === receiveCategoryType) {
        return {
            ...state,
            page: action.page,
            categories: action.categories,
            isLoading: false
        };
    }

    if (action.type === addCategory) {
        return {
            ...state,
            isAdding: true,
            isExcuting: true,
            isUpdating: false
        }
    }

    if (action.type === updateCategories) {
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
