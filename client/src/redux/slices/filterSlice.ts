import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {RootState} from "../store";

export type SortPropertyType = 'rating' | 'title' | 'price' | '-rating' | '-title' | '-price'

export type SortType = {
  name: string;
  sortBy: SortPropertyType
}

// export type SetFiltersType = {
//   activeCategoryIndex: number;
//   sort: SortType;
//   currentPage: number;
// }

interface FilterSliceState {
  searchValue?: string;
  activeCategoryIndex: number;
  sort: SortType;
  currentPage: number;
}

export const defaultSort: SortType = {
  name: "popularity ▼",
  sortBy: "-rating",
}

const initialState: FilterSliceState = {
  searchValue: "",
  activeCategoryIndex: 0,
  sort: defaultSort,
  currentPage: 1,
};

export const filterSlice = createSlice({
  name: "filters",
  initialState: initialState,
  reducers: {
    setActiveCategoryIndex: (state, action: PayloadAction<number>) => {
      state.activeCategoryIndex = action.payload;
      state.currentPage = 1;
    },

    setSort: (state, action: PayloadAction<SortType>) => {
      state.sort = action.payload;
      state.currentPage = 1;
    },

    setCurrentPage: (state, action: PayloadAction<number>) => {
      state.currentPage = action.payload;
    },

    setSearchValue(state, action: PayloadAction<string>) {
      state.searchValue = action.payload;
      state.currentPage = 1;
    },

    setFilters(state, action: PayloadAction<FilterSliceState>) {
      state.currentPage = Number(action.payload.currentPage);
      state.sort = action.payload.sort;
      state.activeCategoryIndex = Number(action.payload.activeCategoryIndex);
    },
  },
});

export const filterSelector = (state: RootState) => state.filters;

export const filterReducer = filterSlice.reducer;
export const { setActiveCategoryIndex, setSort, setCurrentPage, setFilters, setSearchValue } =
  filterSlice.actions;
