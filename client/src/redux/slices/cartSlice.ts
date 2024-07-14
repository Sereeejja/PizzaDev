import {createAsyncThunk, createSlice, PayloadAction} from "@reduxjs/toolkit";
import {RootState} from "../store";
import axios from "axios";
import {BASE_URL} from "../../settings";

export type CartItem = {
  id: string;
  title: string;
  price: number;
  imageUrl: string;
  type: string;
  size: number;
  count: number;
};

type FetchCartItemsResponse = {
  id: number;
  totalPrice: number;
  pizzas: CartItem[];
}
export const fetchCartItemsStatus = createAsyncThunk<FetchCartItemsResponse>(
    "cart/fetchCartItemsStatus",
    async () => {
      console.log('dsadas')
      const { data } = await axios.get<FetchCartItemsResponse>(BASE_URL + "cart");
      console.log('dsad')
      return data;
    }
);

type AddItemCartArgs = {
  pizzaId: number;
  sizeId: number;
  typeId: number;
}
type DeleteItemCartArgs = {
  pizzaId: number;
  sizeId: number;
  typeId: number;
  deleteAll: boolean;
}
type AddItemCartResponse = {
  cartId: number;
  pizzaId: number;
  quantity: number;
  sizeId: number;
  typeId: number;
}

export const addItemToCart = createAsyncThunk<Boolean, AddItemCartArgs>(
    "cart/addItemToCart",
    async (payload: AddItemCartArgs, thunkAPI) => {
      const response = await axios.post<AddItemCartResponse>(BASE_URL + `cart/add/` + payload.pizzaId, {},
           {
            params: {
              sizeId: payload.sizeId,
              typeId: payload.typeId,
            }
          })
      if(response.status === 200) {
        thunkAPI.dispatch(fetchCartItemsStatus())
        return true
      }
      return false;
    }
)

export const removeItemCart = createAsyncThunk<boolean, DeleteItemCartArgs>(
    "cart/removeItem",
    async(payload: DeleteItemCartArgs, thunkAPI) => {
      let urlString = 'cart/'
      if(payload.deleteAll){
        urlString += 'removeAll/'
      }
      else{
        urlString += 'remove/'
      }
      const response = await axios.delete(BASE_URL + urlString + payload.pizzaId,
          {
            params: {
              sizeId: payload.sizeId,
              typeId: payload.typeId,
            }
          })

      if(response.status === 204){
        thunkAPI.dispatch(fetchCartItemsStatus())
        return true;
      }
      return false;
    }
)

export const clearCart = createAsyncThunk<boolean>(
    "cart/clearCart",
    async () => {
      const response = await axios.delete(BASE_URL + 'cart/clear')
      return response.status === 204;
    }
)

interface CartSliceState {
  totalPrice: number;
  items: CartItem[];
}

const initialState: CartSliceState = {
  items: [],
  totalPrice: 0,
};

export const cartSlice = createSlice({
  name: "cart",
  initialState: initialState,
  reducers: {
    addItem(state, action: PayloadAction<CartItem>) {
      const findItem = state.items.find(
        (item) => item.id === action.payload.id,
      );
      if (findItem) {
        findItem.count++;
      } else {
        state.items.push({
          ...action.payload,
          count: 1,
        });
      }
      state.totalPrice += action.payload.price;
    },
    removeItem: (state, action: PayloadAction<string>) => {
      const item = state.items.find((item) => item.id === action.payload);
      if (item){
        state.totalPrice -= item.price * item.count;
      }
      state.items = state.items.filter((item) => item.id !== action.payload);
    },
    clearItems: (state) => {
      state.items = [];
      state.totalPrice = 0;
    },
    incrementItem(state, action: PayloadAction<string>) {
      const item = state.items.find((item) => item.id === action.payload);
      if(item){
        item.count += 1;
        state.totalPrice += item.price;
      }
    },
    decrementItem(state, action: PayloadAction<string>) {
      const item = state.items.find((item) => item.id === action.payload);
      if(item){
        item.count -= 1;
        state.totalPrice -= item.price;
        if (!item.count) {
          state.items = state.items.filter((item) => item.id !== action.payload);
        }
      }
    },
  },
  extraReducers: builder => {
    builder.addCase(fetchCartItemsStatus.fulfilled, (state, action) => {
      state.totalPrice = action.payload.totalPrice;
      state.items = action.payload.pizzas;
    });
    builder.addCase(clearCart.fulfilled, (state) => {
      state.totalPrice = 0;
      state.items = [];
    })
  }
});

export const cartSelector = (state: RootState) => state.cart
export const cartItemsSelector = (state: RootState) => state.cart.items;

export const cartReducer = cartSlice.reducer;
export const { addItem, removeItem, clearItems, incrementItem, decrementItem } =
  cartSlice.actions;
