using MySqlConnector;
using Hospital_Management_System.Objects;

namespace Hospital_Management_System.Data
{
    internal class DatabaseManagement
    {
        private string serverConnectionString = "Server=localhost;User Id=root;Password=password;Database=hospital";

        private MySqlConnection _connection;

        public void AddDoctor(Doctor doctor)
        {
            _connection.Open();
            using var cmd = new MySqlCommand("INSERT INTO Doctors (dFirstName, dLastName, dDateOfBirth, dPhoneNumber, dAddress, dCity, dProvince, dEmailAddr, departmentId) VALUES (@firstName, @lastName, @dob, @phone, @address, @city, @province, @email, @departmentId)", _connection);

            cmd.Parameters.AddWithValue("@firstName", doctor.DFirstName);
            cmd.Parameters.AddWithValue("@lastName", doctor.DLastName);
            cmd.Parameters.AddWithValue("@dob", doctor.DDateOfBirth);
            cmd.Parameters.AddWithValue("@phone", doctor.DPhoneNumber);
            cmd.Parameters.AddWithValue("@address", doctor.DAddress);
            cmd.Parameters.AddWithValue("@city", doctor.DCity);
            cmd.Parameters.AddWithValue("@province", doctor.DProvince);
            cmd.Parameters.AddWithValue("@email", doctor.DEmailAddr);
            cmd.Parameters.AddWithValue("@departmentId", doctor.DepartmentId);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public Doctor GetDoctorById(int doctorId)
        {
            _connection.Open();

            using var cmd = new MySqlCommand("SELECT * FROM doctors WHERE DoctorId = @id", _connection);
            cmd.Parameters.AddWithValue("@id", doctorId);

            try
            {
                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    var doctor = new Doctor
                    {
                        DoctorId = reader.GetInt32("DoctorId"),
                        DFirstName = reader.GetString("dFirstName"),
                        DLastName = reader.GetString("dLastName"),
                        DDateOfBirth = reader.GetDateTime("dDateOfBirth"),
                        DPhoneNumber = reader.GetInt64("dPhoneNumber"),
                        DAddress = reader.GetString("dAddress"),
                        DCity = reader.GetString("dCity"),
                        DProvince = reader.GetString("dProvince"),
                        DEmailAddr = reader.GetString("dEmailAddr"),
                        DepartmentId = reader.GetInt32("departmentId")
                    };

                    return doctor;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }

            return null;
        }


        public bool IsPhoneNumberUnique(long phoneNumber, string tableName)
        {
            _connection.Open();
            using var cmd = new MySqlCommand($"SELECT COUNT(*) FROM {tableName} WHERE {tableName.Substring(0, 1).ToLower()}PhoneNumber = @phone", _connection);
            cmd.Parameters.AddWithValue("@phone", phoneNumber);
            var count = Convert.ToInt32(cmd.ExecuteScalar());
            _connection.Close();
            return count == 0; // If count is zero, then phone number is unique
        }

        public bool IsEmailUnique(string email, string tableName)
        {
            _connection.Open();
            using var cmd = new MySqlCommand($"SELECT COUNT(*) FROM {tableName} WHERE {tableName.Substring(0, 1).ToLower()}EmailAddr = @email", _connection);
            cmd.Parameters.AddWithValue("@email", email);
            var count = Convert.ToInt32(cmd.ExecuteScalar());
            _connection.Close();
            return count == 0; // If count is zero, then email is unique
        }

        public bool IsDepartmentNameUnique(string departmentName)
        {
            _connection.Open();

            using var cmd = new MySqlCommand("SELECT COUNT(*) FROM department WHERE DepartmentName = @departmentName", _connection);
            cmd.Parameters.AddWithValue("@departmentName", departmentName);

            var count = Convert.ToInt32(cmd.ExecuteScalar());

            _connection.Close();

            return count == 0; // If count is zero, then department name is unique
        }


        public void AddPatient(Patient patient)
        {
            _connection.Open();

            using var cmd = new MySqlCommand("INSERT INTO patients (pFirstName, pLastName, pDateOfBirth, pPhoneNumber, pAddress, pCity, pProvince, pEmailAddr) VALUES (@firstName, @lastName, @dob, @phone, @address, @city, @province, @email)", _connection);

            cmd.Parameters.AddWithValue("@firstName", patient.PFirstName);
            cmd.Parameters.AddWithValue("@lastName", patient.PLastName);
            cmd.Parameters.AddWithValue("@dob", patient.PDateOfBirth);
            cmd.Parameters.AddWithValue("@phone", patient.PPhoneNumber);
            cmd.Parameters.AddWithValue("@address", patient.PAddress);
            cmd.Parameters.AddWithValue("@city", patient.PCity);
            cmd.Parameters.AddWithValue("@province", patient.PProvince);
            cmd.Parameters.AddWithValue("@email", patient.PEmailAddr);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public Patient GetPatientById(int patientId)
        {
            _connection.Open();

            using var cmd = new MySqlCommand("SELECT * FROM patients WHERE PatientId = @id", _connection);
            cmd.Parameters.AddWithValue("@id", patientId);

            try
            {
                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    var patient = new Patient
                    {
                        PatientId = reader.GetInt32("PatientId"),
                        PFirstName = reader.GetString("pFirstName"),
                        PLastName = reader.GetString("pLastName"),
                        PDateOfBirth = reader.GetDateTime("pDateOfBirth"),
                        PPhoneNumber = reader.GetInt64("pPhoneNumber"),
                        PAddress = reader.GetString("pAddress"),
                        PCity = reader.GetString("pCity"),
                        PProvince = reader.GetString("pProvince"),
                        PEmailAddr = reader.GetString("pEmailAddr")
                    };

                    return patient;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }

            return null;
        }


        public void AddTreatment(Treatment treatment)
        {
            _connection.Open();

            using var cmd = new MySqlCommand("INSERT INTO treatments (title, doctorId, patientId, status, notes) VALUES (@title, @doctorId, @patientId, @status, @notes)", _connection);

            cmd.Parameters.AddWithValue("@title", treatment.Title);
            cmd.Parameters.AddWithValue("@doctorId", treatment.DoctorId);
            cmd.Parameters.AddWithValue("@patientId", treatment.PatientId);
            cmd.Parameters.AddWithValue("@status", treatment.Status);
            cmd.Parameters.AddWithValue("@notes", treatment.Notes);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void AddStaff(Staff staff)
        {
            _connection.Open();

            using var cmd = new MySqlCommand("INSERT INTO staff (sPosition, sFirstName, sLastName, sDateOfBirth, sPhoneNumber, sAddress, sCity, sProvince, sEmailAddr, departmentId) VALUES (@position, @firstName, @lastName, @dob, @phone, @address, @city, @province, @email, @departmentId)", _connection);

            cmd.Parameters.AddWithValue("@position", staff.SPosition);
            cmd.Parameters.AddWithValue("@firstName", staff.SFirstName);
            cmd.Parameters.AddWithValue("@lastName", staff.SLastName);
            cmd.Parameters.AddWithValue("@dob", staff.SDateOfBirth);
            cmd.Parameters.AddWithValue("@phone", staff.SPhoneNumber);
            cmd.Parameters.AddWithValue("@address", staff.SAddress);
            cmd.Parameters.AddWithValue("@city", staff.SCity);
            cmd.Parameters.AddWithValue("@province", staff.SProvince);
            cmd.Parameters.AddWithValue("@email", staff.SEmailAddr);
            cmd.Parameters.AddWithValue("@departmentId", staff.DepartmentId);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public Staff GetStaffById(int staffId)
        {
            _connection.Open();

            using var cmd = new MySqlCommand("SELECT * FROM staff WHERE StaffId = @id", _connection);
            cmd.Parameters.AddWithValue("@id", staffId);

            try
            {
                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    var staff = new Staff
                    {
                        StaffId = reader.GetInt32("StaffId"),
                        SPosition = reader.GetString("sPosition"),
                        SFirstName = reader.GetString("sFirstName"),
                        SLastName = reader.GetString("sLastName"),
                        SDateOfBirth = reader.GetDateTime("sDateOfBirth"),
                        SPhoneNumber = reader.GetInt64("sPhoneNumber"),
                        SAddress = reader.GetString("sAddress"),
                        SCity = reader.GetString("sCity"),
                        SProvince = reader.GetString("sProvince"),
                        SEmailAddr = reader.GetString("sEmailAddr"),
                        DepartmentId = reader.GetInt32("departmentId")
                    };

                    return staff;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }

            return null;
        }

        public void DeleteDoctor(int doctorId)
        {
            _connection.Open();

            using var cmd = new MySqlCommand("DELETE FROM doctors WHERE DoctorId = @doctorId", _connection);
            cmd.Parameters.AddWithValue("@doctorId", doctorId);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }


        public void AddDepartment(Department department)
        {
            _connection = new MySqlConnection();

            using var cmd = new MySqlCommand("INSERT INTO department (departmentName) VALUES (@name)", _connection);

            cmd.Parameters.AddWithValue("@name", department.DepartmentName);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex) {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }
        public DatabaseManagement()
        {
            createDatabase();
            _connection = new MySqlConnection(serverConnectionString);
        }

        public List<Doctor> GetDoctors()
        {
            List<Doctor> doctors = new List<Doctor>();
            string query = "SELECT * FROM Doctors";

            using MySqlCommand cmd = new MySqlCommand(query, _connection);
            _connection.Open();
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                doctors.Add(new Doctor(
                    reader.GetInt32("doctorId"),
                    reader.GetString("dFirstName"),
                    reader.GetString("dLastName"),
                    reader.GetDateTime("dDateOfBirth"),
                    reader.GetInt64("dPhoneNumber"),
                    reader.GetString("dAddress"),
                    reader.GetString("dCity"),
                    reader.GetString("dProvince"),
                    reader.GetString("dEmailAddr"),
                    reader.GetInt32("departmentId")
                ));
            }

            _connection.Close();
            return doctors;
        }

        public List<Patient> GetPatients()
        {
            List<Patient> patients = new List<Patient>();
            string query = "SELECT * FROM patients";

            using MySqlCommand cmd = new MySqlCommand(query, _connection);
            _connection.Open();
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                patients.Add(new Patient(
                    reader.GetInt32("patientId"),
                    reader.GetString("pFirstName"),
                    reader.GetString("pLastName"),
                    reader.GetDateTime("pDateOfBirth"),
                    reader.GetInt64("pPhoneNumber"),
                    reader.GetString("pAddress"),
                    reader.GetString("pCity"),
                    reader.GetString("pProvince"),
                    reader.GetString("pEmailAddr")
                ));
            }

            _connection.Close();
            return patients;
        }

        public List<Staff> GetStaff()
        {
            List<Staff> staffList = new List<Staff>();
            string query = "SELECT * FROM staff";

            using MySqlCommand cmd = new MySqlCommand(query, _connection);
            _connection.Open();
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                staffList.Add(new Staff(
                    reader.GetInt32("staffId"),
                    reader.GetString("sPosition"),
                    reader.GetString("sFirstName"),
                    reader.GetString("sLastName"),
                    reader.GetDateTime("sDateOfBirth"),
                    reader.GetInt64("sPhoneNumber"),
                    reader.GetString("sAddress"),
                    reader.GetString("sCity"),
                    reader.GetString("sProvince"),
                    reader.GetString("sEmailAddr"),
                    reader.GetInt32("departmentId")
                ));
            }

            _connection.Close();
            return staffList;
        }

        public List<Patient> SearchPatientsByName(string name)
        {
            List<Patient> patients = new List<Patient>();
            string query = $"SELECT * FROM patients WHERE pFirstName LIKE '%{name}%' OR pLastName LIKE '%{name}%'";

            using MySqlCommand cmd = new MySqlCommand(query, _connection);
            _connection.Open();
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                patients.Add(new Patient(
                    reader.GetInt32("patientId"),
                    reader.GetString("pFirstName"),
                    reader.GetString("pLastName"),
                    reader.GetDateTime("pDateOfBirth"),
                    reader.GetInt64("pPhoneNumber"),
                    reader.GetString("pAddress"),
                    reader.GetString("pCity"),
                    reader.GetString("pProvince"),
                    reader.GetString("pEmailAddr")
                ));
            }

            _connection.Close();
            return patients;
        }

