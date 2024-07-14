import {createSlice} from "@reduxjs/toolkit";
import {RootState} from "../store";

interface AppSliceState {
    sizes: number[];
    types: string[];
}
const initialState: AppSliceState = {
    sizes: [26,30,40],
    types: ["Traditional", "Thin"],
}

const appSlice = createSlice({
    name: "app",
    initialState,
    reducers: {},
})

export const appSelector = (state: RootState) => state.app;
export const appReducer = appSlice.reducer;