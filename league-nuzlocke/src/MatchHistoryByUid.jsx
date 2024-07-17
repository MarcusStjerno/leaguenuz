import React from 'react';
import DataAccess from './dataaccess/dataaccess.jsx';

const MatchHistoryByUid = () => {
  // Mock accountInfo data for testing purposes
  const accountInfo = {
    gameName: 'lulufizz',
    tagline: 'EUW',
  };

  return (
    <div>
      <DataAccess accountInfo={accountInfo} />
      <p>asaddsa</p>
    </div>
  );
};

export default MatchHistoryByUid;
