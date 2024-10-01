import React, {useState} from 'react'
import {Grid } from '@mui/material';
import SkeletonElements from '../VideoComponents/SkeletonElements.js';
import LandscapeVideo from './LandscapeVideo';
import { Box } from '@mui/system'

    
const LandscapePageWithLoadMore = ({videosDetails, videoDetailsLoading, isWinnerVideos}) => {
  
    const [displayCount, setDisplayCount] = useState(24);
    const handleShowMore = () => {
        setDisplayCount(displayCount + 24);
      }

    return (

        <Grid container>
        {videoDetailsLoading ? (
          <SkeletonElements/>
        ):
        (
            videosDetails.length > 0 && (
            <Grid item style={{paddingTop:'2vh'}}>
                <LandscapeVideo videos = {videosDetails.slice(0, displayCount)} isWinnerVideos = {isWinnerVideos}/>
            </Grid>
            )
          
        )}
  
        {videosDetails.length > displayCount && (
          
           <Grid container
           marginTop='5vh'
           onClick={handleShowMore}
           sx={{
             cursor: 'pointer',
             transition: 'background-color 0.3s', // Optional: Smooth transition effect
             ':hover': {
               backgroundColor: '#1976D2', // Change color on hover
             },
             background:'#CD5D3D',
             width:'100%',
            marginBottom:'10vh'
           }}
           alignContent={'center'}
           alignItems={'center'}
           >
                 <Box sx={{ height:'5vh' , width:'100%',  border: 1, borderColor: 'grey.500', 
                 display: 'flex',
                 justifyContent: 'center',
                 alignItems: 'center'}}>
                     <div className='textversionBrowseMore' style={{ color: 'white' }}>Browse More</div>
               </Box>
           </Grid>
        )}
        </Grid>
        )}

export default LandscapePageWithLoadMore
