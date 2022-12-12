namespace Imagination.Domain.Image;

public interface IImage
{
    Task ConvertToJpegAndDispose(Stream outputStream, CancellationToken ct = default);
}
