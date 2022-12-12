using Ardalis.GuardClauses;
using SixLabors.ImageSharp;

namespace Imagination.Infrastructure.Image;

internal class SixLaborsImage : Domain.Image.IImage
{
    private readonly SixLabors.ImageSharp.Image _image;

    internal SixLaborsImage(SixLabors.ImageSharp.Image sixLaborsImage)
    {
        Guard.Against.Null(sixLaborsImage, nameof(sixLaborsImage));
        _image = sixLaborsImage;
    }

    public async Task ConvertToJpegAndDispose(Stream outputStream, CancellationToken ct = default)
    {
        try
        {
            await _image.SaveAsJpegAsync(outputStream, ct).ConfigureAwait(false);
        }
        finally
        {
            _image.Dispose();
        }
    }
}
