using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using STUDENT_WEB.DTOs;
using STUDENT_WEB.Services.Contracts;
using System.Threading.Tasks;


namespace STUDENT_WEB.Pages.AddStudent
{
    public class AddStudentBase : ComponentBase
    {
        public ResponseDTO _response = new ResponseDTO();
        public StudentDTO studentDTO = new StudentDTO();
        public AddressDTO Current_Address = new AddressDTO();
        public AddressDTO Permanent_Address = new AddressDTO();
        [Inject]
        public IStudentContract? studentContract { get; set; }
        [Inject]
        public IToastService? Toast { get; set; }
        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        protected async Task CreateStudent_Click(StudentDTO studentDTO)
        {
            try
            {
                studentDTO.Addresses!.Add(Current_Address);
                studentDTO.Addresses!.Add(Permanent_Address);
                _response = await studentContract!.CreateAsync(studentDTO);
                if (_response.IsSuccess)
                {
                    Toast!.ShowSuccess("Student Added Successfully");
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
