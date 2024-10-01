import React,{useEffect, useState} from 'react'
import Grid from '@mui/material/Grid';
import LandscapeVideoComponent from './LandscapeVideoComponent';
import BrowseMoreLandscapeComponent from '../VideoComponents/BrowseMoreLandscapeComponent';

const LandscapeVideo = ({videos, isLoadMoreEnabled, setLandscapeClickedTab, isWinnerVideos}) => {
  
  
  const [isOneVideo, setIsOneVideo] = useState(false);

  const handleLandscapeClickedTab = () => {
      setLandscapeClickedTab();
  }

  useEffect(() => {
    if(videos?.length == 1){
      setIsOneVideo(true)
    }
    
  }, []);

  return (
    <Grid container spacing={2}>
        
        {videos?.map((video, index) => (
          
            <Grid item key={index} xs={12} md={6} lg={3}>
                <LandscapeVideoComponent video={video} isOneVideo={isOneVideo}/>
            </Grid>
        ) )}

          {isLoadMoreEnabled && (
            <Grid  item xs={12} md={6} lg={3}>
                  <BrowseMoreLandscapeComponent setLandscapeClickedTab={handleLandscapeClickedTab}/>
              </Grid>
          )}
        
    </Grid>
  )
}

export default LandscapeVideo
