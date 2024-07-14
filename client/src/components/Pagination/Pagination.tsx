import React from "react";
import ReactPaginate from "react-paginate";
import styles from "./Pagination.module.scss";
import {filterSelector, setCurrentPage} from "../../redux/slices/filterSlice";
import {useDispatch, useSelector} from "react-redux";
import {pizzaSelector} from "../../redux/slices/pizzasSlice";

const Pagination: React.FC = () => {
  const dispatch = useDispatch();
  const {pages} = useSelector(pizzaSelector);
  const {currentPage} = useSelector(filterSelector)

  return (
    <ReactPaginate
      className={styles.root}
      breakLabel="..."
      nextLabel=">"
      onPageChange={(e) => dispatch(setCurrentPage(e.selected + 1))}
      pageRangeDisplayed={4}
      pageCount={pages}
      previousLabel="<"
      renderOnZeroPageCount={null}
      forcePage={currentPage - 1}
    />
  );
};

export default Pagination;
