using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProcessMonitor.Views
{
    /// <summary>
    /// Logika interakcji dla klasy ProcessControlWindow.xaml
    /// </summary>
    public partial class ProcessControlWindow : Window
    {
        public ProcessControlWindow()
        {
            InitializeComponent();
        }

        private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
