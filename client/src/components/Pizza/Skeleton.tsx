import React from "react";
import ContentLoader from "react-content-loader";

const Skeleton:React.FC = (props) => {
  return (
    <ContentLoader
      className={"pizza-block"}
      speed={2}
      width={280}
      height={466}
      viewBox="0 0 280 466"
      backgroundColor="#f3f3f3"
      foregroundColor="#ecebeb"
      {...props}
    >
      <rect x="0" y="263" rx="10" ry="10" width="280" height="25" />
      <rect x="0" y="301" rx="10" ry="10" width="280" height="45" />
      <rect x="0" y="356" rx="10" ry="10" width="108" height="39" />
      <rect x="120" y="356" rx="10" ry="10" width="161" height="40" />
      <circle cx="135" cy="136" r="120" />
    </ContentLoader>
  );
};

export default Skeleton;
