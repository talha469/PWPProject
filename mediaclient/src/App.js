import React, { useState, useEffect } from 'react';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import SignIn from './components/UserForm/SignIn';
import SignUp from './components/UserForm/SignUp';
import Home from './components/Pages/HomePageContent';
import LandscapeVideoMain from './components/VideoComponents/LandscapeVideoMain';
import VideoUpload from './components/AdminForms/VideoUpload';
import ResponsiveAppBar from './components/Navbar/Navbar';
import UserInfoTable from './components/AdminForms/Users';
import VideoInfoTable from './components/AdminForms/VideoInfoTable';
import StatsDisplay from './components/Pages/StatsDisplay';
import GenerateThumbnail from './components/VideoComponents/GenerateThumbnails';

// Helper function to retrieve JWT token from local storage
const getToken = () => localStorage.getItem('JWTToken');

function App() {
  const [isAdmin, setIsAdmin] = useState(false);
  const [userName, setUserName] = useState(false);
  const [userId, setUserId] = useState('');
  const [key, setKey] = useState(0); // Key to trigger re-render

  useEffect(() => {
    // Check if the user is an admin by decoding the JWT token
    checkToken();
  }, []);

  const checkToken = () => {
    const token = getToken();
    if (token) {
      const decodedToken = parseJwt(token);
      
      // Assuming you have 'role' field in the JWT payload
      if (decodedToken && decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] === 'Admin') {
        setIsAdmin(true);
      }
      if(decodedToken){
        setUserName(decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"]);
        setUserId(decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/hash"]);

    }
    }
  }

  // Function to parse JWT token
  const parseJwt = (token) => {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(
      atob(base64)
        .split('')
        .map((c) => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
        .join('')
    );
    return JSON.parse(jsonPayload);
  };


  return (
    <BrowserRouter>
      <ResponsiveAppBar isAdmin={isAdmin} userName={userName}/>
      <Routes key={key}>
        <Route path="/" element={<Home />} />
        <Route path="/login" element={<SignIn />} />
        <Route path="/signup" element={<SignUp />} />
        <Route path="/stats" element={<StatsDisplay />} />
        <Route path="/video/:id" element={<LandscapeVideoMain userId={userId}/>} />
        <Route path="/generateThumbnail" element={<GenerateThumbnail userId={userId}/>} />
        {isAdmin && <Route path="/videoupload" element={<VideoUpload />} />}
        {isAdmin && <Route path="/users" element={<UserInfoTable />} />}
        {isAdmin && <Route path="/videos" element={<VideoInfoTable />} />}
      </Routes>
    </BrowserRouter>
  );
}

export default App;
