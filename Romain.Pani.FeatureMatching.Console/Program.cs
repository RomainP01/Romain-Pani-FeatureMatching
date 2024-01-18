using System.Text.Json;
using Romain.Pani.FeatureMatching;

var objectImagePath = args[0];
var scenesFolderPath = args[1];
var objectImageData = await File.ReadAllBytesAsync(objectImagePath);
var imageScenesData = new List<byte[]>();
foreach (var imagePath in Directory.EnumerateFiles(scenesFolderPath))
{
    var imageBytes = await File.ReadAllBytesAsync(imagePath);
    imageScenesData.Add(imageBytes);
}

var detectObjectInScenesResults = await new
    ObjectDetection().DetectObjectInScenesAsync(objectImageData, imageScenesData);

foreach (var objectDetectionResult in detectObjectInScenesResults)
{
    System.Console.WriteLine($"Points:{JsonSerializer.Serialize(objectDetectionResult.Points)}");
}


    