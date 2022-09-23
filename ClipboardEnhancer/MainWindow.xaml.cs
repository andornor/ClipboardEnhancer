using System;
using System.Diagnostics;
using System.Windows;
using WK.Libraries.SharpClipboardNS;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Text.RegularExpressions;

namespace ClipboardEnhancer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public SharpClipboard Clipboard;
        private readonly ILogger<MainWindow> _logger;
        private LastClipboardTexts lastClipboardTexts;
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

            lastClipboardTexts = new LastClipboardTexts();


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

            //Set clipboard test in UI
            ClipboardText.Text = Clipboard.ClipboardText;

            //Set clipboard history in UI
            lastClipboardTexts.AddClipboardText(Clipboard.ClipboardText);
            HistoryClipboardText.Text = lastClipboardTexts.GetClipboardTextHistory();

            //Email
            Regex regexEmail = new Regex(@"([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})",RegexOptions.IgnoreCase);
            MatchCollection regMatchEmail = regexEmail.Matches(Clipboard.ClipboardText);
            foreach (Match match in regMatchEmail)
            {
                if (match.Success)
                {
                    MainText.Text += match.ToString() + Environment.NewLine;
                }
            }

            //GUID
            Regex regexGUID = new Regex(@"[0-9A-Fa-f]{8}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{12}", RegexOptions.IgnoreCase);
            MatchCollection regMatchGUID = regexGUID.Matches(Clipboard.ClipboardText);
            foreach (Match match in regMatchGUID)
            {
                if (match.Success)
                {
                    MainText.Text += match.ToString() + Environment.NewLine;
                }
            }

        }
    }
}
