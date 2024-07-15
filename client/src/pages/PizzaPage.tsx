import React, {useEffect, useState} from "react";
import {useNavigate, useParams} from "react-router";
import axios from "axios";
import {BASE_URL} from "../settings";

interface IPizza {
    imageUrl: string;
    title: string;
    price: number;
}

const PizzaPage: React.FC = () => {
  const { id } = useParams();
  const navigate = useNavigate()
  const [pizza, setPizza] = useState<IPizza>()

  useEffect(() => {
    async function getPizza() {
      try {
        const { data } = await axios.get(BASE_URL + 'pizza/' + id,);
          setPizza(data)

      } catch (e) {
          alert('Error while getting pizza')
          navigate('/')
          console.log(e);
      }
    }

    getPizza();
  }, []);

  if (!pizza) {
    return (
      <div className="container" style={{ textAlign: "center" }}>
        <h1>Loading...</h1>
      </div>
    );
  }

  return (
    <div className="container" style={{ textAlign: "center" }}>
      <img src={pizza.imageUrl} className="pizza-block__image" />
      <h2>{pizza.title}</h2>
      <p>
        Lorem ipsum dolor sit amet, consectetur adipisicing elit. Eligendi, hic.
      </p>
      <h4>{pizza.price} euro</h4>
    </div>
  );
};

export default PizzaPage;
