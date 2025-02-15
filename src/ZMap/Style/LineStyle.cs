namespace ZMap.Style
{
    public class LineStyle : VectorStyle, IStrokeStyle
    {
        /// <summary>
        /// 线的不透明度（可选，取值范围为 0 ~ 1，默认值为 1）
        /// </summary>
        public CSharpExpression<float?> Opacity { get; set; }

        /// <summary>
        /// 线用的图案（可选，这里填写在 sprite 雪碧图中图标名称。为了图案能无缝填充，图标的高宽需要是 2 的倍数）
        /// </summary>
        public CSharpExpression<string> Pattern { get; set; }

        /// <summary>
        /// 线的宽度（可选，值 >= 0，默认值为 1，单位：像素）
        /// </summary>
        public CSharpExpression<int?> Width { get; set; }

        /// <summary>
        /// 线的颜色（可选，默认值为 #000000。如果设置了 line-pattern，则 line-color 将无效）
        /// </summary>
        public CSharpExpression<string> Color { get; set; }

        /// <summary>
        /// 虚线的破折号部分和间隔的长度（可选，默认值为 [0, 0]。如果设置了 line-pattern，则 line-dasharray 将无效）
        /// </summary>
        public CSharpExpression<float[]> DashArray { get; set; }

        public CSharpExpression<float?> DashOffset { get; set; }
        public CSharpExpression<string> LineJoin { get; set; }
        public CSharpExpression<string> LineCap { get; set; }

        /// <summary>
        /// 线的平移（可选，通过平移 [x, y] 达到一定的偏移量。默认值为 [0, 0]，单位：像素。）
        /// </summary>
        public CSharpExpression<double[]> Translate { get; set; }

        /// <summary>
        /// 线的平移锚点，即相对的参考物（可选，可选值为 map、viewport，默认为 map）
        /// </summary>
        public CSharpExpression<TranslateAnchor> TranslateAnchor { get; set; }

        public CSharpExpression<int?> GapWidth { get; set; }
        public CSharpExpression<int?> Offset { get; set; }

        /// <summary>
        /// 线的模糊度（可选，值 >= 0，默认值为 0，单位：像素）
        /// </summary>
        public CSharpExpression<int?> Blur { get; set; }

        /// <summary>
        /// Disabled by dasharray. Disabled by pattern
        /// </summary>
        public CSharpExpression<int?> Gradient { get; set; }

        public override void Accept(IZMapStyleVisitor visitor, Feature feature)
        {
            base.Accept(visitor, feature);

            Opacity?.Invoke(feature, 1);
            Pattern?.Invoke(feature);
            Width?.Invoke(feature, 1);
            Color?.Invoke(feature, "#000000");
            DashArray?.Invoke(feature);
            LineJoin?.Invoke(feature);
            LineCap?.Invoke(feature);
            Translate?.Invoke(feature);
            TranslateAnchor?.Invoke(feature);
            GapWidth?.Invoke(feature);
            Offset?.Invoke(feature);
            Blur?.Invoke(feature);
            Gradient?.Invoke(feature);
        }

        public override Style Clone()
        {
            return new LineStyle
            {
                MaxZoom = MaxZoom,
                MinZoom = MinZoom,
                ZoomUnit = ZoomUnit,
                Filter = Filter?.Clone(),
                Opacity = Opacity?.Clone(),
                Pattern = Pattern?.Clone(),
                Color = Color?.Clone(),
                Translate = Translate?.Clone(),
                TranslateAnchor = TranslateAnchor?.Clone(),
                Width = Width?.Clone(),
                DashArray = DashArray?.Clone(),
                DashOffset = DashOffset?.Clone(),
                LineJoin = LineJoin?.Clone(),
                LineCap = LineCap?.Clone(),
                GapWidth = GapWidth?.Clone(),
                Offset = Offset?.Clone(),
                Blur = Blur?.Clone(),
                Gradient = Gradient?.Clone()
            };
        }
    }
}