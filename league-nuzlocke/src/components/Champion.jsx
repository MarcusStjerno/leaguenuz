import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './champion.css'; // Import the CSS file for styling

const ChampionsList = () => {
    const [champions, setChampions] = useState([]);

    useEffect(() => {
        const fetchChampions = async () => {
            try {
                const response = await axios.get('http://localhost:5122/api/champions'); // Ensure the endpoint is correct
                setChampions(response.data);
            } catch (error) {
                console.error("Error fetching the champions:", error);
            }
        };

        fetchChampions();
    }, []);

    return (
        <div>
            <center><h1>Lol nuzlocke</h1></center>
            <div className="champion-grid">
                
                {champions.map(champion => (
                    <div key={champion.id} className="champion-card">
                        <img src={champion.imageUrl} alt={champion.name} className="champion-image" />
                    </div>
                ))}
            </div>
        </div>
    );
};

export default ChampionsList;
