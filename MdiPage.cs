#if NETFRAMEWORK
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharedTools
{
    public class MdiPage : UserControl
    {
        protected MdiForm Navigator { get; private set; }

        public MdiPage()
        {
            Load += MdiPage_Load;
        }

        private void MdiPage_Load(object sender, EventArgs e)
        {
            Navigator = (MdiForm)FindForm();
            Task.Factory.StartNew(DoWork);
        }

        protected virtual void DoWork()
        {
        }
    }
}
#endif