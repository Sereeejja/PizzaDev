import "./App.css";
import "./scss/app.scss";
import Home from "./pages/Home";
import NotFound from "./pages/NotFound";
import { Route, Routes } from "react-router";
import Cart from "./pages/Cart";
import PizzaPage from "./pages/PizzaPage";
import MainLayout from "./layouts/MainLayout";
import {store, useAppDispatch} from "./redux/store";
import {useEffect} from "react";
import {useDispatch} from "react-redux";
import {fetchCartItemsStatus} from "./redux/slices/cartSlice";

function App() {
    const dispatch = useAppDispatch();
    useEffect(() => {
        dispatch(fetchCartItemsStatus());
    }, [])
  return (
    <Routes>
      <Route path={'/'} element={<MainLayout />}>
        <Route path="" element={<Home />} />
        <Route path="cart" element={<Cart />} />
        <Route path="*" element={<NotFound />} />
        <Route path={"pizza/:id"} element={<PizzaPage />} />
      </Route>
    </Routes>
  );
}

export default App;
