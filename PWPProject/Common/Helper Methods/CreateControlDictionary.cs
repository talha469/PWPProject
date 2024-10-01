using Common.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper_Methods
{
    public static class CreateControl
    {
        public static Dictionary<string, Control> CreateControlDictionary(string videoId = null)
        {
            var controls = new Dictionary<string, Control>();

            controls["mumeta:get-all-videos"] = new Control
            {
                Href = $"{ApplicationConstants.VideosBaseUrl}",
                Method = "GET",
                Encoding = "json",
                Title = "Get All Videos"
            };

            controls["mumeta:add-video"] = new Control
            {
                Href = $"{ApplicationConstants.VideoBaseUrl}",
                Method = "POST",
                Encoding = "json",
                Title = "Add a new video",
                Schema = new Common.BusinessEntities.Schema
                {
                    Type = "object",
                    Properties = new Dictionary<string, SchemaProperty>
                    {
                        ["VideoId"] = new SchemaProperty
                        {
                            Description = "Video ID",
                            Type = "string"
                        },
                        ["Tags"] = new SchemaProperty
                        {
                            Description = "Video tags",
                            Type = "string"
                        },
                        ["Description"] = new SchemaProperty
                        {
                            Description = "Video description",
                            Type = "string"
                        },
                        ["Url"] = new SchemaProperty
                        {
                            Description = "Video URL",
                            Type = "string"
                        }
                    },
                    Required = new[] { "VideoId", "Url"}
                }
            };


            if (videoId != null)
            {
                controls["mumeta:get-video"] = new Control
                {
                    Href = $"{ApplicationConstants.VideoBaseUrl}/{videoId}",
                    Method = "GET",
                    Encoding = "json",
                    Title = "Get newly added video"
                };

            }


            // Add more controls here if needed

            return controls;
        }

    }
}
