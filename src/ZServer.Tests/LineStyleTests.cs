using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using ZMap.Extensions;
using ZMap.Renderer.SkiaSharp;
using ZMap.Style;

namespace ZServer.Tests
{
    public class LineStyleTests : BaseTests
    {
        [Fact]
        public async Task Stroke()
        {
            var data = GetFeatures();

            var style = new LineStyle
            {
                Opacity = CSharpExpression<float?>.New(1),
                Width = CSharpExpression<int?>.New(2),
                Color = CSharpExpression<string>.New("#3ed53e"),
                LineJoin = CSharpExpression<string>.New("Round"),
                Translate = CSharpExpression<double[]>.New(new double[] { 1, 1 }),
                DashArray = CSharpExpression<float[]>.New(Array.Empty<float>()),
                DashOffset = CSharpExpression<float?>.New(0),
                LineCap = CSharpExpression<string>.New("Round"),
                GapWidth = CSharpExpression<int?>.New(10),
                Offset = CSharpExpression<int?>.New(0),
                Blur = CSharpExpression<int?>.New(0),
                Gradient = CSharpExpression<int?>.New(0)
            };

            var width = 1024;
            var height = 1024;
            var graphicsService =
                new SkiaGraphicsService(Guid.NewGuid().ToString(), width, height);

            foreach (var feature in data)
            {
                graphicsService.Render(Extent, feature.Geometry, style);
            }

            var stream = graphicsService.GetImage("image/png");
            await File.WriteAllBytesAsync($"images/Stroke.png", await stream.ToArrayAsync());
        }

        [Fact]
        public async Task Dash()
        {
            var data = GetFeatures();

            var style = new LineStyle
            {
                Opacity = CSharpExpression<float?>.New(1),
                Width = CSharpExpression<int?>.New(2),
                Color = CSharpExpression<string>.New("#3ed53e"),
                DashArray = CSharpExpression<float[]>.New(new[] { 20, 20f }),
                DashOffset = CSharpExpression<float?>.New(20),
                LineCap = CSharpExpression<string>.New("Round"),
                LineJoin = CSharpExpression<string>.New("Round"),
                Translate = CSharpExpression<double[]>.New(new double[] { 1, 1 }),
                GapWidth = CSharpExpression<int?>.New(10),
                Offset = CSharpExpression<int?>.New(0),
                Blur = CSharpExpression<int?>.New(0),
                Gradient = CSharpExpression<int?>.New(0)
            };

            var width = 1024;
            var height = 1024;
            var graphicsService =
                new SkiaGraphicsService(Guid.NewGuid().ToString(), width, height);

            foreach (var feature in data)
            {
                graphicsService.Render(Extent, feature.Geometry, style);
            }

            var stream = graphicsService.GetImage("image/png");
            await File.WriteAllBytesAsync($"images/Dash.png", await stream.ToArrayAsync());
        }

        [Fact]
        public async Task Cap()
        {
            var data = GetFeatures();

            var style = new LineStyle
            {
                Opacity = CSharpExpression<float?>.New(1),
                Width = CSharpExpression<int?>.New(2),
                Color = CSharpExpression<string>.New("#3ed53e"),
                DashArray = CSharpExpression<float[]>.New(new[] { 5, 5f }),
                DashOffset = CSharpExpression<float?>.New(10),
                LineCap = CSharpExpression<string>.New("Round"),
                LineJoin = CSharpExpression<string>.New("Round"),
                Translate = CSharpExpression<double[]>.New(new double[] { 1, 1 }),
                GapWidth = CSharpExpression<int?>.New(10),
                Offset = CSharpExpression<int?>.New(0),
                Blur = CSharpExpression<int?>.New(0),
                Gradient = CSharpExpression<int?>.New(0)
            };

            var width = 1024;
            var height = 1024;
            var graphicsService =
                new SkiaGraphicsService(Guid.NewGuid().ToString(), width, height);

            foreach (var feature in data)
            {
                graphicsService.Render(Extent, feature.Geometry, style);
            }

            var stream = graphicsService.GetImage("image/png");
            await File.WriteAllBytesAsync($"images/CapDash.png", await stream.ToArrayAsync());
        }
    }
}