import React from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import ProductList from './product/product-list';

export default function App() {
    return (
        <Router>
            <Switch>
                <Route exact path="/" component={ProductList}></Route>
                <Route path="/products" component={ProductList}></Route>
            </Switch>
        </Router>
    );
}