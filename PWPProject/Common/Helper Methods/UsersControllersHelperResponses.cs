using Common.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper_Methods
{
    public static class UsersControllersHelperResponses
    {
        // Generic method to get controls for user
        public static Dictionary<string, Control> GetControlsForUser()
        {
            return new Dictionary<string, Control>
            {
                ["mumeta:create-new-user"] = new Control
                {
                    Href = $"{ApplicationConstants.UserBaseUrl}/User",
                    Method = "POST",
                    Encoding = "json",
                    Title = "Get a video by Id",
                    Schema = new Common.BusinessEntities.Schema
                    {
                        Type = "object",
                        Properties = new Dictionary<string, SchemaProperty>
                        {
                            ["Email"] = new SchemaProperty
                            {
                                Description = "talha@example.co",
                                Type = "string"
                            },
                            ["Username"] = new SchemaProperty
                            {
                                Description = "Talha",
                                Type = "string"
                            },
                            ["Password"] = new SchemaProperty
                            {
                                Description = "Password",
                                Type = "string"
                            },
                            ["Imagepath"] = new SchemaProperty
                            {
                                Description = "/image/talha.jpg",
                                Type = "string"
                            }
                        },
                        Required = new[] { "Email", "Username", "Password" }
                    }
                },
                ["mumeta:delete-user"] = new Control
                {
                    Href = $"{ApplicationConstants.UserBaseUrl}/User/{{id}}",
                    Method = "DELETE",
                    Encoding = "json",
                    Title = "Delete a user by Id",
                    Schema = new Common.BusinessEntities.Schema
                    {
                        Type = "object",
                        Properties = new Dictionary<string, SchemaProperty>
                        {
                            ["id"] = new SchemaProperty
                            {
                                Description = "User Id",
                                Type = "integer"
                            }
                        },
                        Required = new[] { "id" }
                    }
                }
            };

        }

        public static Dictionary<string, Control> GetControlsForStats()
        {
            return new Dictionary<string, Control>
            {
                ["mumeta:get-all-videos"] = new Control
                {
                    Href = $"{ApplicationConstants.UserBaseUrl}/videos",
                    Method = "GET",
                    Encoding = "json",
                    Title = "Get all videos",
                   
                },
                ["mumeta:get-all-users"] = new Control
                {
                    Href = $"{ApplicationConstants.UserBaseUrl}/Users",
                    Method = "GET",
                    Encoding = "json",
                    Title = "Get all users",
                }
            };

        }

    }
}
