using Imagination.Domain.Extensions;
using Imagination.Domain.Image;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace Imagination.Infrastructure.Image;

internal class SixLaborsFormatDetector : IFormatDetector
{
    public async Task<bool> IsImage(Stream stream, CancellationToken ct = default)
    {
        var format = await SixLabors.ImageSharp.Image.DetectFormatAsync(stream.SeekToBegin(), ct)
            .ConfigureAwait(false);

        return format != null;
    }

    public async Task<bool> IsJpeg(Stream stream, CancellationToken ct = default)
    {
        var format = await SixLabors.ImageSharp.Image.DetectFormatAsync(stream.SeekToBegin(), ct)
            .ConfigureAwait(false);

        return format?.Name == JpegFormat.Instance.Name;
    }
}
