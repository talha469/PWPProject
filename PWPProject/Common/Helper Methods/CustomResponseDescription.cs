using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Xml.Linq;

public class CustomResponseDescription : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var methodInfo = context.MethodInfo;

        var responseTypeAttributes = methodInfo.GetCustomAttributes<ProducesResponseTypeAttribute>();

        foreach (var responseTypeAttribute in responseTypeAttributes)
        {
            var responseCode = responseTypeAttribute.StatusCode.ToString();

            if (operation.Responses.TryGetValue(responseCode, out var response))
            {
                var responseType = responseTypeAttribute.Type;

                var summary = GetResponseDescription(methodInfo, responseCode);

                if (!string.IsNullOrEmpty(summary))
                {
                    response.Description = summary;
                }
            }
        }
    }


    private string GetResponseDescription(MethodInfo methodInfo, string responseCode)
    {
        var assembly = Assembly.GetAssembly(methodInfo.DeclaringType);
        var resourceName = assembly.GetManifestResourceNames().FirstOrDefault(s => s.EndsWith(".xml"));

        if (resourceName == null)
            return null;

        using (var stream = assembly.GetManifestResourceStream(resourceName))
        {
            if (stream == null)
                return null;

            var xmlDoc = XDocument.Load(stream);

            var memberName = $"M:{methodInfo.DeclaringType.FullName}.{methodInfo.Name}";

            var memberNode = xmlDoc.Descendants("member")
                .FirstOrDefault(m => m.Attribute("name")?.Value == memberName);

            if (memberNode == null)
                return null;

            var responseElement = memberNode.Elements("response")
                .FirstOrDefault(e => e.Attribute("code")?.Value == responseCode);

            return responseElement?.Value;
        }
    }
}
