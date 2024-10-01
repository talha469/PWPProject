using Common.Enum;
using CommonLibrary.BusinessEntities;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Helper_Methods;

namespace Common.BusinessEntities
{

    public class VideosDto
    {
        public string Id { get; set; }
        public string VideoId { get; set; }

        public string? Tags { get; set; }

        public string? Description { get; set; }

        public string? Url { get; set; }

        public int? VideoStatus { get; set; }

        public DateTime? VideoUploadedDate { get; set; }

        public Links? Links { get; set; }

        public bool isVoted { get; set; }
        public bool isBookmarked { get; set; }

        //use url convertor 
        public string GetUrlEncodedVideoId()
        {
            return URLConvertor.UrlConverter.ConvertToUrl(VideoId);
        }

        public string GetUrlEncodedTags()
        {
            return URLConvertor.UrlConverter.ConvertToUrl(Tags);
        }

        public string GetUrlEncodedDescription()
        {
            return URLConvertor.UrlConverter.ConvertToUrl(Description);
        }

        public string GetUrlEncodedUrl()
        {
            return URLConvertor.UrlConverter.ConvertToUrl(Url);
        }



    }

    public class EditVideo
    {
        public string Id { get; set; }

        public string? Tags { get; set; }

        public string? Description { get; set; }

    }

    public class Links
    {
        public LinkInfo? Self { get; set; }

    }

    public class LinkInfo
    {
        public string Href { get; set; }

        public string Title { get; set; }

        public string Method { get; set; }
        public string Encoding { get; set; }
        public Schema Schema { get; set; }
    }


    public class GetResponse<T>
    {
        [JsonProperty("@controls")]
        public Dictionary<string, Control> Controls { get; set; }

        public int StatusCode { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public DateTime Timestamp { get; set; }

        public string? RequestId { get; set; }
    }

    public class Control
    {
        public string Href { get; set; }
        public string Method { get; set; }

        public string Encoding { get; set; }

        public string Title { get; set; }

        public Schema Schema { get; set; }
    }

    public class Schema
    {
        public string Type { get; set; }

        public Dictionary<string, SchemaProperty> Properties { get; set; }

        public string[] Required { get; set; }
    }

    public class SchemaProperty
    {
        public string Description { get; set; }

        public string Type { get; set; }

        public string Format { get; set; }
    }



}