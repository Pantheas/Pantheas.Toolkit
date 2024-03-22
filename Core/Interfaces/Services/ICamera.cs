namespace Pantheas.Toolkit.Core.Interfaces.Services;

public interface ICamera
{
    bool IsCapturingSupported { get; }

    Task<string> CapturePhotoAsync();
    Task<string> CaptureVideoAsync();
}
