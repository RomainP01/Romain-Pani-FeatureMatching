using System.Reflection;
using System.Text.Json;
using Xunit;

namespace Romain.Pani.FeatureMatching.Tests;

public class FeatureMatchingUnitTest
{
    [Fact]
    public async Task ObjectShouldBeDetectedCorrectly()
    {
        var executingPath = GetExecutingPath();
        var imageScenesData = new List<byte[]>();
        foreach (var imagePath in Directory.EnumerateFiles(Path.Combine(executingPath,
                     "Scenes")))
        {
            var imageBytes = await File.ReadAllBytesAsync(imagePath);
            imageScenesData.Add(imageBytes);
        }

        var objectImageData = await File.ReadAllBytesAsync(Path.Combine(executingPath,
            "Pani-Romain-object.jpg"));
        var detectObjectInScenesResults = await new
            ObjectDetection().DetectObjectInScenesAsync(objectImageData, imageScenesData);

        Assert.Equal("[{\"X\":1864,\"Y\":184},{\"X\":1165,\"Y\":1253},{\"X\":1857,\"Y\":1769},{\"X\":2639,\"Y\":767}]",
            JsonSerializer.Serialize(detectObjectInScenesResults[0].Points));

        Assert.Equal("[{\"X\":1864,\"Y\":1306},{\"X\":2018,\"Y\":870},{\"X\":1461,\"Y\":-157},{\"X\":1451,\"Y\":1757}]",
            JsonSerializer.Serialize(detectObjectInScenesResults[1].Points));
    }

    private static string GetExecutingPath()
    {
        var executingAssemblyPath = Assembly.GetExecutingAssembly().Location;
        var executingPath = Path.GetDirectoryName(executingAssemblyPath);
        return executingPath;
    }
}