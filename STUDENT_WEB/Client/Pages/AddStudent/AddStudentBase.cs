using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using STUDENT_SHARED.DTOs;
using STUDENT_WEB.APIGatewayModels;
using STUDENT_WEB.Pages.StudentList;
using STUDENT_WEB.Services.Contracts;
using System.Text.Json;

namespace STUDENT_WEB.Pages.AddStudent
{
    public class AddStudentBase : ComponentBase
    {
        public ResponseDTO _response = new ResponseDTO();
        public StudentDTO studentDTO = new StudentDTO();
        public AddressDTO Current_Address = new AddressDTO();
        public AddressDTO Permanent_Address = new AddressDTO();
        public List<CountryResponse> _countryResponse = new List<CountryResponse>();
        [Inject]
        public IStudentContract? studentContract { get; set; }

        [Inject]
        public IAPIGatewayContract? apiGatewayContract { get; set; }
        [Inject]
        public IToastService? Toast { get; set; }
        [Inject]
        NavigationManager? navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetCountryData();
        }
        protected async Task GetCountryData()
        {
            _countryResponse = await apiGatewayContract!.GetCountryNameAsync();
            StateHasChanged();
        }
        protected async Task CreateStudent_Click(StudentDTO studentDTO)
        {
            try
            {
                Current_Address.IsPermanent = false;
                Permanent_Address.IsPermanent = true;   
                studentDTO.Addresses!.Add(Current_Address);
                studentDTO.Addresses!.Add(Permanent_Address);
                _response = await studentContract!.CreateAsync(studentDTO);
                if (_response.IsSuccess)
                {
                    Toast!.ShowSuccess("Student Added Successfully");
                }
                await OnInitializedAsync();
                StateHasChanged();
                navigationManager!.NavigateTo("student-list");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
