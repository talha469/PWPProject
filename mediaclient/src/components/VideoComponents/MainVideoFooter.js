import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { Typography, Box, Stack, Avatar } from "@mui/material";
import CheckCircleIcon from "@mui/icons-material/CheckCircle";
import Rating from 'react-rating-stars-component';
import StarsIcon from '@mui/icons-material/Stars';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import { useTheme } from '@mui/material/styles';
import useMediaQuery from '@mui/material/useMediaQuery';
import IconsBucket from '../ApplicationFiles/config';
import BookmarkBorderIcon from '@mui/icons-material/BookmarkBorder';
import BookmarkIcon from '@mui/icons-material/Bookmark';
import api from '../../API/APIRequests';
const MAX_DESCRIPTION_LENGTH = 40;

const MainVideoFooter = ({ videoDetail }) => {
    if(videoDetail){
        
    }
    const navigate = useNavigate();
    const [totalVotes, setTotalVotes] = useState(0)
    const theme = useTheme();
    const isXs = useMediaQuery(theme.breakpoints.only('xs'));
    const isSmOrMd = useMediaQuery(theme.breakpoints.up('sm'));
    const [isBookmarked, setIsBookMarked] = useState(false);

    const size = isXs ? 14 : isSmOrMd ? 30 : 0; // Set the size based on screen size

    const isBigScreen = window.innerWidth > 1024;
    const isMediumScreen = window.innerWidth > 768 && window.innerWidth <= 1024;

    useEffect(() => {
        if(videoDetail){
            setTotalVotes(videoDetail?.totalVotesCasted)
            setIsBookMarked(videoDetail?.isBookmarked)
        } 
    },[])


    const onBookMarkedChanged = () => {
        const getToken = () => localStorage.getItem('JWTToken');
        var token = getToken()
        if(token){
          const videoBookMarked = {
            videoId : videoDetail?.id,
            isBookMarked : !isBookmarked,
          }
    
          const token = localStorage.getItem('JWTToken');
          if(!token){
            navigate(`/login`);
          }
    
          api.post("Bookmark", videoBookMarked)
                .then((result) => {
                    toast.success(result?.message)
                    videoDetail.isBookmarked = !isBookmarked;
                    setIsBookMarked(!isBookmarked)
                })
                .catch((error) => {
                  console.log(error);
                  toast.error(error?.message)
                });
        }
        else{
          navigate(`/login`)
        }
      };


    const onRatingChange = (newRating) => {
        const getToken = () => localStorage.getItem('JWTToken');
        var token = getToken()
        if(token){
            
            const videoVoted = {
                videoId: videoDetail?.id,
                voteType: newRating,
            }

            setTotalVotes(videoDetail?.totalVotesCasted)

            api.post("Vote", videoVoted)
                .then((result) => {
                    
                    toast.success(result?.message)
                    videoDetail.isVoted = true;
                    setTotalVotes(totalVotes + 1);
                })
                .catch((error) => {
                    
                  console.log(error);
                  toast.error(error?.message)
                });
        }
        else {
            
            navigate(`/login`)
        }
    };

    return (
        <Stack direction="column" marginTop= {isBigScreen ? '0vh' : '18vh'} position={{xs:'relative', sm:'relative', md:'relative', lg:'relative'}} 
        zIndex='100' paddingTop={isBigScreen ? '0vh' :isMediumScreen ? '23vh' : '13vh'} className="bottomnavbar_Theme">
            <div className='textversion7'>{videoDetail?.tags}</div>
            <div className='textversion7'>

        {
            !isBigScreen && 
                <>
                {videoDetail?.description.slice(0, MAX_DESCRIPTION_LENGTH)}
                {videoDetail?.description.length > MAX_DESCRIPTION_LENGTH && (
                   <button
                   
                   style={{ background: 'transparent', border: 'none', cursor: 'pointer' }}
                 >
                    <Typography className="searchInputText" fontWeight='bold' variant="body1" fontSize={{xs:'10px', sm:'14px'}} sx={{ opacity: 0.9 }}>
                    { '...see more'}
                                </Typography>
                    
                  </button>
                )}
                </>
        }
        
      </div>

            <Stack direction="row" justifyContent="space-between" sx={{ color: "black" }} py={1} px={0} >
            <Link to={`/channel/${videoDetail?.uploaderUserId}`} style={{ textDecoration: 'none' }}>   
                <Stack direction="row" justifyContent="flex-start">
                
                    <Avatar
                        alt="Deldios Logo"
                        
                        src={videoDetail?.imagePath || `${IconsBucket}/avatarImage.png`}
                        sx={{
                            width: {xs:'30px', sm:'60px'},
                            height: {xs:'30px', sm:'60px'},
                            borderRadius: "80%",
                            marginLeft: "10px", // Adjust margin as needed
                            marginRight: "10px", // Adjust margin as needed
                            backgroundColor: "transparent", // Background color for circular logo
                        }}
                    />
                     
                    
                        <div className='textversion5'>
                        {videoDetail?.username && videoDetail.username.length > 12 ? (
                  <>
                    {videoDetail.username.substring(0, 8)}..
                    <CheckCircleIcon style={{ fontSize: 12, marginLeft: 5, color: 'gray' }} />
                  </>
                ) : (
                  <>
                    {videoDetail?.username}
                    <CheckCircleIcon style={{ fontSize: 12, marginLeft: 5, color: 'gray' }} />
                  </>
                )}
                        </div>
                       
                </Stack>

                </Link>

                <Stack direction="row" gap="20px" alignItems="center">

                    {videoDetail?.isVoted ? (
                        <>
                            <StarsIcon
                            className="IconTheme"

                            />
                            
                        </>

                    ) : (
                        <Box paddingBottom='10px'
                        >
                            <Rating

                                count={5}
                                onChange={onRatingChange}
                                size={size}
                                activeColor="#CD5D3D"
                            />

                        </Box>

                    )}

                    {videoDetail?.isBookmarked ? (
                        <>
                            <BookmarkIcon
                            onClick={onBookMarkedChanged}
                             sx={{
                                fontSize: '30px',
                                color:'#CD5D3D',
                                '&:hover': {
                                    cursor: 'pointer', // Change cursor to hand on hover
                                    // Add any additional styling for the hover state
                                }
                                // Add any additional styling for the checked icon
                            }}/>
                            
                        </>

                    ) : (
                        <Box paddingBottom='0px'
                        >
                             <BookmarkBorderIcon
                                onClick={onBookMarkedChanged}
                                sx={{
                                fontSize: '30px',
                                color:'grey',
                                '&:hover': {
                                    cursor: 'pointer', // Change cursor to hand on hover
                                    // Add any additional styling for the hover state
                                }
                                // Add any additional styling for the checked icon
                                }}/>
                        </Box>

                    )}

                    <div
                        style={{
                            
                            // Padding for inner content
                          display: 'inline-block', // Make it inline-block to fit the content
                      }}
                    >
                        <Typography className="searchInputText" fontWeight='bold' variant="body1" fontSize={{xs:'10px', sm:'14px'}} sx={{ opacity: 0.8 }}>
                            {parseInt(totalVotes).toLocaleString()} Votes
                        </Typography>
                    </div>

                    <div
                        style={{
                            
                              // Padding for inner content
                            marginRight:'10px',
                            display: 'inline-block', // Make it inline-block to fit the content
                        }}
                    >
                        {/* <Typography className="searchInputText"  fontWeight='bold'  variant="body1" fontSize={{xs:'10px', sm:'14px'}} sx={{ opacity: 0.7 }} >
                            {parseInt(videoDetail?.totalViews).toLocaleString()} Views
                        </Typography> */}
                    </div>


                </Stack>

            </Stack>

            {
                isBigScreen && (
                    <Stack
                        className="MainVideoBigScreenDescription" 
                        sx={{padding:'3vh 1vw', borderRadius:'10px'}}>
                        <Typography variant="h5" gutterBottom>
                        <div className='textversion2'>{videoDetail?.description}</div>
                        </Typography>
                
                </Stack>
                )
            }
        </Stack>
    )
}

export default MainVideoFooter
