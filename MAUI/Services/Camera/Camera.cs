using Pantheas.Toolkit.Core.Interfaces.Services;

namespace Pantheas.Toolkit.MAUI.Services.Camera;

public class Camera :
    ICamera
{
    public static CameraServiceOptions Options { get; set; } =
        new CameraServiceOptions();


    public bool IsCapturingSupported =>
        MediaPicker.Default.IsCaptureSupported;


    /// <summary>
    /// <para>Captures a photo - if supported - and saves to to the file system.</para>
    /// The photo is saved to the path definded in <see cref="CameraServiceOptions.StorageFolderPath" />
    /// </summary>
    /// <returns><see cref="Task{string}"/> containing the file path</returns>
    public async Task<string> CapturePhotoAsync()
    {
        if (!IsCapturingSupported)
        {
            return string.Empty;
        }


        FileResult photo = await MediaPicker.Default.CapturePhotoAsync();


        return await SaveToFileSystemAsync(
            photo);
    }

    /// <summary>
    /// <para>Captures a photo - if support - and saves to to the file system.</para>
    /// The video is saved to the path definded in <see cref="CameraServiceOptions.StorageFolderPath" />
    /// </summary>
    /// <returns><see cref="Task{TResult}"/> containing the file path</returns>
    public async Task<string> CaptureVideoAsync()
    {
        if (!IsCapturingSupported)
        {
            return string.Empty;
        }


        FileResult video = await MediaPicker.Default.CaptureVideoAsync();


        return await SaveToFileSystemAsync(
            video);
    }


    private async Task<string> SaveToFileSystemAsync(
        FileResult file)
    {
        string localFilePath = Path.Combine(
            Options.StorageFolderPath,
            file.FileName);

        using Stream sourceStream = await file.OpenReadAsync();
        using FileStream localFileStream = File.OpenWrite(
            localFilePath);

        await sourceStream.CopyToAsync(
            localFileStream);


        return localFilePath;
    }
}
