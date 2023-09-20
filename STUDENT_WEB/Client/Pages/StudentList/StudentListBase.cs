using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using STUDENT_WEB.DTOs;
using STUDENT_WEB.Services.Contracts;
using System.Text.Json;

namespace STUDENT_WEB.Pages.StudentList
{
    public class StudentListBase : ComponentBase
    {
        public ResponseDTO _response = new ResponseDTO();
        public StudentDTO studentDTO = new StudentDTO();
        public AddressDTO Current_Address = new AddressDTO();
        public AddressDTO Permanent_Address = new AddressDTO();
        public List<StudentReponseDTO> studentList = new List<StudentReponseDTO> ();

        [Inject]
        public IStudentContract? studentContract { get; set; }
        [Inject]
        public IToastService? Toast { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        protected async Task LoadData()
        {
            _response = await studentContract!.GetAsync(Guid.Empty);
            string responseData = JsonSerializer.Serialize(_response!.Data);
            studentList = JsonSerializer.Deserialize<List<StudentReponseDTO>>(responseData)!;
            Console.WriteLine(studentList);
            StateHasChanged();
        }

        protected void SetAddress(StudentReponseDTO student)
        {
            var current = student.addresses.FirstOrDefault(x => x.isPermanent == false);
            Current_Address.City = current!.city;
            Current_Address.State = current.state;
            Current_Address.Country = current.country;
            Current_Address.IsPermanent = current.isPermanent;
            Current_Address.ZipCode = current.zipCode;

            var permanent = student.addresses.FirstOrDefault(x => x.isPermanent == true);
            Permanent_Address.City = permanent!.city;
            Permanent_Address.State = permanent.state;
            Permanent_Address.Country = permanent.country;
            Permanent_Address.IsPermanent = permanent.isPermanent;
            Permanent_Address.ZipCode = permanent.zipCode;
        }

        protected async Task DeleteStudent_Click(Guid id)
        {
            try
            {
                _response = await studentContract!.DeleteAsync(id);
                if (_response.IsSuccess)
                {
                    Toast!.ShowSuccess("Student Deleted Successfully");
                }
                await OnInitializedAsync();
                StateHasChanged();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
