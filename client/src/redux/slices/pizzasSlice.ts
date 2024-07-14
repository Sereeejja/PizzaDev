import {createAsyncThunk, createSlice, PayloadAction} from "@reduxjs/toolkit";
import axios from "axios";
import {RootState} from "../store";
import {BASE_URL} from "../../settings";

export type FetchPizzaArgs = {
  category?: number;
  order: string;
  currentPage: number;
  search?: string;
  sortBy?: string;
}

type FetchPizzaResponse = {
  data: PizzaItem[];
  pages: number;
}

export const fetchPizzas = createAsyncThunk<FetchPizzaResponse, FetchPizzaArgs>(
  "pizza/fetchPizzasStatus",
  async (payload, thunkAPI) => {
    const {data} = await axios.get<FetchPizzaResponse>(
      BASE_URL + "pizza",
      {
        params: {
          category: payload.category,
          order: payload.order,
          page: payload.currentPage,
          search: payload.search,
          sortBy: payload.sortBy,
          limit: 4,
        },
      },
    );
    if (!data.data.length) {
      return thunkAPI.rejectWithValue("Pizzas is empty");
    }

    return thunkAPI.fulfillWithValue(data);
  },
);

type PizzaItem = {
  id: string;
  title: string;
  types: number[];
  imageUrl: string;
  price: number;
  sizes: number[];
}

interface PizzaSliceState {
  items: PizzaItem[];
  pages: number;
  status: Status;
}

export enum Status {
  LOADING = 'loading',
  ERROR = 'error',
  SUCCESS = 'success',
}

const initialState: PizzaSliceState = {
  items: [],
  pages: 0,
  status: Status.LOADING, // loading success error
};

export const pizzaSlice = createSlice({
  name: "pizza",
  initialState: initialState,
  reducers: {
    setItems(state, action: PayloadAction<PizzaItem[]>) {
      state.items = action.payload;
    },
  },
  extraReducers: (builder) => {
    builder.addCase(fetchPizzas.pending, (state) => {
      state.status = Status.LOADING
      state.items = [];
    });
    builder.addCase(fetchPizzas.fulfilled, (state, action) => {
      state.items = action.payload.data;
      state.pages = action.payload.pages;
      state.status = Status.SUCCESS
    });
    builder.addCase(fetchPizzas.rejected, (state, action) => {
      console.log(action.error.message);
      state.status = Status.ERROR
      state.pages = 0;
      state.items = [];
    });
  },
});

export const pizzaSelector = (state: RootState) => state.pizza;

export const pizzasReducer = pizzaSlice.reducer;
export const { setItems } = pizzaSlice.actions;
