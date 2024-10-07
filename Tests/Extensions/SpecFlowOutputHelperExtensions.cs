using TechTalk.SpecFlow.Infrastructure;

namespace Tests.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="ISpecFlowOutputHelper"/> to facilitate adding html tags to the SpecFlow output.
    /// </summary>
    public static class SpecFlowOutputHelperExtensions
    {
        /// <summary>
        /// Adds a custom tag to the SpecFlow output.
        /// </summary>
        /// <param name="outputHelper">The SpecFlow output helper instance.</param>
        /// <param name="tag">The tag to be added.</param>
        public static void AddTag(this ISpecFlowOutputHelper outputHelper, string tag)
        {
            outputHelper.WriteLine($"ReplaceTag[{tag}]");
        }

        /// <summary>
        /// Adds an image tag to the SpecFlow output.
        /// </summary>
        /// <param name="outputHelper">The SpecFlow output helper instance.</param>
        /// <param name="imagePath">The path to the image.</param>
        /// <param name="altText">The alt text for the image.</param>
        public static void AddImg(this ISpecFlowOutputHelper outputHelper, string imagePath, string altText)
        {
            var tag = $"<img src=\"{imagePath}\" alt=\"{altText}\" style=\"max-width: 100%; height: auto;\">";
            outputHelper.AddTag(tag);
        }

        /// <summary>
        /// Embeds a Base64 image into the SpecFlow output.
        /// </summary>
        /// <param name="outputHelper">The SpecFlow output helper instance.</param>
        /// <param name="base64">The Base64 encoded image string.</param>
        /// <param name="altText">The alt text for the image.</param>
        public static void EmbedBase64Image(this ISpecFlowOutputHelper outputHelper, string base64, string altText)
        {
            var imgTag = $"<img src=\"data:image/png;base64,{base64}\" alt=\"{altText}\" style=\"max-width: 100%; height: auto;\">";
            outputHelper.AddTag(imgTag);
        }
    }
}
