import React from "react";
import emptyCartImg from "../assets/img/empty-cart.png";
import { Link } from "react-router-dom";

const CartEmpty: React.FC = () => {
  return (
    <>
      <div className="cart cart--empty">
        <h2>Cart is empty 😕</h2>
        <p>
          Most likely, you haven't ordered a pizza yet.
          <br />
          To order a pizza, go to the main page.
        </p>
        <img src={emptyCartImg} alt="Empty cart" />
        <Link to="/" className="button button--black">
          <span>Return back</span>
        </Link>
      </div>
    </>
  );
};

export default CartEmpty;
