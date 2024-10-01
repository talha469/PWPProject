import React, { useState } from 'react';
import { Button, Select, MenuItem, TextField, Container, Grid, InputLabel, makeStyles } from '@material-ui/core';
import api from '../../API/APIRequests';
import toast, { Toaster } from 'react-hot-toast';
import { useNavigate } from 'react-router-dom';

const useStyles = makeStyles((theme) => ({
  imageOption: {
    maxWidth: '10vw',
    maxHeight: '10vh',
    marginRight: theme.spacing(1),
  },
}));

const VideoUpload = () => {
  const classes = useStyles();
  const [selectedOption, setSelectedOption] = useState('');
  const [tags, setTags] = useState('');
  const [description, setDescription] = useState('');
  const navigate = useNavigate();

  const handleOptionChange = (event) => {
    
    setSelectedOption(event.target.value);
  };

  const handleTagsChange = (event) => {
    setTags(event.target.value);
  };

  const handleDescriptionChange = (event) => {
    setDescription(event.target.value);
  };

  const handleUpload = () => {
    const videoPost =  {
        videoId: selectedOption,
        tags: tags,
        description: description
      };

      const token = localStorage.getItem('JWTToken');
      if(!token){
        navigate(`/login`);
      }

      api.post("Videos", videoPost)
            .then((result) => {
                toast.success(result?.message)
                setSelectedOption('')
            })
            .catch((error) => {
              console.log(error);
              toast.error(error?.message)
            });
  };

  return (
    <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', minHeight: '100vh' }}>
      <div><Toaster/></div>
      <Container maxWidth="sm">
        <Grid container spacing={2}>
          <Grid item xs={12}>
            <InputLabel htmlFor="option-select">Choose a video</InputLabel>
            <Select
              value={selectedOption}
              onChange={handleOptionChange}
              fullWidth
              labelId="option-select"
            >
              <MenuItem value="e626a166-d72e-4ee0-9916-6b5bd04af89d">
                <img src="https://vz-6258ec42-3d0.b-cdn.net/e626a166-d72e-4ee0-9916-6b5bd04af89d/thumbnail_b147110a.jpg" 
                alt="e626a166-d72e-4ee0-9916-6b5bd04af89d" className={classes.imageOption} />
                
              </MenuItem>
              <MenuItem value="4cedc9a3-c201-4198-b5a7-d5959a722b75">
                <img src="https://vz-6258ec42-3d0.b-cdn.net/4cedc9a3-c201-4198-b5a7-d5959a722b75/thumbnail_d9bc0f6c.jpg"
                 alt="4cedc9a3-c201-4198-b5a7-d5959a722b75" 
                className={classes.imageOption} />
                
              </MenuItem>
              <MenuItem value="633f103f-e15e-44ee-a3d7-b5fa1cd7335f">
                <img src="https://vz-6258ec42-3d0.b-cdn.net/633f103f-e15e-44ee-a3d7-b5fa1cd7335f/thumbnail_c143adf6.jpg" 
                alt="633f103f-e15e-44ee-a3d7-b5fa1cd7335f" className={classes.imageOption} />
                
              </MenuItem>
              <MenuItem value="52382cbe-235c-42d3-b8ed-bb6bc6af35bb">
                <img src="https://vz-6258ec42-3d0.b-cdn.net/52382cbe-235c-42d3-b8ed-bb6bc6af35bb/thumbnail_a9001f56.jpg" 
                alt="52382cbe-235c-42d3-b8ed-bb6bc6af35bb" className={classes.imageOption} />
                
              </MenuItem>
            </Select>
          </Grid>
          <Grid item xs={12}>
            <TextField
              value={tags}
              onChange={handleTagsChange}
              fullWidth
              label="Tags"
              variant="outlined"
              placeholder="Enter tags"
              multiline
            />
          </Grid>
          <Grid item xs={12}>
            <TextField
              value={description}
              onChange={handleDescriptionChange}
              fullWidth
              label="Description"
              variant="outlined"
              placeholder="Enter description"
              multiline
            />
          </Grid>
          <Grid item xs={12}>
            <Button variant="contained" color="primary" onClick={handleUpload}>
              Generate Thumbnail
            </Button>
          </Grid>
        </Grid>
      </Container>
    </div>
  );
};

export default VideoUpload;
