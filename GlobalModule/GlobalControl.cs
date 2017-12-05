using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalModule
{
    public class GlobalControl
    {
        public static void MessageBoxDialog(string msg, string title, MessageType? type)
        {
            MessageBoxDialog box = null;
            if (type!=null)
                box=new MessageBoxDialog(msg,title,type.Value);
            else if (!string.IsNullOrEmpty(title))
                box = new MessageBoxDialog(msg, title);
            else
                box = new MessageBoxDialog(msg);
            box.ShowDialog();
        }

        public static bool MessageBoxDialogYesOrNo(string msg, string title, MessageType? type)
        {
            MessageBoxDialogYesOrNo box = null;
            if (type != null)
                box = new MessageBoxDialogYesOrNo(msg, title, type.Value);
            else if (!string.IsNullOrEmpty(title))
                box = new MessageBoxDialogYesOrNo(msg, title);
            else
                box = new MessageBoxDialogYesOrNo(msg);
            return box.ShowDialog().Value;
        }
    }
}
