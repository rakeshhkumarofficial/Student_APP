using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using STUDENT_WEB.DTOs;
using STUDENT_WEB.Pages.StudentList;
using STUDENT_WEB.Services.Contracts;
using System;
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

        protected async Task LoadData(Guid Id)
        {
            _response = await studentContract!.GetAsync(Id);
            string responseData = JsonSerializer.Serialize(_response!.Data);
            student = JsonSerializer.Deserialize<StudentReponseDTO>(responseData)!;
            studentUpdateDTO.Name = student.name;
            studentUpdateDTO.Age = student.age;
            studentUpdateDTO.Email = student.email;
            AddressResponseDTO curr = student.addresses.FirstOrDefault(x => x.isPermanent == false)!;
            current_Address.Id = curr.id;
            current_Address.City = curr.city;
            current_Address.State = curr.state;
            current_Address.Country = curr.country;
            current_Address.ZipCode = curr.zipCode;
            current_Address.IsPermanent = curr.isPermanent;

            AddressResponseDTO per = student.addresses.FirstOrDefault(x => x.isPermanent == true)!;
            permanent_Address.Id = per.id;
            permanent_Address.City = per.city;
            permanent_Address.State = per.state;
            permanent_Address.Country = per.country;
            permanent_Address.ZipCode = per.zipCode;
            permanent_Address.IsPermanent = per.isPermanent;
            StateHasChanged();
        }
        protected async Task UpdateStudent_Click(StudentUpdateDTO studentUpdateDTO)
        {
            try
            {
                studentUpdateDTO.Addresses!.Add(current_Address);
                studentUpdateDTO.Addresses!.Add(permanent_Address);
                _response = await studentContract!.UpdateAsync(student.id , studentUpdateDTO);
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


