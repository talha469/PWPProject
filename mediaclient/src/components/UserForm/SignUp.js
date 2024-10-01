import React, { useState } from 'react';
import { Button, TextField, Container, Typography, Link, Box, CircularProgress } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import IconsBucket from '../ApplicationFiles/config';
import api from "../../API/APIRequests.js";
import toast, { Toaster } from 'react-hot-toast';

const SignUp = () => {
  const [username, setUserName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();


  const handleSignUp = async () => {

    if(email.length > 0 && password.length > 0){
      setLoading(true);
      
      const userSignUp =  {
        email: email,
        username:username,
        Password: password
      };
            api.postUser("Users", userSignUp)
            .then((result) => {
              toast.success(result?.message)
              navigate(`/login`);
            })
            .catch((error) => {
              console.log(error);
              toast.error(error?.message)
              setLoading(false);
            });

    }
  };

  const handleSignInLinkClick = () => {
    navigate(`/login`); // Navigate to the home route
  };
  
  return (
    <div
    style={{
      backgroundImage: `url(${IconsBucket}/signUpImage.png)`,
      backgroundSize: 'cover',
      backgroundPosition: 'center',
      minHeight: '90vh', // Set minimum height to cover the entire viewport
      display: 'flex',
      justifyContent: 'center',
      alignItems: 'center',
    }}
  >
   <div><Toaster/></div>
    
    <Container component="main" maxWidth="xs">
 
      <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center',marginBottom:'5vh'  }}>
      <Box
  sx={{
    backgroundColor: 'rgba(9, 9, 9, 0.58)',
    position: 'fixed', // Use 'fixed' position instead of 'absolute'
    top: '50%', // Position the top at 50% of the parent
    left: '50%', // 
    width: '100%', // Use 90% width on small devices
    height: '80vh', // Use 90% height on small devices
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
</Box>
       <Typography component="h1" variant="h3" style={{
        color: 'white',
       fontWeight: 'regular',
       zIndex:1,
       fontFamily:'Trajan-Pro' 
        }}>
        Join Us
      </Typography>

        <TextField
          margin="normal"
          zIndex='2'
          fullWidth
          label="username"
          value={username}
          onChange={(e) => setUserName(e.target.value)}
          InputProps={{
            style: {
              background: 'white', // Set text field background color to black
              opacity: '0.6',
              color: 'black', // Set text color to white
              fontFamily:'Trajan-Pro' 
            },
            inputProps: {
              style: {
                color: 'black', // Set input text color to white
                fontWeight:'bold'
              },
              maxLength: 25,
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
          label="Email"
          type="email"
          value={email}
          autoComplete="off"
          onChange={(e) => setEmail(e.target.value)}
          inputProps={{
            pattern: "[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,}$", // Email pattern
          }}
          InputProps={{
            style: {
              background: 'white', // Set text field background color to black
              opacity: '0.6',
              color: 'white', // Set text color to white
              fontFamily:'Trajan-Pro' 
            },
            inputProps: {
              style: {
                color: 'black', // Set input text color to white
                fontWeight:'bold'
              },
            },
          }}
          InputLabelProps={{
            style: {
              color: 'white', // Set label color to white
            },
          }}
          required // Add the required attribute for HTML5 validation
          error={email && !/^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$/.test(email)} // Show error if the entered email is invalid
          helperText={email && !/^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$/.test(email) ? 'Invalid email format' : ''} // Display error message
        />
        <TextField
          margin="normal"
          fullWidth
          label="Password"
          
          required
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          InputProps={{
            style: {
              background: 'white', // Set text field background color to black
              opacity: '0.6',
              color: 'white', // Set text color to white
            },
            inputProps: {
              style: {
                color: 'black', // Set input text color to white
                fontWeight:'bold'
              },
            },
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
          onClick={handleSignUp}
          style={{ marginTop: '1.5rem',fontFamily:'Trajan-Pro'  }}
        >
          {loading ? (
            <CircularProgress size={24}  style={{ color: 'white' }}  />
          ) : (
            <span>Sign Up</span>
          )}
        </Button>

        <Link to="#" onClick={handleSignInLinkClick} style={{
           display: 'block', marginTop: '2rem',fontWeight:'bold',fontSize:'1rem', zIndex:1, fontFamily:'Trajan-Pro', 
           cursor: 'pointer',
           color:'white'
           }}>
          	Already a member?  &nbsp; SignIn
        </Link>
       
      </div>
      
    </Container>
    </div>
  );
};

export default SignUp;
