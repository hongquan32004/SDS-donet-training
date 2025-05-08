using DTOS.Requests;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using StudentManagementBlazor.GrpcClient;

namespace StudentManagementBlazor.Components.Pages
{
    public partial class UpdateStudent : ComponentBase
    {
        [Inject] protected StudentGrpcClient StudentGrpcClient { get; set; }
        [Inject] protected IJSRuntime JS { get; set; }
        [Inject] protected NavigationManager Navigation { get; set; }

        [Parameter]
        public int Id { get; set; }

        private DTOS.Responses.Student? Student;
        protected override async Task OnInitializedAsync()
        {
            var request = new StudentRequest { Id = Id };
            Student = await StudentGrpcClient.GetStudentById(request);
        }
        async Task HandleUpdate()
        {
            try
            {
                var request = new StudentUpdateRequest
                {
                    Id = Student.Id,
                    Name = Student.Name,
                    Address = Student.Address,
                    DateOfBirth = Student.DateOfBirth
                };
                await StudentGrpcClient.UpdateStudent(request);
                await JS.InvokeVoidAsync("alert", "Cập nhật thành công!");
                Navigation.NavigateTo("/students");
            }
            catch (Exception ex)
            {
                await JS.InvokeVoidAsync("alert", $"Error: {ex.Message}");
            }
        }
        void HandleFailed(EditContext context)
        {
            Console.WriteLine("Form không hợp lệ!");
        }
    }
}
