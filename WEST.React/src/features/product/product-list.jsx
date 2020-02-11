import React from 'react';

export default class ProductList extends React.Component {
    state = {
        products: [
            { id: 1, name: 'DJ Screw - June 27', price: 1000000 },
            { id: 2, name: 'DJ Screw - My mind went blank', price: 1000000 },
        ]
    }

    renderProduct(product: { id: Number, name: String, price: String }) {
        return (
            <div key={product.id}>
                {product.name} - #{product.id} - {product.price}
            </div>
        )
    }

    render() {
        return (
            <div>
                {
                    this.state.products.map(product => {
                        return this.renderProduct(product)
                    })
                }
            </div>
        )
    }
}