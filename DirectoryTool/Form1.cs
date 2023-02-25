namespace DirectoryTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            button1.Enabled = false;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = dialog.SelectedPath;
                textBox1.Text = selectedFilePath;

                StreamWriter outputFile = new StreamWriter(Path.Combine(selectedFilePath, "dirT_80_" + DateTime.Now.ToFileTime() + ".txt"));
                StreamWriter outputFile2 = new StreamWriter(Path.Combine(selectedFilePath, "dirT_NA_" + DateTime.Now.ToFileTime() + ".txt"));

                try
                {
                    //txt and 80 are hard-coded (for testing)
                    DirLen(selectedFilePath, "*.txt", 80, outputFile);
                    FileNameNonAscii(selectedFilePath, "*.txt", outputFile2);
                }
                catch (Exception)
                {
                    throw;
                }
                outputFile.Close();
                outputFile2.Close();
            }
        }

        private static void DirLen(string path, string fileExt, int len, StreamWriter sw)
        {
            foreach (var s in Directory.GetFiles(path, fileExt))
            {
                if (Path.GetFileNameWithoutExtension(s).Length > len)
                {
                    sw.WriteLine(Path.GetFileName(s));
                }
            }
        }

        private static void FileNameNonAscii(string path, string fileExt, StreamWriter sw)
        {
            foreach (var s in Directory.GetFiles(path, fileExt))
            {
                foreach (char c in Path.GetFileNameWithoutExtension(s))
                {
                    //ascii = 0-127
                    if (c > 127)
                    {
                        sw.WriteLine(Path.GetFileName(s));
                    }
                }

            }
        }
    }
}