import React from 'react'
import {Skeleton,Box } from '@mui/material';

const SkeletonElements = () => {

    
    // Detect the device width and set the iframe height accordingly
    const isBigScreen = window.innerWidth > 768;
    const iframeHeight = isBigScreen ? "BigScreen" : "smallScreen";
    

    const skeletonWidth = iframeHeight === "BigScreen" ? 160 : 90;
    const skeletonHeight = iframeHeight === "BigScreen" ? 285 : 150;
    const skeletonCount = 9; // Number of Skeleton elements


  return (
   Array.from({ length: skeletonCount }).map((_, index) => (
     <Box p={1}>
       <Skeleton key={index} variant="rectangular" width={skeletonWidth} height={skeletonHeight} />
     </Box>
   )))
}

export default SkeletonElements
