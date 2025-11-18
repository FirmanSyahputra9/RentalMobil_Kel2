using MySql.Data.MySqlClient;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace RentalMobil_Kel2
{
    public partial class Form1 : Form
    {
        string connStr;
        private SidebarControl sidebar;
        private MainControl main;
        private AuthControl auth;
        private RentalControl rental;
        private ReturnControl ReturnControl;

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            connStr = "server=127.0.0.1;port=3306;user=root;password=;database=rentalmobil;";
            this.Text = "BeRent";
            CheckDatabaseConnection();
            LoadSidebar();
            LoadUserControl(new AuthControl(this));

        }

        public bool CheckDatabaseConnection()
        {
            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("Koneksi Database Berhasil!", "Informasi Koneksi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Koneksi Database Gagal! \nError: {ex.Message}", "Kesalahan Koneksi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public bool Register(string id_user, string nama, string username, string password, string type, bool status)
        {
            int dbStatus = status ? 1 : 0;

            string query = "INSERT INTO user (id_user, nama, username, password, type, status) VALUES (@id, @name, @user, @pass, @type, @status)";

            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id_user);
                    command.Parameters.AddWithValue("@name", nama);
                    command.Parameters.AddWithValue("@user", username);
                    command.Parameters.AddWithValue("@pass", password);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@status", dbStatus);
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Gagal menyimpan data ke database. Error: {ex.Message}", "Kesalahan Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }

        public bool IsUserIdExist(string id_user)
        {
            string query = "SELECT COUNT(*) FROM user WHERE id_user = @id";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", id_user);
                conn.Open();

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }


        // login
        public bool AuthenticateUser(string username, string password)
        {
            string hashedPassword = password;

            string query = "SELECT COUNT(1) FROM user WHERE username = @user AND password = @pass AND status = 1";

            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user", username);
                    command.Parameters.AddWithValue("@pass", hashedPassword);

                    try
                    {
                        connection.Open();
                        int count = Convert.ToInt32(command.ExecuteScalar());

                        return count == 1; 
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Kesalahan Otentikasi DB: {ex.Message}", "Kesalahan Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }

        // Data Mobil
        public DataTable GetCarData()
        {
            string query = @"SELECT code AS KODE, merk AS MEREK, type as TIPE, year AS YEAR, nopol AS NOPOL, price AS HARGA,
                 CASE 
                    WHEN status = 1 THEN 'Tersedia'
                    ELSE 'Tidak Tersedia'
                 END AS Status
                 FROM car";

            DataTable dt = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Gagal mengambil data mahasiswa: {ex.Message}", "Kesalahan DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return dt;
        }

        public bool SaveCarData(string code, string merk, string type, int tahun, string nopol, int harga, bool status)
        {
            int dbStatus = status ? 1 : 0;

            string query = "INSERT INTO car (code, merk, type, year, nopol, price, status) VALUES (@code, @merk, @tipe, @tahun, @nopol, @harga, @status)";

            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@code", code);
                    command.Parameters.AddWithValue("@merk", merk);
                    command.Parameters.AddWithValue("@tipe", type);
                    command.Parameters.AddWithValue("@tahun", tahun);
                    command.Parameters.AddWithValue("@nopol", nopol);
                    command.Parameters.AddWithValue("@harga", harga);
                    command.Parameters.AddWithValue("@status", dbStatus);
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Gagal menyimpan data ke database. Error: {ex.Message}", "Kesalahan Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
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
            panel1.Visible = false;

            sidebar.SetNavigationVisibility(false);

            sidebar.NavigationRequested += Sidebar_NavigationRequested;
        }

        public void LoginSuccess()
        {
            panel1.Visible = true;
            sidebar.SetNavigationVisibility(true);
            LoadUserControl(new MainControl());
        }

        private void Sidebar_NavigationRequested(object? sender, string formName)
        {
            UserControl controlToLoad;

            switch (formName)
            {
                case "Auth":
                    controlToLoad = new AuthControl(this);
                    break;
                case "Home":
                    controlToLoad = new MainControl();
                    break;
                case "Logout":
                    panel1.Visible = false;
                    sidebar.SetNavigationVisibility(false);
                    controlToLoad = (new AuthControl(this));
                    break;
                case "Rental":
                    controlToLoad = new RentalControl();
                    break;
                case "ReturnControl":
                    controlToLoad = new ReturnControl();
                    break;
                case "AddCar":
                    controlToLoad = new AddCarControl(this);
                    break;
                case "AddUser":
                    controlToLoad = new AddUserControl();
                    break;
                case "Exit":
                    Application.Exit();
                    return;
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
            auth = new AuthControl(this);
            auth.Dock = DockStyle.Fill;
            panel2.Controls.Add(auth);
            panel2.Visible = true;
        }

    }
}
