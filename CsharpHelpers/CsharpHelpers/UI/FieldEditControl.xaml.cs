using System.Windows;
using System.Windows.Controls;

namespace CsharpHelpers.UI
{
    /// <summary>
    /// Interaction logic for FieldEditControl.xaml
    /// </summary>
    public partial class FieldEditControl : UserControl
    {
        public static readonly DependencyProperty LabelProperty
            = DependencyProperty.Register("Label", typeof(string), typeof(FieldEditControl));

        public static readonly DependencyProperty TextProperty
            = DependencyProperty.Register("Text", typeof(string), typeof(FieldEditControl),
                new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public string Label
        {
            get { return (string) GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public FieldEditControl()
        {
            InitializeComponent();
        }
    }
}
