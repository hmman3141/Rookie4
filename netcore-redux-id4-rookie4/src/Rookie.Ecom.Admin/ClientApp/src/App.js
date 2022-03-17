import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Category from './components/Category';
import Product from './components/Product'
import ProductEdit from './components/ProductEdit'
import User from './components/User'
import Counter from './components/Counter';
import FetchData from './components/FetchData';

import CallbackPage from './components/callback/CallbackPage';
import ProfilePage from './components/profile/ProfilePage';

export default () => (
    <Layout>
        <Route exact path="/" component={Home} />
        <Route path="/counter" component={Counter} />
        <Route path="/category/:page?" component={Category} />
        <Route path="/product/:page?" component={Product} />
        <Route path="/productEdit" component={ProductEdit} />
        <Route path="/user/:page?" component={User} />
        <Route path="/fetch-data/:startDateIndex?" component={FetchData} />

        <Route path="/profile" component={ProfilePage} />
        <Route path="/callback" component={CallbackPage} />
    </Layout>
);
