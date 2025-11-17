namespace RentalMobil_Kel2
{
    public partial class Form1 : Form
    {
        private SidebarControl sidebar;
        private MainControl main;
        private AuthControl auth;
        private RentalControl renatal;
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            LoadSidebar();
            LoadMain();
        }
        private void LoadUserControl(UserControl newControl)
        {
            panel2.Controls.Clear();
            newControl.Dock = DockStyle.Fill;
            panel2.Controls.Add(newControl);
        }

        private void LoadSidebar()
        {
            sidebar = new SidebarControl();
            sidebar.Dock = DockStyle.Fill;
            panel1.Controls.Add(sidebar);
            panel2.Visible = true;

            sidebar.NavigationRequested += Sidebar_NavigationRequested;
        }

        private void Sidebar_NavigationRequested(object? sender, string formName)
        {
            UserControl controlToLoad;

            switch (formName)
            {
                case "Auth":
                    controlToLoad = new AuthControl();
                    break;
                case "Rental":
                    controlToLoad = new RentalControl();
                    break;
                default:
                    return;
            }
            LoadUserControl(controlToLoad);
        }


        private void LoadMain()
        {
            panel2.Controls.Clear();
            main = new MainControl();
            main.Dock = DockStyle.Fill;
            panel2.Controls.Add(main);
            panel2.Visible = true;
        }
        private void LoadAuth()
        {
            panel2.Controls.Clear();
            auth = new AuthControl();
            auth.Dock = DockStyle.Fill;
            panel2.Controls.Add(auth);
            panel2.Visible = true;
        }

    }
}
