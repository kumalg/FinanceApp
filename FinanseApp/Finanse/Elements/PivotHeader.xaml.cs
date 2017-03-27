﻿using Windows.UI.Xaml;

namespace Finanse.Elements {
    public sealed partial class PivotHeader {
        public static readonly DependencyProperty GlyphProperty = DependencyProperty.Register("Glyph", typeof(string), typeof(PivotHeader), null);

        public string Glyph {
            get { return GetValue(GlyphProperty) as string; }
            set {  SetValue(GlyphProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(PivotHeader), null);

        public string Label {
            get { return GetValue(LabelProperty) as string; }
            set { SetValue(LabelProperty, value); }
        }


        public PivotHeader() {
            InitializeComponent();
            DataContext = this;
        }
    }
}
