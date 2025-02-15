using System.IO;
using SkiaSharp;
using ZMap.Extensions;
using ZMap.Renderer.SkiaSharp.Extensions;
using ZMap.Renderer.SkiaSharp.Utilities;
using ZMap.Style;

namespace ZMap.Renderer.SkiaSharp
{
    public class ResourceFillStyleRenderer : FillStyleRenderer
    {
        private static readonly SKPathEffect Times1 = SKPathEffect.Create2DLine(1,
            SKMatrix.CreateSkew(10, 10).Concat(SKMatrix.CreateRotationDegrees(45)));

        private static readonly SKPathEffect Times2 = SKPathEffect.Create2DLine(1,
            SKMatrix.CreateSkew(10, 10).Concat(SKMatrix.CreateRotationDegrees(135)));

        private static readonly SKPathEffect Times = SKPathEffect.CreateSum(Times1, Times2);

        public ResourceFillStyleRenderer(ResourceFillStyle style) :
            base(style)
        {
        }

        protected override SKPaint CreatePaint()
        {
            var style = (ResourceFillStyle)Style;

            var opacity = style.Opacity.Value ?? 1;
            var color = style.Color?.Value;
            var uri = style.Uri?.Value;
            var antialias = Style.Antialias;

            if (uri == null)
            {
                return base.CreatePaint();
            }

            SKPaint paint;
            switch (uri.Scheme)
            {
                case "file":
                {
                    var path = uri.ToPath();
                    paint = GetDefaultPaint(color, opacity, antialias);
                    if (File.Exists(path))
                    {
                        paint.Shader = SKShader.CreateBitmap(SKBitmap.Decode(path), SKShaderTileMode.Repeat,
                            SKShaderTileMode.Repeat);
                    }

                    break;
                }
                case "shape":
                {
                    paint = GetDefaultPaint(color, opacity, antialias);
                    paint.PathEffect = uri.DnsSafeHost switch
                    {
                        "times" => Times,
                        _ => paint.PathEffect
                    };

                    break;
                }
                default:
                {
                    paint = GetDefaultPaint(color, opacity, antialias);
                    break;
                }
            }

            return paint;
        }

        private SKPaint GetDefaultPaint(string color, float opacity, bool antialias)
        {
            var paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                IsAntialias = antialias,
                Color = ColorUtilities.GetColor(color, opacity)
            };
            return paint;
        }
    }
}