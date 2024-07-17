import './App.css';
import React from 'react';
import MatchHistoryByUid from './MatchHistoryByUid';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import SummonerInfo from './components/SummonerInfo';
import ChampionsList from './components/Champion';

const App = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<SummonerInfo />} />
      </Routes>
    </Router>
  );
};

export default App;
