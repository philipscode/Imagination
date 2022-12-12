namespace Imagination.Domain.Extensions;

public static class StreamExtensions
{
    public static Stream SeekToBegin(this Stream stream)
    {
        if (stream.Position != 0)
        {
            stream.Seek(0, SeekOrigin.Begin);
        }

        return stream;
    }
}
