using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Assignment_3._3._2_WPF
{
    public partial class MainWindow : Window
    {

        public ObservableCollection<Student> Students { get; set; }

        public List<MonthOfAdmission> Months { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Students = new ObservableCollection<Student>();
            Months = Enum.GetValues(typeof(MonthOfAdmission)).Cast<MonthOfAdmission>().ToList();

            this.DataContext = this;

            AdmissionMonthComboBox.ItemsSource = Months;
        }

        //add student
        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
            var name = StudentNameTextBox.Text;
            var age = int.TryParse(AgeTextBox.Text, out int result) ? result : 0;
            var admissionMonth = (MonthOfAdmission)AdmissionMonthComboBox.SelectedValue;

            if (!string.IsNullOrEmpty(name) && age > 0 && admissionMonth != null)
            {
                var student = new Student
                {
                    StudentID = Students.Count + 1,
                    Name = name,
                    Age = age,
                    AdmissionMonth = admissionMonth
                };

                // Add student to ObservableCollection
                Students.Add(student);

                // Clear input fields
                StudentNameTextBox.Clear();
                AgeTextBox.Clear();
                AdmissionMonthComboBox.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Please enter valid data.");
            }
        }

        //delete student
        private void DeleteStudentButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem is Student selectedStudent)
            {
                Students.Remove(selectedStudent);
            }
            else
            {
                MessageBox.Show("Please select a student to delete.");
            }
        }

        private void LoadInitialData()
        {
            Students.Add(new Student() { StudentID = 101, Name = "Mahesh Chand", Age = 17, AdmissionMonth = MonthOfAdmission.February });
            Students.Add(new Student() { StudentID = 201, Name = "Mike Gold", Age = 17, AdmissionMonth = MonthOfAdmission.April });
            Students.Add(new Student() { StudentID = 244, Name = "Mathew Cochran", Age = 18, AdmissionMonth = MonthOfAdmission.September });
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }

    public enum MonthOfAdmission
    {
        January,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }

    public class Student
    {
        public int StudentID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public MonthOfAdmission AdmissionMonth { get; set; }
    }
}
