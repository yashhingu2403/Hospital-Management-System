using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management_System.Objects
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string DFirstName { get; set; }
        public string DLastName { get; set; }
        public DateTime DDateOfBirth { get; set; }
        public long DPhoneNumber { get; set; }
        public string DAddress { get; set; }
        public string DCity { get; set; }
        public string DProvince { get; set; }
        public string DEmailAddr { get; set; }
        public int DepartmentId { get; set; }
        public int Age { get; private set; } // calculated property

        public Doctor(int doctorId=0, string dFirstName="", string dLastName="", DateTime dDateOfBirth=new DateTime(), long dPhoneNumber=0, string dAddress="", string dCity="", string dProvince="", string dEmailAddr="", int departmentId=0)
        {
            DoctorId = doctorId;
            DFirstName = dFirstName;
            DLastName = dLastName;
            DDateOfBirth = dDateOfBirth;
            DPhoneNumber = dPhoneNumber;
            DAddress = dAddress;
            DCity = dCity;
            DProvince = dProvince;
            DEmailAddr = dEmailAddr;
            DepartmentId = departmentId;

            Age = CalculateAge(dDateOfBirth);
        }

        private int CalculateAge(DateTime dateOfBirth)
        {
            int age = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > DateTime.Today.AddYears(-age)) age--;
            return age;
        }
        public override string ToString()
        {
            return $"{DFirstName} {DLastName} (ID: {DoctorId}), Age: {Age}, Email: {DEmailAddr}, Department ID: {DepartmentId}";
        }
    }

    public class Patient
    {
        public int PatientId { get; set; }
        public string PFirstName { get; set; }
        public string PLastName { get; set; }
        public DateTime PDateOfBirth { get; set; }
        public long PPhoneNumber { get; set; }
        public string PAddress { get; set; }
        public string PCity { get; set; }
        public string PProvince { get; set; }
        public string PEmailAddr { get; set; }
        public int Age { get; private set; } // calculated property

        public Patient(int patientId = 0, string pFirstName = "", string pLastName = "", DateTime pDateOfBirth = new DateTime(), long pPhoneNumber = 0, string pAddress = "", string pCity = "", string pProvince = "", string pEmailAddr = "")
        {
            PatientId = patientId;
            PFirstName = pFirstName;
            PLastName = pLastName;
            PDateOfBirth = pDateOfBirth;
            PPhoneNumber = pPhoneNumber;
            PAddress = pAddress;
            PCity = pCity;
            PProvince = pProvince;
            PEmailAddr = pEmailAddr;

            Age = CalculateAge(pDateOfBirth);
        }

        private int CalculateAge(DateTime dateOfBirth)
        {
            int age = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > DateTime.Today.AddYears(-age)) age--;
            return age;
        }
        public override string ToString()
        {
            return $"{PFirstName} {PLastName} (ID: {PatientId}), Age: {Age}, Email: {PEmailAddr}";
        }
    }

    public class Staff
    {
        public int StaffId { get; set; }
        public string SPosition { get; set; }
        public string SFirstName { get; set; }
        public string SLastName { get; set; }
        public DateTime SDateOfBirth { get; set; }
        public long SPhoneNumber { get; set; }
        public string SAddress { get; set; }
        public string SCity { get; set; }
        public string SProvince { get; set; }
        public string SEmailAddr { get; set; }
        public int DepartmentId { get; set; }
        public int Age { get; private set; } // calculated property

        public Staff(int staffId = 0, string sPosition="", string sFirstName = "", string sLastName = "", DateTime sDateOfBirth = new DateTime(), long sPhoneNumber = 0, string sAddress = "", string sCity = "", string sProvince = "", string sEmailAddr = "", int departmentId = 0)
        {
            StaffId = staffId;
            SPosition = sPosition;
            SFirstName = sFirstName;
            SLastName = sLastName;
            SDateOfBirth = sDateOfBirth;
            SPhoneNumber = sPhoneNumber;
            SAddress = sAddress;
            SCity = sCity;
            SProvince = sProvince;
            SEmailAddr = sEmailAddr;
            DepartmentId = departmentId;

            Age = CalculateAge(sDateOfBirth);
        }

        private int CalculateAge(DateTime dateOfBirth)
        {
            int age = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > DateTime.Today.AddYears(-age)) age--;
            return age;
        }
        public override string ToString()
        {
            return $"{SFirstName} {SLastName} (ID: {StaffId}), Position: {SPosition}, Age: {Age}, Email: {SEmailAddr}, Department ID: {DepartmentId}";
        }
    }

    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public Department(int departmentId=0, string departmentName="")
        {
            DepartmentId = departmentId;
            DepartmentName = departmentName;
        }
        public override string ToString()
        {
            return $"{DepartmentName} (ID: {DepartmentId})";
        }
    }

    public class Treatment
    {
        public int TreatmentId { get; set; }
        public string Title { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }

        public Treatment(int treatmentId, string title, int doctorId, int patientId, string status, string notes)
        {
            TreatmentId = treatmentId;
            Title = title;
            DoctorId = doctorId;
            PatientId = patientId;
            Status = status;
            Notes = notes;
        }

        public override string ToString()
        {
            return $"Treatment (ID: {TreatmentId}), Title: {Title}, Doctor ID: {DoctorId}, Patient ID: {PatientId}, Status: {Status}";
        }
    }

}
