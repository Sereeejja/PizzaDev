import { configureStore } from "@reduxjs/toolkit";
import { filterReducer } from "./slices/filterSlice";
import { cartReducer } from "./slices/cartSlice";
import { pizzasReducer } from "./slices/pizzasSlice";
import {useDispatch} from "react-redux";
import {appReducer} from "./slices/appSlice";

export const store = configureStore({
  reducer: {
    filters: filterReducer,
    cart: cartReducer,
    pizza: pizzasReducer,
    app: appReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>

type AppDispatch = typeof store.dispatch
export const useAppDispatch = () => useDispatch<AppDispatch>()
