using Imagination.Domain.Result;

namespace Imagination.Domain.Image;

public interface IImageFactory
{
    Task<Result<IImage, FailureReason>> CreateFromStream(Stream stream, CancellationToken ct = default);
}
