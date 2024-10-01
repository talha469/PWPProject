import React, { useState, useEffect } from 'react';
import { Grid } from '@mui/material';
import api from "../../API/APIRequests.js";
import SkeletonElements from '../VideoComponents/SkeletonElements.js';

import LandscapePageWithLoadMore from '../VideoComponents/LandscapePageWithLoadMore.js';

const Home = () => {
  const [videosDetails, setVideosDetails] = useState([]);
  const [videoDetailsLoading, setVideoDetailsLoading] = useState(false)

  
  const getData = () => {
    setVideoDetailsLoading(true);
  
    
    api.get("Videos")
    .then((result) => {
      setVideosDetails(result?.data)
      setVideoDetailsLoading(false);
      
  })
  .catch((error) => {
    
    console.log(error);
  });
   
  };
  
  useEffect(() => {
    getData();
  }, []);

  return (
      
    <Grid container paddingLeft={{xs:'0px', md:'8vw'}} paddingRight={{xs:'0px', md:'8vw'}} paddingBottom={{xs:'8vh'}}>

    {videoDetailsLoading ? (
      <SkeletonElements/>
    ):
    (
      <Grid container>
        <Grid item marginBottom={2}>
          <LandscapePageWithLoadMore videosDetails={videosDetails} videoDetailsLoading={videoDetailsLoading} isWinnerVideos={true}/>
        </Grid>
       
      </Grid>
      
    )}
    </Grid>
    )}
export default Home;
