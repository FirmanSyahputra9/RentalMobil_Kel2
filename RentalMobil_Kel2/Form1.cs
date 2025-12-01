using MySql.Data.MySqlClient;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
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

        public bool Register(string id_user, string nama, string username, string password, string type, bool status, string alamat, string no_hp)
        {
            int dbStatus = status ? 1 : 0;

            string query = "INSERT INTO user (id_user, nama, username, password, type, status, alamat, no_hp) VALUES (@id_user, @name, @username, @password, @type, @status, @alamat, @no_hp)";

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
                    command.Parameters.AddWithValue("@alamat", alamat);
                    command.Parameters.AddWithValue("@no_hp", no_hp);
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

        public bool AddUser(string id_user, string nama, string username, string password, string type, bool status, string alamat)
        {
            int dbStatus = status ? 1 : 0;

            string query = "INSERT INTO user (id_user, nama, username, password, type, status, alamat) VALUES (@id_user, @name, @username, @password, @type, @status, @alamat)";

            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_user", id_user);
                    command.Parameters.AddWithValue("@nama", nama);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@status", dbStatus);
                    command.Parameters.AddWithValue("@alamat", alamat);
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


        // UserControl
        public DataTable GetCarData()
        {
            string query = @"SELECT id,code AS KODE, merk AS MEREK, type as TIPE, year AS TAHUN, nopol AS NOPOL, price AS HARGA, 
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


        public byte[] GetCarImageByCode(string code)
        {
            string query = "SELECT image_blob FROM car WHERE code = @code";
            byte[] imageBytes = null;

            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@code", code);
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            if (result is byte[] bytes)
                            {
                                imageBytes = bytes;
                            }
                            else
                            {
                                MessageBox.Show("Tipe data gambar yang diambil tidak sesuai.", "Kesalahan Konversi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Kode {code} tidak memiliki gambar tersimpan.");
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Gagal mengambil gambar mobil dari DB: {ex.Message}", "Kesalahan DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return imageBytes;
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

        public bool SaveCarData(string code, string merk, string type, int tahun, string nopol, int harga, bool status, byte[] imageBlob)
        {
            int dbStatus = status ? 1 : 0;

            string query = "INSERT INTO car (code, merk, type, year, nopol, price, status, image_blob) VALUES (@code, @merk, @tipe, @tahun, @nopol, @harga, @status, @imageBlob)";

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
                    if (imageBlob != null)
                    {
                        command.Parameters.Add("@imageBlob", MySqlDbType.LongBlob, imageBlob.Length).Value = imageBlob;
                    }
                    else
                    {
                        command.Parameters.Add("@imageBlob", MySqlDbType.LongBlob).Value = DBNull.Value;
                    }

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
        public bool UpdateCarData(string code, string merk, string type, int tahun, string nopol, int harga, bool status, bool showTersedia, byte[] imageBlob)
        {
            int dbStatus = status ? 1 : 0;
            int dbShowIndex = showTersedia? 1 : 0;
            string query = @"UPDATE car SET 
                        merk = @merk, 
                        type = @type, 
                        year = @tahun, 
                        nopol = @nopol, 
                        price = @harga, 
                        status = @status,
                        `show` = @showIndex,
                        image_blob = @imageBlob
                      WHERE code = @code";

            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@merk", merk);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@tahun", tahun);
                    command.Parameters.AddWithValue("@nopol", nopol);
                    command.Parameters.AddWithValue("@harga", harga);
                    command.Parameters.AddWithValue("@status", dbStatus);
                    command.Parameters.AddWithValue("@showIndex", dbShowIndex   );


                    if (imageBlob != null)
                    {
                        command.Parameters.Add("@imageBlob", MySqlDbType.LongBlob, imageBlob.Length).Value = imageBlob;
                    }
                    else
                    {
  
                        command.Parameters.Add("@imageBlob", MySqlDbType.LongBlob).Value = DBNull.Value;
                    }

           
                    command.Parameters.AddWithValue("@code", code);

                    try
                    {
                        connection.Open();

                        Console.WriteLine("=== DEBUG UPDATE ===");
                        Console.WriteLine("QUERY:\n" + query);

                        foreach (MySqlParameter p in command.Parameters)
                        {
                            Console.WriteLine($"{p.ParameterName} = {p.Value}");
                        }

                        int rowsAffected = command.ExecuteNonQuery();

                        Console.WriteLine("Rows Affected: " + rowsAffected);

                        return rowsAffected > 0;
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show(
                            $"ERROR MYSQL:\n{ex.Message}\n\nKode Error: {ex.Number}",
                            "Kesalahan Database",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        return false;
                    }


                }
            }
        }


        public bool UpdateCarStatus(string code, bool status)
        {
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

                     MessageBox.Show($"Gagal memperbarui data mobil. Error: {ex.Message} \n\n SQL: {query}", "Kesalahan Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        // UserControl
        public DataTable GetUserData()
        {
            string query = @"SELECT id_user AS NIK, nama AS NAMA, username AS USERNAME, password AS PASSWORD,type AS ROLE, alamat AS ALAMAT, no_hp AS 'NO_HP', 
             CASE 
                WHEN status = 1 THEN 'Aktif'
                ELSE 'Belum Aktif'
             END AS STATUS
             FROM user";

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
                        MessageBox.Show($"Gagal mengambil data user: {ex.Message}", "Kesalahan DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return dt;
        }

        public bool updateUser(string id_user, string nama, string username, string password, string type, bool status, string alamat, string no_hp)
        {
            int dbStatus = status ? 1 : 0;

            string query = "UPDATE user set nama = @nama, username = @username, password = @password, type = @type, status = @status, alamat = @alamat, no_hp = @no_hp WHERE id_user = @id_user";

            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_user", id_user);
                    command.Parameters.AddWithValue("@nama", nama);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@status", dbStatus);
                    command.Parameters.AddWithValue("@alamat", alamat);
                    command.Parameters.AddWithValue("@no_hp", no_hp);
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Gagal mengupdate data ke database. Error: {ex.Message}", "Kesalahan Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }


        // RentalControl

        public DataTable GetCarReadyData()
        {
            string query = @"SELECT id,code AS KODE, merk AS MEREK, type as TIPE, year AS TAHUN, nopol AS NOPOL, price AS HARGA,
             CASE 
                WHEN status = 1 THEN 'Tersedia'
                ELSE 'Tidak Tersedia'
             END AS Status,
             CASE 
                WHEN `show` = 1 THEN 'Ya'
                ELSE 'Tidak'
             END AS Tampil 
             FROM car WHERE status = 1";

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


        public DataTable GetRentalData()
        {
            string query = @"
                        SELECT 
                            u.id_user AS NIK,
                            u.nama AS NAMA,
                            u.alamat AS ALAMAT,
                            u.no_hp AS NO_HP,
                            c.id AS ID_MOBIL,
                            c.nopol AS NOPOL,
                            c.merk AS MEREK,
                            c.type AS TIPE,
                            c.year AS TAHUN,
                            c.price AS HARGA,
                            c.status AS STATUS,
                            r.rental_date AS TANGGAL_PINJAM,
                            r.rental_return AS TANGGAL_KEMBALI
                        FROM rental r
                        JOIN user u ON u.id_user = r.user_id
                        JOIN car c ON c.id = r.car_id
                        WHERE c.status = 0;
                    ";

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
                        MessageBox.Show($"Gagal mengambil data rental: {ex.Message}", "Kesalahan DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return dt;
        }

        public DataTable GetRentalingData()
        {
            string query = @"
                        SELECT 
                            u.id_user AS NIK,
                            u.nama AS NAMA,
                            u.alamat AS ALAMAT,
                            u.no_hp AS NO_HP,
                            c.id AS ID_MOBIL,
                            c.nopol AS NOPOL,
                            c.merk AS MEREK,
                            c.type AS TIPE,
                            c.year AS TAHUN,
                            c.price AS HARGA,
                            c.status AS STATUS,
                            r.id AS RENTAL_ID,
                            r.rental_date AS TANGGAL_PINJAM,
                            r.rental_return AS TANGGAL_KEMBALI
                        FROM rental r
                        JOIN user u ON u.id_user = r.user_id
                        JOIN car c ON c.id = r.car_id
                        WHERE c.status = 0;
                    ";

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
                        MessageBox.Show($"Gagal mengambil data rental: {ex.Message}", "Kesalahan DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return dt;
        }

        public bool CreateRentalData(string user_id, string car_id, DateTime rental_date, DateTime rental_return, int statusInt)
        {
            string insertQuery = @"INSERT INTO rental (user_id, car_id, rental_date, rental_return) 
                           VALUES (@user_id, @car_id, @rental_date, @rental_return)";

            string updateQuery = @"UPDATE car SET status = @status WHERE id = @car_id";

            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    using (MySqlCommand cmdInsert = new MySqlCommand(insertQuery, connection, transaction))
                    {
                        cmdInsert.Parameters.AddWithValue("@user_id", user_id);
                        cmdInsert.Parameters.AddWithValue("@car_id", car_id);
                        cmdInsert.Parameters.AddWithValue("@rental_date", rental_date);
                        cmdInsert.Parameters.AddWithValue("@rental_return", rental_return);
                        cmdInsert.ExecuteNonQuery();
                    }
                    using (MySqlCommand cmdUpdate = new MySqlCommand(updateQuery, connection, transaction))
                    {
                        cmdUpdate.Parameters.AddWithValue("@status", statusInt);
                        cmdUpdate.Parameters.AddWithValue("@car_id", car_id);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return true;
                }
                catch (MySqlException ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(
                        $"Gagal menyimpan data ke database. Error: {ex.Message}",
                        "Kesalahan Database",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return false;
                }
            }
        }

        public bool ReturnCar(int carID, DateTime tglPinjam, DateTime tglKembali, DateTime tglSekarang, decimal denda)
        {
            string updateRentalQuery = @"
                UPDATE rental 
                SET 
                    return_date = @tglSekarang,
                    denda = @denda
                WHERE car_id = @car_id
                  AND rental_date = @tglPinjam
                  AND rental_return = @tglKembali
                  AND return_date IS NULL
            ";

            string updateCarQuery = @"UPDATE car SET status = 1 WHERE id = @car_id";

            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    using (MySqlCommand cmd1 = new MySqlCommand(updateRentalQuery, connection, transaction))
                    {
                        cmd1.Parameters.AddWithValue("@car_id", carID);
                        cmd1.Parameters.AddWithValue("@tglSekarang", tglSekarang);
                        cmd1.Parameters.AddWithValue("@denda", denda);
                        cmd1.Parameters.AddWithValue("@tglPinjam", tglPinjam);
                        cmd1.Parameters.AddWithValue("@tglKembali", tglKembali);

                        cmd1.ExecuteNonQuery();
                    }

                    using (MySqlCommand cmd2 = new MySqlCommand(updateCarQuery, connection, transaction))
                    {
                        cmd2.Parameters.AddWithValue("@car_id", carID);
                        cmd2.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return true;
                }
                catch (MySqlException ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(
                        $"Gagal memperbarui data di database.\nError: {ex.Message}",
                        "Kesalahan Database",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return false;
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
                        controlToLoad = new RentalControl(this);
                    }
                    else
                    {
                        controlToLoad = new RentalUserControl(this);
                    }

                        break;
                case "ReturnControl":
                    controlToLoad = new ReturnControl(this);
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
                        controlToLoad = new AddUserControl(this);
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
