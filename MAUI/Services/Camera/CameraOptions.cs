namespace Pantheas.Toolkit.MAUI.Services.Camera;

public class CameraServiceOptions
{
    private const string FOLDER_NAME = "camera";


    public string StorageFolderPath { get; set; } =
        GetStorageFolderPath();



    private static string GetStorageFolderPath()
    {
        string appStoragePath = Environment.GetFolderPath(
            Environment.SpecialFolder.MyDocuments);


        return Path.Combine(
            appStoragePath,
            FOLDER_NAME);
    }
}
   