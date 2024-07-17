/*import react, { useeffect, usestate } from 'react';

const dataaccess = ({ accountinfo }) => {
  const [puuid, setpuuid] = usestate(null);

  const getpuuid = async (summonername, tagline) => {
    try {
      const response = await fetch(`http://localhost:5122/riot/account/v1/accounts/by-riot-id/{gamename}/{tagline}`);
      const data = await response.json();

      if (response.ok) {
        setpuuid(data.puuid);
      } else {
        console.error(`error fetching summoner puuid: ${data.message}`);
      }
    } catch (error) {
      console.error('error fetching summoner puuid:', error.message);
    }
  };

  useeffect(() => {
    if (accountinfo) {
      getpuuid(accountinfo.summonername, accountinfo.tagline);
    }
  }, [accountinfo]);

  return (
    <div>
      <h2>data access component</h2>
      <p>summoner name: {accountinfo.summonername}</p>
      <p>tagline: {accountinfo.tagline}</p>
      {puuid && <p>puuid: {puuid}</p>}
    </div>
  );
};

export default dataaccess;*/

import React, { useEffect, useState } from 'react';

const DataAccess = ({ accountInfo }) => {
  const [puuid, setPuuid] = useState(null);
  const [error, setError] = useState(null);

  const fetchData = async (gameName, tagLine) => {
    try {
      const response = await fetch(`http://localhost:5000/api/summoner`);
      if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}`);
      }
      const data = await response.json();
      setPuuid(data.puuid); // Assuming your API returns JSON with a 'puuid' property 
      setError(null);
    } catch (error) {
      console.error('Error fetching data:', error.message);
      setError(`Error fetching data: ${error.message}`);
    }
  };

  useEffect(() => { 
    if (accountInfo && accountInfo.gameName && accountInfo.tagline) {
      fetchData(accountInfo.gameName, accountInfo.tagline);
    }
  }, [accountInfo]);

  return (
    <div>
      <h2>Data Access Component</h2>
      {accountInfo ? (
        <>
          <p>Summoner Name: {accountInfo.gameName}</p>
          <p>Tagline: {accountInfo.tagline}</p>
          {error ? (
            <p>{error}</p>
          ) : (
            puuid ? (
              <p>PUUID: {puuid}</p>
            ) : (
              <p>Loading PUUID...</p>
            )
          )}
        </>
      ) : (
        <p>No account information available.</p>
      )}
    </div>
  );
};

export default DataAccess;
