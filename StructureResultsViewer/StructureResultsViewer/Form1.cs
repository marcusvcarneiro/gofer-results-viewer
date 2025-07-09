namespace StructureResultsViewer
{
    public partial class Form1 : Form
    {
        Controller _modelController;
        public Form1()
        {
            _modelController = new Controller();
            InitializeComponent();
            fileLoaderControl1.ModelController = _modelController;
            //spreadSheetControl1.ModelController = _modelController;
            plotControl1.ModelController = _modelController;
            graphicUserControl1.ModelController = _modelController;
            tabControl1.TabPages[0].Text = "Structure";
            tabControl1.TabPages[1].Text = "Model";
            UserControlBase? ucbase = (tabControl1.TabPages[0].Controls[0]) as UserControlBase;
            if (ucbase != null)
            {
                ucbase.ModelController = _modelController;
                ucbase = (tabControl1.TabPages[1].Controls[0]) as UserControlBase;
                if (ucbase != null)
                {
                    ucbase.ModelController = _modelController;
                }
            }
            Size = new Size(886, 661);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form elementForm = new ElementSelectionForm(_modelController);
            elementForm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void plotControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
