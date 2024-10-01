import React, { useState,useEffect } from 'react';
import { Container, Typography, Box, Grid, Paper } from '@mui/material';
import api from "../../API/APIRequests.js";

// StatsDisplay Component
const StatsDisplay = () => {
    const [data, setData] = useState([]);

    const getData = () => {
        
        api.get("Stats")
        .then((result) => {
          setData(result?.data)
          
      })
      .catch((error) => {
        console.log(error);
        
      });
      };
      
      useEffect(() => {
        getData();
      }, []);

  return (
    <Container maxWidth="sm">
      <Box my={4}>
        <Typography variant="h4" component="h1" gutterBottom>
          Statistics
        </Typography>
        <Grid container spacing={3}>
          <Grid item xs={12}>
            <Paper elevation={3} style={{ padding: '16px' }}>
              <Typography variant="h6" component="h2">
                Total Videos
              </Typography>
              <Typography variant="body1">
                {data.totalVideos}
              </Typography>
            </Paper>
          </Grid>
          <Grid item xs={12}>
            <Paper elevation={3} style={{ padding: '16px' }}>
              <Typography variant="h6" component="h2">
                Total Users
              </Typography>
              <Typography variant="body1">
                {data.totalUsers}
              </Typography>
            </Paper>
          </Grid>
          <Grid item xs={12}>
            <Paper elevation={3} style={{ padding: '16px' }}>
              <Typography variant="h6" component="h2">
                Most Voted Video
              </Typography>
              <Typography variant="body1">
                {data.mostVotedVideo}
              </Typography>
            </Paper>
          </Grid>
          <Grid item xs={12}>
            <Paper elevation={3} style={{ padding: '16px' }}>
              <Typography variant="h6" component="h2">
                Most Bookmarked Video
              </Typography>
              <Typography variant="body1">
                {data.mostBookmarkedVideo}
              </Typography>
            </Paper>
          </Grid>
        </Grid>
      </Box>
    </Container>
  );
};

// Exporting StatsDisplay as the default export
export default StatsDisplay;