        public List<Doctor> SearchDoctorsByName(string name)
        {
            List<Doctor> doctors = new List<Doctor>();
            string query = $"SELECT * FROM Doctors WHERE dFirstName LIKE '%{name}%' OR dLastName LIKE '%{name}%'";

            using MySqlCommand cmd = new MySqlCommand(query, _connection);
            _connection.Open();
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                doctors.Add(new Doctor(
                    reader.GetInt32("doctorId"),
                    reader.GetString("dFirstName"),
                    reader.GetString("dLastName"),
                    reader.GetDateTime("dDateOfBirth"),
                    reader.GetInt64("dPhoneNumber"),
                    reader.GetString("dAddress"),
                    reader.GetString("dCity"),
                    reader.GetString("dProvince"),
                    reader.GetString("dEmailAddr"),
                    reader.GetInt32("departmentId")
                ));
            }

            _connection.Close();
            return doctors;
        }

        public List<Staff> SearchStaffByName(string name)
        {
            List<Staff> staffList = new List<Staff>();
            string query = $"SELECT * FROM staff WHERE sFirstName LIKE '%{name}%' OR sLastName LIKE '%{name}%'";

            using MySqlCommand cmd = new MySqlCommand(query, _connection);
            _connection.Open();
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                staffList.Add(new Staff(
                    reader.GetInt32("staffId"),
                    reader.GetString("sPosition"),
                    reader.GetString("sFirstName"),
                    reader.GetString("sLastName"),
                    reader.GetDateTime("sDateOfBirth"),
                    reader.GetInt64("sPhoneNumber"),
                    reader.GetString("sAddress"),
                    reader.GetString("sCity"),
                    reader.GetString("sProvince"),
                    reader.GetString("sEmailAddr"),
                    reader.GetInt32("departmentId")
                ));
            }

