-- Populate User table with static data
INSERT INTO PWPProject.dbo.Users
VALUES
    ('user1@example.com', 'user1', 'passwordhash1', NULL, 0, '2023-01-01'),
    ('user2@example.com', 'user2', 'passwordhash2', NULL, 0, '2023-02-15'),
    ('admin@example.com', 'admin', 'adminpasswordhash', NULL, 1, '2023-03-20'),
    ('user3@example.com', 'user3', 'passwordhash3', NULL, 0, '2023-04-10');


INSERT INTO Video (VideoId, UserId, Tags, Description, Url, VideoExtension, IsEncoded, AvailableResolutions, VideoStatus, IsDismiss, IsApproved, VideoFormatType, IsSellingVideo, VideoUploadedDate)
VALUES
    ('video1', 1, 'tag2', 'Description for video 1', 'http://example.com/video1.mp4', 'mp4', 1, '1080p, 720p', 1, 0, 1, 1,0, '2023-01-01'),
    ('video2', 2, 'tag4', 'Description for video 2', 'http://example.com/video2.mp4', 'mp4', 1, '1080p, 720p', 1, 0, 1, 1,0, '2023-02-15'),
    ('video3', 3, 'tag6', 'Description for video 3', 'http://example.com/video3.mp4', 'mp4', 1, '1080p, 720p', 1, 0, 1, 1,0, '2023-03-20'),
    ('video4', 4, 'tag8', 'Description for video 4', 'http://example.com/video4.mp4', 'mp4', 1, '1080p, 720p', 1, 0, 1, 1,0, '2023-04-10');


INSERT INTO PWPProject.dbo.Votes( VideoId, VoteType, VoteDate)
VALUES
    ('video1', 1, '2023-01-01'),
    ('video2', 3, '2023-02-15'),
    ('video3', 4, '2023-03-20'),
    ('video4', 1, '2023-04-10');

INSERT INTO PWPProject.dbo.BookMark(UserId, VideoId,isBookMarked, BookmarkDate)
VALUES
    ( 1, 'video1',1, '2023-01-01'),
    ( 2, 'video2',1, '2023-02-15'),
    ( 3, 'video3',1, '2023-03-20'),
    ( 4, 'video4',1, '2023-04-10');

