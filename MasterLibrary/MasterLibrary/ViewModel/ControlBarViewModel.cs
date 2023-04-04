using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using Window = System.Windows.Window;

namespace MasterLibrary.ViewModel
{
    public class ControlBarViewModel : BaseViewModel
    {
        public ICommand closeWindowCommand { get; set; }
        public ICommand maximizeWindowCommand { get; set; }
        public ICommand minimizeWindowCommand { get; set; }
        public ICommand MouseMoveWindowCommand { get; set; }

        public ControlBarViewModel()
        {
            closeWindowCommand = new RelayCommand<UserControl>((p) => { return p != null; }, (p) => { 
                FrameworkElement window = getWindowParent(p);
                var w = (window as Window);
                if (w != null)
                {
                    w.Close();
                }
                }
            );

            //maximizeWindowCommand = new RelayCommand<UserControl>((p) => { return p != null; }, (p) => {
            //    FrameworkElement window = getWindowParent(p);
            //    var w = (window as Window);
            //    if (w != null)
            //    {
            //        if (w.WindowState != WindowState.Maximized)
            //        {
            //            w.WindowState = WindowState.Maximized;
            //        }
            //        else
            //        {
            //            w.WindowState = WindowState.Normal;
            //        }
            //    }
            //}
            //);

            minimizeWindowCommand = new RelayCommand<UserControl>((p) => { return p != null; }, (p) => {
                FrameworkElement window = getWindowParent(p);
                var w = (window as Window);
                if (w != null)
                {
                    if (w.WindowState != WindowState.Minimized)
                    {
                        w.WindowState = WindowState.Minimized;
                    }
                    else
                    {
                        w.WindowState = WindowState.Maximized;
                    }
                }
            }
            );

            MouseMoveWindowCommand = new RelayCommand<UserControl>((p) => { return p != null; }, (p) =>
            {
                FrameworkElement window = getWindowParent(p);
                var w = (window as Window);
                if (w != null)
                {
                    w.DragMove();
                }
            }
            );
        }

        FrameworkElement getWindowParent(UserControl p)
        {
            FrameworkElement parent = p;
            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }
            return parent;
        } 
    }
}