            _connection.Close();
            return staffList;
        }

        public List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();
            string query = "SELECT * FROM department";

            using MySqlCommand cmd = new MySqlCommand(query, _connection);
            _connection.Open();
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                departments.Add(new Department(
                    reader.GetInt32("departmentId"),
                    reader.GetString("departmentName")
                ));
            }

            _connection.Close();
            return departments;
        }

        public List<Treatment> GetTreatmentsByDoctorId(int doctorId)
        {
            List<Treatment> treatments = new List<Treatment>();
            string query = "SELECT * FROM treatments WHERE doctorId = @doctorId";

            using MySqlCommand cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@doctorId", doctorId);

            _connection.Open();
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                treatments.Add(new Treatment(
                    reader.GetInt32("treatmentId"),
                    reader.GetString("title"),
                    reader.GetInt32("doctorId"),
                    reader.GetInt32("patientId"),
                    reader.GetString("status"),
                    reader.GetString("notes")
                ));
            }

            _connection.Close();
            return treatments;
        }

        public List<Treatment> GetTreatmentsByPatientId(int patientId)
        {
            List<Treatment> treatments = new List<Treatment>();
            string query = "SELECT * FROM treatments WHERE patientId = @patientId";

            using MySqlCommand cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@patientId", patientId);

            _connection.Open();
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                treatments.Add(new Treatment(
                    reader.GetInt32("treatmentId"),
                    reader.GetString("title"),
                    reader.GetInt32("doctorId"),
                    reader.GetInt32("patientId"),
                    reader.GetString("status"),
                    reader.GetString("notes")
                ));
            }

            _connection.Close();
            return treatments;
        }



        public void createDatabase()
        {
            string dbConnectionString = "Server=localhost;User Id=root;Password=password;";



            using (MySqlConnection serverConnection = new MySqlConnection(dbConnectionString))
            {
                serverConnection.Open();

                using (MySqlCommand cmdCheck = new MySqlCommand("SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = 'hospital'", serverConnection))
                {
                    if (cmdCheck.ExecuteScalar() == null)
                    {
                        Console.WriteLine("Creating database...");

                        using (MySqlCommand cmdCreateDB = new MySqlCommand("CREATE DATABASE hospital;", serverConnection))
                        {
                            cmdCreateDB.ExecuteNonQuery();
                        }

                        Console.WriteLine("Database created successfully!");

                        using (MySqlConnection dbConnection = new MySqlConnection(serverConnectionString))
                        {
                            dbConnection.Open();

                            string createSQL = @"
                            CREATE TABLE department(
	                            departmentId INTEGER PRIMARY KEY AUTO_INCREMENT,
	                            departmentName VARCHAR(20) NOT NULL
                            );

                            CREATE TABLE Doctors(
	                            doctorId INTEGER PRIMARY KEY AUTO_INCREMENT,
	                            dFirstName VARCHAR(20) NOT NULL,
	                            dLastName VARCHAR(20) NOT NULL,
	                            dDateOfBirth DATE NOT NULL,
	                            dPhoneNumber BIGINT NOT NULL UNIQUE CHECK (dPhoneNumber >= 1000000000 AND dPhoneNumber <= 9999999999),
	                            dAddress VARCHAR(100) NOT NULL,
	                            dCity VARCHAR(20) NOT NULL,
	                            dProvince VARCHAR(20) NOT NULL,
	                            dEmailAddr VARCHAR(50) NOT NULL UNIQUE CHECK (REGEXP_LIKE(dEmailAddr, '^[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$')),
	                            departmentId INTEGER NOT NULL,
	                            CONSTRAINT sys_doctor_department FOREIGN KEY(departmentId) REFERENCES department(departmentId)
                            );

                            CREATE TABLE patients(
	                            patientId INTEGER PRIMARY KEY AUTO_INCREMENT,
	                            pFirstName VARCHAR(20) NOT NULL,
	                            pLastName VARCHAR(20) NOT NULL,
	                            pDateOfBirth DATE NOT NULL,
	                            pPhoneNumber BIGINT NOT NULL UNIQUE CHECK (pPhoneNumber >= 1000000000 AND pPhoneNumber <= 9999999999),
	                            pAddress VARCHAR(100) NOT NULL,
	                            pCity VARCHAR(20) NOT NULL,
	                            pProvince VARCHAR(20) NOT NULL,
	                            pEmailAddr VARCHAR(50) NOT NULL UNIQUE CHECK (REGEXP_LIKE(pEmailAddr, '^[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$'))
                            );

                            CREATE TABLE treatments(
	                            treatmentId INTEGER PRIMARY KEY AUTO_INCREMENT,
	                            title VARCHAR(50),
	                            doctorId INTEGER NOT NULL,
	                            patientId INTEGER NOT NULL,
	                            status VARCHAR(2) CHECK (status IN ('CO', 'ON')),
	                            notes VARCHAR(1000),
	                            CONSTRAINT sys_treatment_doctor FOREIGN KEY (doctorId) REFERENCES Doctors(doctorId),
	                            CONSTRAINT sys_treatment_patient FOREIGN KEY (patientId) REFERENCES patients(patientId)
                            );

                            CREATE TABLE staff(
	                            staffId INTEGER PRIMARY KEY AUTO_INCREMENT,
	                            sPosition VARCHAR(20) NOT NULL,
	                            sFirstName VARCHAR(20) NOT NULL,
	                            sLastName VARCHAR(20) NOT NULL,
	                            sDateOfBirth DATE NOT NULL,
	                            sPhoneNumber BIGINT NOT NULL UNIQUE CHECK (sPhoneNumber >= 1000000000 AND sPhoneNumber <= 9999999999),
	                            sAddress VARCHAR(100) NOT NULL,
	                            sCity VARCHAR(20) NOT NULL,
	                            sProvince VARCHAR(20) NOT NULL,
	                            sEmailAddr VARCHAR(50) NOT NULL UNIQUE CHECK (REGEXP_LIKE(sEmailAddr, '^[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$')),
	                            departmentId INTEGER NOT NULL,
	                            CONSTRAINT sys_staff_department FOREIGN KEY(departmentId) REFERENCES department(departmentId)
                            );";


                            string populateSQL = @"INSERT INTO department (departmentId, departmentName) VALUES (1, 'Cardiology');
INSERT INTO department (departmentId, departmentName) VALUES (2, 'Neurology');
INSERT INTO department (departmentId, departmentName) VALUES (3, 'Orthopedics');
INSERT INTO department (departmentId, departmentName) VALUES (4, 'Radiology');
INSERT INTO department (departmentId, departmentName) VALUES (5, 'General Medicine');
INSERT INTO department (departmentId, departmentName) VALUES (6, 'Surgery');

INSERT INTO Doctors (doctorId, dFirstName, dLastName, dDateOfBirth, dPhoneNumber, dAddress, dCity, dProvince, dEmailAddr, departmentId) VALUES (1, 'John', 'Doe', '1978-01-12', 1000000001, '123 Elm St.', 'CityA', 'ProvinceA', 'john.doe@email.com', 1);
INSERT INTO Doctors (doctorId, dFirstName, dLastName, dDateOfBirth, dPhoneNumber, dAddress, dCity, dProvince, dEmailAddr, departmentId) VALUES (2, 'Jane', 'Smith', '1985-03-23', 1000000002, '456 Maple St.', 'CityB', 'ProvinceB', 'jane.smith@email.com', 2);
INSERT INTO Doctors (doctorId, dFirstName, dLastName, dDateOfBirth, dPhoneNumber, dAddress, dCity, dProvince, dEmailAddr, departmentId) VALUES (3, 'Robert', 'Brown', '1973-06-15', 1000000003, '789 Pine St.', 'CityC', 'ProvinceC', 'robert.brown@email.com', 3);
INSERT INTO Doctors (doctorId, dFirstName, dLastName, dDateOfBirth, dPhoneNumber, dAddress, dCity, dProvince, dEmailAddr, departmentId) VALUES (4, 'Alice', 'White', '1988-09-05', 1000000004, '012 Cedar St.', 'CityD', 'ProvinceD', 'alice.white@email.com', 4);
INSERT INTO Doctors (doctorId, dFirstName, dLastName, dDateOfBirth, dPhoneNumber, dAddress, dCity, dProvince, dEmailAddr, departmentId) VALUES (5, 'David', 'Green', '1983-12-25', 1000000005, '345 Birch St.', 'CityE', 'ProvinceE', 'david.green@email.com', 5);

INSERT INTO patients (patientId, pFirstName, pLastName, pDateOfBirth, pPhoneNumber, pAddress, pCity, pProvince, pEmailAddr) VALUES(1, 'Mark', 'Black', '1993-05-12', 1000000010, '678 Oak St.', 'CityX', 'ProvinceX', 'mark.black@email.com');
INSERT INTO patients (patientId, pFirstName, pLastName, pDateOfBirth, pPhoneNumber, pAddress, pCity, pProvince, pEmailAddr) VALUES(2, 'Emily', 'Blue', '1995-07-17', 1000000011, '901 Ash St.', 'CityY', 'ProvinceY', 'emily.blue@email.com');
INSERT INTO patients (patientId, pFirstName, pLastName, pDateOfBirth, pPhoneNumber, pAddress, pCity, pProvince, pEmailAddr) VALUES(3, 'Luke', 'Gray', '1992-08-11', 1000000012, '102 Cherry St.', 'CityZ', 'ProvinceZ', 'luke.gray@email.com');
INSERT INTO patients (patientId, pFirstName, pLastName, pDateOfBirth, pPhoneNumber, pAddress, pCity, pProvince, pEmailAddr) VALUES(4, 'Liam', 'Green', '1996-01-21', 1000000013, '305 Birch St.', 'CityW', 'ProvinceW', 'liam.green@email.com');
INSERT INTO patients (patientId, pFirstName, pLastName, pDateOfBirth, pPhoneNumber, pAddress, pCity, pProvince, pEmailAddr) VALUES(5, 'Sophia', 'Brown', '1990-04-05', 1000000014, '215 Cedar St.', 'CityV', 'ProvinceV', 'sophia.brown@email.com');
INSERT INTO patients (patientId, pFirstName, pLastName, pDateOfBirth, pPhoneNumber, pAddress, pCity, pProvince, pEmailAddr) VALUES(6, 'Olivia', 'Pink', '1994-11-12', 1000000015, '562 Maple St.', 'CityU', 'ProvinceU', 'olivia.pink@email.com');
INSERT INTO patients (patientId, pFirstName, pLastName, pDateOfBirth, pPhoneNumber, pAddress, pCity, pProvince, pEmailAddr) VALUES(7, 'Benjamin', 'Orange', '1998-06-14', 1000000016, '924 Elm St.', 'CityT', 'ProvinceT', 'benjamin.orange@email.com');
INSERT INTO patients (patientId, pFirstName, pLastName, pDateOfBirth, pPhoneNumber, pAddress, pCity, pProvince, pEmailAddr) VALUES(8, 'Ethan', 'White', '1991-07-16', 1000000017, '241 Pine St.', 'CityS', 'ProvinceS', 'ethan.white@email.com');
INSERT INTO patients (patientId, pFirstName, pLastName, pDateOfBirth, pPhoneNumber, pAddress, pCity, pProvince, pEmailAddr) VALUES(9, 'Ava', 'Purple', '1997-02-19', 1000000018, '635 Spruce St.', 'CityR', 'ProvinceR', 'ava.purple@email.com');
INSERT INTO patients (patientId, pFirstName, pLastName, pDateOfBirth, pPhoneNumber, pAddress, pCity, pProvince, pEmailAddr) VALUES(10, 'Mia', 'Red', '1989-10-03', 1000000019, '748 Oak St.', 'CityQ', 'ProvinceQ', 'mia.red@email.com');

INSERT INTO treatments (treatmentId, title, doctorId, patientId, status, notes) VALUES(1, 'High Blood Pressure', 1, 1, 'CO', 'Patient prescribed beta-blockers.');
INSERT INTO treatments (treatmentId, title, doctorId, patientId, status, notes) VALUES(2, 'Migraine', 2, 1, 'ON', 'Migraine trigger analysis scheduled next week.');
INSERT INTO treatments (treatmentId, title, doctorId, patientId, status, notes) VALUES(3, 'Broken Leg', 3, 2, 'CO', 'Fracture detected. Plaster applied.');
INSERT INTO treatments (treatmentId, title, doctorId, patientId, status, notes) VALUES(4, 'Asthma', 4, 2, 'ON', 'Review inhaler usage and schedule spirometry.');
INSERT INTO treatments (treatmentId, title, doctorId, patientId, status, notes) VALUES(5, 'Heart Murmur', 1, 3, 'CO', 'Surgery successful. Recovery underway.');
INSERT INTO treatments (treatmentId, title, doctorId, patientId, status, notes) VALUES(6, 'Epilepsy', 2, 3, 'ON', 'Review medication and seizure triggers.');
INSERT INTO treatments (treatmentId, title, doctorId, patientId, status, notes) VALUES(7, 'Joint Pain', 3, 4, 'CO', 'Arthritis detected, physiotherapy recommended.');
INSERT INTO treatments (treatmentId, title, doctorId, patientId, status, notes) VALUES(8, 'Diabetes', 5, 4, 'ON', 'Blood sugar monitoring. Diet consultation scheduled.');
INSERT INTO treatments (treatmentId, title, doctorId, patientId, status, notes) VALUES(9, 'Cold', 5, 5, 'CO', 'Prescribed rest and over-the-counter cold medicine.');
INSERT INTO treatments (treatmentId, title, doctorId, patientId, status, notes) VALUES(10, 'Insomnia', 2, 5, 'ON', 'Sleep hygiene counseling to be done.');
INSERT INTO treatments (treatmentId, title, doctorId, patientId, status, notes) VALUES(11, 'Skin Rash', 5, 6, 'CO', 'Allergic reaction noted. Antihistamines prescribed.');
INSERT INTO treatments (treatmentId, title, doctorId, patientId, status, notes) VALUES(12, 'Nausea', 5, 6, 'ON', 'Patient advised to maintain food diary to identify triggers.');
INSERT INTO treatments (treatmentId, title, doctorId, patientId, status, notes) VALUES(13, 'Dental Pain', 5, 7, 'CO', 'Referred to a dentist for cavity treatment.');
INSERT INTO treatments (treatmentId, title, doctorId, patientId, status, notes) VALUES(14, 'High Cholesterol', 1, 7, 'ON', 'Dietary adjustments recommended.');
INSERT INTO treatments (treatmentId, title, doctorId, patientId, status, notes) VALUES(15, 'Tinnitus', 2, 8, 'CO', 'Noise exposure analysis and management advice given.');
INSERT INTO treatments (treatmentId, title, doctorId, patientId, status, notes) VALUES(16, 'Depression', 2, 8, 'ON', 'Starting on SSRIs and scheduled therapy.');
INSERT INTO treatments (treatmentId, title, doctorId, patientId, status, notes) VALUES(17, 'Back Pain', 3, 9, 'CO', 'Physiotherapy and exercises advised.');
INSERT INTO treatments (treatmentId, title, doctorId, patientId, status, notes) VALUES(18, 'Thyroid Disorder', 5, 9, 'ON', 'Regular thyroid function tests to be conducted.');
INSERT INTO treatments (treatmentId, title, doctorId, patientId, status, notes) VALUES(19, 'Cough', 5, 10, 'CO', 'Chest X-ray taken. Bronchitis detected. Antibiotics prescribed.');
INSERT INTO treatments (treatmentId, title, doctorId, patientId, status, notes) VALUES(20, 'Glaucoma', 2, 10, 'ON', 'Referred to ophthalmologist for further management.');


INSERT INTO staff (staffId, sPosition, sFirstName, sLastName, sDateOfBirth, sPhoneNumber, sAddress, sCity, sProvince, sEmailAddr, departmentId) VALUES(1, 'Nurse', 'Nina', 'Jones', '1991-04-13', 1000000020, '234 Spruce St.', 'CityF', 'ProvinceF', 'nina.jones@email.com', 6);
INSERT INTO staff (staffId, sPosition, sFirstName, sLastName, sDateOfBirth, sPhoneNumber, sAddress, sCity, sProvince, sEmailAddr, departmentId) VALUES(2, 'Nurse', 'Tom', 'Graves', '1994-11-03', 1000000021, '567 Fir St.', 'CityG', 'ProvinceG', 'tom.graves@email.com', 1);
INSERT INTO staff (staffId, sPosition, sFirstName, sLastName, sDateOfBirth, sPhoneNumber, sAddress, sCity, sProvince, sEmailAddr, departmentId) VALUES(3, 'Attendant', 'Rose', 'Gardner', '1989-02-22', 1000000022, '890 Redwood St.', 'CityH', 'ProvinceH', 'rose.gardner@email.com', 2);
INSERT INTO staff (staffId, sPosition, sFirstName, sLastName, sDateOfBirth, sPhoneNumber, sAddress, sCity, sProvince, sEmailAddr, departmentId) VALUES(4, 'Attendant', 'Harry', 'Mason', '1990-06-15', 1000000023, '123 Redbud St.', 'CityI', 'ProvinceI', 'harry.mason@email.com', 3);
INSERT INTO staff (staffId, sPosition, sFirstName, sLastName, sDateOfBirth, sPhoneNumber, sAddress, sCity, sProvince, sEmailAddr, departmentId) VALUES(5, 'Receptionist', 'Lily', 'Parker', '1992-09-12', 1000000024, '456 Dogwood St.', 'CityJ', 'ProvinceJ', 'lily.parker@email.com', 4);
INSERT INTO staff (staffId, sPosition, sFirstName, sLastName, sDateOfBirth, sPhoneNumber, sAddress, sCity, sProvince, sEmailAddr, departmentId) VALUES(6, 'Nurse', 'Oscar', 'Stone', '1993-01-05', 1000000025, '789 Sycamore St.', 'CityK', 'ProvinceK', 'oscar.stone@email.com', 5);
INSERT INTO staff (staffId, sPosition, sFirstName, sLastName, sDateOfBirth, sPhoneNumber, sAddress, sCity, sProvince, sEmailAddr, departmentId) VALUES(7, 'Attendant', 'Lucy', 'Woods', '1995-05-17', 1000000026, '012 Redbud St.', 'CityL', 'ProvinceL', 'lucy.woods@email.com', 6);
INSERT INTO staff (staffId, sPosition, sFirstName, sLastName, sDateOfBirth, sPhoneNumber, sAddress, sCity, sProvince, sEmailAddr, departmentId) VALUES(8, 'Receptionist', 'Ella', 'Hill', '1987-12-24', 1000000027, '345 Beech St.', 'CityM', 'ProvinceM', 'ella.hill@email.com', 1);



                        ";

                            using (MySqlCommand cmdCreateTables = new MySqlCommand(createSQL, dbConnection))
                            {
                                cmdCreateTables.ExecuteNonQuery();
                            }

                            using (MySqlCommand cmdPopulateTables = new MySqlCommand(populateSQL, dbConnection))
                            {
                                cmdPopulateTables.ExecuteNonQuery();
                            }

                            Console.WriteLine("Tables created and populated successfully!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Database already exists!");
                    }
                }
            }
        }
    }

}
