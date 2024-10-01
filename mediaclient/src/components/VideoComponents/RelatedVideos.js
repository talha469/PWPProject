
import { Stack, Box, Skeleton } from "@mui/material";

import VideoCard from "./VideoCard";
const RelatedVideos = ({ direction, videos }) => {
  

  return (
    
    <Stack direction={ direction || "row"} flexWrap="wrap" justifyContent="start" alignItems="start" gap={2}>
      {!videos  ? (
        // Display skeleton when loading
        <>
        <Skeleton variant="rectangular" width={320} height={180} />
        <Skeleton variant="rectangular" width={320} height={180} />
        <Skeleton variant="rectangular" width={320} height={180} />
        <Skeleton variant="rectangular" width={320} height={180} />
        </>
      ) : (
      videos?.map((item, idx) => (
        <Box key={idx}>
          {item.id && <VideoCard videoDetails={item}  /> }
        </Box>
      ))
      )}
    </Stack>
  );
}

export default RelatedVideos;
