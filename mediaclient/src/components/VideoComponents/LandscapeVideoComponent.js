import { Tooltip } from '@mui/material'
import React, {useRef, useState, useEffect} from 'react'
import { Link } from 'react-router-dom';
import Grid from '@mui/material/Grid';
import TagsandDescription from './TagsandDescription';
import Hls from 'hls.js';

const LandscapeVideoComponent = ({video, isOneVideo}) => {

  const [isHovered, setIsHovered] = useState(false);
  const isBigScreen = window.innerWidth > 1024;
  const isSmallScreen = window.innerWidth < 768;
  
  const videoRef = useRef(null);

  useEffect(() => {
    if (Hls.isSupported()) {
      const hls = new Hls();
      hls.loadSource(video?.url);
      hls.attachMedia(videoRef.current);
      return () => {
        hls.destroy();
      };
    } else if (videoRef.current.canPlayType('application/vnd.apple.mpegurl')) {
      videoRef.current.src = video?.url;
    }
  }, [video?.url]);

  //Actions
  const handleMouseEnter = () => {
    setIsHovered(true)
    if(videoRef.current){
      videoRef.current.muted = true;
      videoRef.current.play();
    }
  }

  const handleMouseLeave = () => {
    setIsHovered(false)
    if(videoRef.current){
      videoRef.current.pause();
    }
  }

  return (
    <Grid container>
            
              <Grid item container direction={{xs:'row', md:'row', lg:'column'}} 
              onMouseEnter = {handleMouseEnter}
              onMouseLeave = {handleMouseLeave}
              
              sx={{
                cursor: isHovered ? 'pointer' : 'default'
              }}>
                
                   <Grid item xs={12}  md={12} lg={3} marginLeft={{xs:'0vw', md:'0vw'}} >
                   <Link to={`/video/${video?.id}`} style={{ textDecoration: 'none' }}>
                      <video ref = {videoRef}
                      width= {isOneVideo ? (isBigScreen ? '100%' : '100%') :  '100%'} height="100%"
                       style={{borderRadius: isSmallScreen ?'0px' : '10px', background:'black',objectFit: 'cover'}} />
                       </Link>
                  </Grid>

                <div style={{ display: 'flex', paddingTop:'5px'}}>
                  <div className='textversionShortChannelName' style={{ display: 'flex', alignItems: 'flex-start', marginBottom:'5px', marginLeft:'5px' }}>
                      <Tooltip title={`${video?.username}`}>              
                    </Tooltip>

                      <Link to={`/video/${video?.id}`} style={{ textDecoration: 'none' }}>
                        <TagsandDescription video={video} cssClassName={"videoTagsandDescLandscape"}/>
                      </Link>
                  </div>
                </div>
               
              </Grid>
    </Grid>
  )
}

export default LandscapeVideoComponent
