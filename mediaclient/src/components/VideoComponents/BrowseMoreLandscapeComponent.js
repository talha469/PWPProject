import { Box } from '@mui/system'
import React, { useState} from 'react'
import Grid from '@mui/material/Grid';

const BrowseMoreLandscapeComponent = ({setLandscapeClickedTab}) => {
  const [isHovered, setIsHovered] = useState(false);
  const [colorText, setColorText] = useState('white');


  const handleShortClick = () => {
    setLandscapeClickedTab()
  }

 //Actions
 const handleMouseEnter = () => {
    setIsHovered(true)
    setColorText("white")
  }

  const handleMouseLeave = () => {
    setIsHovered(false)
    setColorText("white")
  }

  return (
    <Grid container>
      <Grid container
      onClick={() => handleShortClick()}
      onMouseEnter = {handleMouseEnter}
      onMouseLeave = {handleMouseLeave}
      sx={{
        cursor: 'pointer',
        transition: 'background-color 0.3s', // Optional: Smooth transition effect
        ':hover': {
          backgroundColor: '#1976D2', // Change color on hover
        },
        background:'#CD5D3D'
      }}
      alignContent={'center'}
      alignItems={'center'}
      >
            <Box sx={{ height:'5vh' , width:'40vw',  border: 1, borderColor: 'grey.500',
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'center'}}>
                <div className='textversionBrowseMore' style={{ color: colorText }}>Browse More</div>
          </Box>
      </Grid>
    </Grid>
  )
}

export default BrowseMoreLandscapeComponent
