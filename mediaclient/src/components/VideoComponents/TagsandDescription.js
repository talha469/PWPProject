import React from 'react'
import {Typography,Tooltip } from '@mui/material';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';

const TagsandDescription = ({video, cssClassName}) => {
  
  return (
    <div style={{ display: 'flex', flexDirection: 'column'}}>
      
      <div>

        <Tooltip title={`${video?.tags}, ${video?.description}`}>
              <Typography
            className={cssClassName}
            style={{ fontFamily: 'Proxima-Nova-Bold' }}
            sx={{ fontSize: { xs: '14px', sm: '14px', md: '14px' } }}
          >
            {video?.tags}
          </Typography>

          <Typography
            className={cssClassName}
            style={{ fontFamily: 'Proxima-Nova-Bold' }}
            sx={{ fontSize: { xs: '14px', sm: '14px', md: '14px' } }}
          >
            {video?.description}
          </Typography>
          
        </Tooltip>
      </div>

      <div style={{ display: 'flex', flexDirection: 'column', alignContent:'center', justifyContent:'center'}}>
          <Typography
            className={"channelNameWIthTags"}
            style={{ fontFamily: 'Proxima-Nova-Light' }}
            sx={{ fontSize: { xs: '12px', sm: '12px', md: '14px' } }}
          >
             
                {video?.username && video.username.length > 12 ? (
                  <>
                    {video.username.substring(0, 12)}..
                    <CheckCircleIcon style={{ fontSize: 12, marginLeft: 5, color: 'gray' }} />
                  </>
                ) : (
                  <>
                    {video?.username}
                  </>
                )}
           
          </Typography>
      </div>
    </div>
  );
  
}

export default TagsandDescription
