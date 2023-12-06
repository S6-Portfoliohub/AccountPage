using MessagingLayer.Models;
using System.Text.Json;

namespace MessagingLayer.Helper
{
    public static class SerializeMessagesHelper
    {
        public static ProjectMessageModel SerializeProject(string project)
        {
            ProjectMessageModel? projectMessage;
            try
            {
                projectMessage = JsonSerializer.Deserialize<ProjectMessageModel>(project);
            }
            catch (Exception)
            {
                throw new JsonException();
            }

            if (projectMessage == null)
            {
                throw new JsonException();
            }

            return projectMessage;
        }
    }
}
