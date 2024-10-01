import React, { useState } from 'react';
import { Button, TextField, Container, Link,IconButton,InputAdornment ,CircularProgress, Box,Divider } from '@mui/material';
import { Visibility, VisibilityOff } from '@mui/icons-material';
import {
  ToastContainer
} from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import IconsBucket from '../ApplicationFiles/config';
import api from "../../API/APIRequests.js";


const SignIn = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [showPassword, setShowPassword] = useState(false);

  const handleSignIn = async () => {
    if(email.length > 0 && password.length > 0){
      setLoading(true);
     
      const userSignIn =  {
        email: email,
        passwordHash: password
      };

      api.post("Authentication", userSignIn)
            .then((result) => {
                localStorage.setItem("JWTToken", result.token)
              navigate(`/`);
              window.location.href = window.location.origin;
            })
            .catch((error) => {
              console.log(error);
              setLoading(false);
            });
    }
  };

  const handleSignUpLinkClick = () => {
    navigate(`/signup`);
  };

  const handlePasswordVisibility = () => {
    setShowPassword(!showPassword);
  };

  return (
    <div
    style={{
      backgroundImage: `url(${IconsBucket}/signInImage.png)`,
      backgroundSize: 'cover',
      backgroundPosition: 'center',
      minHeight: '90vh', // Set minimum height to cover the entire viewport
      display: 'flex',
      justifyContent: 'center',
      alignItems: 'center',
    }}
  >

   <ToastContainer / >
    <Container component="main" maxWidth="xs">
 
      <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', marginBottom:'140px' }}>
      <Box
  sx={{
    backgroundColor: 'rgba(9, 9, 9, 0.58)',
    position: 'absolute', // Use 'fixed' position instead of 'absolute'
    top: '50%', // Position the top at 50% of the parent
    left: '50%', // 
    width: '100%', // Use 90% width on small devices
    height: '80%', // Use 90% height on small devices
    display: 'flex',
    transform: 'translate(-50%, -50%)',
    justifyContent: 'center',
    alignItems: 'center',
    '@media (min-width: 768px)': { // Apply these styles for medium devices and larger
      width: '50%', // Use 80% width on medium and larger devices
      height: '80%', // Use 80% height on medium and larger devices
      transform: 'translate(-50%, -50%)',
    },
  }}
>
  {/* You can put any background content here */}
</Box>

      <div className='textversion10'>Sign In</div>
   
        <TextField
          margin="normal"
          fullWidth
          label="Email"
          type="email"
          value={email}
          autoComplete="off"
          onChange={(e) => setEmail(e.target.value)}
          InputProps={{
            style: {
              background: 'white', // Set text field background color to black
              opacity: '0.6',
              color: 'black', // Set text color to white
              height:'50px',
              fontFamily:'Trajan-Pro'
            },
            inputProps: {
              style: {
                color: 'black', // Set input text color to white
                fontWeight:'bold',
                fontFamily:'Trajan-Pro'
              },
            },
          }}
          InputLabelProps={{
            style: {
              color: 'white', // Set label color to white
            },
          }}
        />
        <TextField
          margin="normal"
          fullWidth
          label="Password"
          type={showPassword ? 'text' : 'password'}
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          InputProps={{
            style: {
              background: 'white', // Set text field background color to black
              opacity: '0.6',
              color: 'black', // Set text color to white
              height:'50px'
            },
            inputProps: {
              style: {
                color: 'black', // Set input text color to white
                fontWeight:'bold'
              },
            }, endAdornment: (
              <InputAdornment position="end">
                <IconButton
                  aria-label="toggle password visibility"
                  onClick={handlePasswordVisibility}
                  edge="end"
                >
                  {showPassword ? <Visibility /> : <VisibilityOff />}
                </IconButton>
              </InputAdornment>
            ),
          }}
          InputLabelProps={{
            style: {
              color: 'white', // Set label color to white
            },
          }}
        />

        <Button
          fullWidth
          variant="contained"
          color="primary"
          onClick={handleSignIn}
          style={{ marginTop: '1.5rem',fontFamily:'Trajan-Pro' }}
        >
          {loading ? (
            <CircularProgress size={24}  style={{ color: 'white' }}  />
          ) : (
            <span>Sign In</span>
          )}
        </Button>

        <Divider style={{ backgroundColor: 'white', margin: '1rem 0' }} />

        <Link to="#" onClick={handleSignUpLinkClick} style={{
           display: 'block', fontFamily:'Trajan-Pro', marginTop: '2rem',fontWeight:'bold',fontSize:'1rem', zIndex:1, 
           cursor: 'pointer',
           color:'white'
           }}>
          	New to the club? Sign Up
        </Link>

      </div>
      
    </Container>
    </div>
  );
};

export default SignIn;
