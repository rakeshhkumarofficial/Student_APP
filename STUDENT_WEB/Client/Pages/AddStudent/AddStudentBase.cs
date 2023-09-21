using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using STUDENT_SHARED.DTOs;
using STUDENT_WEB.Models;
using STUDENT_WEB.Pages.StudentList;
using STUDENT_WEB.Services.Contracts;
using System.Text.Json;

namespace STUDENT_WEB.Pages.AddStudent
{
    public class AddStudentBase : ComponentBase
    {
        public ResponseDTO _response = new ResponseDTO();
        public StudentDTO studentDTO = new StudentDTO();
        public StudentAddressModel studentAddressModel = new StudentAddressModel();
        public List<CountryResponse> _countryResponse = new List<CountryResponse>();
        [Inject]
        public IStudentContract? studentContract { get; set; }

        [Inject]
        public IAPIGatewayContract? apiGatewayContract { get; set; }
        [Inject]
        public IToastService? Toast { get; set; }
        [Inject]
        NavigationManager? navigationManager { get; set; }

        //protected override async Task OnInitializedAsync()
        //{
        //    await GetCountryData();
        //}
        //protected async Task GetCountryData()
        //{
        //    _countryResponse = await apiGatewayContract!.GetCountryNameAsync();
            
        //    StateHasChanged();
        //}
        protected async Task CreateStudent_Click(StudentAddressModel studentAddressModel)
        {
            try
            {
                
                studentDTO.Name = studentAddressModel.Name;
                studentDTO.Email = studentAddressModel.Email;
                studentDTO.Gender = studentAddressModel.Gender;
                studentDTO.DateOfBirth = studentAddressModel.DateOfBirth;
                studentDTO.IsHindi = studentAddressModel.IsHindi;
                studentDTO.IsEnglish = studentAddressModel.IsEnglish;

                var currentAddress = new AddressDTO
                {
                    City = studentAddressModel.CurrentCity,
                    State = studentAddressModel.CurrentState,
                    Country = studentAddressModel.CurrentCountry,
                    ZipCode = studentAddressModel.CurrentZipCode,
                    IsPermanent = false
                };
                studentDTO.Addresses!.Add(currentAddress);
                var permanentAddress = new AddressDTO
                {
                    City = studentAddressModel.PermanentCity,
                    State = studentAddressModel.PermanentState,
                    Country = studentAddressModel.PermanentCountry,
                    ZipCode = studentAddressModel.PermanentZipCode,
                    IsPermanent = true
                };
                studentDTO.Addresses.Add(permanentAddress);
                
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
