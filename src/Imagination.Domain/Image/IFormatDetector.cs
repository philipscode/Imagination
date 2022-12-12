namespace Imagination.Domain.Image;

public interface IFormatDetector
{
    Task<bool> IsImage(Stream stream, CancellationToken ct = default);
    
    Task<bool> IsJpeg(Stream stream, CancellationToken ct = default);
}
