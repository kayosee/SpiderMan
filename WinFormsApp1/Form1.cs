namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CefSharp.WinForms.ChromiumWebBrowser browser=new CefSharp.WinForms.ChromiumWebBrowser();
            browser.Dock = DockStyle.Fill;
            this.Controls.Add(browser);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
