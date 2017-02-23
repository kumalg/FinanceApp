﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Finanse.Models     {
    public sealed class ExpandPanel : ContentControl {
        public ExpandPanel() {
            this.DefaultStyleKey = typeof(ExpandPanel);
        }

        private bool _useTransitions = true;
        private VisualState _collapsedState;
        private Windows.UI.Xaml.Controls.Primitives.ToggleButton toggleExpander;
        private FrameworkElement contentElement;

        public static readonly DependencyProperty HeaderContentProperty =
        DependencyProperty.Register("HeaderContent", typeof(object),
        typeof(ExpandPanel), null);

        public static readonly DependencyProperty IsExpandedProperty =
        DependencyProperty.Register("IsExpanded", typeof(bool),
        typeof(ExpandPanel), new PropertyMetadata(true));

        public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register("CornerRadius", typeof(CornerRadius),
        typeof(ExpandPanel), null);

        public object HeaderContent {
            get {
                return GetValue(HeaderContentProperty);
            }
            set {
                SetValue(HeaderContentProperty, value);
            }
        }

        public bool IsExpanded {
            get {
                return (bool)GetValue(IsExpandedProperty);
            }
            set {
                SetValue(IsExpandedProperty, value);
            }
        }

        public CornerRadius CornerRadius {
            get {
                return (CornerRadius)GetValue(CornerRadiusProperty);
            }
            set {
                SetValue(CornerRadiusProperty, value);
            }
        }

        private void ChangeVisualState(bool useTransitions) {
            if (IsExpanded) {
                if (contentElement != null) {
                    contentElement.Visibility = Visibility.Visible;
                }
                VisualStateManager.GoToState(this, "Expanded", useTransitions);
            }
            else {
                VisualStateManager.GoToState(this, "Collapsed", useTransitions);
                _collapsedState = (VisualState)GetTemplateChild("Collapsed");
                if (_collapsedState == null) {
                    if (contentElement != null) {
                        contentElement.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        protected override void OnApplyTemplate() {
            base.OnApplyTemplate();
            toggleExpander = (Windows.UI.Xaml.Controls.Primitives.ToggleButton)
                GetTemplateChild("ExpandCollapseButton");
            if (toggleExpander != null) {
                toggleExpander.Click += (object sender, RoutedEventArgs e) =>
                {
                    IsExpanded = !IsExpanded;
                    toggleExpander.IsChecked = IsExpanded;
                    ChangeVisualState(_useTransitions);
                };
            }
            contentElement = (FrameworkElement)GetTemplateChild("Content");
            if (contentElement != null) {
                _collapsedState = (VisualState)GetTemplateChild("Collapsed");
                if ((_collapsedState != null) && (_collapsedState.Storyboard != null)) {
                    _collapsedState.Storyboard.Completed += (object sender, object e) =>
                    {
                        contentElement.Visibility = Visibility.Collapsed;
                    };
                }
            }
            ChangeVisualState(false);
        }
    }
}
