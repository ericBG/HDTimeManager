using System;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace HDTimeManager
{
    public class Extensions
    {
        public static readonly DependencyProperty SelectedProperty = DependencyProperty.RegisterAttached(
            "Selected", typeof (IList), typeof (Extensions), new FrameworkPropertyMetadata(default(IList), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, HookSelectionChanged));

        private static void HookSelectionChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ListBox lb = sender as ListBox;
            if (lb == null)
                throw new ArgumentException("This property currently only supports DependencyObjects inheriting from ListBox.", nameof(sender));
            lb.SelectionChanged += SelectionChanged;
        }

        private static void SelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs) 
            => SetSelected((ListBox)sender, ((ListBox)sender).SelectedItems.Cast<object>().ToList());

        public static void SetSelected(DependencyObject element, IList value) => element.SetValue(SelectedProperty, value);

        public static IList GetSelected(DependencyObject element) => (IList) element.GetValue(SelectedProperty);
    }
}