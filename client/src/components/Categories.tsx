import React from "react";
import { useDispatch, useSelector } from "react-redux";
import {
  filterSelector,
  setActiveCategoryIndex,
} from "../redux/slices/filterSlice";

const categories: string[] = [
  "All",
  "Meat",
  "Vegetarian",
  "Grilled",
  "Spicy",
  "Closed",
];

const Categories: React.FC = () => {
  const { activeCategoryIndex } = useSelector(filterSelector);
  const dispatch = useDispatch();

  return (
    <div className="categories">
      <ul>
        {categories.map((categoryName, index) => (
          <li
            key={index}
            className={activeCategoryIndex === index ? "active" : ""}
            onClick={() => dispatch(setActiveCategoryIndex(index))}
          >
            {categoryName}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default Categories;
