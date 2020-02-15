using System.ComponentModel;
using System.Windows.Forms;

namespace CreatePhotosFolder
{
    public static class InvokeExtension
    {
        public static void InvokeIfRequired<T>(this T control, MethodInvoker action, bool block = false) where T : ISynchronizeInvoke
        {
            var source = (ISynchronizeInvoke)control;

            if (source.InvokeRequired)
            {
                if (block)
                    source.Invoke(action, null);
                else
                    source.BeginInvoke(action, null);
            }
            else
            {
                action();
            }
        }
    }
}
