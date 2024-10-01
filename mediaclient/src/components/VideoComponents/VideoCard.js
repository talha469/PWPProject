import React from 'react';
import { Link } from "react-router-dom";
import {Card, CardContent, CardMedia, Box, Skeleton, Tooltip } from "@mui/material";

const linkStyles = {
  textDecoration: "none", // Remove underlines
  color: "inherit", // Inherit the color from the parent
};


const issmallScreen = window.innerWidth <= 768;
const isMediumScreen = window.innerWidth > 768 && window.innerWidth <= 1024;

const VideoCard = ({ videoDetails}) => (
  
  <Card className='bottomnavbar_Theme' sx={{ width: issmallScreen ? '90vw' : isMediumScreen ? '80vw' : '400px' , height: issmallScreen ? '100%' : isMediumScreen ? '150px' : '110px', boxShadow: "none" }}>
    <Link to={videoDetails?.videoId ? `/video/${videoDetails?.id}` : `/video/cV2gBU6hKfY`} style={linkStyles}>
      <Box className="bottomnavbar_Theme" display="flex">
        {videoDetails?.videoId ? (
          
          <CardMedia
            className='bottomnavbar_Theme'
            image = {`https://vz-6258ec42-3d0.b-cdn.net/${videoDetails?.videoId}/thumbnail_1.jpg`}
            alt={videoDetails?.videoThumbnail?.title}
            sx={{ flex: '80%', width: { xs: '100%', sm: '400px' }, height: issmallScreen ? '100%' : isMediumScreen ? '150px' : '110px', borderRadius: 4 }}
          />
        ) : (
          <Skeleton className="bottomnavbar_Theme" variant="rectangular" sx={{ flex: '70%', width: { xs: '100%', sm: '358px' }, height: { xs: 180, sm: 100 }, borderRadius: 4 }} />
        )}
        
        <CardContent className='bottomnavbar_Theme' sx={{ flex: '67%' }}>
        <Tooltip title={`${videoDetails?.tags}, ${videoDetails?.description}`}>
            <div className='textversion8'>{videoDetails?.tags}</div>
            <div className='textversion8'>
        {videoDetails?.description && videoDetails?.description.length > 40
            ? `${videoDetails?.description.substring(0, 40)}...`
            : videoDetails?.description}

         </div>

         
            
        </Tooltip> 
          {/* <TagsandDescription video={videoDetails} isRelatedVideos={true}/> */}
        
        
        </CardContent>
        
      </Box>
    </Link>
  </Card>
);

export default VideoCard;
