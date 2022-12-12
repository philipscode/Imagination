using System.Net.Mime;
using Imagination;
using Imagination.Domain.Extensions;
using Imagination.Domain.Image;
using Imagination.Domain.Result;
using Imagination.Infrastructure;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureServices();
builder.Services.AddTelemetry();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapPost(
    "/convert",
    async (
        HttpRequest request,
        [FromServices] IFormatDetector formatDetector,
        [FromServices] IImageFactory imageFactory,
        CancellationToken ct) =>
    {
        request.EnableBuffering();
        var body = request.Body;
        
        if (await formatDetector.IsJpeg(body, ct))
        {
            return Results.File(body.SeekToBegin(), contentType: MediaTypeNames.Image.Jpeg);
        }

        if (!await formatDetector.IsImage(body, ct))
        {
            return Results.Text("Empty or invalid image file");
        }

        return await imageFactory.CreateFromStream(body, ct)
            .Match(
                image => Results.Stream(
                    outputStream => image.ConvertToJpegAndDispose(outputStream, ct),
                    contentType: MediaTypeNames.Image.Jpeg),
                error => Results.Text(error.Value));
    });

app.Run();
