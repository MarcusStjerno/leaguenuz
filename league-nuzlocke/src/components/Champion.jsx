import React, { useState, useEffect } from 'react';
import axios from 'axios';

const ChampionsList = () => {
    const [champions, setChampions] = useState([]);

    useEffect(() => {
        const fetchChampions = async () => {
            try {
                const response = await axios.get('http://localhost:5000/api/champs');
                setChampions(response.data);
            } catch (error) {
                console.error("Error fetching the champions:", error);
            }
        };

        fetchChampions();
    }, []);

    return (
        <div>
            <h1>League of Legends Champions</h1>
            <ul>
                {champions.map(champion => (
                    <li key={champion.id}>
                        <h2>{champion.name}</h2>
                        <p>{champion.title}</p>
                        <p>{champion.blurb}</p>
                        <img src={champion.image} alt={champion.name} />
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default ChampionsList;
