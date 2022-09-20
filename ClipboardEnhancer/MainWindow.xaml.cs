using System;
using System.Diagnostics;
using System.Windows;
using WK.Libraries.SharpClipboardNS;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ClipboardEnhancer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        


        public SharpClipboard Clipboard;

        private readonly ILogger<MainWindow> _logger;

        public MainWindow()
        {
            InitializeComponent();


            using ILoggerFactory loggerFactory = 
                LoggerFactory.Create(builder =>
                    builder.AddSimpleConsole(options =>
                    {
                        options.IncludeScopes = true;
                        options.SingleLine = true;
                        options.TimestampFormat = "hh:mm:ss ";
                    }));

            _logger = loggerFactory.CreateLogger<MainWindow>();
            Clipboard = new SharpClipboard();
            Clipboard.ClipboardChanged += ClipboardChanged;

        }

        private void ClipboardChanged(Object? sender, WK.Libraries.SharpClipboardNS.SharpClipboard.ClipboardChangedEventArgs e)
        {
            if (e.ContentType == SharpClipboard.ContentTypes.Text)
            {
                Debug.WriteLine(Clipboard.ClipboardText);
            }
            Debug.WriteLine(e.SourceApplication.Name);
            Debug.WriteLine(e.SourceApplication.Title);
            _logger.LogInformation("Clipboard data changed to: {val}", Clipboard.ClipboardText);
        }
    }
}
