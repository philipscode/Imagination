using Imagination.Domain.Extensions;
using Imagination.Domain.Image;
using Imagination.Domain.Result;
using SixLabors.ImageSharp;
using IImage = Imagination.Domain.Image.IImage;

namespace Imagination.Infrastructure.Image;

internal class SixLaborsImageFactory : IImageFactory
{
    public async Task<Result<IImage, FailureReason>> CreateFromStream(Stream stream, CancellationToken ct = default)
    {
        try
        {
            var image = await SixLabors.ImageSharp.Image.LoadAsync(stream.SeekToBegin(), ct)
                .ConfigureAwait(false);
            return new SixLaborsImage(image);
        }
        catch (UnknownImageFormatException)
        {
            return FailureReason.From("Unknown image format");
        }
        catch (InvalidImageContentException e)
        {
            return FailureReason.From($"Invalid image content: {e.Message}");
        }
    }
}
