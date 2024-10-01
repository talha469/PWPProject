import React, { useState, useEffect } from 'react';
import api from "../../API/APIRequests.js";
import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, Button, Dialog, DialogTitle, DialogContent, DialogActions, TextField } from '@mui/material';
import toast, { Toaster } from 'react-hot-toast';

const VideoInfoTable = () => {
  const [videos, setVideos] = useState([]);
  const [editVideoId, setEditVideoId] = useState(null);
  const [deleteVideoId, setDeleteVideoId] = useState(null);
  const [videoDetailsLoading, setVideoDetailsLoading] = useState(false);

  const getData = () => {
    setVideoDetailsLoading(true);
    api.get("Videos")
      .then((result) => {
        setVideos(result?.data);
        setVideoDetailsLoading(false);

      })
      .catch((error) => {
        console.log(error);
      });
  };
  
  useEffect(() => {
    getData();
  }, []);

  const handleEdit = (videoId) => {
    setEditVideoId(videoId);
  };

  const handleDelete = (videoId) => {
    setDeleteVideoId(videoId);
  };

  const handleEditSubmit = (updatedVideo) => {
    api.put("Video", updatedVideo)
      .then((result) => {
        getData();
        toast.success(result?.message);
      })
      .catch((error) => {
        
        console.log(error);
        if(error.message == '401'){
            toast.error("You are unauthorized to perform this action"); // Render the error message
        }
        
      })
      .finally(() => {
        setEditVideoId(null);
      });
  };
  
  

  const handleDeleteConfirm = () => {
    api.delete("Video", deleteVideoId)
      .then((result) => {
        toast.success(result?.message);
        getData();
      })
      .catch((error) => {
        console.log(error);
      });
    setDeleteVideoId(null);
  };

  return (
    <div style={{ padding: '50px' }}>
        <div><Toaster/></div>
      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Video ID</TableCell>
              <TableCell>Tags</TableCell>
              <TableCell>Description</TableCell>
              <TableCell>Actions</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {videos.map((video) => (
              <TableRow key={video.videoId}>
                <TableCell>{video.videoId}</TableCell>
                <TableCell>{video.tags}</TableCell>
                <TableCell>{video.description}</TableCell>
                <TableCell>
                  <Button onClick={() => handleEdit(video.id)}>Edit</Button>
                  <Button onClick={() => handleDelete(video?.id)}>Delete</Button>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
        {editVideoId !== null && (
          <EditVideoDialog
            video={videos.find((video) => video.id === editVideoId)}
            onSubmit={handleEditSubmit}
            onClose={() => setEditVideoId(null)}
          />
        )}
        {deleteVideoId !== null && (
          <DeleteVideoDialog
            videoId={deleteVideoId}
            description={videos.find((video) => video.id === deleteVideoId).description}
            onConfirm={handleDeleteConfirm}
            onClose={() => setDeleteVideoId(null)}
          />
        )}
      </TableContainer>
    </div>
  );
};

const EditVideoDialog = ({ video, onSubmit, onClose }) => {
  const [updatedVideo, setUpdatedVideo] = useState({ ...video });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setUpdatedVideo((prevVideo) => ({ ...prevVideo, [name]: value }));
  };

  const handleSubmit = () => {
    onSubmit(updatedVideo);
  };

  return (
    <Dialog open onClose={onClose}>
      <DialogTitle>Edit Video</DialogTitle>
      <DialogContent>
        <TextField label="Video ID" name="videoId" value={updatedVideo.videoId} disabled fullWidth sx={{ marginBottom: '10px', marginTop: '20px' }} />
        <TextField label="Tags" name="tags" value={updatedVideo.tags} onChange={handleChange} fullWidth sx={{ marginBottom: '10px' }} />
        <TextField label="Description" name="description" value={updatedVideo.description} onChange={handleChange} fullWidth sx={{ marginBottom: '10px' }} />
      </DialogContent>
      <DialogActions>
        <Button onClick={handleSubmit}>Save</Button>
        <Button onClick={onClose}>Cancel</Button>
      </DialogActions>
    </Dialog>
  );
};

const DeleteVideoDialog = ({ description, onConfirm, onClose }) => {
  return (
    <Dialog open onClose={onClose}>
      <DialogTitle>Delete Video</DialogTitle>
      <DialogContent>
        <p>Are you sure you want to delete the video with description "{description}"?</p>
      </DialogContent>
      <DialogActions>
        <Button onClick={onConfirm}>Confirm</Button>
        <Button onClick={onClose}>Cancel</Button>
      </DialogActions>
    </Dialog>
  );
};

export default VideoInfoTable;
