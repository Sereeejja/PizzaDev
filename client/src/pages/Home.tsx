import React, { useContext, useEffect, useRef } from "react";
import qs from "qs";
import Categories from "../components/Categories";
import Sort, {options, OptionType} from "../components/Sort";
import Skeleton from "../components/Pizza/Skeleton";
import PizzaBlock from "../components/Pizza/PizzaBlock";
import Pagination from "../components/Pagination/Pagination";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router";
import {defaultSort, filterSelector, setFilters} from "../redux/slices/filterSlice";
import {FetchPizzaArgs, fetchPizzas, pizzaSelector} from "../redux/slices/pizzasSlice";
import {useAppDispatch} from "../redux/store";

const Home:React.FC = () => {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();

  const isSearch = useRef(false);
  const isMounted = useRef(false);

  const { items, status } = useSelector(pizzaSelector);
  const { activeCategoryIndex, sort, currentPage, searchValue } = useSelector(filterSelector);

  const getPizzas = async () => {
    const category = activeCategoryIndex > 0 ? activeCategoryIndex : undefined;
    const sortBy = sort.sortBy.replace("-", "");
    const order = sort.sortBy.includes("-") ? "desc" : "asc";
    const search = searchValue ? searchValue : undefined;

    dispatch(
      fetchPizzas({ category, sortBy, search, currentPage, order }),
    );
  };

  React.useEffect(() => {
    if (isMounted.current) {
      const queryString = qs.stringify({
        sortProperty: sort.sortBy,
        category: activeCategoryIndex,
        currentPage,
      });
      navigate(`?` + queryString);
    }
    isMounted.current = true;
  }, [activeCategoryIndex, sort, searchValue, currentPage]);

  React.useEffect(() => {
    if (window.location.search) {
      const params = (qs.parse(window.location.search.substring(1)) as unknown) as FetchPizzaArgs;
      const sortOption = options.find(
        (obj) => obj.sortBy === params.sortBy,
      ) || defaultSort;
      dispatch(
        setFilters({
          ...params,
          sort: sortOption,
          activeCategoryIndex: params.category ? params.category : 0,
        }),
      );
      isSearch.current = true;
    }
  }, []);

  useEffect(() => {
    window.scrollTo(0, 0);

    if (!isSearch.current) {
      getPizzas();
    }
    isSearch.current = false;
  }, [activeCategoryIndex, sort, searchValue, currentPage]);

  return (
    <div className="container">
      <div className="content__top">
        <Categories />
        <Sort />
      </div>
      <h2 className="content__title">All pizzas</h2>
      {status === "error" ? (
        <div className={"content__error-info"}>
          <h2>Error getting pizzas 😕</h2>
          <p>Maybe you have problem with the internet!</p>
        </div>
      ) : (
        <div className="content__items">
          {status === "loading"
            ? [...Array(9)].map((_, index) => <Skeleton key={index} />)
            : items.map((pizza) => <PizzaBlock key={pizza.id} {...pizza} />)}
        </div>
      )}

      <Pagination />
    </div>
  );
};

export default Home;
