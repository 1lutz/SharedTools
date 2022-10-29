#if NETFRAMEWORK
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace SharedTools
{
    public static class WpfExtensions
    {
        public static void FillFromGrouping<K, T>(this TreeView treeView, IEnumerable<IGrouping<K, T>> groupedList, bool expandAll = false)
        {
            foreach (IGrouping<K, T> temp in groupedList)
            {
                TreeViewItem root = new TreeViewItem() { Header = temp.Key };
                foreach (object item in temp) root.Items.Add(new TreeViewItem() { Header = item });
                if (expandAll) root.IsExpanded = true;
                treeView.Items.Add(root);
            }
        }

        public static TParent FindParent<TParent>(this DependencyObject quelle) where TParent : DependencyObject
        {
            while (!(quelle is TParent))
            {
                quelle = VisualTreeHelper.GetParent(quelle);
                if (quelle == null) return null;
            }
            return (TParent)quelle;
        }

        public static TParent FindParent<TParent, TContainer>(this DependencyObject quelle) where TParent : DependencyObject where TContainer : DependencyObject
        {
            while (!(quelle is TParent))
            {
                quelle = VisualTreeHelper.GetParent(quelle);
                if (quelle == null || quelle is TContainer) return null;
            }
            return (TParent)quelle;
        }

        public static bool HasParent<TParent>(this DependencyObject quelle) where TParent : DependencyObject
        {
            return FindParent<TParent>(quelle) != null;
        }

        public static bool HasParent<TParent, TContainer>(this DependencyObject quelle) where TParent : DependencyObject where TContainer : DependencyObject
        {
            return FindParent<TParent, TContainer>(quelle) != null;
        }

        public static T HitTestChildren<T>(this ItemsControl parent, Point point) where T : DependencyObject
        {
            UIElement element = parent.InputHitTest(point) as UIElement;
            return element?.FindParent<T, ItemsControl>();
        }

        public static T HitTestChildren<T>(this ItemsControl parent, MouseButtonEventArgs e) where T : DependencyObject
        {
            return HitTestChildren<T>(parent, e.GetPosition(parent));
        }

        public static ScrollViewer GetScrollViewer(this DependencyObject o)
        {
            if (o is ScrollViewer) return (ScrollViewer)o;

            for (int x = 0; x < VisualTreeHelper.GetChildrenCount(o); x++)
            {
                ScrollViewer result = GetScrollViewer(VisualTreeHelper.GetChild(o, x));
                if (result != null) return result;
            }
            return null;
        }

        private delegate void SetPropertyThreadSafeDelegate<TControl, TResult>(TControl control, Expression<Func<TControl, TResult>> property, TResult value);

        public static void SetPropertyThreadSafe<TControl, TResult>(this TControl control, Expression<Func<TControl, TResult>> property, TResult value) where TControl : DispatcherObject
        {
            MemberExpression memberExpression = (MemberExpression)property.Body;
            if (memberExpression.NodeType != ExpressionType.Parameter && memberExpression.NodeType != ExpressionType.MemberAccess) throw new ArgumentException("property");
            PropertyInfo info = (PropertyInfo)memberExpression.Member;

            if (control.CheckAccess())
            {
                info.SetValue(control, value, null);
            }
            else
            {
                try
                {
                    control.Dispatcher.Invoke(new SetPropertyThreadSafeDelegate<TControl, TResult>(SetPropertyThreadSafe), new object[] { control, property, value });
                }
                catch (ObjectDisposedException) { }
            }
        }

        public static void BeginInvoke(this Dispatcher dispatcher, Action func)
        {
            dispatcher.BeginInvoke(func);
        }

        public static void BeginInvoke<T>(this Dispatcher dispatcher, Action<T> func, T param)
        {
            dispatcher.BeginInvoke(func, param);
        }

        public static void BeginInvoke<T1, T2>(this Dispatcher dispatcher, Action<T1, T2> func, T1 param1, T2 param2)
        {
            dispatcher.BeginInvoke(func, param1, param2);
        }

        public static void BeginInvoke<T1, T2, T3>(this Dispatcher dispatcher, Action<T1, T2, T3> func, T1 param1, T2 param2, T3 param3)
        {
            dispatcher.BeginInvoke(func, param1, param2, param3);
        }

        public static void BeginInvoke<T1, T2, T3, T4>(this Dispatcher dispatcher, Action<T1, T2, T3, T4> func, T1 param1, T2 param2, T3 param3, T4 param4)
        {
            dispatcher.BeginInvoke(func, param1, param2, param3, param4);
        }

        public static void RepeatVideo(this MediaElement mediaElement, Uri source)
        {
            mediaElement.Source = source;
            mediaElement.Play();
            mediaElement.MediaEnded += MediaElement_MediaEnded;
        }

        private static void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            ((MediaElement)sender).Position = TimeSpan.FromMilliseconds(1);
        }

        public static void LoadWithEvents(this BitmapImage bild, Uri source, EventHandler completed, EventHandler<ExceptionEventArgs> failed)
        {
            bild.DownloadCompleted += completed;
            bild.DownloadFailed += failed;
            bild.BeginInit();
            bild.UriSource = source;
            bild.EndInit();
            if (!bild.IsDownloading) completed(null, EventArgs.Empty);
        }

        public static void TakeScreenshot(this FrameworkElement element, string pfad)
        {
            int breite = (int)element.RenderSize.Width;
            int höhe = (int)element.RenderSize.Height;
            RenderTargetBitmap target = new RenderTargetBitmap(breite, höhe, 96, 96, PixelFormats.Default);
            VisualBrush zeichner = new VisualBrush(element);
            DrawingVisual visual = new DrawingVisual();

            using (DrawingContext kontext = visual.RenderOpen())
            {
                kontext.DrawRectangle(zeichner, null, new Rect(0, 0, breite, höhe));
            }
            target.Render(visual);
            BitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(target));

            using (FileStream fs = new FileStream(pfad, FileMode.Create, FileAccess.Write))
            {
                encoder.Save(fs);
            }
        }
    }
}
#endif