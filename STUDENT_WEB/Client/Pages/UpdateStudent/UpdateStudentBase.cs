using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using STUDENT_SHARED.DTOs;
using STUDENT_WEB.Services.Contracts;
using System.Text.Json;

namespace STUDENT_WEB.Pages.UpdateStudent
{
    public class UpdateStudentBase : ComponentBase
    {
        public ResponseDTO _response = new ResponseDTO();
        public StudentUpdateDTO studentUpdateDTO = new StudentUpdateDTO();
        public AddressUpdateDTO current_Address = new AddressUpdateDTO();
        public AddressUpdateDTO permanent_Address = new AddressUpdateDTO();
        public StudentReponseDTO student = new StudentReponseDTO();
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
            Guid.TryParse(idValue, out var parsedId) ;
            await LoadData(parsedId);
        }

        protected void NavigateToStudentList()
        {
            navigationManager!.NavigateTo("student-list");
        }
        protected async Task LoadData(Guid Id)
        {
            _response = await studentContract!.GetAsync(Id);
            string responseData = JsonSerializer.Serialize(_response!.Data);
            student = JsonSerializer.Deserialize<StudentReponseDTO>(responseData)!;
            
            studentUpdateDTO.Name = student.Name;
            studentUpdateDTO.Age = student.Age;
            studentUpdateDTO.Email = student.Email;
            AddressResponseDTO curr = student.Addresses.FirstOrDefault(x => x.IsPermanent == false)!;
            current_Address.Id = curr.Id;
            current_Address.City = curr.City;
            current_Address.State = curr.State;
            current_Address.Country = curr.Country;
            current_Address.ZipCode = curr.ZipCode;
            current_Address.IsPermanent = curr.IsPermanent;

            AddressResponseDTO per = student.Addresses.FirstOrDefault(x => x.IsPermanent == true)!;
            permanent_Address.Id = per.Id;
            permanent_Address.City = per.City;
            permanent_Address.State = per.State;
            permanent_Address.Country = per.Country;
            permanent_Address.ZipCode = per.ZipCode;
            permanent_Address.IsPermanent = per.IsPermanent;
            StateHasChanged();
        }
        protected async Task UpdateStudent_Click(StudentUpdateDTO studentUpdateDTO)
        {
            try
            {
                studentUpdateDTO.Addresses!.Add(current_Address);
                studentUpdateDTO.Addresses!.Add(permanent_Address);
                _response = await studentContract!.UpdateAsync(student.Id , studentUpdateDTO);
                if (_response.IsSuccess)
                {
                    Toast!.ShowSuccess("Student Updated Successfully");
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


