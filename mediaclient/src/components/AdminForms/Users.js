import React, { useState,useEffect } from 'react';
import api from "../../API/APIRequests.js";
import { Table, TableBody, TableCell,Select,MenuItem, TableContainer, TableHead, TableRow, Paper, Button, Dialog, DialogTitle, DialogContent, DialogActions, TextField } from '@mui/material';
import { toast } from 'react-toastify';

const UserInfoTable = () => {
  const [users, setUsersDetails] = useState([]);
  const [editUserId, setEditUserId] = useState(null);
  const [deleteUserId, setDeleteUserId] = useState(null);
  const [videoDetailsLoading, setVideoDetailsLoading] = useState(false)
  const [ deleteUserUrl, setdeleteUserUrl] = useState('')
  

  const getData = () => {
    setVideoDetailsLoading(true);
    
    api.get("Users")
    .then((result) => {
      setUsersDetails(result?.data)
      setVideoDetailsLoading(false);

      const addVideoHref = result?.controls?.['mumeta:delete-user']?.href;
          console.log(addVideoHref)
          setdeleteUserUrl(addVideoHref);
      

      
  })
  .catch((error) => {
    console.log(error);
    
  });
  };
  
  useEffect(() => {
    getData();
  }, []);


  const handleEdit = (userId) => {
    setEditUserId(userId);
  };

  const handleDelete = (userId) => {
    setDeleteUserId(userId);
  };

  const handleEditSubmit = (updatedUser) => {
    
    const user = {
      userId : updatedUser?.userId,
      email :  updatedUser?.email,
      username : updatedUser?.username,
      role : updatedUser?.role
    }

    api.put("User", user)
    .then((result) => {
      getData();
  })
  .catch((error) => {
    console.log(error);
    
  });

    setEditUserId(null);
  };


  const handleDeleteConfirm = () => {
    debugger
    const segments = deleteUserUrl.split('/');
        const secondLastSegment = segments[segments.length - 2]; 
    console.log(secondLastSegment)
    
    api.delete(secondLastSegment, deleteUserId)
      .then((result) => {
        toast.success(result?.message)
        getData();
      })
      .catch((error) => {
        console.log(error);
      });
  
    setDeleteUserId(null);
  };

  return (
    <div style={{ padding: '50px' }}>
    <TableContainer component={Paper}>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>User ID</TableCell>
            <TableCell>Username</TableCell>
            <TableCell>Email</TableCell>
            <TableCell>Role</TableCell>
            <TableCell>Actions</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {users.map((user) => (
            <TableRow key={user.userId}>
              <TableCell>{user.userId}</TableCell>
              <TableCell>{user.username}</TableCell>
              <TableCell>{user.email}</TableCell>
              <TableCell>{user.role}</TableCell>
              <TableCell>
                <Button onClick={() => handleEdit(user.userId)}>Edit</Button>
                <Button onClick={() => handleDelete(user.userId)}>Delete</Button>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
      {editUserId !== null && (
        <EditUserDialog
          user={users.find((user) => user.userId === editUserId)}
          onSubmit={handleEditSubmit}
          onClose={() => setEditUserId(null)}
        />
      )}
     {deleteUserId !== null && (
        <DeleteUserDialog
          userId={deleteUserId} // Pass only the userId
          username={users.find((user) => user.userId === deleteUserId).username}
          onConfirm={handleDeleteConfirm}
          onClose={() => setDeleteUserId(null)}
        />
      )}


    </TableContainer>
    
    </div>
  );
};

const EditUserDialog = ({ user, onSubmit, onClose }) => {
  const [updatedUser, setUpdatedUser] = useState({ ...user });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setUpdatedUser((prevUser) => ({ ...prevUser, [name]: value }));
  };

  const handleSubmit = () => {
 
    onSubmit(updatedUser);
  };

  return (
    <Dialog open onClose={onClose}>
      <DialogTitle>Edit User</DialogTitle>
      <DialogContent>
        <TextField label="User ID" name="userId" value={updatedUser.userId} disabled fullWidth sx={{ marginBottom: '10px', marginTop: '20px' }} />
        <TextField label="Username" name="username" value={updatedUser.username} onChange={handleChange} fullWidth sx={{ marginBottom: '10px' }} />
        <TextField label="Email" name="email" value={updatedUser.email} onChange={handleChange} fullWidth sx={{ marginBottom: '10px' }} />
        <Select
          label="Role"
          name="role"
          value={updatedUser.role}
          onChange={handleChange}
          fullWidth
          sx={{ marginBottom: '10px' }}
        >
          <MenuItem value="Admin">Admin</MenuItem>
          <MenuItem value="User">User</MenuItem>
        </Select>
      </DialogContent>
      <DialogActions>
        <Button onClick={handleSubmit}>Save</Button>
        <Button onClick={onClose}>Cancel</Button>
      </DialogActions>
    </Dialog>
  );
};

const DeleteUserDialog = ({ username, onConfirm, onClose }) => {
  return (
    <Dialog open onClose={onClose}>
      <DialogTitle>Delete User</DialogTitle>
      <DialogContent>
        <p>Are you sure you want to delete user "{username}"?</p>
      </DialogContent>
      <DialogActions>
        <Button onClick={onConfirm}>Confirm</Button>
        <Button onClick={onClose}>Cancel</Button>
      </DialogActions>
    </Dialog>
  );
};

export default UserInfoTable;
