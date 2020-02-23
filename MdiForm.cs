#if NETFRAMEWORK
using System;
using System.Windows.Forms;

namespace SharedTools
{
    public class MdiForm : Form
    {
        protected MdiForm(Type startpage)
        {
            MdiPage page = (MdiPage)Activator.CreateInstance(startpage);
            page.Dock = DockStyle.Fill;
            Controls.Add(page);
        }

        public T NavigateTo<T>(params object[] extraData) where T : MdiPage, new()
        {
            if (InvokeRequired)
            {
                return this.InvokeGeneric(() => NavigateTo<T>(extraData));
            }
            else
            {
                T page = null;

                foreach (Control control in Controls)
                {
                    if (page == null && control is T)
                    {
                        page = (T)control;
                        control.Visible = true;
                    }
                    else
                    {
                        control.Visible = false;
                    }
                }
                if (page != null) return page;
                page = (T)Activator.CreateInstance(typeof(T), extraData);
                page.Dock = DockStyle.Fill;
                Controls.Add(page);
                return page;
            }
        }
    }
}
#endif