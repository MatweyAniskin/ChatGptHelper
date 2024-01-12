using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ChatGptHelper.Pages
{
    public class PagesDelegates
    {
        public delegate void PageDefaultDelegate();
        public delegate void PageDelegate(Page page);
        public delegate void WindowDelegate(Window window);
    }
}
