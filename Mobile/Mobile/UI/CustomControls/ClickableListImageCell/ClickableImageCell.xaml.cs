using System.Windows.Input;
using Xamarin.Forms;

namespace Mobile.UI.CustomControls.ClickableListImageCell
{
    /// <summary>
    /// Custom control with picture & text = button/ClickableCell
    /// </summary>
    public partial class ClickableImageCell : ContentView
    {
        /// <summary>
        /// Custom control with picture & text = button/ClickableCell
        /// </summary>
        public ClickableImageCell()
        {
            InitializeComponent();
            SetClickGesture();
        }
       

        #region Properties
        /// <summary>
        /// Shows picture.
        /// </summary>
        public string ImageSource
        {
            set { SetValue(ImageSourceProperty, value); }
            get { return (string) GetValue(ImageSourceProperty); }
        }
        /// <summary>
        /// Shows title.
        /// </summary>
        public string Text
        {
            set { SetValue(TextProperty, value); }
            get { return (string) GetValue(TextProperty); }
        }
        /// <summary>
        /// Sets event on the click.
        /// </summary>
        public ICommand TapCommand
        {
            set { SetValue(TapCommandProperty, value); }
            get { return (ICommand) GetValue(TapCommandProperty); }
        }

        #endregion

        #region Bindable Properties

        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource),
            typeof(string), typeof(ClickableImageCell), "Icon.png", BindingMode.TwoWay,
            propertyChanged: ImageSourcePropertyChanged);

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text),
            typeof(string), typeof(ClickableImageCell), string.Empty, BindingMode.TwoWay,
            propertyChanged: TextPropertyChanged);

        public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(nameof(TapCommand),
            typeof(ICommand), typeof(ClickableImageCell));

        private ICommand MainCommand => new Command(() =>
        {
            TapCommand?.Execute(null);
        });

        #endregion

        #region PropertyChanged Methods

        private static void ImageSourcePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (ClickableImageCell) bindable;
            control.Image.Source = Xamarin.Forms.ImageSource.FromFile(newvalue.ToString());
        }

        private static void TextPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (ClickableImageCell) bindable;
            control.Label.Text = newvalue.ToString();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets click event in tapping on control.
        /// </summary>
        private void SetClickGesture()
        {
            GestureRecognizers.Add(new TapGestureRecognizer { Command = MainCommand });
        }

        #endregion
    }
}