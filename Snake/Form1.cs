namespace Snake
{
    public partial class Form1 : Form
    {
        private Field _field;
        private FieldController _fieldController;
        private FieldView _fieldView;
        public Form1()
        {
            InitializeComponent();
            _field = new Field();
            _fieldController = new FieldController(_field);
            _fieldView = new FieldView(_field);
        }

        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            _fieldController.Start();
            _fieldView.StartOn(panel1.CreateGraphics());
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            var d = new Dictionary<Keys, Direction>()
            {
                {Keys.Left, Direction.ToLeft},
                {Keys.Right, Direction.ToRight},
                {Keys.Up, Direction.ToTop},
                {Keys.Down, Direction.ToBottom}
            };
            try
            {
                _field.SnakeDirection = d[e.KeyCode];
            } catch {}
        }
    }
}