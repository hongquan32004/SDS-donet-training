using DTOS.Requests;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using StudentManagementBlazor.GrpcClient;

namespace StudentManagementBlazor.Components.Pages
{
    public partial class CreateStudent : ComponentBase
    {
        [Inject] protected StudentGrpcClient StudentGrpcClient { get; set; }
        [Inject] protected IJSRuntime JS { get; set; }
        [Inject] protected NavigationManager Navigation { get; set; }

        private StudentCreateRequest CreateRequest = new StudentCreateRequest
        {
            Classroom = new ClassRoom()
        };

        private async Task HandleSubmit()
        {
            try
            {
                await StudentGrpcClient.CreateStudent(CreateRequest);
                await JS.InvokeVoidAsync("alert", "Tạo sinh viên thành công!!");
                Navigation.NavigateTo("/students");
            }
            catch (Exception ex)
            {
                JS.InvokeVoidAsync("alert", $"Lỗi: {ex.Message}");
            }

        }
        void HandleFailed(EditContext context)
        {
            Console.WriteLine("Form không hợp lệ!");
        }
    }
}
