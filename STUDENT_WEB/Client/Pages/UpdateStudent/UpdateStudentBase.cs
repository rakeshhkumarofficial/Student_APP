using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using STUDENT_SHARED.DTOs;
using STUDENT_WEB.Models;
using STUDENT_WEB.Services;
using STUDENT_WEB.Services.Contracts;
using System.Text.Json;

namespace STUDENT_WEB.Pages.UpdateStudent
{
    public class UpdateStudentBase : ComponentBase
    {
        public ResponseDTO _response = new ResponseDTO();
        public StudentUpdateDTO studentUpdateDTO = new StudentUpdateDTO();
        public Guid studentId = Guid.Empty; 
        public Guid current_AddressId = Guid.Empty;
        public Guid permanent_AddressId = Guid.Empty;
        public StudentReponseDTO student = new StudentReponseDTO();
        public StudentAddressModel studentAddressModel = new StudentAddressModel();
        public List<CountryResponse> _countryResponse = new List<CountryResponse>();
        [Inject]
        public IAPIGatewayContract? apiGatewayContract { get; set; }
        [Inject]
        public IStudentContract? studentContract { get; set; }
        [Inject]
        public IToastService? Toast { get; set; }
        [Inject]
        NavigationManager? navigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var uri = new Uri(navigationManager!.Uri);
            var idValue = System.Web.HttpUtility.ParseQueryString(uri.Query).Get("id");
            Guid.TryParse(idValue, out var studentId) ;
            await LoadData(studentId);
        }

        protected void NavigateToStudentList()
        {
            navigationManager!.NavigateTo("student-list");
        }

        protected async Task LoadData(Guid Id)
        {
            _response = await studentContract!.GetAsync(Id, "" , 1 , 1);
            string responseData = JsonSerializer.Serialize(_response!.Data);
            student = JsonSerializer.Deserialize<StudentReponseDTO>(responseData)!;

            // covert studentResponseDTO to StudentAddressModel
            studentId = student.Id;
            studentAddressModel.Name = student.Name;
            studentAddressModel.Email = student.Email;
            studentAddressModel.Gender = student.Gender;
            studentAddressModel.DateOfBirth = student.DateOfBirth;
            studentAddressModel.IsHindi = student.IsHindi;
            studentAddressModel.IsEnglish = student.IsEnglish;
            studentAddressModel.ProfileImage = student.ProfileImage;

            AddressResponseDTO curr = student.Addresses.FirstOrDefault(x => x.IsPermanent == false)!;
            current_AddressId = curr.Id;
            studentAddressModel.CurrentCity = curr.City;
            studentAddressModel.CurrentState = curr.State;
            studentAddressModel.CurrentCountry = curr.Country;
            studentAddressModel.CurrentZipCode = curr.ZipCode;

            AddressResponseDTO per = student.Addresses.FirstOrDefault(x => x.IsPermanent == true)!;
            permanent_AddressId = per.Id;
            studentAddressModel.PermanentCity = per.City;
            studentAddressModel.PermanentState = per.State;
            studentAddressModel.PermanentCountry = per.Country;
            studentAddressModel.PermanentZipCode = per.ZipCode;
            StateHasChanged();
        }
        protected async Task UpdateStudent_Click(StudentAddressModel studentAddressModel)
        {
            try
            {
                studentUpdateDTO.Name = studentAddressModel.Name;
                studentUpdateDTO.Email = studentAddressModel.Email;
                studentUpdateDTO.Gender = studentAddressModel.Gender;
                studentUpdateDTO.DateOfBirth = studentAddressModel.DateOfBirth;
                studentUpdateDTO.IsHindi = studentAddressModel.IsHindi;
                studentUpdateDTO.IsEnglish = studentAddressModel.IsEnglish;
                studentUpdateDTO.ProfileImage = studentAddressModel.ProfileImage;

                studentUpdateDTO.Addresses!.Add(new AddressUpdateDTO
                {
                    Id = current_AddressId,
                    City = studentAddressModel.CurrentCity,
                    State = studentAddressModel.CurrentState,
                    Country = studentAddressModel.CurrentCountry,
                    ZipCode = studentAddressModel.CurrentZipCode,
                    IsPermanent = false
                });
                studentUpdateDTO.Addresses.Add(new AddressUpdateDTO
                {
                    Id = permanent_AddressId,
                    City = studentAddressModel.PermanentCity,
                    State = studentAddressModel.PermanentState,
                    Country = studentAddressModel.PermanentCountry,
                    ZipCode = studentAddressModel.PermanentZipCode,
                    IsPermanent = true
                });
                _response = await studentContract!.UpdateAsync(studentId , studentUpdateDTO);
                if (_response.IsSuccess)
                {
                    Toast!.ShowSuccess(_response.Message!);
                    await OnInitializedAsync();
                    StateHasChanged();
                    navigationManager!.NavigateTo("student-list");
                }
                else
                {
                    Toast!.ShowWarning(_response.Message!);
                }
               
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}


