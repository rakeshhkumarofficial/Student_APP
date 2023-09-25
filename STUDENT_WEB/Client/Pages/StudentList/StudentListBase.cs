using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using STUDENT_SHARED.DTOs;
using STUDENT_WEB.Models;
using STUDENT_WEB.Services.Contracts;
using System.Text.Json;

namespace STUDENT_WEB.Pages.StudentList
{
    public class StudentListBase : ComponentBase
    {
        public ResponseDTO _response = new ResponseDTO();
        public StudentDTO studentDTO = new StudentDTO();
        public AddressDTO Current_Address = new AddressDTO();
        public DataModelDTO dataModelDTO = new DataModelDTO();
        private Guid studentId { get; set; } = Guid.Empty;
        public StudentReponseDTO studentResponse = new StudentReponseDTO();
        public List<StudentReponseDTO> studentList = new List<StudentReponseDTO>();
        [Inject]
        public IStudentContract? studentContract { get; set; }
        [Inject]
        public IToastService? Toast { get; set; }
        [Inject]
        public IJSRuntime jSRuntime { get; set; }
        public bool isLoading = true;
        public int TotalCount = 0;
        public string searchString = "";
        public int index = 1;
        public int limit = 3;
        public int LastPage = 1;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            isLoading = false;
        }
        
        protected async Task LoadData()
        {
            _response = await studentContract!.GetAsync(Guid.Empty , searchString , index , limit);
            string responseData = JsonSerializer.Serialize(_response!.Data);
            dataModelDTO = JsonSerializer.Deserialize<DataModelDTO>(responseData)!;
            studentList = dataModelDTO.Data;
            TotalCount = dataModelDTO.TotalCount;
            LastPage = (int)Math.Ceiling((double)TotalCount / limit);
            StateHasChanged();
        }

        protected void SetAddress()
        {
            AddressResponseDTO current = new AddressResponseDTO();
            current = studentResponse.Addresses.FirstOrDefault(x => x.IsPermanent == false)!;
            Current_Address.City = current!.City;
            Current_Address.State = current.State;
            Current_Address.Country = current.Country;
            Current_Address.IsPermanent = current.IsPermanent;
            Current_Address.ZipCode = current.ZipCode;
        }

        public void HandleDelete(Guid Id)
        {
            studentId = Id;
            jSRuntime.InvokeVoidAsync("ShowDeleteConfirmationModal");
        }
        protected async Task ConfirmDelete_Click(bool isConfirmed)
        {
            if (isConfirmed && studentId != Guid.Empty)
            {
                try
                {
                    _response = await studentContract!.DeleteAsync(studentId);
                    if (_response.IsSuccess)
                    {
                        Toast!.ShowSuccess("Student Deleted Successfully");
                    }
                    await jSRuntime.InvokeVoidAsync("HideDeleteConfirmationModal");
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
}
