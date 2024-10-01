import * as React from 'react';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import Menu from '@mui/material/Menu';
import Container from '@mui/material/Container';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import Tooltip from '@mui/material/Tooltip';
import MenuItem from '@mui/material/MenuItem';
import AdbIcon from '@mui/icons-material/Adb';
import { useNavigate } from 'react-router-dom';

const settings = ['Logout'];

function ResponsiveAppBar({isAdmin, userName}) {
  const [anchorElUser, setAnchorElUser] = React.useState(null);
  const navigate = useNavigate();

  const handleOpenUserMenu = (event) => {
    setAnchorElUser(event.currentTarget);
  };

  const handleCloseUserMenu = () => {
    setAnchorElUser(null);
  };

  const handleUploadClick = () => {
    navigate(`/videoupload`);
    console.log("Button clicked!");
  };

  const handleGenerateThumbnail = () => {
    navigate(`/generateThumbnail`);
    console.log("Button clicked!");
  };


  const handleUsersClick = () => {
    navigate(`/users`);
    console.log("Button clicked!");
  };

  const handleVideosClick = () => {
    navigate(`/videos`);
    console.log("Button clicked!");
  };

  const handleRegisteruUsersClick = () => {
    navigate(`/login`);
    console.log("Button clicked!");
  };

  const handleStatsUsersClick = () => {
    navigate(`/stats`);
  };

  

  const handleHomeClick = () => {
    navigate(`/`);
    console.log("Button clicked!");
  };

  const handleLogout = () => {
    localStorage.removeItem('JWTToken');
    window.location.href = window.location.origin;
    
  };

  return (
    <AppBar position="static">
      <Container maxWidth="xl">
        <Toolbar disableGutters>
          <IconButton edge="start" color="inherit" aria-label="menu">
            <AdbIcon onClick={handleHomeClick}/>
          </IconButton>
          <Box sx={{ flexGrow: 1 }} />
          {isAdmin && (<>
            <Button
            variant="contained"
            color="primary"
            onClick={handleUploadClick}
            sx={{ mr: 2 }}
          >
            Upload
          </Button>

          
          <Button
            variant="contained"
            color="primary"
            onClick={handleUsersClick}
            sx={{ mr: 2 }}
          >
            Users
          </Button>
          <Button
            variant="contained"
            color="primary"
            onClick={handleVideosClick}
          >
            Videos
          </Button>
          
          </>)}

          <Button
              variant="contained"
              color="primary"
              onClick={handleStatsUsersClick}
              sx={{ ml: 2, mr:2 }}
              >
              Stats
              </Button>

              <Button
              variant="contained"
              color="primary"
              onClick={handleGenerateThumbnail}
              sx={{ ml: 2, mr:2 }}
              >
              Generate Thumbnail
              </Button>

          {!userName && (
              <Button
              variant="contained"
              color="primary"
              onClick={handleRegisteruUsersClick}
              sx={{ mr: 2 }}
              >
              Sign In
              </Button>
          )}
          
         
          {userName && (
            <>
             <Box sx={{ flexGrow: 0 }}>
            <Tooltip title="Open settings">
              <IconButton onClick={handleOpenUserMenu} sx={{ p: 0 }}>
                <Avatar alt={userName} src="/static/images/avatar/2.jpg" />
              </IconButton>
            </Tooltip>
            <Menu
              sx={{ mt: '45px' }}
              id="menu-appbar"
              anchorEl={anchorElUser}
              anchorOrigin={{
                vertical: 'top',
                horizontal: 'right',
              }}
              keepMounted
              transformOrigin={{
                vertical: 'top',
                horizontal: 'right',
              }}
              open={Boolean(anchorElUser)}
              onClose={handleCloseUserMenu}
            >
              {settings.map((setting) => (
                <MenuItem key={setting} onClick={setting === 'Logout' ? handleLogout : handleCloseUserMenu}>
                  <Typography textAlign="center">{setting}</Typography>
                </MenuItem>
              ))}
            </Menu>
          </Box>
            </>
          )}

         
        </Toolbar>
      </Container>
    </AppBar>
  );
}
export default ResponsiveAppBar;
