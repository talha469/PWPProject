import React, { useState } from 'react';
import { Box, Button, CircularProgress, FormControl, InputLabel, MenuItem, Select, Typography } from '@mui/material';
import axios from 'axios';

const GenerateThumbnail = () => {
    const [selectedId, setSelectedId] = useState('');
    const [imageUrl, setImageUrl] = useState('');
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');

    const ids = ['c45cdb85-949d-4fa0-9d58-39fd1167e54b', 'a18778ca-bfba-49d7-9823-e41bf0965a04'];

    const handleGenerateThumbnail = async () => {
        if (!selectedId) {
            setError('Please select an ID');
            return;
        }

        setError('');
        setLoading(true);

        try {
            const response = await axios.get(`https://localhost:7222/AuxilaryService?videoId=${selectedId}`);
            setImageUrl(response.data?.thumbnailUrl);
        } catch (err) {
            setError('Failed to generate thumbnail');
        } finally {
            setLoading(false);
        }
    };

    return (
        <Box 
            display="flex" 
            flexDirection="column" 
            alignItems="center" 
            justifyContent="center" 
            height="100vh"
        >
            <Typography variant="h4" gutterBottom>
                Generate Thumbnail
            </Typography>
            <FormControl variant="outlined" sx={{ minWidth: 200, marginBottom: 2 }}>
                <InputLabel>Select ID</InputLabel>
                <Select
                    value={selectedId}
                    onChange={(e) => setSelectedId(e.target.value)}
                    label="Select ID"
                >
                    <MenuItem value="">
                        <em>None</em>
                    </MenuItem>
                    {ids.map(id => (
                        <MenuItem key={id} value={id}>{id}</MenuItem>
                    ))}
                </Select>
            </FormControl>
            <Button 
                variant="contained" 
                color="primary" 
                onClick={handleGenerateThumbnail} 
                disabled={loading}
            >
                {loading ? <CircularProgress size={24} /> : 'Generate Thumbnail'}
            </Button>

            {error && <Typography color="error" style={{ marginTop: 16 }}>{error}</Typography>}
            {imageUrl && (
                <Box mt={4}>
                    <img 
                        src={imageUrl} 
                        alt="Thumbnail" 
                        style={{ maxWidth: '100%', maxHeight: '500px', height: 'auto', width: 'auto' }} 
                    />
                </Box>
            )}
        </Box>
    );
};

export default GenerateThumbnail;
