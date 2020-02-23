#if NETFRAMEWORK
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;

namespace SharedTools
{
    public static class WinFormsExtensions
    {
        public static void AppendLine(this TextBox textBox, string line)
        {
            textBox.AppendText(line + Environment.NewLine);
        }

        public static int PreferredWidth(this ListBox listBox)
        {
            int größteBreite = 0;

            using (Graphics g = listBox.CreateGraphics())
            {
                foreach (object item in listBox.Items)
                {
                    int breite = (int)Math.Ceiling(g.MeasureString(item.ToString(), listBox.Font).Width);
                    if (breite > größteBreite) größteBreite = breite;
                }
            }
            return größteBreite + listBox.Margin.Horizontal;
        }

        public static void FillFromGrouping(this TreeView treeView, IEnumerable<IGrouping<string, string>> groupedList)
        {
            foreach (IGrouping<string, string> klasse in groupedList)
            {
                TreeNode root = new TreeNode(klasse.Key);
                foreach (string item in klasse) root.Nodes.Add(item);
                treeView.Nodes.Add(root);
            }
        }

        public static void FillFromGrouping<T>(this TreeView treeView, IEnumerable<IGrouping<string, T>> groupedList, Func<T, string> valueSelector)
        {
            foreach (IGrouping<string, T> klasse in groupedList)
            {
                TreeNode root = new TreeNode(klasse.Key);
                foreach (T elem in klasse) root.Nodes.Add(valueSelector(elem));
                treeView.Nodes.Add(root);
            }
        }

        public static void FillFromGrouping(this ListView listView, IEnumerable<IGrouping<string, string>> groupedList)
        {
            foreach (IGrouping<string, string> klasse in groupedList)
            {
                ListViewGroup root = new ListViewGroup(klasse.Key);
                listView.Groups.Add(root);

                foreach (string elem in klasse)
                {
                    ListViewItem lvi = new ListViewItem(elem);
                    lvi.Group = root;
                    listView.Items.Add(lvi);
                }
            }
        }

        public static void FillFromGrouping<T>(this ListView listView, IEnumerable<IGrouping<string, T>> groupedList, Func<T, string> valueSelector)
        {
            foreach (IGrouping<string, T> klasse in groupedList)
            {
                ListViewGroup root = new ListViewGroup(klasse.Key);
                listView.Groups.Add(root);

                foreach (T elem in klasse)
                {
                    ListViewItem lvi = new ListViewItem(valueSelector(elem));
                    lvi.Group = root;
                    listView.Items.Add(lvi);
                }
            }
        }

        private delegate void SetPropertyThreadSafeDelegate<TControl, TResult>(TControl control, Expression<Func<TControl, TResult>> property, TResult value);

        public static void SetPropertyThreadSafe<TControl, TResult>(this TControl control, Expression<Func<TControl, TResult>> property, TResult value) where TControl : Control
        {
            if (control.IsDisposed) return;
            MemberExpression memberExpression = (MemberExpression)property.Body;
            if (memberExpression.NodeType != ExpressionType.Parameter && memberExpression.NodeType != ExpressionType.MemberAccess) throw new ArgumentException("property");
            PropertyInfo info = (PropertyInfo)memberExpression.Member;

            if (control.InvokeRequired)
            {
                try
                {
                    control.Invoke(new SetPropertyThreadSafeDelegate<TControl, TResult>(SetPropertyThreadSafe), new object[] { control, property, value });
                }
                catch (ObjectDisposedException) { }
            }
            else
            {
                info.SetValue(control, value, null);
            }
        }

        public static void SetFullscreenMode(this Form fenster, bool vollbild)
        {
            if (vollbild)
            {
                fenster.WindowState = FormWindowState.Normal;
                fenster.FormBorderStyle = FormBorderStyle.None;
                fenster.Bounds = Screen.GetBounds(fenster);
            }
            else
            {
                fenster.WindowState = FormWindowState.Maximized;
                fenster.FormBorderStyle = FormBorderStyle.Sizable;
            }
        }

        public static void InvokeGeneric<T>(this Control c, Action func)
        {
            if (c.InvokeRequired)
                c.Invoke(func);
            else
                func();
        }

        public static T InvokeGeneric<T>(this Control c, Func<T> func)
        {
            if (c.InvokeRequired)
                return (T)c.Invoke(func);
            else
                return func();
        }

        public static void BeginInvokeGeneric<T>(this Control c, Action func)
        {
            if (c.InvokeRequired)
                c.BeginInvoke(func);
            else
                func();
        }
    }
}
#endif