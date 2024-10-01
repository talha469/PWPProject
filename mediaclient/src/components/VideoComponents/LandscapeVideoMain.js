import React, { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import ReactPlayer from "react-player";
import { Typography, Box, Stack, Skeleton,Avatar  } from "@mui/material";
import CheckCircleIcon from "@mui/icons-material/CheckCircle";
import Rating from 'react-rating-stars-component';
import RelatedVideos from "./RelatedVideos";
import StarsIcon from '@mui/icons-material/Stars';
import axios from "axios";
import {toast,ToastContainer} from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import MainVideoFooter from "./MainVideoFooter";
import LandscapeVideoComponent from "../VideoComponents/LandscapeVideoComponent.js";
import LandscapeVideo from "../VideoComponents/LandscapeVideoComponent.js";
import api from "../../API/APIRequests.js"

const LandscapeVideoMain = ({userId}) => {
  const [videoDetail, setVideoDetail] = useState(null);
  const [relatedVideos, setRelatedVideos] = useState(null);
  const { id } = useParams();
  const [videoDetailsLoading, setVideoDetailsLoading] = useState(false)
  const [relatedVideosLoading, setRelatedVideosLoading] = useState(false)
  const [addVideoUrl, setAddVideoUrl] = useState('');

  const getToken = () => localStorage.getItem('JWTToken');

  useEffect(() => {
      const userSearchParams = {
        userId: userId
      }

      setVideoDetailsLoading(true)
      
      api.get('Video',id, userSearchParams)
        .then((result) => {
          setVideoDetail(result?.data)
          setVideoDetailsLoading(false);
          
          const addVideoHref = result?.controls?.['mumeta:get-all-videos']?.href;
          console.log(addVideoHref)
          setAddVideoUrl(addVideoHref);
      })
      .catch((error) => {
        setVideoDetailsLoading(false);
        console.log(error);
      });
    
    setRelatedVideosLoading(true)
     const urlSegment = addVideoUrl.split('/').pop(); 
    api.get(urlSegment)
    .then((result) => {
      setRelatedVideos(result?.data)
      setRelatedVideosLoading(false);
      
  })
  .catch((error) => {
    
    console.log(error);
  });
   }, [id]);

   // Detect the device width and set the iframe height accordingly
   const isBigScreen = window.innerWidth > 1024;
   const issmallScreen = window.innerWidth <= 768;
   const isMediumScreen = window.innerWidth > 768 && window.innerWidth <= 1024;
   const iframeHeight = isMediumScreen ? "MediumScreen" : isBigScreen ? "BigScreen" : "smallScreen";


return (
    <Box minHeight="90vh" marginLeft={{ lg: '9vw' }} marginRight={{lg:'9vw'}}>
      <Stack direction={{ xs: "column", md: "column", lg:'row' }}>
        <Box flex={1} flexDirection='column' position='relative' zIndex='100'>
          {!videoDetailsLoading ? (
          <Box sx={{ width: "100%", position: isBigScreen ? 'relative' :  "fixed", height: { iframeHeight }}} zIndex='200'>
          {iframeHeight === "BigScreen" && (
            <iframe
            className="bottomnavbar_Theme"
              src={`https://iframe.mediadelivery.net/embed/157447/${videoDetail?.videoId}?autoplay=false&loop=false&muted=false&preload=false`}
              loading="lazy"
              style={{
                borderRadius: '10px',
                border: 0,
                width: "100%",
                height: "72vh",
                paddingTop:'2vh',
                zIndex:'100'
              }}
              allow="accelerometer;gyroscope;autoplay;encrypted-media;picture-in-picture;"
              allowFullScreen={true}
            ></iframe>
          )}

          {iframeHeight === "smallScreen" && (
            <iframe
              src={`https://iframe.mediadelivery.net/embed/157447/${videoDetail?.videoId}?autoplay=false&loop=false&muted=false&preload=false`}
              loading="lazy"
              style={{
                border: 0,
                width: "100%",
                minHeight: "30vh",
                backgroundColor:'black'
              }}
              allow="accelerometer;gyroscope;autoplay;encrypted-media;picture-in-picture;"
              allowFullScreen={true}
            ></iframe>
          )}

            {iframeHeight === "MediumScreen" && (
            <iframe
              src={`https://iframe.mediadelivery.net/embed/157447/${videoDetail?.videoId}?autoplay=false&loop=false&muted=false&preload=false`}
              loading="lazy"
              style={{
                border: 0,
                width: "100%",
                height: "40vh",
                backgroundColor:'white'
              }}
              allow="accelerometer;gyroscope;autoplay;encrypted-media;picture-in-picture;"
              allowFullScreen={true}
            ></iframe>
          )}
             </Box>
             
              ) : (
             <Box sx={{ width: "100%", position: "Absolute", height: { iframeHeight }}}  top={{ xs: '8vh', md: '10vh' }}>
                 {iframeHeight === "BigScreen" ? (
                  <Skeleton variant="rectangular" width='100%' style={{height:'72vh'}} />
                 ) : iframeHeight === "MediumScreen" ? (
                  <Skeleton variant="rectangular" width='100%' style={{height:'68vh'}} />
                  ) :
                 (
                  <div style={{ width: '100%', height: '220px', backgroundColor: 'black', marginTop:'14px' }}>
                    {/* You can add your black loading screen content here */}
                </div>
                  ) }
             </Box>
          )}
          
          <Box>
            {!videoDetailsLoading && (
              <MainVideoFooter videoDetail={videoDetail}/>
            )}
              
          </Box>
      </Box>
      
      
      <Box px={isBigScreen ? '20px' : '0px'} py={'10px'} justifyContent="center" alignItems="center">
        {!relatedVideosLoading && (
          <RelatedVideos videos={relatedVideos} direction="column" /> 
          
        )}
          
      </Box>
      </Stack>
    </Box>
  );


};

export default LandscapeVideoMain;