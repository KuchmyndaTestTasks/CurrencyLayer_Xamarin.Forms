using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.UI.CustomControls.ClickableListImageCell
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClickableListImageCell : ContentView
    {
        public ClickableListImageCell()
        {
            InitializeComponent();
        }
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource),
            typeof(string), typeof(ClickableListImageCell), "Icon.png", BindingMode.TwoWay,
            propertyChanged: ImageSourcePropertyChanged);
            
        private static void ImageSourcePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (ClickableListImageCell) bindable;
            control.Image.Source = Xamarin.Forms.ImageSource.FromFile(newvalue.ToString());
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
            typeof(string), typeof(ClickableListImageCell), string.Empty, BindingMode.TwoWay,
            propertyChanged: TextPropertyChanged);

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command),
            typeof(Command), typeof(ClickableListImageCell), null, BindingMode.TwoWay,
            propertyChanged: CommandPropertyChanged);

        private static void CommandPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (ClickableListImageCell) bindable;
            control.Command = (Command) newvalue;
        }

        private static void TextPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (ClickableListImageCell) bindable;
            control.Label.Text = newvalue.ToString();
        }

        public string ImageSource
        {
            set { SetValue(ImageSourceProperty, value); }
            get { return (string) GetValue(ImageSourceProperty); }
        }

        public string Text
        {
            set { SetValue(TextProperty, value); }
            get { return (string) GetValue(TextProperty); }
        }

        public Command Command
        {
            set { SetValue(CommandProperty, value); }
            get { return (Command) GetValue(CommandProperty); }
        }
    }
}