using MySql.Data.MySqlClient;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace RentalMobil_Kel2
{
    public partial class Form1 : Form
    {
        string connStr;
        private SidebarControl sidebar;
        private SidebarUserControl sidebarUser;
        private MainControl main;
        private AuthControl auth;
        private RentalControl rental;
        private RentalUserControl rentalUser;
        private ReturnControl ReturnControl;
        private string _loggedInUserType = string.Empty;

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

        public bool IsAdmin()
        {
            return _loggedInUserType.ToLower() == "admin";
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

            string query = "INSERT INTO user (id_user, nama, username, password, type, status) VALUES (@id_user, @name, @username, @password, @type, @status)";

            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_user", id_user);
                    command.Parameters.AddWithValue("@name", nama);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
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


        public int AuthenticateUser(string username, string password)
        {
            string hashedPassword = password;

            string query = "SELECT status,type FROM user WHERE username = @user AND password = @pass LIMIT 1";

            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user", username);
                    command.Parameters.AddWithValue("@pass", hashedPassword);

                    try
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int status = reader.GetInt32("status");
                                string userType = reader.GetString("type");
                                _loggedInUserType = userType;
                                if (status == 0)
                                    return 0; 

                                return 1;
                            }
                            else
                            {
                                _loggedInUserType = string.Empty;
                                return -1;
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        _loggedInUserType = string.Empty;
                        MessageBox.Show($"Kesalahan Otentikasi DB: {ex.Message}", "Kesalahan Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }
                }
            }
        }

        public DataTable GetCarData()
        {
            string query = @"SELECT code AS KODE, merk AS MEREK, type as TIPE, year AS TAHUN, nopol AS NOPOL, price AS HARGA, 
             CASE 
                WHEN status = 1 THEN 'Tersedia'
                ELSE 'Tidak Tersedia'
             END AS Status,
             CASE 
                WHEN `show` = 1 THEN 'Ya'
                ELSE 'Tidak'
             END AS Tampil 
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
                        MessageBox.Show($"Gagal mengambil data mobil: {ex.Message}", "Kesalahan DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return dt;
        }

        public int GetCarCountByMerk(string merk)
        {
            string query = "SELECT COUNT(*) FROM car WHERE merk = @merk";
            int count = 0;

            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@merk", merk);
                    try
                    {
                        connection.Open();
                        count = Convert.ToInt32(command.ExecuteScalar());
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Gagal mengambil hitungan mobil: {ex.Message}", "Kesalahan Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return count;
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

        // Tambahkan ini ke dalam class Form1
        public bool UpdateCarData(string code, string merk, string type, int tahun, string nopol, int harga, bool status)
        {
            int dbStatus = status ? 1 : 0;
            // Perintah UPDATE menggunakan KODE sebagai kunci (WHERE code = @code)
            string query = @"UPDATE car SET 
                        merk = @merk, 
                        type = @tipe, 
                        year = @tahun, 
                        nopol = @nopol, 
                        price = @harga, 
                        status = @status 
                     WHERE code = @code";

            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Semua parameter dimasukkan, termasuk @code di WHERE
                    command.Parameters.AddWithValue("@merk", merk);
                    command.Parameters.AddWithValue("@tipe", type);
                    command.Parameters.AddWithValue("@tahun", tahun);
                    command.Parameters.AddWithValue("@nopol", nopol);
                    command.Parameters.AddWithValue("@harga", harga);
                    command.Parameters.AddWithValue("@status", dbStatus);
                    command.Parameters.AddWithValue("@code", code);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Gagal memperbarui data mobil. Error: {ex.Message}", "Kesalahan Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }

        // Di dalam class Form1

        public bool UpdateCarStatus(string code, bool status)
        {
            // Ubah status boolean menjadi integer 1 (true = Tersedia) atau 0 (false = Tidak Tersedia) untuk DB
            int dbStatus = status ? 1 : 0;

            string query = "UPDATE car SET status = @status WHERE code = @code";

            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@status", dbStatus);
                    command.Parameters.AddWithValue("@code", code);
                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Gagal memperbarui status mobil. Error: {ex.Message}", "Kesalahan Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }

        public bool UpdateCarshow(string code, bool show)
        {
            int dbShow = show ? 1 : 0;
            string query = "UPDATE car SET `show` = @show WHERE code = @code";
            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@show", dbShow);
                    command.Parameters.AddWithValue("@code", code);
                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Gagal memperbarui status tampil mobil. Error: {ex.Message}", "Kesalahan Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            panel1.Controls.Clear();
            UserControl controlToLoad;
            if (_loggedInUserType.ToLower() == "admin")
            {
                sidebar = new SidebarControl();
                controlToLoad = sidebar;
                sidebar.NavigationRequested += Sidebar_NavigationRequested;
                sidebar.SetNavigationVisibility(true);
            }
            else if (_loggedInUserType.ToLower() == "user")
            {
                sidebarUser = new SidebarUserControl(); 
                controlToLoad = sidebarUser;
     
                sidebarUser.NavigationRequested += Sidebar_NavigationRequested;
                sidebarUser.SetNavigationVisibility(true);
            }
            else 
            {
                sidebarUser = new SidebarUserControl();
                controlToLoad = sidebarUser;
                sidebarUser.NavigationRequested += Sidebar_NavigationRequested;
                sidebarUser.SetNavigationVisibility(false); 
            }

            controlToLoad.Dock = DockStyle.Fill;
            panel1.Controls.Add(controlToLoad);

            panel2.Visible = true;
            panel1.Visible = true;
        }

        public void LoginSuccess()
        {
            panel1.Visible = true;
            LoadSidebar();

            if (IsAdmin())
            {
                MessageBox.Show("Login Admin Berhasil!", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Login User Berhasil!", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

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
                    _loggedInUserType = string.Empty;
                    if (IsAdmin())
                    {
                        sidebar.SetNavigationVisibility(false);
                    }
                    else
                    {
                        sidebarUser.SetNavigationVisibility(false);
                    }
                    controlToLoad = (new AuthControl(this));
                    break;
                case "Rental":
                    if (IsAdmin())
                    {
                        controlToLoad = new RentalControl();
                    }
                    else
                    {
                        controlToLoad = new RentalUserControl();
                    }

                        break;
                case "ReturnControl":
                    controlToLoad = new ReturnControl();
                    break;
                case "AddCar":
                    if (IsAdmin())
                    {
                        controlToLoad = new AddCarControl(this);
                    }
                    else
                    {
                        MessageBox.Show("Anda tidak memiliki izin untuk mengakses menu ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    break;
                case "AddUser":
                    if (IsAdmin())
                    {
                        controlToLoad = new AddUserControl();
                    }
                    else
                    {
                        MessageBox.Show("Anda tidak memiliki izin untuk mengakses menu ini.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
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
